/*
 *  --------------------------------------------------------------------------------------------
 *
 *    VVV        VVV A           Virtual Acoustics (VA) | http://www.virtualacoustics.org
 *     VVV      VVV AAA          Licensed under the Apache License, Version 2.0
 *      VVV    VVV   AAA
 *       VVV  VVV     AAA        Copyright 2015-2018
 *        VVVVVV       AAA       Institute of Technical Acoustics (ITA)
 *         VVVV         AAA      RWTH Aachen University
 *
 *  --------------------------------------------------------------------------------------------
 */

#ifndef IW_VACORE_PROTOTYPE_HEARINGAID_RENDERER
#define IW_VACORE_PROTOTYPE_HEARINGAID_RENDERER

#ifdef VACORE_WITH_RENDERER_PROTOTYPE_HEARING_AID

// VA includes
#include "../../../Motion/VAMotionModelBase.h"
#include "../../../Motion/VASharedMotionModel.h"
#include "../../../Motion/VASampleAndHoldMotionModel.h"
#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include "../../../Scene/VAScene.h"
#include <VA.h>
#include "../../../VACoreImpl.h"
#include <VAObject.h>
#include <VAObjectPool.h>
#include "../../../VASourceListenerMetrics.h"

// ITA includes
#include <ITABufferedAudiofileWriter.h>
#include <ITADataSourceRealization.h>
#include <ITASampleFrame.h>
#include <ITAStringUtils.h>

// 3rdParty Includes
#include <tbb/concurrent_queue.h>

// STL Includes
#include <list>
#include <set>

// VA forwards
class CVACoreImpl;
class CVASceneState;
class CVASceneStateDiff;
class CVASignalSourceManager;
class CVASoundSourceDesc;

// Internal forwards
class CVAPTHASoundPath;
class CVAPTHASoundPathFactory;

//! Prototype Rendere for Hearing Aid Auralization
/**
  * The prototype hearing aid audio renderer implements sound propagation for
  * virtual hearing aids, based on the concept of binaural synthesis.
  *
  * It accounts for 
  *		- multichannel receiver directivity (HARTF), n-channel filters possible
  *		- source directivity
  *		- medium propagation delay
  *		- medium absorption over distance
  *		- room reflections (IS and RT) by loading multi-channel room impulse responses
  *		- distance gain / 1-by-r law / spherical spreading attenuation
  *		- Doppler shifts (source and listener movement in medium of finite speed of sound)
  *
  */
class CVAPTHearingAidRenderer : public IVAAudioRenderer, public ITADatasourceRealization, public CVAObject 
{
public:
	CVAPTHearingAidRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVAPTHearingAidRenderer();

	//! Handle a user requested reset
	/**
	  * This method resets the entire scene by removing all entities. The
	  * modules have to reset their internal state and return to a clean
	  * scene. This call should be blocking until reset is done.
	  */
	void Reset();
	
	//! Load a user requested scene
	/**
	  * This method loads a scene, usually a file path to geometry data.
	  */
	inline void LoadScene( const std::string& ) {};
	
	//! Handle a scene state change
	/**
	  * This method updates the internal representation of the VA Scene
	  * by setting up or deleting the sound path entities as well as
	  * modifying existing ones that have changed their state, i.e.
	  * pose or dataset
	  */
	void UpdateScene( CVASceneState* pNewSceneState );

	//! Handle a state change in global auralisation mode
	/**
	  * This method updates internal settings for the global auralisation
	  * mode affecting the activation/deactivation of certain components
	  * of the sound path entities
	  */
	void UpdateGlobalAuralizationMode( int iGlobalAuralizationMode );
	
	//! Render output sample blocks
	/**
	  * This method renders the sound propagation based on the binaural approach
	  * by evaluating motion and events that are retarded in time, i.e. it switches
	  * filter parts and magnitudes of the HRIR or Directivity. It also considers
	  * the effective auralisation mode.
	  */
	void ProcessStream( const ITAStreamInfo* pStreamInfo );

	//! Returns the renderers output stream datasource
	ITADatasource* GetOutputDatasource();

	// --= Module interface =--

	CVAStruct CallObject( const CVAStruct& oArgs );

	void onStartDumpListeners(const std::string& sFilenameFormat);
	void onStopDumpListeners();

protected:

	//! Internal source representation
	class Source : public CVAPoolObject 
	{
	public:
		class Config
		{
		public:
			bool bMotionModelLogInputEnabled;
			bool bMotionModelLogEstimatedEnabled;

			double dMotionModelWindowSize;
			double dMotionModelWindowDelay;

			int iMotionModelNumHistoryKeys;
		};

		Source( const Config& oConf_ )
			: oConf( oConf_ )
		{};

		const Config oConf;
		CVASoundSourceDesc* pData;			//!< (Unversioned) Source description
		CVASharedMotionModel* pMotionModel;
		bool bDeleted;
		VAVec3 vPredPos;				//!< Estimated position
		VAVec3 vPredView;				//!< Estimated Orientation (View-Vektor)
		VAVec3 vPredUp;					//!< Estimated Orientation (Up-Vektor)

		//! Pool-Konstruktor
		void PreRequest()
		{
			pData = nullptr;

			CVABasicMotionModel::Config oDefaultConfig;
			oDefaultConfig.bLogEstimatedOutputEnabled = oConf.bMotionModelLogEstimatedEnabled;;
			oDefaultConfig.bLogInputEnabled = oConf.bMotionModelLogInputEnabled;
			oDefaultConfig.dWindowDelay = oConf.dMotionModelWindowDelay;
			oDefaultConfig.dWindowSize = oConf.dMotionModelWindowSize;
			oDefaultConfig.iNumHistoryKeys = oConf.iMotionModelNumHistoryKeys;
			pMotionModel = new CVASharedMotionModel( new CVABasicMotionModel( oDefaultConfig ), true );
		};

		//! Pool-Destruktor
		void PreRelease()
		{
			delete pMotionModel;
			pMotionModel = nullptr;
		};

		double GetCreationTimestamp() const
		{ 
			return m_dCreationTimeStamp; 
		};

	private:
		double m_dCreationTimeStamp;  //!< Date of creation within streaming context
	};


	//! Internal listener representation
	class Listener : public CVAPoolObject 
	{
	public:
		class Config
		{
		public:
			bool bMotionModelLogInputEnabled;
			bool bMotionModelLogEstimatedEnabled;

			double dMotionModelWindowSize;
			double dMotionModelWindowDelay;

			int iMotionModelNumHistoryKeys;
		};

		Listener( CVACoreImpl* pCore, const Config& oConf )
		: m_pCore( pCore ), m_oConf( oConf )
		{};

		CVACoreImpl* m_pCore;
		const Config m_oConf;

		CVAListenerDesc* pData;				//!< (Unversioned) Listener description
		CVASharedMotionModel* pMotionModel;
		bool bDeleted;
		VAVec3 vPredPos;				//!< Estimated position
		VAVec3 vPredView;				//!< Estimated Orientation (View-Vektor)
		VAVec3 vPredUp;					//!< Estimated Orientation (Up-Vektor)

		ITASampleFrame* psfOutput;			//!< Accumulated listener output signals
		ITAAudiofileWriter* pListenerOutputAudioFileWriter;	//!< File writer used for dumping the listener signals
		
		void PreRequest() 
		{	
			pData = nullptr;

			CVABasicMotionModel::Config oListenerMotionConfig;
			oListenerMotionConfig.bLogEstimatedOutputEnabled = m_oConf.bMotionModelLogEstimatedEnabled;
			oListenerMotionConfig.bLogInputEnabled = m_oConf.bMotionModelLogInputEnabled;
			oListenerMotionConfig.dWindowDelay = m_oConf.dMotionModelWindowDelay;
			oListenerMotionConfig.dWindowSize = m_oConf.dMotionModelWindowSize;
			oListenerMotionConfig.iNumHistoryKeys = m_oConf.iMotionModelNumHistoryKeys;
			pMotionModel = new CVASharedMotionModel( new CVABasicMotionModel( oListenerMotionConfig ), true );

			pListenerOutputAudioFileWriter = nullptr;
			psfOutput = nullptr;
		};

		void PreRelease() 
		{
			delete pMotionModel;
			pMotionModel = nullptr;
			delete psfOutput;

			FinalizeDump();
		};

		void InitDump(const std::string& sFilename) {
			std::string sOutput(sFilename);
			sOutput = SubstituteMacro(sOutput, "ListenerName", pData->sName);
			sOutput = SubstituteMacro(sOutput, "ListenerID", IntToString(pData->iID));
			
			ITAAudiofileProperties props;
			props.dSampleRate = m_pCore->GetCoreConfig()->oAudioDriverConfig.dSampleRate;
			props.eDomain = ITADomain::ITA_TIME_DOMAIN;
			props.eQuantization = ITAQuantization::ITA_FLOAT;
			props.iChannels = 2;
			props.iLength = 0;
			pListenerOutputAudioFileWriter = ITABufferedAudiofileWriter::create(sOutput, props);
		}

		void FinalizeDump() {
			delete pListenerOutputAudioFileWriter;
			pListenerOutputAudioFileWriter = nullptr;
		}
	};

private:

	const CVAAudioRendererInitParams oParams; //!< Create a const copy of the init params

	CVACoreImpl* m_pCore;						//!< Pointer to VACore

	CVASceneState* m_pCurSceneState;
	CVASceneState* m_pNewSceneState;

	int m_iCurGlobalAuralizationMode;
	
	IVAObjectPool* m_pSoundPathPool;
	CVAPTHASoundPathFactory* m_pSoundPathFactory;
	std::list< CVAPTHASoundPath* > m_lSoundPaths;	//!< List of sound paths in user context (VACore calls)	

	IVAObjectPool* m_pSourcePool;
	IVAObjectPool* m_pListenerPool;

	std::map< int, Source* > m_mSources;		//!< Internal list of sources
	std::map< int, Listener* > m_mListeners;	//!< Internal list of listener

	double m_dUpdateRateDS;						//!< Update rate for direct sound		[default: 120.0f]
	double m_dUpdateRateIS;						//!< Update rate for early reflections	[default: 10.0f]
	double m_dUpdateRateRT;						//!< Update rate for reverberation		[default: 1.0f]

	double m_dRenderingDelayInMs;				//!< Delay for rendering. This will value will be added to the VDL [default: 0.0f]

	double m_dRenderingGain;					//!< Gain factor specifically for HAA module [default: 1.0f]

	std::vector<int> m_viHARIRChannels;			//!< Vector containing the input channel selection of the HARIR daff files [default: 1,2,3,4]

	bool m_bDumpListeners;
	//std::string m_sDumpListenersFilenameFormat;
	double m_dDumpListenersGain;
	ITAAtomicInt m_iDumpListenersFlag;
	
	int m_iHRIRFilterLength;				//!< Length of the HRIR filter DSP module

	int m_iDefaultVDLSwitchingAlgorithm;

	Listener::Config m_oDefaultListenerConf; //!< Default listener config for factory object creation
	Source::Config m_oDefaultSourceConf; //!< Default source config for factory object creation

	class UpdateMessage : public CVAPoolObject {
	public:
		// Sources: New, deleted
		std::vector<Source*> vNewSources, vDelSources;

		// Listeners: New, deleted
		std::vector<Listener*> vNewListeners, vDelListeners;

		// Sound paths: New, deleted
		std::vector<CVAPTHASoundPath*> vNewPaths, vDelPaths;

		void PreRequest() {
			vNewSources.clear();
			vDelSources.clear();
			vNewListeners.clear();
			vDelListeners.clear();
			vNewPaths.clear();
			vDelPaths.clear();
		}
	};

	IVAObjectPool* m_pUpdateMessagePool;
	UpdateMessage* m_pUpdateMessage;

	//! Data in context of audio process
	struct
	{
		tbb::concurrent_queue<UpdateMessage*> m_qpUpdateMessages;
		std::list< CVAPTHASoundPath* > m_lSoundPaths;						//!< List of all artificial reverb paths
		//tbb::concurrent_queue< CBFSoundPath* > m_qpNewSoundPaths;		//!< Lock-free queue: new paths
		//tbb::concurrent_queue< CBFSoundPath* > m_qpDelSoundPaths;		//!< Lock-free queue: removed paths
		std::list< Source* > m_lSources;						//!< List of sources
		//tbb::concurrent_queue< Source* > m_qpNewSources;		//!< Lock-free queue: new sources
		//tbb::concurrent_queue< Source* > m_qpDelSources;		//!< Lock-free queue: removed sources
		std::list< Listener* > m_lListener;						//!< List of sources
		//tbb::concurrent_queue< Listener* > m_qpNewListeners;	//!< Lock-free queue: new listener
		//tbb::concurrent_queue< Listener* > m_qpDelListeners;	//!< Lock-free queue: removed listener
		ITASampleBuffer m_sbTemp;	//!< Temporally used buffer to store a block of samples during processing
		ITAAtomicInt m_iResetFlag;	//!< Reset status flag: 0=normal_op, 1=reset_request, 2=reset_ack
		ITAAtomicInt m_iStatus;		//!< Current status flag: 0=stopped, 1=running
	} ctxAudio;

	void Init( const CVAStruct& oArgs );

	void ManageSoundPaths( const CVASceneState* pCurScene,
		                   const CVASceneState* pNewScene,
						   const CVASceneStateDiff* pDiff );
	void UpdateSources();
	CVAPTHearingAidRenderer::Listener* CreateListener( int iID, const CVAReceiverState* pListenerState );
	void DeleteListener( int iID );
	CVAPTHearingAidRenderer::Source* CreateSource( int iID, const CVASoundSourceState* pSourceState );
	void DeleteSource( int iID );
	CVAPTHASoundPath* CreateSoundPath( Source* pSource, Listener* pListener );
	void DeleteSoundPath( CVAPTHASoundPath* pPath );
	
	void UpdateTrajectories();
	void UpdateSoundPaths();

	void SampleTrajectoriesInternal( double dTime );

	void SyncInternalData();
	void ResetInternalData();

	friend class CVAPTHASoundPath;
	friend class CVAPTHAListenerPoolFactory;
	friend class CVAPTHASourcePoolFactory;

	//! Not for use, avoid C4512
	inline CVAPTHearingAidRenderer operator=( const CVAPTHearingAidRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_PROTOTYPE_HEARING_AID

#endif // IW_VACORE_PROTOTYPE_HEARINGAID_RENDERER

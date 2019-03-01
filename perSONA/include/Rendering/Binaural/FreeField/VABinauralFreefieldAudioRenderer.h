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


#ifndef IW_VACORE_BINAURALFREEFIELDAUDIORENDERER
#define IW_VACORE_BINAURALFREEFIELDAUDIORENDERER

#ifdef VACORE_WITH_RENDERER_BINAURAL_FREE_FIELD

#include <VA.h>

// VA includes
#include "../../../Motion/VAMotionModelBase.h"
#include "../../../Motion/VASharedMotionModel.h"
#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include "../../../Scene/VAScene.h"
#include "../../../VASourceListenerMetrics.h"
#include "../../../VACoreImpl.h"

// ITA includes
#include <ITABufferedAudioFileWriter.h>
#include <ITADataSourceRealization.h>
#include <ITAVariableDelayLine.h>
#include <ITASampleFrame.h>
#include <ITAStringUtils.h>

// 3rdParty includes
#include <tbb/concurrent_queue.h>

// STL Includes
#include <list>
#include <set>

// VA forwards
class CVASceneState;
class CVASceneStateDiff;
class CVASignalSourceManager;
class CVASoundSourceDesc;

// Internal forwards
class CVABFFSoundPath;
class CVABFFSoundPathFactory;

//! Binaural Freefield Audio Renderer
/**
  * The binaural freefield audio renderer implements sound propagation with
  * no propagation disturbance in the medium (for eample no reflecting surfaces).
  *
  * It accounts for
  *		- source directivity
  *		- medium propagation delay
  *		- medium absorption over distance
  *		- distance gain / 1-by-r law / spherical spreading attenuation
  *		- two channel binaural receiver directivity (HRTF)
  *		- Doppler shifts (source and listener movement in medium of finite speed of sound)
  *
  */
class CVABinauralFreeFieldAudioRenderer : public IVAAudioRenderer, public ITADatasourceRealization, public CVAObject
{
public:
	CVABinauralFreeFieldAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVABinauralFreeFieldAudioRenderer();

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
	void SetParameters( const CVAStruct& oParams );
	CVAStruct GetParameters( const CVAStruct& );

	void onStartDumpListeners( const std::string& sFilenameFormat );
	void onStopDumpListeners();

protected:

	//! Internal source representation
	class CVABFFSource : public CVAPoolObject
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

		inline CVABFFSource( const Config& oConf_ )
			: oConf( oConf_ )
		{};

		const Config oConf;
		CVASoundSourceDesc* pData;			//!< (Unversioned) Source description
		CVASharedMotionModel* pMotionModel;
		bool bDeleted;
		VAVec3 vPredPos;				//!< Estimated position
		VAVec3 vPredView;				//!< Estimated Orientation (View-Vektor)
		VAVec3 vPredUp;					//!< Estimated Orientation (Up-Vektor)
		bool bValidTrajectoryPresent;	//!< Estimation possible -> valid trajectory present

		//! Pool-Konstruktor
		inline void PreRequest()
		{
			pData = nullptr;

			CVABasicMotionModel::Config oDefaultConfig;
			oDefaultConfig.bLogEstimatedOutputEnabled = oConf.bMotionModelLogEstimatedEnabled;
			oDefaultConfig.bLogInputEnabled = oConf.bMotionModelLogInputEnabled;
			oDefaultConfig.dWindowDelay = oConf.dMotionModelWindowDelay;
			oDefaultConfig.dWindowSize = oConf.dMotionModelWindowSize;
			oDefaultConfig.iNumHistoryKeys = oConf.iMotionModelNumHistoryKeys;
			pMotionModel = new CVASharedMotionModel( new CVABasicMotionModel( oDefaultConfig ), true );

			bValidTrajectoryPresent = false;
		};

		inline void PreRelease()
		{
			delete pMotionModel;
			pMotionModel = nullptr;
		};

		inline double GetCreationTimestamp() const
		{
			return m_dCreationTimeStamp;
		};

	private:
		double m_dCreationTimeStamp;  //!< Date of creation within streaming context
	};


	//! Internal listener representation
	class CVABFFListener : public CVAPoolObject
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

		inline CVABFFListener( CVACoreImpl* pCore, const Config& oConf )
			: pCore( pCore )
			, oConf( oConf )
		{};

		CVACoreImpl* pCore;
		const Config oConf;

		CVAListenerDesc* pData;				//!< (Unversioned) Listener description
		CVASharedMotionModel* pMotionModel;
		bool bDeleted;
		VAVec3 vPredPos;				//!< Estimated position
		VAVec3 vPredView;				//!< Estimated Orientation (View-Vektor)
		VAVec3 vPredUp;					//!< Estimated Orientation (Up-Vektor)
		bool bValidTrajectoryPresent;	//!< Estimation possible -> valid trajectory present

		ITASampleFrame* psfOutput;			//!< Accumulated listener output signals
		ITAAudiofileWriter* pListenerOutputAudioFileWriter;	//!< File writer used for dumping the listener signals

		inline void PreRequest()
		{
			pData = nullptr;

			CVABasicMotionModel::Config oListenerMotionConfig;
			oListenerMotionConfig.bLogEstimatedOutputEnabled = oConf.bMotionModelLogEstimatedEnabled;
			oListenerMotionConfig.bLogInputEnabled = oConf.bMotionModelLogInputEnabled;
			oListenerMotionConfig.dWindowDelay = oConf.dMotionModelWindowDelay;
			oListenerMotionConfig.dWindowSize = oConf.dMotionModelWindowSize;
			oListenerMotionConfig.iNumHistoryKeys = oConf.iMotionModelNumHistoryKeys;
			pMotionModel = new CVASharedMotionModel( new CVABasicMotionModel( oListenerMotionConfig ), true );

			bValidTrajectoryPresent = false;

			pListenerOutputAudioFileWriter = nullptr;
			psfOutput = nullptr;
		};

		inline void PreRelease()
		{
			delete pMotionModel;
			pMotionModel = nullptr;
			delete psfOutput;

			FinalizeDump();
		};

		inline void InitDump( const std::string& sFilename )
		{
			std::string sOutput( sFilename );
			sOutput = SubstituteMacro( sOutput, "ListenerName", pData->sName );
			sOutput = SubstituteMacro( sOutput, "ListenerID", IntToString( pData->iID ) );

			ITAAudiofileProperties props;
			props.dSampleRate = pCore->GetCoreConfig()->oAudioDriverConfig.dSampleRate;
			props.eDomain = ITADomain::ITA_TIME_DOMAIN;
			props.eQuantization = ITAQuantization::ITA_FLOAT;
			props.iChannels = 2;
			props.iLength = 0;
			pListenerOutputAudioFileWriter = ITABufferedAudiofileWriter::create( sOutput, props );
		};

		inline void FinalizeDump()
		{
			delete pListenerOutputAudioFileWriter;
			pListenerOutputAudioFileWriter = nullptr;
		};
	};

private:

	const CVAAudioRendererInitParams m_oParams; //!< Create a const copy of the init params

	CVACoreImpl* m_pCore;					//!< Pointer to VACore

	CVASceneState* m_pCurSceneState;
	CVASceneState* m_pNewSceneState;

	int m_iCurGlobalAuralizationMode;

	IVAObjectPool* m_pSoundPathPool;
	CVABFFSoundPathFactory* m_pSoundPathFactory;
	std::list< CVABFFSoundPath* > m_lSoundPaths;	//!< List of sound paths in user context (VACore calls)	

	IVAObjectPool* m_pSourcePool;
	IVAObjectPool* m_pListenerPool;

	std::map< int, CVABFFSource* > m_mSources;	//!< Internal list of sources
	std::map< int, CVABFFListener* > m_mListeners;	//!< Internal list of listener

	bool m_bDumpListeners;
	double m_dDumpListenersGain;
	ITAAtomicInt m_iDumpListenersFlag;

	int m_iHRIRFilterLength;				//!< Length of the HRIR filter DSP module

	int m_iDefaultVDLSwitchingAlgorithm;
	double m_dAdditionalStaticDelaySeconds;        //!< Additional delay in seconds for delay compensation

	CVABFFListener::Config m_oDefaultListenerConf; //!< Default listener config for factory object creation
	CVABFFSource::Config m_oDefaultSourceConf; //!< Default source config for factory object creation

	class CVABFFUpdateMessage : public CVAPoolObject
	{
	public:
		std::list< CVABFFSource* > vNewSources;
		std::list< CVABFFSource* > vDelSources;
		std::list< CVABFFListener* > vNewListeners;
		std::list< CVABFFListener* > vDelListeners;
		std::list< CVABFFSoundPath* > vNewPaths;
		std::list< CVABFFSoundPath* > vDelPaths;

		void PreRequest()
		{
			vNewSources.clear();
			vDelSources.clear();
			vNewListeners.clear();
			vDelListeners.clear();
			vNewPaths.clear();
			vDelPaths.clear();
		}
	};

	IVAObjectPool* m_pUpdateMessagePool; // really necessary?
	CVABFFUpdateMessage* m_pUpdateMessage;

	//! Data in context of audio process
	struct
	{
		tbb::concurrent_queue< CVABFFUpdateMessage* > m_qpUpdateMessages;	//!< Update messages list
		std::list< CVABFFSoundPath* > m_lSoundPaths;	//!< List of sound paths
		std::list< CVABFFSource* > m_lSources;			//!< List of sources
		std::list< CVABFFListener* > m_lListeners;		//!< List of listeners
		ITASampleBuffer m_sbTempL;	//!< Temporally used buffer to store a block of samples during processing (left ear)
		ITASampleBuffer m_sbTempR;	//!< Temporally used buffer to store a block of samples during processing (right ear)
		ITAAtomicInt m_iResetFlag;	//!< Reset status flag: 0=normal_op, 1=reset_request, 2=reset_ack
		ITAAtomicInt m_iStatus;		//!< Current status flag: 0=stopped, 1=running
	} ctxAudio;

	void Init( const CVAStruct& oArgs );

	void ManageSoundPaths( const CVASceneState* pCurScene, const CVASceneState* pNewScene, const CVASceneStateDiff* pDiff );
	void UpdateSources();
	CVABFFListener* CreateListener( int iID, const CVAReceiverState* );
	void DeleteListener( int iID );
	CVABFFSource* CreateSource( int iID, const CVASoundSourceState* );
	void DeleteSource( int iID );
	CVABFFSoundPath* CreateSoundPath( CVABFFSource*, CVABFFListener* );
	void DeleteSoundPath( CVABFFSoundPath* );

	void UpdateTrajectories();
	void UpdateSoundPaths();

	void SampleTrajectoriesInternal( double dTime );

	void SyncInternalData();
	void ResetInternalData();

	friend class CVABFFSoundPath;
	friend class CVABFFListenerPoolFactory;
	friend class CVABFFSourcePoolFactory;

	//! Not for use, avoid C4512
	inline CVABinauralFreeFieldAudioRenderer operator=( const CVABinauralFreeFieldAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // ( VACORE_WITH_RENDERER_BINAURAL_FREE_FIELD

#endif // IW_VACORE_BINAURALFREEFIELDAUDIORENDERER

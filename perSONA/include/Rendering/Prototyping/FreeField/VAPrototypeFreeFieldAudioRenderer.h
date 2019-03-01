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

#ifndef IW_VACORE_PROTOTYPEFREEFIELDAUDIORENDERER
#define IW_VACORE_PROTOTYPEFREEFIELDAUDIORENDERER

#ifdef VACORE_WITH_RENDERER_PROTOTYPE_FREE_FIELD

// VA includes
#include <VA.h>
#include <VACore.h>

// VACore includes
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

// 3rdParty Includes
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
class CVAPFFSoundPath;
class CVAPFFSoundPathFactory;

//! Prototype Free-field Audio Renderer
/**
  * The prototype free-field audio renderer implements sound propagation with
  * no propagation disturbance in the medium, i.e. by reflecting surfaces.
  * It creates a multi-channel signal, corresponding to a microphone with given number of channels.
  *
  * It accounts for
  *		- source directivity
  *		- medium propagation delay
  *		- medium absorption over distance
  *		- distance gain / 1-by-r law / spherical spreading attenuation
  *		- Doppler shifts (source and listener movement in medium of finite speed of sound)
  *
  */
class CVAPrototypeFreeFieldAudioRenderer : public IVAAudioRenderer, public ITADatasourceRealization, public CVAObject
{
public:
	CVAPrototypeFreeFieldAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVAPrototypeFreeFieldAudioRenderer();

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
	void LoadScene( const std::string& ) {};

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
	  * This method renders the sound propagation
	  * by evaluating motion and events that are retarded in time, i.e. it switches
	  * filter parts and magnitudes of the directivity. It also considers
	  * the effective auralisation mode.
	  */
	void ProcessStream( const ITAStreamInfo* pStreamInfo );

	//! Returns the renderers output stream datasource
	ITADatasource* GetOutputDatasource();

	CVAStruct CallObject( const CVAStruct& oArgs );

protected:

	//! Internal source representation
	class CVAPFFSource : public CVAPoolObject
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

		CVAPFFSource( const Config& oConf_ )
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

			bValidTrajectoryPresent = false;
		};

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
	class CVAPFFReceiver : public CVAPoolObject
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

		CVAPFFReceiver( CVACoreImpl* pCore, const Config& oConf )
			: pCore( pCore ), oConf( oConf )
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

		void PreRequest()
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

		void PreRelease()
		{
			delete pMotionModel;
			pMotionModel = nullptr;
			delete psfOutput;

			if( pListenerOutputAudioFileWriter )
			{
				delete pListenerOutputAudioFileWriter;
				pListenerOutputAudioFileWriter = nullptr;
			}
		};
	};

private:

	const CVAAudioRendererInitParams m_oParams; //!< Create a const copy of the init params

	CVACoreImpl* m_pCore;					//!< Pointer to VACore

	CVASceneState* m_pCurSceneState;
	CVASceneState* m_pNewSceneState;

	int m_iCurGlobalAuralizationMode;

	IVAObjectPool* m_pSoundPathPool;
	CVAPFFSoundPathFactory* m_pSoundPathFactory;
	std::list< CVAPFFSoundPath* > m_lSoundPaths;	//!< List of sound paths in user context (VACore calls)	

	IVAObjectPool* m_pSourcePool;
	IVAObjectPool* m_pListenerPool;

	std::map< int, CVAPFFSource* > m_mSources;	//!< Internal list of sources
	std::map< int, CVAPFFReceiver* > m_mReceivers;	//!< Internal list of listener
	
	int m_iDefaultVDLSwitchingAlgorithm;

	bool m_bRecordSoundReceiverOutputSignals; //!< Listener dumping enabled
	double m_dRecordSoundReceiverOutputGain; //!< Listener dumping output gain (avoid clipping but preserve gain)
	std::string m_sBaseFolder; //!< Base folder where dumps and motion stuff can go

	int m_iRecordSoundReceiversNumChannels; //!< Number of maximum channels for a sound receiver


	CVAPFFReceiver::Config m_oDefaultListenerConf; //!< Default listener config for factory object creation
	CVAPFFSource::Config m_oDefaultSourceConf; //!< Default source config for factory object creation

	class CVAPFFUpdateMessage : public CVAPoolObject
	{
	public:
		std::list< CVAPFFSource* > vNewSources;
		std::list< CVAPFFSource* > vDelSources;
		std::list< CVAPFFReceiver* > vNewReceivers;
		std::list< CVAPFFReceiver* > vDelReceivers;
		std::list< CVAPFFSoundPath* > vNewPaths;
		std::list< CVAPFFSoundPath* > vDelPaths;

		inline void PreRequest()
		{
			vNewSources.clear();
			vDelSources.clear();
			vNewReceivers.clear();
			vDelReceivers.clear();
			vNewPaths.clear();
			vDelPaths.clear();
		}
	};

	IVAObjectPool* m_pUpdateMessagePool; // really necessary?
	CVAPFFUpdateMessage* m_pUpdateMessage;
	//! Data in context of audio process
	struct
	{
		tbb::concurrent_queue< CVAPFFUpdateMessage* > m_qpUpdateMessages;	//!< Update messages list
		std::list< CVAPFFSoundPath* > m_lSoundPaths;	//!< List of sound paths
		std::list< CVAPFFSource* > m_lSources;			//!< List of sources
		std::list< CVAPFFReceiver* > m_lReceivers;		//!< List of receivers
		ITASampleBuffer m_sbTemp;	//!< Temporally used buffer to store a block of samples during processing
		ITAAtomicInt m_iResetFlag;	//!< Reset status flag: 0=normal_op, 1=reset_request, 2=reset_ack
		ITAAtomicInt m_iStatus;		//!< Current status flag: 0=stopped, 1=running
	} ctxAudio;

	void Init( const CVAStruct& oArgs );

	void ManageSoundPaths( const CVASceneState* pCurScene,
		const CVASceneState* pNewScene,
		const CVASceneStateDiff* pDiff );
	void UpdateSources();
	CVAPFFReceiver* CreateReceiver( int iID, const CVAReceiverState* );
	void DeleteReceiver( int iID );
	CVAPFFSource* CreateSource( int iID, const CVASoundSourceState* );
	void DeleteSource( int iID );
	CVAPFFSoundPath* CreateSoundPath( CVAPFFSource*, CVAPFFReceiver* );
	void DeleteSoundPath( CVAPFFSoundPath* );

	void UpdateTrajectories();
	void UpdateSoundPaths();

	void SampleTrajectoriesInternal( double dTime );

	void SyncInternalData();
	void ResetInternalData();

	friend class CVAPFFSoundPath;
	friend class CVAPFFListenerPoolFactory;
	friend class CVAPFFSourcePoolFactory;

	//! Not for use, avoid C4512
	inline CVAPrototypeFreeFieldAudioRenderer operator=( const CVAPrototypeFreeFieldAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_PROTOTYPE_FREE_FIELD

#endif // IW_VACORE_PROTOTYPEFREEFIELDAUDIORENDERER

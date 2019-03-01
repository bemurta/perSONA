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

#ifndef IW_VACORE_BINAURALAIRTRAFFICNOISEAUDIORENDERER
#define IW_VACORE_BINAURALAIRTRAFFICNOISEAUDIORENDERER

#if VACORE_WITH_RENDERER_BINAURAL_AIR_TRAFFIC_NOISE

// VA includes
#include <VA.h>
#include <VAObjectPool.h>

#include "../../../Motion/VAMotionModelBase.h"
#include "../../../Motion/VASharedMotionModel.h"
#include "../../../Rendering/VAAudioRenderer.h"
#include "../../../Rendering/VAAudioRendererRegistry.h"
#include "../../../Scene/VAScene.h"
#include "../../../VASourceListenerMetrics.h"
#include "../../../VACoreImpl.h"

// ITA includes
#include <ITADataSourceRealization.h>
#include <ITASampleBuffer.h>
#include <ITAVariableDelayLine.h>

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

// Internal forward declarations
class CVABATNSoundPath;
class CVABATNSoundPathFactory;


//! Air Traffic Noise Audio Renderer (VATSS project)
/**
  * Manages sound pathes from jet plane sound sources to a
  * binaural receiver including multiple audio effects:
  *		- Directivity
  *		- Doppler-Shifts
  *		- Air-Absorption [TODO?!]
  *		- 1/r-Distance-Law
  *
  */
class CVABinauralAirTrafficNoiseAudioRenderer : public IVAAudioRenderer, public ITADatasourceRealization
{
public:
	CVABinauralAirTrafficNoiseAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVABinauralAirTrafficNoiseAudioRenderer();

	//! Reset scene
	void Reset();

	//! Dummy
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

	CVAStruct GetParameters( const CVAStruct& oArgs );
	void SetParameters( const CVAStruct& oArgs );

private:

	const CVAAudioRendererInitParams m_oParams; //!< Create a const copy of the init params

	//! Interne Beschreibung einer Schallquelle
	class CVABATNSource : public CVAPoolObject
	{
	public:
		class Config
		{
		public:
			double dMotionModelWindowSize;
			double dMotionModelWindowDelay;

			int iMotionModelNumHistoryKeys;
		};

		inline CVABATNSource( const Config& oConf_ )
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

		// Pool-Konstruktor
		inline void PreRequest()
		{
			pData = nullptr;

			CVABasicMotionModel::Config oDefaultConfig;
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
	class CVABATNSoundReceiver : public CVAPoolObject
	{
	public:
		class Config
		{
		public:
			double dMotionModelWindowSize;
			double dMotionModelWindowDelay;

			int iMotionModelNumHistoryKeys;
		};

		inline CVABATNSoundReceiver( CVACoreImpl* pCore, const Config& oConf )
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

		ITASampleFrame* psfOutput;			//!< Accumulated listener output signals @todo check if sample frame is also deleted after usage

		inline void PreRequest()
		{
			pData = nullptr;

			CVABasicMotionModel::Config oListenerMotionConfig;
			oListenerMotionConfig.dWindowDelay = oConf.dMotionModelWindowDelay;
			oListenerMotionConfig.dWindowSize = oConf.dMotionModelWindowSize;
			oListenerMotionConfig.iNumHistoryKeys = oConf.iMotionModelNumHistoryKeys;
			pMotionModel = new CVASharedMotionModel( new CVABasicMotionModel( oListenerMotionConfig ), true );

			bValidTrajectoryPresent = false;

			psfOutput = nullptr;
		};

		// Pool-Destruktor
		inline void PreRelease()
		{
			delete pMotionModel;
			pMotionModel = nullptr;
		};
	};

private:

	CVACoreImpl* m_pCore;					//!< Pointer to VACore

	CVASceneState* m_pCurSceneState;
	CVASceneState* m_pNewSceneState;

	int m_iCurGlobalAuralizationMode;

	IVAObjectPool* m_pSoundPathPool;
	CVABATNSoundPathFactory* m_pSoundPathFactory;	//!< Erzeuger für Schallpfade als Pool-Objekte
	std::list< CVABATNSoundPath* > m_lSoundPaths;	//!< Liste aller Schallpfade (im Thread-Kontext: VACore)	

	IVAObjectPool* m_pSourcePool;
	IVAObjectPool* m_pListenerPool;

	std::map< int, CVABATNSource* > m_mSources;		//!< Interne Abbildung der verfügbaren Quellen
	std::map< int, CVABATNSoundReceiver* > m_mListeners;	//!< Interne Abbildung der verfügbaren Hörer

	double m_dGroundPlanePosition;			//!< Position of ground plane (height) for reflection calculation

	int m_iHRIRFilterLength;				//!< Length of the HRIR filter DSP module

	int m_iDefaultVDLSwitchingAlgorithm;	//!< Umsetzungsalgorithmus der Variablen Verzögerungsleitung


	bool m_bPropagationDelayExternalSimulation; //!< If true, internal simulation is skipped and parameters are expected to be set using external SetParameters() call
	bool m_bGroundReflectionExternalSimulation; //!< If true, internal simulation is skipped and parameters are expected to be set using external SetParameters() call
	bool m_bTemporalVariationsExternalSimulation; //!< If true, internal simulation is skipped and parameters are expected to be set using external SetParameters() call
	bool m_bDirectivityExternalSimulation; //!< If true, internal simulation is skipped and parameters are expected to be set using external SetParameters() call
	bool m_bAirAbsorptionExternalSimulation; //!< If true, internal simulation is skipped and parameters are expected to be set using external SetParameters() call
	bool m_bSpreadingLossExternalSimulation; //!< If true, internal simulation is skipped and parameters are expected to be set using external SetParameters() call

	CVABATNSoundReceiver::Config m_oDefaultListenerConf; //!< Default listener config for factory object creation
	CVABATNSource::Config m_oDefaultSourceConf; //!< Default source config for factory object creation

	class CVABATNUpdateMessage : public CVAPoolObject
	{
	public:
		std::list< CVABATNSource* > vNewSources;
		std::list< CVABATNSource* > vDelSources;
		std::list< CVABATNSoundReceiver* > vNewListeners;
		std::list< CVABATNSoundReceiver* > vDelListeners;
		std::list< CVABATNSoundPath* > vNewPaths;
		std::list< CVABATNSoundPath* > vDelPaths;

		inline void PreRequest()
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
	CVABATNUpdateMessage* m_pUpdateMessage;

	//! Data in context of audio process
	struct
	{
		tbb::concurrent_queue< CVABATNUpdateMessage* > m_qpUpdateMessages;	//!< Update messages list
		std::list< CVABATNSoundPath* > m_lSoundPaths;	//!< List of sound paths
		std::list< CVABATNSource* > m_lSources;			//!< List of sources
		std::list< CVABATNSoundReceiver* > m_lListener;		//!< List of listeners
		ITASampleBuffer m_sbTempBufD;	//!< Temporally used buffer to store a block of samples during processing (direct sound)
		ITASampleBuffer m_sbTempBufR;	//!< Temporally used buffer to store a block of samples during processing (reflected sound)
		ITAAtomicInt m_iResetFlag;	//!< Reset status flag: 0=normal_op, 1=reset_request, 2=reset_ack
		ITAAtomicInt m_iStatus;		//!< Current status flag: 0=stopped, 1=running
	} ctxAudio;

	void Init( const CVAStruct& oArgs );

	void ManageSoundPaths( const CVASceneState* pCurScene, const CVASceneState* pNewScene, const CVASceneStateDiff* pDiff );
	void UpdateSources();
	CVABATNSoundReceiver* CreateSoundReceiver( int iID, const CVAReceiverState* );
	void DeleteListener( int iID );
	CVABATNSource* CreateSoundSource( int iID, const CVASoundSourceState* );
	void DeleteSource( int iID );
	CVABATNSoundPath* CreateSoundPath( CVABATNSource*, CVABATNSoundReceiver* );
	void DeleteSoundPath( CVABATNSoundPath* );

	void UpdateTrajectories();
	void UpdateSoundPaths();

	void SampleTrajectoriesInternal( double dTime );

	void SyncInternalData();
	void ResetInternalData();

	friend class CVABATNSoundPath;
	friend class CVABATNSoundReceiverPoolFactory;
	friend class CVABATNSourcePoolFactory;

	//! Not for use, avoid C4512
	inline CVABinauralAirTrafficNoiseAudioRenderer operator=( const CVABinauralAirTrafficNoiseAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_BINAURAL_AIR_TRAFFIC_NOISE

#endif // IW_VACORE_BINAURALAIRTRAFFICNOISEAUDIORENDERER

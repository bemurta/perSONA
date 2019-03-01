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

#ifndef IW_VACORE_AMBISONICSFREEFIELDAUDIORENDERER
#define IW_VACORE_AMBISONICSFREEFIELDAUDIORENDERER

#ifdef VACORE_WITH_RENDERER_AMBISONICS_FREE_FIELD

// VA includes
#include "../../../Motion/VAMotionModelBase.h"
#include "../../../Motion/VASampleAndHoldMotionModel.h"
#include "../../../Motion/VASharedMotionModel.h"
#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include "../../../Scene/VAScene.h"
#include <VA.h>
#include <VAObjectPool.h>
#include "../../../VASourceListenerMetrics.h"
#include "../../../VACoreImpl.h"

// ITA includes
#include <ITABufferedAudioFileWriter.h>
#include <ITADataSourceRealization.h>
#include <ITASampleFrame.h>
#include <ITAStringUtils.h>


// 3rdParty includes
#include <tbb/concurrent_queue.h>
#include <ITAVariableDelayLine.h>
#include <ITASampleBuffer.h>

// STL Includes
#include <list>
#include <set>

// VA forwards
class CVASceneState;
class CVASceneStateDiff;
class CVASignalSourceManager;
class CVASoundSourceDesc;

// Internal forwards
class CVAAFFSoundPath;
class CVAAFFSoundPathFactory;

//! Ambisonics Freefield Audio Renderer
/**
* The Ambisonics freefield audio renderer implements sound propagation with
* no propagation disturbance in the medium, i.e. by reflecting surfaces.
*
* It accounts for
*		- source directivity
*		- medium propagation delay
*		- medium absorption over distance
*		- distance gain / 1-by-r law / spherical spreading attenuation
*		- Doppler shifts (source and listener movement in medium of finite speed of sound)
*
*/
class CVAAmbisonicsFreeFieldAudioRenderer : public IVAAudioRenderer, public ITADatasourceRealizationEventHandler, public CVAObject
{
public:
	CVAAmbisonicsFreeFieldAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVAAmbisonicsFreeFieldAudioRenderer();

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
	* This method renders the sound propagation based on the Ambisonics approach
	* by evaluating motion and events that are retarded in time, i.e. it switches
	* filter parts and magnitudes of or Directivity. It also considers
	* the effective auralisation mode.
	*/
	void HandleProcessStream( ITADatasourceRealization*, const ITAStreamInfo* pStreamInfo );

	//! Returns the renderers output stream datasource
	ITADatasource* GetOutputDatasource();

	// --= Module interface =--

	CVAStruct CallObject( const CVAStruct& oArgs );

	void onStartDumpListeners( const std::string& sFilenameFormat );
	void onStopDumpListeners();

	double dAzimuthSource2ReproCenter( const CVAMotionState* pMotionState );
	double dElevationSource2ReproCenter( const CVAMotionState* pMotionState );

protected:

	//! Internal source representation
	class CVAAFFSource : public CVAPoolObject
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

		CVAAFFSource( const Config& oConf_ )
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
	class CVAAFFListener : public CVAPoolObject
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

		CVAAFFListener( CVACoreImpl* pCore, const Config& oConf )
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

			FinalizeDump();
		};

		void InitDump( const std::string& sFilename ) {
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
		}

		void FinalizeDump()
		{
			delete pListenerOutputAudioFileWriter;
			pListenerOutputAudioFileWriter = nullptr;
		}
	};

private:
	ITADatasourceRealization* m_pdsOutput; //!< Output datasource
	const CVAAudioRendererInitParams m_oParams; //!< Create a const copy of the init params

	CVACoreImpl* m_pCore;					//!< Pointer to VACore

	CVASceneState* m_pCurSceneState;
	CVASceneState* m_pNewSceneState;

	VAVec3 m_vUserPosVirtualScene;				//!< Position des Hoerers in der virtuellen Umgebung 
	VAVec3 m_vUserPosRealWorld;					//!< Position des Hörers in der CAVE (oder im Reproduktionssystem)
	VAVec3 m_vReproSystemVirtualPosition;		//!< Position der CAVE (oder des Reproduktionssystems) in der virtuellen Welt. Center position of loudspeaker array.
	VAVec3 m_vReproSystemRealPosition;			//!< Position of the reproduction center in the real world

	VAVec3 m_vUserViewVirtualScene;
	VAVec3 m_vUserViewRealWorld;
	VAVec3 m_vReproSystemVirtualView;
	VAVec3 m_vReproSystemRealView;

	VAVec3 m_vUserUpVirtualScene;
	VAVec3 m_vUserUpRealWorld;
	VAVec3 m_vReproSystemVirtualUp;
	VAVec3 m_vReproSystemRealUp;
	//VAVec3 m_vecCaveCenterPos;					//!< Erwartete Hoererposition bzw. Punkt für den Ambisonics das Schallfeld berechnet

	int m_iCurGlobalAuralizationMode;

	IVAObjectPool* m_pSoundPathPool;
	CVAAFFSoundPathFactory* m_pSoundPathFactory;
	std::list< CVAAFFSoundPath* > m_lSoundPaths;	//!< List of sound paths in user context (VACore calls)	

	IVAObjectPool* m_pSourcePool;
	IVAObjectPool* m_pListenerPool;

	std::map< int, CVAAFFSource* > m_mSources;	//!< Internal list of sources
	std::map< int, CVAAFFListener* > m_mListeners;	//!< Internal list of listener

	int m_iMaxOrder;
	int m_iNumChannels;
	bool m_bDumpListeners;
	double m_dDumpListenersGain;
	ITAAtomicInt m_iDumpListenersFlag;

	int m_iDefaultVDLSwitchingAlgorithm;
	double m_dAdditionalStaticDelaySeconds;        //!< Additional delay in seconds for delay compensation

	CVAAFFListener::Config m_oDefaultListenerConf; //!< Default listener config for factory object creation
	CVAAFFSource::Config m_oDefaultSourceConf; //!< Default source config for factory object creation

	class CVAAFFUpdateMessage : public CVAPoolObject
	{
	public:
		std::list< CVAAFFSource* > vNewSources;
		std::list< CVAAFFSource* > vDelSources;
		std::list< CVAAFFListener* > vNewListeners;
		std::list< CVAAFFListener* > vDelListeners;
		std::list< CVAAFFSoundPath* > vNewPaths;
		std::list< CVAAFFSoundPath* > vDelPaths;

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
	CVAAFFUpdateMessage* m_pUpdateMessage;

	//! Data in context of audio process
	struct
	{
		tbb::concurrent_queue< CVAAFFUpdateMessage* > m_qpUpdateMessages;	//!< Update messages list
		std::list< CVAAFFSoundPath* > m_lSoundPaths;	//!< List of sound paths
		std::list< CVAAFFSource* > m_lSources;			//!< List of sources
		std::list< CVAAFFListener* > m_lListeners;		//!< List of listeners
		ITASampleBuffer m_sbTemp;	//!< Temporally used buffer to store a block of samples during processing
		ITAAtomicInt m_iResetFlag;	//!< Reset status flag: 0=normal_op, 1=reset_request, 2=reset_ack
		ITAAtomicInt m_iStatus;		//!< Current status flag: 0=stopped, 1=running
	} ctxAudio;

	void Init( const CVAStruct& oArgs );

	std::vector<double> vdRealvalued_basefunctions( double elevation, double azimuth, int maxOrder );
	double dNormalizeConst( int m, int n );
	int iKronecker( int m );
	std::vector<double> dAssociateLegendre( int N, double mu );
	int iFactorial( int in );
	int GetIndex( int m, int n );

	void ManageSoundPaths( const CVASceneState* pCurScene,
		const CVASceneState* pNewScene,
		const CVASceneStateDiff* pDiff );
	void UpdateSources();
	CVAAFFListener* CreateListener( int iID, const CVAReceiverState* );
	void DeleteListener( int iID );
	CVAAFFSource* CreateSource( int iID, const CVASoundSourceState* );
	void DeleteSource( int iID );
	CVAAFFSoundPath* CreateSoundPath( CVAAFFSource*, CVAAFFListener* );
	void DeleteSoundPath( CVAAFFSoundPath* );

	void UpdateTrajectories();
	void UpdateSoundPaths();

	void SampleTrajectoriesInternal( double dTime );

	void SyncInternalData();
	void ResetInternalData();

	friend class CVAAFFSoundPath;
	friend class CVAAFFListenerPoolFactory;
	friend class CVAAFFSourcePoolFactory;

	//! Not for use, avoid C4512
	inline CVAAmbisonicsFreeFieldAudioRenderer operator=( const CVAAmbisonicsFreeFieldAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_AMBISONICS_FREE_FIELD

#endif // IW_VACORE_AmbisonicsFREEFIELDAUDIORENDERER

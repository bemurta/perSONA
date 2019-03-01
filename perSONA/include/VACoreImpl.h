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

#ifndef IW_VA_CORE_IMPL
#define IW_VA_CORE_IMPL

#include <ITAAtomicPrimitives.h>
#include <ITACriticalSection.h>
#include <ITAStreamProbe.h>
#include <ITAStreamDetector.h>

#include <VAInterface.h>
#include "VACoreConfig.h"
#include "VAObjectContainer.h"
#include "VAObject.h"
#include "VAObjectRegistry.h"
#include "VAProfiler.h"
#include "Scene/VASceneState.h"
#include "Rendering/VAAudioRenderer.h"

#include "Medium/VAHomogeneousMedium.h"

#include <VistaInterProcComm/Concurrency/VistaMutex.h>
#include <VistaInterProcComm/Concurrency/VistaTicker.h>

#include <list>
#include <memory>

class CVAAudioSignalSourceManager;
class CVAAudiostreamTracker;
class IVAAudioReproduction;
class CVACoreEventManager;
class CVACoreThread;
class CVADirectivityManager;
class CVASceneManager;
class CVASceneState;
class DAFFContentIR;
class IVAAudioDriverBackend;
class ITAClock;
class ITADatasource;
class ITASoundSampler;
class ITASoundSamplePool;
class ITAStreamAmplifier;
class ITAStreamPatchbay;


class CVACoreImpl : public IVAInterface, public CVAObject, public VistaTicker::AfterPulseFunctor
{
public:
	CVACoreImpl( const CVAStruct& oArgs, std::ostream* pos );
	virtual ~CVACoreImpl();

	void SetOutputStream( std::ostream* pos );
	void GetVersionInfo( CVAVersionInfo* ) const;

	int GetState() const;
	void Initialize();
	void Finalize();
	void Reset();

	void Tidyup();

	void AttachEventHandler( IVAEventHandler* pCoreEventHandler );
	void DetachEventHandler( IVAEventHandler* pCoreEventHandler );

	void RegisterModule( CVAObject* pObject );
	void GetModules( std::vector< CVAModuleInfo >& m_viModuleInfos ) const;
	CVAStruct CallModule( const std::string& sModuleName, const CVAStruct& oArgs );
	CVAStruct GetSearchPaths() const;
	CVAStruct GetCoreConfiguration( const bool ) const;
	CVAStruct GetHardwareConfiguration() const;
	CVAStruct GetFileList( const bool bRecursive = true, const std::string& sFileSuffixFilter = "*" ) const;

	// Directivities
	int CreateDirectivityFromParameters( const CVAStruct& oParams, const std::string& sName = "" );
	bool DeleteDirectivity( const int iID );
	CVADirectivityInfo GetDirectivityInfo( const int iID ) const;
	void GetDirectivityInfos( std::vector< CVADirectivityInfo >& voDest ) const;
	void SetDirectivityName( const int iID, const std::string& sName );
	std::string GetDirectivityName( const int iID ) const;
	void SetDirectivityParameters( const int iID, const CVAStruct& oParams );
	CVAStruct GetDirectivityParameters( const int iID, const CVAStruct& oParams ) const;

	// Acoustic materials
	int CreateAcousticMaterial( const CVAAcousticMaterial& oMaterial, const std::string& sName = "" );
	int CreateAcousticMaterialFromParameters( const CVAStruct& oParams, const std::string& sName = "" );
	bool DeleteAcousticMaterial( const int iID );
	CVAAcousticMaterial GetAcousticMaterialInfo( const int iID ) const;
	void GetAcousticMaterialInfos( std::vector< CVAAcousticMaterial >& voDest ) const;
	void SetAcousticMaterialName( const int iID, const std::string& sName );
	std::string GetAcousticMaterialName( const int iID ) const;
	void SetAcousticMaterialParameters( const int iID, const CVAStruct& oParams );
	CVAStruct GetAcousticMaterialParameters( const int iID, const CVAStruct& oParams ) const;

	// Geometry meshes
	int CreateGeometryMesh( const CVAGeometryMesh& oMesh, const std::string& sName = "" );
	int CreateGeometryMeshFromParameters( const CVAStruct& oParams, const std::string& sName = "" );
	bool DeleteGeometryMesh( const int iID );
	CVAGeometryMesh GetGeometryMesh( const int iID ) const;
	void GetGeometryMeshIDs( std::vector< int >& viIDs ) const;
	void SetGeometryMeshName( const int iID, const std::string& sName );
	std::string GetGeometryMeshName( const int iID ) const;
	void SetGeometryMeshParameters( const int iID, const CVAStruct& oParams );
	CVAStruct GetGeometryMeshParameters( const int iID, const CVAStruct& oParams ) const;
	void SetGeometryMeshEnabled( const int iID, const bool bEnabled = true );
	bool GetGeometryMeshEnabled( const int iID ) const;

	// Signal sources
	std::string CreateSignalSourceBufferFromParameters( const CVAStruct& oParams, const std::string& sName = "" );
	std::string CreateSignalSourceTextToSpeech( const std::string& sName = "" );
	std::string CreateSignalSourceSequencer( const std::string& sName = "" );
	std::string CreateSignalSourceNetworkStream( const std::string& sInterface, const int iPort, const std::string& sName = "" );
	std::string CreateSignalSourceEngine( const CVAStruct& oParams, const std::string& sName = "" );
	std::string CreateSignalSourceMachine( const CVAStruct& oParams, const std::string& sName = "" );
	bool DeleteSignalSource( const std::string& sID );
	std::string RegisterSignalSource( IVAAudioSignalSource* pSignalSource, const std::string& sName = "" );
	bool UnregisterSignalSource( IVAAudioSignalSource* pSignalSource );
	CVASignalSourceInfo GetSignalSourceInfo( const std::string& sSignalSourceID ) const;
	void GetSignalSourceInfos( std::vector< CVASignalSourceInfo >& voInfos ) const;
	int GetSignalSourceBufferPlaybackState( const std::string& sSignalSourceID ) const;
	void SetSignalSourceBufferPlaybackAction( const std::string& sSignalSourceID, const int iPlaybackAction );
	void SetSignalSourceBufferPlaybackPosition( const std::string& sSignalSourceID, const double dPlaybackPosition );
	void SetSignalSourceBufferLooping( const std::string& sSignalSourceID, const bool bLooping = true );
	bool GetSignalSourceBufferLooping( const std::string& sSignalSourceID ) const;
	void SetSignalSourceParameters( const std::string& sSignalSourceID, const CVAStruct& oParams );
	CVAStruct GetSignalSourceParameters( const std::string& sSignalSourceID, const CVAStruct& oParams ) const;
	int AddSignalSourceSequencerSample( const std::string& sSignalSourceID, const CVAStruct& oArgs );
	int AddSignalSourceSequencerPlayback( const std::string& sSignalSourceID, const int iSoundID, const int iFlags, const double dTimeCode );
	void RemoveSignalSourceSequencerSample( const std::string& sSignalSourceID, const int iSoundID );

	// Synced updates
	bool GetUpdateLocked() const;
	void LockUpdate();
	int UnlockUpdate();

	// Sound sources
	int CreateSoundSource( const std::string& sName = "" );
	void GetSoundSourceIDs( std::vector< int >& viSoundSourceIDs );
	CVASoundSourceInfo GetSoundSourceInfo( const int iID ) const;
	int CreateSoundSourceExplicitRenderer( const std::string& sRendererID, const std::string& sName = "" );
	int DeleteSoundSource( const int iID );
	void SetSoundSourceEnabled( const int iID, const bool bEnabled = true );
	bool GetSoundSourceEnabled( const int iID ) const;
	std::string GetSoundSourceName( const int iID ) const;
	void SetSoundSourceName( const int iID, const std::string& sName );
	std::string GetSoundSourceSignalSource( const int iID ) const;
	int GetSoundSourceGeometryMesh( const int iID ) const;
	void SetSoundSourceGeometryMesh( const int iSoundSourceID, const int iGeometryMeshID );
	void SetSoundSourceSignalSource( const int iSoundSourceID, const std::string& sSignalSourceID );
	int GetSoundSourceAuralizationMode( const int iID ) const;
	void SetSoundSourceAuralizationMode( const int iSoundSourceID, const int iAuralizationMode );
	void SetSoundSourceParameters( const int iID, const CVAStruct& oParams );
	CVAStruct GetSoundSourceParameters( const int iID, const CVAStruct& oParams ) const;
	int GetSoundSourceDirectivity( const int iID ) const;
	void SetSoundSourceDirectivity( const int iSoundSourceID, const int iDirectivityID );
	double GetSoundSourceSoundPower( const int iID ) const;
	void SetSoundSourceSoundPower( const int iSoundSourceID, const double dSoundPower );
	bool GetSoundSourceMuted( const int iID ) const;
	void SetSoundSourceMuted( const int iID, const bool bMuted = true );
	void GetSoundSourcePose( const int iID, VAVec3& vPos, VAQuat& qOrient ) const;
	void SetSoundSourcePose( const int iID, const VAVec3& vPos, const VAQuat& qOrient );
	VAVec3 GetSoundSourcePosition( const int iID ) const;
	void SetSoundSourcePosition( const int iID, const VAVec3& v3Pos );
	VAQuat GetSoundSourceOrientation( const int iID ) const;
	void SetSoundSourceOrientation( const int iID, const VAQuat& qOrient );
	void GetSoundSourceOrientationVU( const int iID, VAVec3& v3View, VAVec3& v3Up ) const;
	void SetSoundSourceOrientationVU( const int iID, const VAVec3& v3View, const VAVec3& v3Up );

	// Sound receiver
	void GetSoundReceiverIDs( std::vector< int >& viIDs ) const;
	int CreateSoundReceiver( const std::string& sName = "" );
	int CreateSoundReceiverExplicitRenderer( const std::string& sRendererID, const std::string& sName = "" );
	int DeleteSoundReceiver( const int iID );
	CVASoundReceiverInfo GetSoundReceiverInfo( const int iID ) const;
	void SetSoundReceiverEnabled( const int iID, const bool bEnabled = true );
	bool GetSoundReceiverEnabled( const int iID ) const;
	std::string GetSoundReceiverName( const int iID ) const;
	void SetSoundReceiverName( const int iID, const std::string& sName );
	int GetSoundReceiverAuralizationMode( const int iID ) const;
	void SetSoundReceiverAuralizationMode( const int iSoundReceiverID, const int iAuralizationMode );
	void SetSoundReceiverParameters( const int iID, const CVAStruct& oParams );
	CVAStruct GetSoundReceiverParameters( const int iID, const CVAStruct& oArgs ) const;
	int GetSoundReceiverDirectivity( const int iID ) const;
	void SetSoundReceiverDirectivity( const int iSoundReceiverID, const int iDirectivityID );
	int GetSoundReceiverGeometryMesh( const int iID ) const;
	void SetSoundReceiverGeometryMesh( const int iSoundReceiverID, const int iGeometryMeshID );
	void GetSoundReceiverPose( const int iID, VAVec3& vPos, VAQuat& qOrient ) const;
	void SetSoundReceiverPose( const int iID, const VAVec3& vPos, const VAQuat& qOrient );
	VAVec3 GetSoundReceiverPosition( const int iID ) const;
	void SetSoundReceiverPosition( const int iID, const VAVec3& v3Pos );
	void GetSoundReceiverOrientationVU( const int iID, VAVec3& vView, VAVec3& vUp ) const;
	void SetSoundReceiverOrientationVU( const int iID, const VAVec3& vView, const VAVec3& vUp );
	VAQuat GetSoundReceiverOrientation( const int iID ) const;
	void SetSoundReceiverOrientation( const int iID, const VAQuat& qOrient );
	VAQuat GetSoundReceiverHeadAboveTorsoOrientation( const int iID ) const;
	void SetSoundReceiverHeadAboveTorsoOrientation( const int iID, const VAQuat& qOrient );
	void GetSoundReceiverRealWorldPositionOrientationVU( const int iID, VAVec3& v3Pos, VAVec3& v3View, VAVec3& v3Up ) const;
	void SetSoundReceiverRealWorldPositionOrientationVU( const int iID, const VAVec3& v3Pos, const VAVec3& v3View, const VAVec3& v3Up );
	void GetSoundReceiverRealWorldPose( const int iID, VAVec3& v3Pos, VAQuat& qOrient ) const;
	void SetSoundReceiverRealWorldPose( const int iID, const VAVec3& v3Pos, const VAQuat& qOrient );
	VAQuat GetSoundReceiverRealWorldHeadAboveTorsoOrientation( const int iID ) const;
	void SetSoundReceiverRealWorldHeadAboveTorsoOrientation( const int iID, const VAQuat& qOrient );


	// Homogeneous medium
	void SetHomogeneousMediumSoundSpeed( const double dSoundSpeed );
	double GetHomogeneousMediumSoundSpeed() const;
	void SetHomogeneousMediumTemperature( const double dDegreesCentigrade );
	double GetHomogeneousMediumTemperature() const;
	void SetHomogeneousMediumStaticPressure( const double dPressurePascal );
	double GetHomogeneousMediumStaticPressure() const;
	void SetHomogeneousMediumRelativeHumidity( const double dRelativeHumidityPercent );
	double GetHomogeneousMediumRelativeHumidity();
	void SetHomogeneousMediumShiftSpeed( const VAVec3& v3TranslationSpeed );
	VAVec3 GetHomogeneousMediumShiftSpeed() const;
	void SetHomogeneousMediumParameters( const CVAStruct& oParams );
	CVAStruct GetHomogeneousMediumParameters( const CVAStruct& oArgs );

	//! Scenes
	std::string CreateScene( const CVAStruct& oParams, const std::string& sName = "" );
	void GetSceneIDs( std::vector< std::string >& vsIDs ) const;
	CVASceneInfo GetSceneInfo( const std::string& sID ) const;
	std::string GetSceneName( const std::string& sID ) const;
	void SetSceneName( const std::string& sID, const std::string& sName );
	void SetSceneEnabled( const std::string& sID, const bool bEnabled = true );
	bool GetSceneEnabled( const std::string& sID ) const;

	// Sound portals
	int CreateSoundPortal( const std::string& sName = "" );
	void GetSoundPortalIDs( std::vector< int >& viIDs );
	CVASoundPortalInfo GetSoundPortalInfo( const int iID ) const;
	std::string GetSoundPortalName( const int iID ) const;
	void SetSoundPortalName( const int iID, const std::string& sName );
	void SetSoundPortalMaterial( const int iSoundPortalID, const int iMaterialID );
	int GetSoundPortalMaterial( const int iSoundPortalID ) const;
	void SetSoundPortalNextPortal( const int iSoundPortalID, const int iNextSoundPortalID );
	int GetSoundPortalNextPortal( const int iSoundPortalID ) const;
	void SetSoundPortalSoundReceiver( const int iSoundPortalID, const int iSoundReceiverID );
	int GetSoundPortalSoundReceiver( const int iSoundPortalID ) const;
	void SetSoundPortalSoundSource( const int iSoundPortalID, const int iSoundSourceID );
	int GetSoundPortalSoundSource( const int iSoundPortalID ) const;
	CVAStruct GetSoundPortalParameters( const int iID, const CVAStruct& oArgs ) const;
	void SetSoundPortalParameters( const int iID, const CVAStruct& oParams );
	void SetSoundPortalPosition( const int iSoundPortalID, const VAVec3& vPos );
	VAVec3 GetSoundPortalPosition( const int iSoundPortalID ) const;
	void SetSoundPortalOrientation( const int iSoundPortalID, const VAQuat& qOrient );
	VAQuat GetSoundPortalOrientation( const int iSoundPortalID ) const;
	void SetSoundPortalEnabled( const int iSoundPortalID, const bool bEnabled = true );
	bool GetSoundPortalEnabled( const int iSoundPortalID ) const;

	// Rendering
	void GetRenderingModules( std::vector< CVAAudioRendererInfo >&, const bool ) const;
	double GetRenderingModuleGain( const std::string& sModuleID ) const;
	void SetRenderingModuleGain( const std::string& sModuleID, const double dGain );
	bool GetRenderingModuleMuted( const std::string& sModuleID ) const;
	void SetRenderingModuleMuted( const std::string& sModuleID, const bool bMuted );
	void SetRenderingModuleParameters( const std::string& sModuleID, const CVAStruct& oParams );
	CVAStruct GetRenderingModuleParameters( const std::string& sModuleID, const CVAStruct& oParams ) const;
	int GetRenderingModuleAuralizationMode( const std::string& sModuleID ) const;
	void SetRenderingModuleAuralizationMode( const std::string& sModuleID, const int iAuralizationMode );

	// Reproduction
	void GetReproductionModules( std::vector< CVAAudioReproductionInfo >&, const bool ) const;
	void SetReproductionModuleMuted( const std::string& sModuleID, const bool bMuted );
	double GetReproductionModuleGain( const std::string& sModuleID ) const;
	void SetReproductionModuleGain( const std::string& sModuleID, const double dGain );
	bool GetReproductionModuleMuted( const std::string& sModuleID ) const;
	void SetReproductionModuleParameters( const std::string& sModuleID, const CVAStruct& oParams );
	CVAStruct GetReproductionModuleParameters( const std::string& sModuleID, const CVAStruct& oParams ) const;

	// Global controls
	bool GetInputMuted() const;
	void SetInputMuted( const bool bMuted );
	double GetInputGain() const;
	void SetInputGain( const double dGain );
	double GetOutputGain() const;
	void SetOutputGain( const double dGain );
	bool GetOutputMuted() const;
	void SetOutputMuted( const bool bMuted );
	int GetGlobalAuralizationMode() const;
	void SetGlobalAuralizationMode( const int iAuralizationMode );
	int GetActiveSoundReceiver() const;
	void SetActiveSoundReceiver( const int iSoundReceiverID );
	int GetActiveSoundReceiverExplicitRenderer( const std::string& sRendererID ) const;
	void SetActiveSoundReceiverExplicitRenderer( const int iSoundReceiverID, const std::string& sRendererID );
	double GetCoreClock() const;
	void SetCoreClock( const double dSeconds );

	std::string SubstituteMacros( const std::string& sStr ) const;
	std::string FindFilePath( const std::string& sFilePath ) const;

	CVAObjectInfo GetObjectInfo() const;
	CVAStruct CallObject( const CVAStruct& oArgs );

	// Ticker callback
	bool operator()();

	const CVACoreConfig* GetCoreConfig() const;
	const CVASceneManager* GetSceneManager() const;
	const CVAAudioSignalSourceManager* GetSignalSourceManager() const;

	bool IsStreaming() const;

	CVAHomogeneousMedium oHomogeneousMedium;


private:
	ITACriticalSection m_csReentrance;  //!< Lock for reentrance check
	ITACriticalSection m_csSyncMod;		//!< Lock für synchronisierte Szene-Modifikationen
	VistaMutex m_mxSyncModLock;			//!< Synchronisierungs-Token
	ITAAtomicLong m_lSyncModOwner;		//!< ID des Thread der Synchronisierungs-Token hat
	long m_lSyncModSpinCount;			//!< Spin count der Locks auf das Synchronisierungs-Token
	double m_dSyncEntryTime;			//!< Time when beginsync was entered

	CVAObjectRegistry m_oModules;		//!< Komponenten Registrierung

	ITAAtomicInt m_iGlobalAuralizationMode;
	VistaTicker* m_pTicker;

	void InitializeAudioDriver();
	void FinalizeAudioDriver();
	void FinalizeRenderingModules();
	void FinalizeReproductionModules();

	IVAAudioDriverBackend* m_pAudioDriverBackend;
	ITAStreamAmplifier* m_pInputAmp;
	ITAStreamPatchbay* m_pR2RPatchbay;		//!< Patchbay linking audio renderers to audio reproduction modules
	ITAStreamPatchbay* m_pOutputPatchbay;	//!< Patchbay linking audio reproduction modules to hardware outputs

	ITAStreamDetector* m_pInputStreamDetector;
	ITAStreamDetector* m_pOutputStreamDetector;

	CVAAudiostreamTracker* m_pOutputTracker;

	// --= Signal sources and sounds =----------------------------------

	CVAAudioSignalSourceManager* m_pSignalSourceManager;
	ITASoundSamplePool* m_pGlobalSamplePool;
	ITASoundSampler* m_pGlobalSampler;	// Sequencer für ambient sounds

	// --= Directivity & HRIRs =----------------------------------------

	CVADirectivityManager* m_pDirectivityManager;

	// --= Rendering & Reproduction =-----------------------------------

	class CVAAudioRendererDesc : public CVAAudioRendererInfo
	{
	public:
		IVAAudioRenderer* pInstance;
		int iR2RPatchBayInput;							//!< Input on the renderer-reproduction patch bay
		std::vector< std::string > vsOutputs;			//!< Target reproduction modules
		ITAStreamProbe* pOutputRecorder; //!< If a non-nullptr recorder is set, it will be deleted on destruction
		ITAStreamDetector* pOutputDetector; //!< If a non-nullptr detector is set, it will be deleted on destruction

		inline CVAAudioRendererDesc( IVAAudioRenderer* pInstance )
			: pInstance( pInstance )
			, pOutputRecorder( NULL )
			, pOutputDetector( NULL )
		{
		};

		inline void Finalize()
		{
			delete pInstance;
			delete pOutputRecorder;
			delete pOutputDetector;
		};
	};
	std::vector< CVAAudioRendererDesc > m_voRenderers;

	class CVAAudioReproductionModuleDesc : public CVAAudioReproductionInfo
	{
	public:
		IVAAudioReproduction* pInstance;
		int iR2RPatchBayOutput;				//! Output channel on the renderer-reproduction patch bay
		int iOutputPatchBayInput;			//! Input channel on the output patchbay
		std::vector< const CVAHardwareOutput* > vpOutputs;
		ITAStreamDetector* pInputDetector; //!< If a non-nullptr detector is set, it will be deleted on destruction
		ITAStreamDetector* pOutputDetector; //!< If a non-nullptr detector is set, it will be deleted on destruction
		ITAStreamProbe* pInputRecorder; //!< If a non-nullptr recorder is set, it will be deleted on destruction
		ITAStreamProbe* pOutputRecorder; //!< If a non-nullptr recorder is set, it will be deleted on destruction

		inline CVAAudioReproductionModuleDesc( IVAAudioReproduction* pInstance )
			: pInstance( pInstance )
			, pInputDetector( NULL )
			, pOutputDetector( NULL )
			, pInputRecorder( NULL )
			, pOutputRecorder( NULL )
		{
		};

		inline void Finalize()
		{
			delete pInstance;
			delete pInputDetector;
			delete pOutputDetector;
			delete pInputRecorder;
			delete pOutputRecorder;
		};
	};
	std::vector< CVAAudioReproductionModuleDesc > m_voReproductionModules;

	// --= Scene =------------------------------------------------------

	CVASceneManager* m_pSceneManager;
	CVASceneState* m_pNewSceneState;	//! Neuer Szenezustand für Änderungen

	ITAAtomicInt m_iCurActiveSoundReceiver;	// Aktueller aktiver Hörer im Core-Thread
	ITAAtomicInt m_iNewActiveSoundReceiver;	// Neuer aktiver Hörer für den Core-Thread
	ITAAtomicInt m_iUpdActiveSoundReceiver;	// Neuer aktiver Hörer (Puffer für Änderung)

	// --= Progress =---------------------------------------------------

	ITACriticalSection m_csProgress;
	CVAProgress m_Progress;	// Fortschritt der momentan durchgeführten Aktion

	// --= Core-Thread =------------------------------------------------

	CVACoreThread* m_pCoreThread;

	class CVACoreThreadData
	{
	public:
		int iSceneStateID;					// Szene-Zustand (ID)

	};

	CVACoreThreadData m_xCoreThreadData;
	CVASceneState* m_pCurSceneState;

	// --= Other =------------------------------------------------------

	CVACoreConfig m_oCoreConfig;
	ITAAtomicInt m_iState;					// Modaler Zustand (siehe core states)
	double m_dOutputGain, m_dInputGain;
	bool m_bOutputMuted, m_bInputMuted;

	ITAStreamProbe* m_pStreamProbeDeviceInput;
	ITAStreamProbe* m_pStreamProbeFinal;

	ITAClock* m_pClock;
	double m_dStreamClockOffset;			// Offset des Stream-Starts
	ITAAtomicFloat m_fCoreClockOffset;		// Offset der Core-Clock

	// --= Profiling =--

	CVAProfilerMeasure m_oCoreThreadLoopTotalDuration;

	// -------------------------------------------------

	void InitProgress( const std::string& sAction, const std::string& sSubaction, const int iMaxStep );

	void SetProgress( const std::string& sAction, const std::string& sSubaction, const int iCurrentStep );

	void FinishProgress();

	CVACoreEventManager* m_pEventManager;

	// Diese Hilfsmethoden bedienen die Locks um die serielle Ausführung
	// externer Methoden zu gewährleisten und die Reentrance zu blockieren
	// (siehe externe Methoden).

	// Schleifen-Funktion des Kern-Threads
	void CoreThreadLoop();

	//!< Sends detector values to clients
	void SendAudioDeviceDetectorUpdateEvent();

	//!< Sends rendering detector values to clients
	void SendRenderingModuleOutputDetectorsUpdateEvents();

	//!< Sends reproduction detector values to clients
	void SendReproductionModuleOIDetectorsUpdateEvents();

	//! Initialize rendering modules
	void InitializeAudioRenderers();

	//! Initialize reproduction modules
	void InitializeReproductionModules();

	//! Patch renderer and reproduction modules
	void PatchRendererToReproductionModules();

	//! Patch reproduction modules and output
	void PatchReproductionModulesToOutput();

	//! Recursive file search
	void RecursiveFileList( const std::string& sBasePath, CVAStruct&, const std::string sFileSuffixMask ) const;
	void FileList( const std::string& sBasePath, std::vector< std::string >& vsFileList, const std::string& sFileSuffixMask ) const;
	void FolderList( const std::string& sBasePath, std::vector< std::string >& vsFolderList ) const;

	friend class CVACoreThread;
	friend class CVAAudioSignalSourceManager;
};

#endif // IW_VA_CORE_IMPL

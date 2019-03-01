/*
 *
 *    VVV        VVV A
 *     VVV      VVV AAA        Virtual Acoustics
 *      VVV    VVV   AAA       Real-time auralisation for Virtual Reality    
 *       VVV  VVV     AAA
 *        VVVVVV       AAA     (c) Copyright Institut für Technische Akustik (ITA)
 *         VVVV         AAA        RWTH Aachen (http://www.akustik.rwth-aachen.de)
 *
 *  ---------------------------------------------------------------------------------
 *
 *    File:			VABinauralArtificialReverbAudioRenderer.h   
 *
 *    Purpose:		Binaural artificial reverberation audio renderer using
 *					parametric descriptors of virtual room only
 *
 *    Author(s):	Jonas Stienen (jst@akustik.rwth-aachen.de)
 *                  Lukas Aspöck (las@akustik.rwth-aachen.de)
 *
 *  ---------------------------------------------------------------------------------
 */
  
// $Id:  $

#ifndef IW_VACORE_BINAURALARTIFICIALREVERBDAUDIORENDERER
#define IW_VACORE_BINAURALARTIFICIALREVERBDAUDIORENDERER

#ifdef VACORE_WITH_RENDERER_BINAURAL_ARTIFICIAL_REVERB

// VA includes
#include <VA.h>
#include <VAObjectPool.h>
#include "../../../VASourceListenerMetrics.h"
#include "../../../Motion/VAMotionModelBase.h"
#include "../../../Motion/VASharedMotionModel.h"
#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include "../../../Scene/VAScene.h"

// ITA includes
#include <ITABufferedAudioFileWriter.h>
#include <ITADataSourceRealization.h>
#include <ITASampleFrame.h>

// 3rdParty includes
#include <tbb/concurrent_queue.h>

// STL includes
#include <list>
#include <set>

// External VA forward declarations
class CVACoreImpl;
class CVASceneState;
class CVASceneStateDiff;
class CVASignalSourceManager;
class CVASoundSourceDesc;

// External forward declarations
class ITAUPConvolution;

// Internal forward declarations
class CBARPath;
class CBARPathFactory;
class CBARSimulator;

//! Binaural Artificial Reverberation Audio Renderer
/**
  * Creates artificial reverberation for a scene using
  * parametric room information, like volume, surface area 
  * and reverberation time.
  *
  */
class CVABinauralArtificialReverbAudioRenderer : public IVAAudioRenderer, public ITADatasourceRealization, public CVAObject 
{
public:

	CVABinauralArtificialReverbAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVABinauralArtificialReverbAudioRenderer();

	CVAObjectInfo GetObjectInfo() const;
	CVAStruct CallObject( const CVAStruct& );

	void Reset();
	inline void LoadScene( const std::string& ) {};
	void UpdateScene( CVASceneState* pNewSceneState );
	void UpdateGlobalAuralizationMode( int iGlobalAuralizationMode );
	void ProcessStream( const ITAStreamInfo* pStreamInfo );
	ITADatasource* GetOutputDatasource();

protected:

	//! Internal representation of a listener
	class Listener : public CVAPoolObject 
	{
	public:
		CVAListenerDesc* pData;				//!< Unversioned data, referenceable
		CVASharedMotionModel* pMotionModel;	//!< Motion descriptor
		bool bDeleted;						//!< Deletion marker
		VAVec3 vPredPos;					//!< Estimated position
		VAVec3 vPredView;				//!< Estimated Orientation (View-Vektor)
		VAVec3 vPredUp;					//!< Estimated Orientation (Up-Vektor)
		VAVec3 vLastARUpdatePos;			//!< Last AR filter update position
		VAVec3 vLastARUpdateView;		//!< Last AR filter update view vector
		VAVec3 vLastARUpdateUp;			//!< Last AR filter update up vector

		ITASampleBuffer* psbInput;			//!< Accumulated listener input signals
		ITASampleFrame* psfOutput;			//!< Accumulated listener output signal (+reverb)
		ITAUPConvolution* pConvolverL;		//!< Artificial reverb filtering DSP element (left)
		ITAUPConvolution* pConvolverR;		//!< Artificial reverb filtering DSP element (right)

		IVADirectivity* pCurHRIR;
		IVADirectivity* pNewHRIR;

		ITAAtomicBool bARFilterUpdateRequired;

		void PreRequest() 
		{
			CVABasicMotionModel::Config oDefaultConfig;
			oDefaultConfig.bLogEstimatedOutputEnabled = false;
			oDefaultConfig.bLogInputEnabled = false;
			oDefaultConfig.dWindowDelay = 0.1;
			oDefaultConfig.dWindowSize = 0.1;
			oDefaultConfig.iNumHistoryKeys = 1000;
			pMotionModel = new CVASharedMotionModel( new CVABasicMotionModel( oDefaultConfig ), true );

			pData = NULL;
			psbInput = NULL;
			psfOutput = NULL;
			pCurHRIR = NULL;
			pNewHRIR = NULL;
			bARFilterUpdateRequired = true;
			vLastARUpdateView.Set( 0, 0, -1 );
			vLastARUpdateUp.Set( 0, 1, 0 );
		};
		
		void PreRelease() 
		{
			delete pMotionModel;
			pMotionModel = NULL;
		};
	};

private:

	//! Internal representation of a sound source
	class Source : public CVAPoolObject
	{
	public:
		CVASoundSourceDesc* pData;			//!< Unversioned data, referenceable
		CVASharedMotionModel* pMotionModel;	//!< Motion descriptor
		bool bDeleted;						//!< Deletion marker
		VAVec3 vPredPos;					//!< Estimated position
		VAVec3 vPredView;				//!< Estimated Orientation (View-Vector)
		VAVec3 vPredUp;					//!< Estimated Orientation (Up-Vector)

		inline void PreRequest()
		{
			pData = NULL;
			
			CVABasicMotionModel::Config oDefaultConfig;
			oDefaultConfig.bLogEstimatedOutputEnabled = false;
			oDefaultConfig.bLogInputEnabled = false;
			oDefaultConfig.dWindowDelay = 0.1;
			oDefaultConfig.dWindowSize = 0.1;
			oDefaultConfig.iNumHistoryKeys = 1000;
			pMotionModel = new CVASharedMotionModel( new CVABasicMotionModel( oDefaultConfig ), true );
		};

		inline void PreRelease()
		{
			delete pMotionModel;
			pMotionModel = NULL;
		};

		inline double GetCreationTimestamp() const
		{ 
			return m_dCreationTimeStamp; 
		};

	private:
		double m_dCreationTimeStamp;  //!< date of creation in streaming context
	};

	const CVAAudioRendererInitParams m_oParams; //!< Create a const copy of the init params

	CVACoreImpl* m_pCore;					//!< Pointer to VACore
	
	double m_dReverberationTime;			//!< Reverberation time [s]
	double m_dRoomVolume;					//!< Room volume [m^3]
	double m_dRoomSurfaceArea;				//!< Room surface area [m^2]
	double m_dSoundPowerCorrectionFactor;			//!< Sound power correction factor (BRIR vs. Direct Sound level)

	double m_dTimeSlotResolution;			//!< Resolution of time slots for AR calculation
	double m_dMaxReflectionDensity;
	double m_dScatteringCoefficient;

	int m_iMaxReverbFilterLengthSamples;			//!< Maximum length of the artificial reverb filter DSP module

	int m_iDefaultVDLSwitchingAlgorithm;	//!< Switching algorithm of variable delay lines

	double m_dPosThreshold;					//!< Update threshold for listener movement [m]
	double m_dAngleThreshold;				//!< Update threshold for listener orientation [degree]

	CVASceneState* m_pCurSceneState;		//!< Pointer to current scene in renderer
	CVASceneState* m_pNewSceneState;		//!< Pointer to new scene in renderer

	int m_iCurGlobalAuralizationMode;		//!< Current auralization mode in renderer
	
	IVAObjectPool* m_pARPathPool;			//!< Storage manager for artificial reverberation paths
	CBARPathFactory* m_pARPathFactory;		//!< Path creation factory
	std::list< CBARPath* > m_lARPaths;		//!< List of all artificial reverb paths (in renderer context)	

	IVAObjectPool* m_pSourcePool;			//!< Storage manager for sources
	IVAObjectPool* m_pListenerPool;			//!< Storage manager for listener	

	CBARSimulator* m_pARSimulator;			//!< Artificial reverberation simulation instance
	ITAAtomicBool m_bForceARUpdateOnce;			//!< Triggers a forced AR update on next stream process loop

	std::map< int, Source* > m_mSources;	//!< Internal list of sources
	std::map< int, Listener* > m_mListener;	//!< Internal list of listener


	//! Data in context of audio process
	struct
	{
		std::list< CBARPath* > m_lARPaths;						//!< List of all artificial reverb paths
		tbb::concurrent_queue< CBARPath* > m_qpNewARPaths;		//!< Lock-free queue: new paths
		tbb::concurrent_queue< CBARPath* > m_qpDelARPaths;		//!< Lock-free queue: removed paths
		std::list< Source* > m_lSources;						//!< List of sources
		tbb::concurrent_queue< Source* > m_qpNewSources;		//!< Lock-free queue: new sources
		tbb::concurrent_queue< Source* > m_qpDelSources;		//!< Lock-free queue: removed sources
		std::list< Listener* > m_lListener;						//!< List of sources
		tbb::concurrent_queue< Listener* > m_qpNewListeners;	//!< Lock-free queue: new listener
		tbb::concurrent_queue< Listener* > m_qpDelListeners;	//!< Lock-free queue: removed listener
		ITASampleBuffer m_sbTempBuf1;
		ITAAtomicInt m_iResetFlag; // 0=normal_op, 1=reset_request, 2=reset_ack
		ITAAtomicInt m_iStatus; // 0=stopped, 1=running
	} ctxAudio;


	//! Initialize renderer using the configuration passed through by arguments struct
	/** 
	  * \note	This method is called in constructor of renderer and throughs exceptions.
	  *			On error, the renderer is not created and the pointer remains unusable.
	  *
	  */
	void Init( const CVAStruct& oArgs );

	
	// --= Update internal lists (sources, listener, artificial reverb paths) =--

	void ManageArtificialReverbPaths( const CVASceneStateDiff* pDiff );
	Listener* CreateListener( const int iID );
	void DeleteListener( const int iID);
	Source* CreateSource( const int iID );
	void DeleteSource( const int iID );
	void CreateArtificialReverbPath( Source* pSource, Listener* pListener );
	void DeleteArtificialReverbPath( CBARPath* pPath );

	
	// --= Async updates (user context) =--

	void UpdateTrajectories();
	void UpdateArtificialReverbPaths( const bool bForceUpdate = true );

	//! Update artificial reverberation filter for a listener
	/**
	  * \note This method is not thread safe
	  */
	void UpdateArtificialReverbFilter( Listener* ) const;
		
	// --= Sync updates (audio thread context) =--

	void SampleTrajectoriesInternal( const double dTime );
	void SyncInternalData();
	void ResetInternalData();


	friend class CBARPath;
	friend class CBARSimulator;

	//! Not for use, avoid C4512
	inline CVABinauralArtificialReverbAudioRenderer operator=( const CVABinauralArtificialReverbAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_BINAURAL_ARTIFICIAL_REVERB

#endif // IW_VACORE_BINAURALARTIFICIALREVERBDAUDIORENDERER

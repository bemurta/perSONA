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


#ifndef IW_VACORE_PT_GENERIC_PATH_AUDIO_RENDERER
#define IW_VACORE_PT_GENERIC_PATH_AUDIO_RENDERER

#ifdef VACORE_WITH_RENDERER_PROTOTYPE_GENERIC_PATH

// VA includes
#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include "../../../Scene/VAScene.h"
#include <VA.h>
#include <VAObjectPool.h>
#include"../../../VACoreImpl.h"

// ITA includes
#include <ITASampleFrame.h>
#include <ITAStreamInfo.h>
#include <ITADataSourceRealization.h>

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
class ITADatasourceRealization;

// Internal forwards
class CVAPTGenericSoundPath;
class CVAPTGenericSoundPathFactory;

//! Generic sound path audio renderer
/**
  * The generic sound path audio renderer is a convolution-only
  * engine that renders single input multiple channel impulse responses 
  * using the sound source signals. For each sound path (source -> listener)
  * a convolution processor is created that can be feeded with an arbitrary IR
  * by the user, hence the name 'generic'.
  *
  */
class CVAPTGenericPathAudioRenderer : public IVAAudioRenderer, public ITADatasourceRealizationEventHandler
{
public:
	CVAPTGenericPathAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVAPTGenericPathAudioRenderer();
	void Reset();
	inline void LoadScene( const std::string& ) {};
	void UpdateScene( CVASceneState* );
	void UpdateGlobalAuralizationMode( int );
	ITADatasource* GetOutputDatasource();

	void HandleProcessStream( ITADatasourceRealization*, const ITAStreamInfo* );
	void HandlePostIncrementBlockPointer( ITADatasourceRealization* ) {};

	void SetParameters( const CVAStruct& );
	CVAStruct GetParameters( const CVAStruct& ) const;

	std::string HelpText() const;

protected:

	//! Internal source representation
	class CVAPTGPSource : public CVAPoolObject 
	{
	public:
		CVASoundSourceDesc* pData; //!< (Unversioned) Source description
		bool bDeleted;
	};


	//! Internal listener representation
	class CVAPTGPListener : public CVAPoolObject 
	{
	public:
		CVACoreImpl* pCore;
		CVAListenerDesc* pData;				//!< (Unversioned) Listener description
		bool bDeleted;
		ITASampleFrame* psfOutput;
	};

private:

	const CVAAudioRendererInitParams m_oParams; //!< Create a const copy of the init params

	CVACoreImpl* m_pCore;					//!< Pointer to VACore

	CVASceneState* m_pCurSceneState;
	CVASceneState* m_pNewSceneState;

	int m_iCurGlobalAuralizationMode;
	
	IVAObjectPool* m_pSoundPathPool;
	CVAPTGenericSoundPathFactory* m_pSoundPathFactory;
	std::list< CVAPTGenericSoundPath* > m_lSoundPaths;	//!< List of sound paths in user context (VACore calls)	

	IVAObjectPool* m_pSourcePool;
	IVAObjectPool* m_pListenerPool;

	std::map< int, CVAPTGPSource* > m_mSources;	//!< Internal list of sources
	std::map< int, CVAPTGPListener* > m_mListeners;	//!< Internal list of listener
		
	int m_iIRFilterLengthSamples; //!< Length of the HRIR filter DSP module
	int m_iNumChannels;		      //!< Number of channels per sound path
	bool m_bOutputMonitoring;     //!< Shows output infos / warnings if the overall listener output is zero (no filter loaded)
	ITADatasourceRealization* m_pOutput;
	ITASampleBuffer m_sfTempBuffer;

	class CVAPTGPUpdateMessage : public CVAPoolObject
	{
	public:
		std::list< CVAPTGPSource* > vNewSources;
		std::list< CVAPTGPSource* > vDelSources;
		std::list< CVAPTGPListener* > vNewListeners;
		std::list< CVAPTGPListener* > vDelListeners;
		std::list< CVAPTGenericSoundPath* > vNewPaths;
		std::list< CVAPTGenericSoundPath* > vDelPaths;

		inline void PreRequest()
		{
			vNewSources.clear();
			vDelSources.clear();
			vNewListeners.clear();
			vDelListeners.clear();
			vNewPaths.clear();
			vDelPaths.clear();
		};
	};

	IVAObjectPool* m_pUpdateMessagePool; // really necessary?
	CVAPTGPUpdateMessage* m_pUpdateMessage;

	//! Data in context of audio process
	struct
	{
		tbb::concurrent_queue< CVAPTGPUpdateMessage* > m_qpUpdateMessages;	//!< Update messages list
		std::list< CVAPTGenericSoundPath* > m_lSoundPaths;	//!< List of sound paths
		std::list< CVAPTGPSource* > m_lSources;			//!< List of sources
		std::list< CVAPTGPListener* > m_lListener;		//!< List of listeners
		ITAAtomicInt m_iResetFlag;	//!< Reset status flag: 0=normal_op, 1=reset_request, 2=reset_ack
		ITAAtomicInt m_iStatus;		//!< Current status flag: 0=stopped, 1=running
	} ctxAudio;

	void Init( const CVAStruct& );

	void ManageSoundPaths( const CVASceneState* pCurScene,
		                   const CVASceneState* pNewScene,
						   const CVASceneStateDiff* pDiff );
	void UpdateSources();
	CVAPTGPListener* CreateListener( int iID, const CVAReceiverState* );
	void DeleteListener( int iID );
	CVAPTGPSource* CreateSource( int iID, const CVASoundSourceState* );
	void DeleteSource( int iID );
	CVAPTGenericSoundPath* CreateSoundPath( CVAPTGPSource*, CVAPTGPListener* );
	void DeleteSoundPath( CVAPTGenericSoundPath* );
	
	void SyncInternalData();
	void ResetInternalData();

	void UpdateGenericSoundPath( int iListenerID, int iSourceID, const std::string& sIRFilePath );
	void UpdateGenericSoundPath( int iListenerID, int iSourceID, int iChannel, const std::string& sIRFilePath );
	void UpdateGenericSoundPath( int iListenerID, int iSourceID, ITASampleFrame& sfIR );
	void UpdateGenericSoundPath( int iListenerID, int iSourceID, int iChannel, ITASampleBuffer& sbIR );
	void UpdateGenericSoundPath( const int iListenerID, const int iSourceID, const double dDelaySeconds );

	friend class CVAPTGenericSoundPath;
	friend class CVAPTGPListenerPoolFactory;
	friend class CVAPTGPSourcePoolFactory;

	//! Not for use, avoid C4512
	inline CVAPTGenericPathAudioRenderer operator=( const CVAPTGenericPathAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_PROTOTYPE_GENERIC_PATH

#endif // IW_VACORE_PT_GENERIC_PATH_AUDIO_RENDERER

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

#ifndef IW_VACORE_AMBIENT_MIXER_AUDIO_RENDERER
#define IW_VACORE_AMBIENT_MIXER_AUDIO_RENDERER

#ifdef VACORE_WITH_RENDERER_AMBIENT_MIXER

// VA includes
#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include <VA.h>

// ITA includes
#include <ITADataSourceRealization.h>

//! Ambient Mixer Audio Renderer
/**
  * The mixer for ambient sources routes all sound sources with 
  * a signal that are not explicitly used by another renderer to
  * the listenr output. It applies the gains in the chain but does
  * not consider any auralization modes.
  *
  */
class CVAAmbientMixerAudioRenderer : public IVAAudioRenderer, ITADatasourceRealizationEventHandler
{
public:
	CVAAmbientMixerAudioRenderer( const CVAAudioRendererInitParams& );
	virtual ~CVAAmbientMixerAudioRenderer();

	void Reset();
	inline void LoadScene( const std::string& ) {};
	void UpdateScene( CVASceneState* );
	inline void UpdateGlobalAuralizationMode( int ) {};
	void HandleProcessStream( ITADatasourceRealization*, const ITAStreamInfo* );
	inline void HandlePostIncrementBlockPointer( ITADatasourceRealization* ) {};
	ITADatasource* GetOutputDatasource();

private:
	ITADatasourceRealization* m_pDataSource;
	const CVAAudioRendererInitParams m_oParams; //!< Create a const copy of the init params
	CVASceneState* m_pNewSceneState;
	CVASceneState* m_pCurSceneState;
	ITAAtomicBool m_bIndicateReset, m_bResetAck;
	inline CVAAmbientMixerAudioRenderer operator=( const CVAAmbientMixerAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_AMBIENT_MIXER

#endif // IW_VACORE_AMBIENT_MIXER_AUDIO_RENDERER

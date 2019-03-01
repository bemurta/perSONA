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

#ifndef IW_VACORE_BINAURAL_OUTDOOR_NOISE_AUDIO_RENDERER
#define IW_VACORE_BINAURAL_OUTDOOR_NOISE_AUDIO_RENDERER

#if VACORE_WITH_RENDERER_BINAURAL_OUTDOOR_NOISE

// VA includes
#include <VA.h>
#include <VAObjectPool.h>

#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include "../../VAOutdoorNoiseAudioRenderer.h"

#include "../../../Motion/VAMotionModelBase.h"
#include "../../../Motion/VASharedMotionModel.h"
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
class CVABinauralOutdoorNoiseAudioRenderer : public CVAOutdoorNoiseAudioRenderer, public ITADatasourceRealization
{
public:
	CVABinauralOutdoorNoiseAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVABinauralOutdoorNoiseAudioRenderer();

	//! Render output sample blocks
	/**
	  * This method renders the sound propagation based on the binaural approach
	  * by evaluating the abstract incidence waves at receiver, which are convolved
	  * using an HRTF for each direction.
	  */
	void ProcessStream( const ITAStreamInfo* pStreamInfo );

	//! Returns the renderers output stream datasource
	ITADatasource* GetOutputDatasource();

private:
	int m_iHRIRFilterLength;				//!< Length of the HRIR filter DSP module

	//! Not for use, avoid C4512
	inline CVABinauralOutdoorNoiseAudioRenderer operator=( const CVABinauralOutdoorNoiseAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_BINAURAL_OUTDOOR_NOISE

#endif // IW_VACORE_BINAURAL_OUTDOOR_NOISE_AUDIO_RENDERER

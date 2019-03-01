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

#ifndef IW_VACORE_PT_DUMMY_AUDIO_RENDERER
#define IW_VACORE_PT_DUMMY_AUDIO_RENDERER

#ifdef VACORE_WITH_RENDERER_PROTOTYPE_DUMMY

// VA includes
#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include <VA.h>

// STL includes
#include <string>

// ITA includes
#include <ITADataSourceRealization.h>

//! Dummy Audio Renderer
/**
  * The dummy renderer prototype is for core testing purposes and
  * can also serve as a simple example if you start with
  * your own renderer from scratch.
  * It basically does nothing and is implemented header only.
  * To create a valid output data source, the first output
  * is used to determine the number of output channels. The
  * output buffer will be filled with zeros.
  *
  */
class CVAPTDummyAudioRenderer : public IVAAudioRenderer, ITADatasourceRealizationEventHandler
{
public:
	CVAPTDummyAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVAPTDummyAudioRenderer();

	inline void Reset() {};
	inline void LoadScene( const std::string& ) {};
	inline void UpdateScene( CVASceneState* ) {};
	inline void UpdateGlobalAuralizationMode( int ) {};
	void HandleProcessStream( ITADatasourceRealization*, const ITAStreamInfo* );
	inline void HandlePostIncrementBlockPointer( ITADatasourceRealization* ) {};
	inline ITADatasource* GetOutputDatasource() { return m_pDataSource; };

private:
	ITADatasourceRealization* m_pDataSource;
	const CVAAudioRendererInitParams oParams; //!< Create a const copy of the init params

	//! Not for use, avoid C4512
	inline CVAPTDummyAudioRenderer operator=( const CVAPTDummyAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // VACORE_WITH_RENDERER_PROTOTYPE_DUMMY

#endif // IW_VACORE_PT_DUMMY_AUDIO_RENDERER

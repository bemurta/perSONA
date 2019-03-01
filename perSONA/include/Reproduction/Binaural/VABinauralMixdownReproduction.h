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

#ifndef IW_VACORE_BINAURALDOWNMIXREPRODUCTION
#define IW_VACORE_BINAURALDOWNMIXREPRODUCTION

#ifdef VACORE_WITH_REPRODUCTION_BINAURAL_MIXDOWN

#include "../VAAudioReproduction.h"
#include "../VAAudioReproductionRegistry.h"
#include "../../VACoreImpl.h"
#include "../../Scene/VAMotionState.h"

#include <ITASampleFrame.h>

class ITADatasource;
class StreamFilter;

class CVABinauralMixdownReproduction : public IVAAudioReproduction
{
public:
	CVABinauralMixdownReproduction( const CVAAudioReproductionInitParams& oParams );
	~CVABinauralMixdownReproduction();

	void SetInputDatasource( ITADatasource* );
	ITADatasource* GetOutputDatasource();
	int GetNumInputChannels() const;

	std::vector< const CVAHardwareOutput* > GetTargetOutputs() const;

	//! Returns number of virtual loudspeaker
	int GetNumVirtualLoudspeaker() const;

	//! Sets the active listener of this reproduction module
	/**
	  * Information on virtual position of listener is used
	  * for binaural downmix with related HRIR.
	  */
	void SetTrackedListener( const int iListenerID );

	void UpdateScene( CVASceneState* pNewState );

private:
	
	std::string m_sName;
	CVAAudioReproductionInitParams m_oParams;

	int m_iHRIRFilterLength;
	
	std::vector< const CVAHardwareOutput* > m_vpTargetOutputs;
	const CVAHardwareOutput* m_pVirtualOutput;

	int m_iListenerID;

	ITASampleFrame m_sfHRIRTemp;
	StreamFilter* m_pdsStreamFilter;

	std::vector< int > m_viLastHRIRIndex;
};

#endif // VACORE_WITH_REPRODUCTION_BINAURAL_MIXDOWN

#endif // IW_VACORE_BINAURALDOWNMIXREPRODUCTION

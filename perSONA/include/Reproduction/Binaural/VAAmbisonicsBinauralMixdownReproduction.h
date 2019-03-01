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

#ifndef IW_VACORE_AMBISONICSBINAURALMIXDOWNREPRODUCTION
#define IW_VACORE_AMBISONICSBINAURALMIXDOWNREPRODUCTION

#ifdef VACORE_WITH_REPRODUCTION_AMBISONICS_BINAURAL_MIXDOWN

#include "../VAAudioReproduction.h"
#include "../VAAudioReproductionRegistry.h"
#include "../../VACoreImpl.h"
#include "Eigen\dense"
#include "Eigen\SVD"
#include "Eigen\Jacobi"

#include <ITASampleFrame.h>

class ITADatasource;
class ITAStreamPatchbay;
class CMixdownStreamFilter;

class CVAAmbisonicsBinauralMixdownReproduction : public IVAAudioReproduction
{
public:
	CVAAmbisonicsBinauralMixdownReproduction( const CVAAudioReproductionInitParams& oParams );
	~CVAAmbisonicsBinauralMixdownReproduction();

	void SetInputDatasource( ITADatasource* );
	ITADatasource* GetOutputDatasource();
	int GetNumInputChannels() const;
	
	int GetAmbisonicsTruncationOrder() const;

	//! Returns number of virtual loudspeaker
	int GetNumVirtualLoudspeaker() const;

	//! Sets the active listener of this reproduction module
	/**
	  * Information on virtual position of listener is used
	  * for binaural downmix with related HRIR.
	  */
	void SetTrackedListener( const int iListenerID );
	Eigen::MatrixXd CalculatePseudoInverse(Eigen::MatrixXd);
	void UpdateScene( CVASceneState* pNewState );

private:
	
	std::string m_sName;
	CVAAudioReproductionInitParams m_oParams;

	int m_iHRIRFilterLength;
	int m_iAmbisonicsTruncationOrder;

	std::vector< const CVAHardwareOutput* > m_vpTargetOutputs;
	const CVAHardwareOutput* m_pVirtualOutput;

	int m_iListenerID;

	ITASampleFrame m_sfHRIRTemp;
	CMixdownStreamFilter* m_pdsStreamFilter;
	ITAStreamPatchbay* m_pDecoderMatrixPatchBay;

	std::vector< int > m_viLastHRIRIndex;
};

#endif // VACORE_WITH_REPRODUCTION_AMBISONICS_BINAURAL_MIXDOWN

#endif // IW_VACORE_AMBISONICSBINAURALMIXDOWNREPRODUCTION

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

#ifndef IW_VACORE_AMBISONICSREPRODUCTION
#define IW_VACORE_AMBISONICSREPRODUCTION

#ifdef VACORE_WITH_REPRODUCTION_AMBISONICS

#include "../VAAudioReproduction.h"
#include "../VAAudioReproductionRegistry.h"
#include "../../VACoreImpl.h"
#include <ITASampleFrame.h>
#include <ITANumericUtils.h>
#include <ITAConstants.h>
#include "Eigen\Dense"
#include "Eigen\SVD"
#include "Eigen\Jacobi"
#include "Eigen\Core"
#include "Eigen\Eigen"

class ITAStreamPatchbay;

class CVAAmbisonicsReproduction : public IVAAudioReproduction
{
public:
	CVAAmbisonicsReproduction( const CVAAudioReproductionInitParams& oParams );
	~CVAAmbisonicsReproduction();

	void SetInputDatasource( ITADatasource* );
	ITADatasource* GetOutputDatasource();
	int GetNumInputChannels() const;	
	int GetAmbisonicsTruncationOrder() const;
	void UpdateScene( CVASceneState* pNewState );
	Eigen::MatrixXd CalculatePseudoInverse(Eigen::MatrixXd);

private:
	void GetCalculatedReproductionCenterPos(VAVec3 &vec3CalcPos);
	bool m_bUseRemax;
	std::string m_sName;
	CVAAudioReproductionInitParams m_oParams;
	int m_iAmbisonicsTruncationOrder;
	std::vector< const CVAHardwareOutput* > m_vpTargetOutputs;	
	ITAStreamPatchbay* m_pDecoderMatrixPatchBay;
	VAVec3 m_v3ReproductionCenterPos;
	const CVAHardwareOutput* m_pOutput;
};

#endif // VACORE_WITH_REPRODUCTION_AMBISONICS

#endif // IW_VACORE_AMBISONICSREPRODUCTION

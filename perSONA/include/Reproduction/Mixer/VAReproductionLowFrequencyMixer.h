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

#ifndef IW_VA_REPRODUCTION_MIXER_LOW_FREQUENCY
#define IW_VA_REPRODUCTION_MIXER_LOW_FREQUENCY

#ifdef VACORE_WITH_REPRODUCTION_MIXER_LOW_FREQUENCY

#include "../../VACoreConfig.h"
#include "../VAAudioReproduction.h"
#include "../VAAudioReproductionRegistry.h"

#include <ITADataSourceRealization.h>

#include <vector>

class CVAReproductionLowFrequencyMixer : public IVAAudioReproduction, public ITADatasourceRealizationEventHandler
{
public:
	CVAReproductionLowFrequencyMixer( const CVAAudioReproductionInitParams& oParams );
	~CVAReproductionLowFrequencyMixer();
	void SetInputDatasource( ITADatasource* pDatasource);
	ITADatasource* GetOutputDatasource();
	int GetNumInputChannels() const;
	inline void UpdateScene( CVASceneState* pNewState ) {};
	void HandleProcessStream( ITADatasourceRealization* pSender, const ITAStreamInfo* pStreamInfo );
	void HandlePostIncrementBlockPointer(ITADatasourceRealization* pSender );

private:
	ITADatasource* m_pdsInputDataSource;
	ITADatasourceRealization* m_pdsOutputDataSource;
	CVAAudioReproductionInitParams m_oParams;
	std::vector< int > m_viMixingChannels; // Logical mixing channel numbers, so first channel = 1 and so on
};

#endif // VACORE_WITH_REPRODUCTION_MIXER_LOW_FREQUENCY

#endif // IW_VA_REPRODUCTION_MIXER_LOW_FREQUENCY

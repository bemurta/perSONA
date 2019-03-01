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

#ifndef IW_VACORE_REPRODUCTIONTALKTHROUGH
#define IW_VACORE_REPRODUCTIONTALKTHROUGH

#ifdef VACORE_WITH_REPRODUCTION_TALKTHROUGH

#include "../../VACoreConfig.h"
#include "../VAAudioReproduction.h"
#include "../VAAudioReproductionRegistry.h"

#include <ITADataSourceRealization.h>

class CVAReproductionTalkthrough : public IVAAudioReproduction, public ITADatasourceRealizationEventHandler
{
public:
	CVAReproductionTalkthrough( const CVAAudioReproductionInitParams& oParams );
	~CVAReproductionTalkthrough();
	void SetInputDatasource( ITADatasource* pDatasource);
	ITADatasource* GetOutputDatasource();
	int GetNumInputChannels() const;
	inline void UpdateScene( CVASceneState* ) {};
	void HandleProcessStream( ITADatasourceRealization* pSender, const ITAStreamInfo* pStreamInfo );
	void HandlePostIncrementBlockPointer(ITADatasourceRealization* pSender );

private:
	ITADatasource* m_pdsInputDatasource;
	ITADatasourceRealization* m_pdsOutputDatasource;
	CVAAudioReproductionInitParams m_oParams;
};

#endif // VACORE_WITH_REPRODUCTION_TALKTHROUGH

#endif // IW_VACORE_REPRODUCTIONTALKTHROUGH

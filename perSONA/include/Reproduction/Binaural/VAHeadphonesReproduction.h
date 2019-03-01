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

#ifndef IW_VACORE_HEADPHONEREPRODUCTION
#define IW_VACORE_HEADPHONEREPRODUCTION

#ifdef VACORE_WITH_REPRODUCTION_HEADPHONES

#include "../VAAudioReproduction.h"
#include "../VAAudioReproductionRegistry.h"
#include "../../Scene/VAMotionState.h"
#include "../../VACoreImpl.h"
#include <VAObject.h>

#include <ITADataSourceRealization.h>
#include <ITASampleFrame.h>

#include <vector>

class HPEQStreamFilter;

//! Individualized static headphones reproduction
/**
  * This class implements two-channel static equalization for the perpose of individualized headphone playback
  */
class CVAHeadphonesReproduction : public IVAAudioReproduction, public CVAObject
{
public:
	CVAHeadphonesReproduction( const CVAAudioReproductionInitParams& oParams );
	~CVAHeadphonesReproduction();

	void SetInputDatasource( ITADatasource* );
	ITADatasource* GetOutputDatasource();
	int GetNumInputChannels() const;

	inline void SetTrackedListener( const int ) {};
	inline void UpdateScene( CVASceneState* ) {};

	CVAObjectInfo GetObjectInfo() const;
	CVAStruct CallObject( const CVAStruct& oArgs );

	void SetParameters( const CVAStruct& );
	CVAStruct GetParameters( const CVAStruct& ) const;

private:
	
	std::string m_sName;
	CVAAudioReproductionInitParams m_oParams;
	HPEQStreamFilter* m_pHPEQStreamFilter;
	ITASampleFrame m_sfHpIRInv;
};

#endif // VACORE_WITH_REPRODUCTION_HEADPHONES

#endif // IW_VACORE_HEADPHONEREPRODUCTION

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

#ifndef IW_VACORE_NCTCREPRODUCTION
#define IW_VACORE_NCTCREPRODUCTION

#ifdef VACORE_WITH_REPRODUCTION_BINAURAL_NCTC

#include "../VAAudioReproduction.h"
#include "../VAAudioReproductionRegistry.h"
#include "../../Scene/VAMotionState.h"
#include "../../VACoreImpl.h"
#include <VAObject.h>

#include <ITAHDFTSpectra.h>

class ITANCTC;
class ITANCTCStreamFilter;
class CVADirectivityDAFFHRIR;

class CVANCTCReproduction : public IVAAudioReproduction, public CVAObject
{
public:
	CVANCTCReproduction( const CVAAudioReproductionInitParams& oParams );
	~CVANCTCReproduction();

	void SetInputDatasource( ITADatasource* );
	ITADatasource* GetOutputDatasource();

	int GetNumInputChannels() const;

	CVAObjectInfo GetObjectInfo() const;
	CVAStruct CallObject( const CVAStruct& oArgs );

	std::vector< const CVAHardwareOutput* > GetTargetOutputs() const;

	//! Returns number of virtual loudspeaker
	int GetNumVirtualLoudspeaker() const;

	//! Sets the active listener of this reproduction module
	/**
	  * Information on virtual position of listener is used
	  * for binaural downmix with related HRIR.
	  */
	void SetTrackedListener( int iListenerID );

	void UpdateScene( CVASceneState* pNewState );

private:
	
	std::string m_sName;
	CVAAudioReproductionInitParams m_oParams;
	
	std::vector< const CVAHardwareOutput* > m_vpTargetOutputs;
	
	bool m_bTrackedListenerHRIR;
	int m_iListenerID;
	CVADirectivityDAFFHRIR* m_pDefaultHRIR;

	ITANCTC* m_pNCTC;
	ITANCTCStreamFilter* m_pdsStreamFilter;
	std::vector< ITABase::CHDFTSpectra* > m_vpSpectra;
	double m_dGain;

	mutable int m_iDebugExportCTCFilters; //! Exports given number of filters during streaming
	mutable std::string m_sDebugExportCTCFiltersBaseName; //! Export file base name
};

#endif // VACORE_WITH_REPRODUCTION_BINAURAL_NCTC

#endif // IW_VACORE_NCTCREPRODUCTION

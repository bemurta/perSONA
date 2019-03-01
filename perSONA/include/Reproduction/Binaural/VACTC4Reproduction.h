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

#ifndef IW_VACORE_CTC4REPRODUCTION
#define IW_VACORE_CTC4REPRODUCTION

#ifdef VACORE_WITH_REPRODUCTION_BINAURAL_CTC4

#include <Reproduction/VAAudioReproductionModule.h>
#include <Reproduction/VAAudioReproductionModuleRegistry.h>

class ITAQuadCTC;
class CVADirectivityDAFFHRIR;
class CVACoreConfig;

/**
 * Diese Klasse realisiert die Wiedergabe mittels getrackter
 * dynamischer 4-Kanal Übersprechkompensation (virtueller Kopfhörer).
 */

class CVACTC4Reproduction : public IVAAudioReproduction
{
public:
	CVACTC4Reproduction( const CVAAudioReproductionInitParams& oParams );
	virtual ~CVACTC4Reproduction();

	void SetInputDatasource( ITADatasource* );
	ITADatasource* GetOutputDatasource();
	int GetNumInputChannels() const;

	std::vector< const CVAHardwareOutput* > GetTargetOutputs() const;
	
	//! Sets the active listener of this reproduction module
	void SetTrackedListener( const int iListenerID );

	void UpdateScene( CVASceneState* pNewState );
		                    
protected:
	ITAQuadCTC* m_pCTC;
	CVADirectivityDAFFHRIR* m_pHRIR;
private:
	std::string m_sName;
	CVAAudioReproductionInitParams m_oParams;
	std::vector< const CVAHardwareOutput* > m_vpTargetOutputs;
	int m_iListenerID;
};

VA_REGISTER_AUDIO_REPRODUCTION_MODULE_CLASS( CVACTC4Reproduction, "CTC4" )

/*
 *  Legacy CTC by Tobias Lentz
 */

#if (VACORE_TLE_CTC==1)



#pragma comment(lib, "ITACTC-TLED")

class ITADualCTC_TLE;

class CVACTC4ReproductionTLE : public IVAReproductionModule {
public:
	CVACTC4ReproductionTLE();
	virtual ~CVACTC4ReproductionTLE();

	// --= Schnittstelle IVAReproductionModule =------------------------

	const CVAReproductionModuleParams& GetParams() const;
	ITADatasource* GetOutputStreamingDatasource() const;

	void Initialize(const CVACoreConfig* pCoreConfig, ITADatasource* pdsInput);
	void Finalize();

	void updateHeadPosition(double px, double py, double pz,
							double vx, double vy, double vz,
							double ux, double uy, double uz);
		                    
protected:
	CVAReproductionModuleParams m_params;
	ITADualCTC_TLE* m_pCTC;
};

#endif // (VACORE_TLE_CTC==1)

#endif // VACORE_WITH_REPRODUCTION_BINAURAL_CTC4

#endif // IW_VACORE_CTC4REPRODUCTION

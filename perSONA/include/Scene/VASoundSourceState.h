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

#ifndef IW_VACORE_SOUNDSOURCESTATE
#define IW_VACORE_SOUNDSOURCESTATE

#include "VASceneStateBase.h"

#include <VAStruct.h>

class CVAMotionState;
class IVADirectivity;

// Diese Klasse bescheibt den dynamischen (versionierten) Zustand einer Schallquelle
class CVASoundSourceState : public CVASceneStateBase
{
public:
	// Initialisieren (in Grundzustand versetzen)
	// (Zustand danach: Nicht-finialisiert, Referenzzähler = 0)
	void Initialize( double dModificationTime );

	// Daten eines anderen Zustand übernehmen
	void Copy( const CVASoundSourceState* pSrc, double dModificationTime );

	// Fixieren (Fixiert alle enthaltenen Objekte)
	void Fix();

	//! Getter
	const CVAMotionState* GetMotionState() const;

	//!Get sound source volume
	/**
	  * Volume / gain is calculated by dividing sound power with the amplitude calibration factor
	  * The resulting gain factor is relative to a certain sound pressure (level) at 1m, usually 94 dB SPL re 20uPa (but can also be set to 124 dB)
	  *
	  * @param[in] dAmplitudeCalibration Calibration factor amplifying source volume to achieve a certain sound pressure at amplitudes of 1.0
	  * 
	  * @sa VACoreImpl::GetAmplitudeCalibration()
	  */
	double GetVolume( const double dAmplitudeCalibration ) const;

	int GetDirectivityID() const;
	const IVADirectivity* GetDirectivityData() const;
	int GetAuralizationMode() const;

	//! Sets the sound source sound power in Watts (not dB!)
	void SetSoundPower( const double dPowerInWatts );

	//! Returns the sound source sound power in Watts (not dB!)
	double GetSoundPower() const;

	//! Setter
	void SetDirectivityID( int iDirectivityID );
	void SetDirectivityData( const IVADirectivity* pDirData );
	void SetAuralizationMode( int iAuralizationMode );

	//! Sets parameters
	void SetParameters( const CVAStruct& oParams );

	//! Returns parameters
	CVAStruct GetParameters( const CVAStruct& oArgs ) const;

	// Modifier
	CVAMotionState* AlterMotionState();

	// Als Zeichenkette zurückgeben
	//std::string ToString() const;

protected:
	//! Destruktion vor Pool-Release
	void PreRelease();

private:
	struct
	{
		CVAMotionState* pMotionState; //!< Motion state pointer
		double dSoundPower; //!< Sound power [W]
		int iAuraMode; //!< Current auralization mode
		int iDirectivityID; //!< Directivity identifier
		const IVADirectivity* pDirectivity;		//!< Pointer to directivity data set
		CVAStruct oParams; //!< Sound source parameters for special implementations

	} data;
};

#endif // IW_VACORE_SOUNDSOURCESTATE

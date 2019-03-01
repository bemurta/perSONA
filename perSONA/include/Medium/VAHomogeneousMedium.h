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

#ifndef IW_VA_HOMOGENEOUS_MEDIUM
#define IW_VA_HOMOGENEOUS_MEDIUM

#include <VABase.h>
#include <VAStruct.h>

//! Homogeneous medium definitions
class CVAHomogeneousMedium
{
public:
	double dSoundSpeed; //!< Speed of sound in m/s
	double dStaticPressurePascal; //!< Static pressure in Pascal
	double dTemperatureDegreeCentigrade; //!< Temperature in degree centigrade
	double dRelativeHumidityPercent; //!< Relative humidity
	VAVec3 v3ShiftSpeed; //!< Medium shift speed in m/s
	CVAStruct oParameters; //!< Special parameters, for prototyping

	//! Default constructor sets parameters to VA defaults defined in VABase
	inline CVAHomogeneousMedium()
	{
		dSoundSpeed = g_dDefaultSpeedOfSound;
		dStaticPressurePascal = g_dDefaultStaticPressure;
		dTemperatureDegreeCentigrade = g_dDefaultTemperature;
		dRelativeHumidityPercent = g_dDefaultRelativeHumidity;
		v3ShiftSpeed.Set( 0.0f, 0.0f, 0.0f );
	};
};

#endif // IW_VA_HOMOGENEOUS_MEDIUM

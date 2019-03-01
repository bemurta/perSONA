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

#ifndef IW_VACORE_ATMOSPHERE
#define IW_VACORE_ATMOSPHERE

#include <ITAThirdOctaveMagnitudeSpectrum.h>

//! Luftschall-Dämpfungsfaktoren in Terzen berechnen (nach ISO 9613-1:1993)
// (ACHTUNG: Andere Bedeutung als in Raven: Dämpfungsfaktoren! 1=>Keine Dämpfung)
void GetAirAbsorptionMagnitudesThirdOctave( float* pfAirAbsCoeffs,				// Absorptionskoeffizienten [Faktor/m] (nicht Dezibel!)
	double dDistance,						// Abstand [m]									   
	double dTemperature,					// Temperatur [°C]
	double dPressure,						// Statischer Luftdruck [Pa]
	double dHumidity );					// Luftfeuchtigkeit [%]

// In dB/m
void GetAirAbsorptionAttenuationThirdOctaveDecibel( float* pfAirAbsCoeffs,		// Absorptionskoeffizienten [Faktor/m] (nicht Dezibel!)
	double dDistance,				// Abstand [m]									   
	double dTemperature,			// Temperatur [°C]
	double dPressure,				// Statischer Luftdruck [Pa]
	double dHumidity );		    // Luftfeuchtigkeit [%]

//! Berechnung der Luftschall-Absorption in Terzen
/**
  * Standard: ISO 9613-1:1993
  *
  * \note Andere Bedeutung als in Raven: Dämpfungsfaktoren! 1=>Keine Dämpfung)
  *
  * \param oAirAbsMags Terzband-Resultat (Call-By-Reference)
  * \param fDistance Abstand [m]
  * \param fTemperature Temperatur [°C]
  * \param fStaticPressure Statischer Luftdruck [Pa]
  * \param fHumidity Luftfeuchtigkeit [%]
  */
void CalculateAirAbsorptionAttenuation( ITABase::CThirdOctaveFactorMagnitudeSpectrum& oAirAbsMags, double dDistance, double dTemperature, double dStaticPressure, double dHumidity );

//! Berechnung der Luftschall-Dämpfung in Terzen als Faktoren (von Jonas implementiert)
/**
  * Standard: ISO 9613-1:1993
  *
  * \note Wertebereich der gains: [0..1]
  *
  * \param oAirAbsMags Terzband-Resultat (Call-By-Reference)
  * \param fDistance Abstand [m]
  * \param fTemperature Temperatur [°C]
  * \param fStaticPressure Statischer Luftdruck [Pa]
  * \param fHumidity Luftfeuchtigkeit [%]
  */
void GetAirAbsorptionMagnitudes( ITABase::CThirdOctaveFactorMagnitudeSpectrum& oMags, double dDistance, double dTemperature, double dStaticPressure, double dHumidity );

#endif // IW_VACORE_ATMOSPHERE

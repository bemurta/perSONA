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

#ifndef IW_VACORE_SOUNDSOURCEDESC
#define IW_VACORE_SOUNDSOURCEDESC

#include <ITAAtomicPrimitives.h>
#include <VAPoolObject.h>
#include <VAAudioSignalSource.h>

class ITASampleBuffer;

//! Diese Klasse beschreibt die statischen (unversionierten) Daten einer Schallquelle
class CVASoundSourceDesc : public CVAPoolObject
{
public:
	int iID;											//!< Schallquellen-ID
	std::string sName;									//!< Angezeigter Name
	ITAAtomicBool bMuted;								//!< Stummgeschaltet? (jst: Berechnungen sollen trotzdem durchgeführt werden? bActive dazu?)
	ITAAtomicBool bEnabled;								//!< Enabled/disable from rendering
	ITAAtomicBool bInitPositionOrientation;				//!< Wurde die Position/Orientierung initialisiert? (jst: really required?)
	ITAAtomicPtr< IVAAudioSignalSource > pSignalSource;	//!< Zugeordnete Signalquelle
	ITAAtomicPtr<ITASampleBuffer> pSignalSourceInputBuf;	//!< Puffer der Eingangsdaten der Signalquelle
	float fSoundPower;
	std::string sExplicitRendererID;					//!< Explicit renderer for this sound source (empty = all)
	
	void PreRequest()
	{
		iID = -1;
		sName = "";
		bMuted = false;
		bInitPositionOrientation = false;
		pSignalSource = nullptr;
		pSignalSourceInputBuf = nullptr;
	}
};

#endif // IW_VACORE_SOUNDSOURCEDESC

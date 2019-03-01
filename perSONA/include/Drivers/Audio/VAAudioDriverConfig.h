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

#ifndef IW_VACORE_AUDIODRIVERCONFIG
#define IW_VACORE_AUDIODRIVERCONFIG

// Includes
#include <VACoreDefinitions.h>
#include <VAStruct.h>
#include <string>

//! Audio driver configuration for backends
/**
 * Diese Datenklasse speichert die Parameter für das Audio-Streaming in VA.
 * Dies schließt ein: Die Treiber-Architektur, das Audio-Gerät, usw.
 */
class CVAAudioDriverConfig
{
public:
	static const int AUTO; //!< Literal for automatic detection of channel number
	static const double DEFAULT_SAMPLERATE;	//!< Default sampling rate [Hz]

	std::string sDriver;	// Name der Treiber-Architektur (z.B. "ASIO")
	std::string sDevice;	// Name des Audiogerätes (z.B. "RME Hammerfall DSP")
	double dSampleRate;		// Abtastrate [Hz]
	int iBuffersize;		// Streaming-Puffergröße [Anzahl Samples] (0 => automatisch bestimmen)
	int iInputChannels;		// Anzahl Eingangskanäle
	int iOutputChannels;	// Anzahl Ausgangskanäle 

	CVAAudioDriverConfig();
	virtual ~CVAAudioDriverConfig();

	// Einstellungen aus einem Struct lesen
	void Init( const CVAStruct& oArgs );
};

#endif // IW_VACORE_AUDIODRIVERCONFIG

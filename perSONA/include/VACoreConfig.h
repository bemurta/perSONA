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

#ifndef IW_VACORE_CORECONFIG
#define IW_VACORE_CORECONFIG

// VA includes
#include "Drivers/Audio/VAAudioDriverConfig.h"
#include "Reproduction/VAAudioReproduction.h"
#include "VAHardwareSetup.h"
#include "VAMacroMap.h"
#include "Medium/VAHomogeneousMedium.h"

#include <VAStruct.h>

// STL includes
#include <string>
#include <vector>

class CVACoreImpl;

class CVACoreConfig
{
public:
	CVAAudioDriverConfig oAudioDriverConfig;	//!< Initial audio interface settings
	CVAHardwareSetup oHardwareSetup;			//!< Initial reproduction hardware setup

	CVAMacroMap mMacros;				//!< Macros used in configuration file
	std::vector< std::string > vsSearchPaths; //!< Search paths (existing directories only)
	
	bool bRecordDeviceInputEnabled; //!< Switch to store entire device input stream record to hard drive
	std::string sRecordDeviceInputFilePath;	//!< File path where to store the input stream record to hard drive

	bool bRecordDeviceOutputEnabled;  //!< Switch to store entire device output stream record to hard drive
	std::string sRecordFinalOutputFilePath;	//!< File path where to store the output stream record to hard drive

	int iTriggerUpdateMilliseconds;

	CVAHomogeneousMedium oInitialHomogeneousMedium; //!< Initial homogeneous medium information

	double dDefaultAmplitudeCalibration; //!< Defines the conversion between physical pressure and digital or electrical amplitudes ( usually 1.0 -> 94 dB SPL re 20uPa @ 1m )
	double dDefaultDistance; //!< Definse the default distance when spherical spreading is deactivated (auralization mode)
	double dDefaultMinimumDistance; //!< Defines the minimum distance that should be kept from a sound source (point source can get infinitely loud)

	//! Initialize the configuration with a struct
	/**
	  * \note: Errors are forwarded as exceptions, see \CVAException
	  */
	void Init( const CVAStruct& oData );

	//! Returns the static configuration as a VAStruct
	const CVAStruct& GetStruct() const;

private:
	CVAStruct m_oData;
};

#endif // IW_VACORE_CORECONFIG


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

#ifndef IW_VACORE_ROOM_ACOUSTICS_AUDIO_RENDERER_CONFIG
#define IW_VACORE_ROOM_ACOUSTICS_AUDIO_RENDERER_CONFIG

// STL Includes
#include <vector>
#include <string>


//! Room Acoustics Audio Renderer configuration class
/**
  * This class stores static configuration parameters only
  */
class CVARoomAcousticsAudioRendererConfig
{
public:
	CVARoomAcousticsAudioRendererConfig() {};
	virtual ~CVARoomAcousticsAudioRendererConfig() {};

	//! Read information from ini file
	bool ReadFromINIFile( const std::string& sINIFilename, const std::string& sSection );
	
	int iSetup;								//!< Simulation setup, see CVARoomAcousticsAudioRenderer
	std::string sSimulationDataBasePath;	//!< Data base path for simulation instances (possibly on remote machine)
	std::string sServerIP;					//!< Server adress/IP
	std::vector< std::string > vsHybridLocalTask;	//!< Type of tasks assigned for local scheduler (only for hybrid mode)
	std::vector< std::string > vsHybridRemoteTask;	//!< Type of tasks assigned for remote scheduler (only for hybrid mode)
};

#endif // IW_VACORE_ROOM_ACOUSTICS_AUDIO_RENDERER_CONFIG

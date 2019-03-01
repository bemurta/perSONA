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

#ifndef IW_VACORE_CORE_FACTORY
#define IW_VACORE_CORE_FACTORY

#include <VACoreDefinitions.h>
#include <VAStruct.h>

#include <string>
#include <iostream>

#ifndef VACORE_DEFAULT_CONFIGFILE
#define VACORE_DEFAULT_CONFIGFILE "\"VACore.ini\""
#endif // VACORE_DEFAULT_CONFIGFILE

// Foward declarations
class IVAInterface;

namespace VACore
{
	//! Factory method - creates a VACore instance
	/** 
	  * @param[in] oArgs Arguments with configuration for the core
	  * @return Pointer to the core instance (VA interface API from VABase)
	  */
	VACORE_API IVAInterface* CreateCoreInstance( const CVAStruct& oArgs, std::ostream* pOutputStream = nullptr );

	//! Parses input INI configuration file and converts it into a VA core config struct
	/**
	  * @param[in] sConfigFilePath File path to core config (INI) file
	  * @return Core configuration
	  */
	VACORE_API CVAStruct LoadCoreConfigFromFile( const std::string& sConfigFilePath );

	//! Store configuration to INI file from VA core config struct
	/**
	  * @param[in] oCoreConfig Core configuration struct
	  * @param[in] sConfigFilePath File path to core config (INI) file
	  */
	VACORE_API void StoreCoreConfigToFile( const CVAStruct& oCoreConfig, const std::string& sConfigFilePath );

	//! Factore method - create a VACore instance with configuration file
	/**
	  * @param[in] sConfigFile Configuration file, or default file
	  * @return Pointer to the core instance (VA interface API from VABase)
	  *
	  * @sa GetCoreConfigFromFile()
	  */
	inline IVAInterface* CreateCoreInstance( const std::string& sConfigFile = VACORE_DEFAULT_CONFIGFILE, std::ostream* pOutputStream = nullptr )
	{
		return CreateCoreInstance( VACore::LoadCoreConfigFromFile( sConfigFile ), pOutputStream );
	};

	//! Returns the filesystem path of the VACore shared lib (e.g. VACore.dll)
	/**
	  * @return Core library path
	  */
	VACORE_API std::string GetCoreLibFilePath();

} // End of namespace "VACore"

#endif // IW_VACORE_CORE_FACTORY

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

#ifndef IW_VACORE_HARDWARESETUP
#define IW_VACORE_HARDWARESETUP

// VA includes
#include <VAException.h>
#include <VABase.h>
#include <VAStruct.h>

// STL includes
#include <string>
#include <vector>

//! Physical hardware devices and patching (hardware / soundcard inputs and outputs)
class CVAHardwareDevice
{
public:
	std::string sIdentifier;	//!< Identifier of this hardware device in the setup, i.e. "LS1"
	std::string sType;			//!< Hardware device type [deprecated, use derivations of this class]
	std::string sDesc;			//!< Versatile description of device
	VAVec3 vPos;				//!< Cartesian coordinates
	VAQuat qOrient;	//!< Orientation in yaw pitch roll (YPR) convention in degree
	std::vector< int > viChannels;	//!< Channel list (hardware patch of sound card)
	std::string sDataFileName; //!<  [deprecated, use derivations of this class]
};

//!< Inputs that combines hardware devices
class CVAHardwareInput
{
public:
	std::string sIdentifier;		//!< Name of group
	std::string sDesc;				//!< Versatile description of group
	std::vector< const CVAHardwareDevice* > vpDevices;	//!< List of connected output hardware devices (same order as output channels)
	bool bActive;					//!< Active/Inactive flag

	std::vector< int > GetPhysicalInputChannels() const;
	bool IsActive() const;
};

//!< Outputs that combine hardware devices
class CVAHardwareOutput
{
public:
	std::string sIdentifier;		//!< Name of group
	std::string sDesc;				//!< Versatile description of group
	std::vector< const CVAHardwareDevice* > vpDevices;	//!< List of connected output hardware devices (same order as output channels)
	bool bEnabled;					//!< Enabled/Disabled (ignore hardware group for signal processing)

	std::vector< int > GetPhysicalOutputChannels() const;
	bool IsEnabled() const;
};

class CVAHardwareSetup
{
public:
	inline CVAHardwareSetup() {};

	//! Initialize hardware setup with a struct
	void Init( const CVAStruct& oArgs );

	std::vector< CVAHardwareInput > voInputs;	//!< Input groups for the hardware setup
	std::vector< CVAHardwareOutput > voOutputs; //!< Output groups for the hardware setup

	const CVAHardwareInput* GetInput( const std::string& sInput ) const;
	std::vector< const CVAHardwareDevice* > GetDeviceListFromInputGroup( const std::string& sInputIdentifier ) const;

	//! Return devices from a logical output group
	std::vector< const CVAHardwareDevice* > GetDeviceListFromOutputGroup( const std::string& sOutputIdentifier ) const;
	const CVAHardwareOutput* GetOutput( const std::string& sOutput ) const;

	CVAStruct GetStruct() const;

private:
	std::vector< CVAHardwareDevice > voHardwareInputDevices;	//!< Hardware input devices list
	std::vector< CVAHardwareDevice > voHardwareOutputDevices;	//!< Hardware output devices list
};

#endif // IW_VACORE_HARDWARESETUP

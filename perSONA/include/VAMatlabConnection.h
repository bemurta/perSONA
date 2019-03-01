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

#ifndef IW_VA_MATLAB_CONNECTION
#define IW_VA_MATLAB_CONNECTION

// STL includes
#include <string>
#include <vector>

class IVAInterface;
class CVAMatlabTracker;
class IVANetClient;

//! Connection data class
class CVAMatlabConnection
{
public:
	IVANetClient* pClient;
	IVAInterface* pCoreInterface;
	CVAMatlabTracker* pVAMatlabTracker;

	CVAMatlabConnection();
	~CVAMatlabConnection();
};

#endif // INCLUDE_WATCHER_VA_MATLAB_CONNECTION

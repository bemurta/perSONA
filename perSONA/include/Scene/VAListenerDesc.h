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

#ifndef IW_VA_LISTENERDESC
#define IW_VA_LISTENERDESC

//! This class describes static (unversioned) sound source information
class CVAListenerDesc : public CVAPoolObject
{
public:
	int iID;					//!< Sound source identifier
	std::string sName;			//!< Versatile name
	ITAAtomicBool bEnabled;		//!< Enabled/disabled for rendering
	std::string sExplicitRendererID; //!< Explicit renderer identifier, or empty
	// @todo bInitPositionOrientation

	inline void PreRequest()
	{
		iID = -1;
		bEnabled = false;
		sName = "";
	};
};

#endif // IW_VA_LISTENERDESC

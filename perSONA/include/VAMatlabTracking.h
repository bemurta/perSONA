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

#ifndef IW_VA_MATLAB_TRACKING
#define IW_VA_MATLAB_TRACKING

// STL includes
#include <string>
#include <vector>

// Vista include
#include <VistaBase/VistaVector3D.h>
#include <VistaBase/VistaQuaternion.h>

class NatNetClient;
class IVAInterface;

//! Wrapper class for NatNet tracking client
class CVAMatlabTracker
{
public:
	CVAMatlabTracker();
	void Reset();
	bool Initialize( std::string sServerAdress, std::string sLocalAdress );
	bool Uninitialize();
	bool IsConnected() const;

	IVAInterface* pVACore;       //!< Pointer to (networked) core instance
	
	int iTrackedSoundReceiverID; //!< -1 if deactivated, will be preferred over source
	int iTrackedSoundReceiverHeadRigidBodyIndex;    //!< Starts with 1 (default)
	int iTrackedSoundReceiverTorsoRigidBodyIndex;    //!< Starts with 1 (default)
	VistaVector3D vTrackedSoundReceiverTranslation;	//!< Position offset from pivot point (default NatNet rigid body barycenter)
	VistaQuaternion qTrackedSoundReceiverRotation;	//!< Orientation rotation from default orientation (initial NatNet rigid body orientation)

	int iTrackedRealWorldSoundReceiverID; //!< -1 if deactivated
	int iTrackedRealWorldSoundReceiverHeadRigidBodyIndex;    //!< Starts with 1 (default)
	int iTrackedRealWorldSoundReceiverTorsoRigidBodyIndex;    //!< Starts with 1 (default)
	VistaVector3D vTrackedRealWorldSoundReceiverTranslation;	//!< Position offset from pivot point (default NatNet rigid body barycenter)
	VistaQuaternion qTrackedRealWorldSoundReceiverRotation;	//!< Orientation rotation from default orientation (initial NatNet rigid body orientation)

	int iTrackedSoundSourceID;	//!< -1 if deactivated
	int iTrackedSoundSourceRigidBodyIndex;    //!< Starts with 1 (default)
	VistaVector3D vTrackedSoundSourceTranslation;	//!< Position offset from pivot point (default NatNet rigid body barycenter)
	VistaQuaternion qTrackedSoundSourceRotation;	//!< Orientation rotation from default orientation (initial NatNet rigid body orientation)

private:
	NatNetClient* m_pTrackerClient;
	bool m_bConnected;
};

#endif // IW_VA_MATLAB_TRACKING

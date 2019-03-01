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

#ifndef IW_VACORE_MOTIONSTATE
#define IW_VACORE_MOTIONSTATE

#include <VABase.h>

#include "../Scene/VASceneStateBase.h"

//! Klasse für Bewegungszustände (Zeitmarke, Position und Geschwindigkeit, Orientierung)
/**
  * Diese Klasse bescheibt einen dynamischen (versionierten) Bewegungszustand eines Objektes. Sie
  * beinhaltet die Position und Geschwindigkeit sowie die Orientierung des Objektes zu einem
  * gewissen Zeitpunkt.
  *
  */
class CVAMotionState : public CVASceneStateBase
{
public:
	class CVAPose
	{
	public:
		inline CVAPose() {};
		inline ~CVAPose() {};

		inline void Reset()
		{
			vPos.Set( 0.0f, 0.0f, 0.0f );
			qOrient.Set( 0.0f, 0.0f, 0.0f, 1.0f );
		};

		VAVec3 vPos;
		VAQuat qOrient;
	};

	void Initialize( double dModificationTime );

	//! Retrieve data from another motion state [copy] and assign new time stamp
	void Copy( const CVAMotionState* pSrc, double dModificationTime );

	//! Mark as fixed, now changes allowed afterwards
	void Fix();

	VAVec3 GetPosition() const;
	VAVec3 GetView() const;
	VAVec3 GetUp() const;
	VAQuat GetOrientation() const;
	void SetPosition( const VAVec3& vPos );
	void SetOrientation( const VAQuat& qOrient );
	void SetOrientationVU( const VAVec3& vView, const VAVec3& vUp );

	VAQuat GetHeadAboveTorsoOrientation() const;
	void SetHeadAboveTorsoOrientation( const VAQuat& qOrient );

	VAQuat GetRealWorldHeadAboveTorsoOrientation() const;
	void SetRealWorldHeadAboveTorsoOrientation( const VAQuat& qOrient );

	CVAPose GetRealWorldPose() const;
	void SetRealWorldPose( const CVAPose& );

private:
	struct
	{
		VAVec3 vPos;	//!< Position [m]
		VAVec3 vView;	//!< View vector
		VAVec3 vUp;		//!< Up vector
		CVAPose oRealWorldPose; //!< Pose in real world coordination system (i.e. playback room position)
		VAQuat qHATO; //!< Head-above-torso orientation
		VAQuat qRealWorldHATO; //!< Real-world head-above-torso orientation
	} data;
};

#endif // IW_VACORE_MOTIONSTATE

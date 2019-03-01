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

#ifndef IW_VACORE_MOTIONMODEL
#define IW_VACORE_MOTIONMODEL

#include <VABase.h>

class CVAMotionState;

//! Interface for motion filtering and prediction
/**
  * Interfae for filtering and predicting motion trajectories
  *	to achieve smooth movement transitions at any given frame rate,
  *	i.e. at block rate of the audio process
  */
class IVAMotionModel
{
public:
	inline virtual ~IVAMotionModel() {};

	//! Feed a motion state
	/**
	  * This method receives a new motion state to be used within the motion
	  * model engine.
	  *
	  * \param pNewState Informationen über Bewegung und Zeitpunkt
	  */
	virtual void InputMotionKey( const CVAMotionState* pNewState ) = 0;

	//! Release motion keys [todo: for retarded positions]
	// virtual void ReleaseMotionKeys( double dTime )=0;

	//! Position prediction
	/**
	  * This method returns a predicted position
	  *
	  * \param dTime Time stamp of requested position key
	  * \param vPredPos Predicted position (Call-by-Reference)
	  *
	  * \return True, if estimation possible (motion data present)
	  *
	  * \note	The time requested has to be ascending during runtime, i.e. no
	  *			backwards running time requests possible
	  */
	virtual bool EstimatePosition( double dTime, VAVec3& vPredPos ) = 0;

	//! Orientation prediction
	/**
	  * This method returns a predicted orientation as view and up vectors
	  *
	  * \param dTime Time stamp of requested position key
	  * \param vViewPos Predicted view vector (Call-by-Reference)
	  * \param vUpPos Predicted up vector (Call-by-Reference)
	  *
	  * \return True, if estimation possible (motion data present)
	  *
	  * \note	The time requested has to be ascending during runtime, i.e. no
	  *			backwards running time requests possible
	  */
	virtual bool EstimateOrientation( double dTime, VAVec3& vView, VAVec3& vUp ) = 0;

	//! Orientation prediction
	/**
	  * This method returns a predicted orientation as view and up vectors
	  *
	  * \param dTime Time stamp of requested position key
	  * \param vOrientYPR Predicted view vector (Call-by-Reference, in degree)
	  *
	  * \return True, if estimation possible (motion data present)
	  *
	  * \note	The time requested has to be ascending during runtime, i.e. no
	  *			backwards running time requests possible
	  */
	virtual bool EstimateOrientation( double dTime, VAQuat& qOrient ) = 0;

	//!< Reset internal data
	virtual void Reset() = 0;
};

#endif // IW_VACORE_MOTIONMODEL

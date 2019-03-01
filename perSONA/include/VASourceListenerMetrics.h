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

#ifndef IW_VACORE_SOURCELISTENERMETRICS
#define IW_VACORE_SOURCELISTENERMETRICS

#include <VA.h>

class CVASoundSourceState;
class CVAReceiverState;

/**
  * \note All angular metrics are in degrees
  */
class CVASourceReceiverMetrics
{
public:
	double dAzimuthS2L, dElevationS2L;	//!< Angular direction source receiver [°]
	double dAzimuthL2S, dElevationL2S;	//!< Angular direction receiver source [°]
	double dDistance;					//!< Distance source receiver (may be ero) [m]

	CVASourceReceiverMetrics();

	CVASourceReceiverMetrics( const CVASoundSourceState*, const CVAReceiverState* );

	//! Determine metrics (relative angles and distance) in degree/meters
	void Calc( const VAVec3& vSourcePos, const VAVec3& vSourceView, const VAVec3& vSourceUp, const VAVec3& vListenerPos, const VAVec3& vListenerView, const VAVec3& vListenerUp );
};

#endif // IW_VACORE_SOURCELISTENERMETRICS

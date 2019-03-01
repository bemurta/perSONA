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

#ifndef IW_VACORE_ROOMACOUSTICSCLUSTERSCHEDULER
#define IW_VACORE_ROOMACOUSTICSCLUSTERSCHEDULER

#if (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#include "VARoomAcousticsScheduler.h"

//! Cluster-Implementierung
class CVAClusterRaven : public IRavenSimulationSchedulerInterface {
public:
	CVAClusterRaven(IVARoomAcousticsSimulationTaskCreator*);
	~CVAClusterRaven();

	// ... TODO stienen: evtl. R_RavenClusterFrontend implementieren und benutzen

};

#endif // (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#endif // IW_VACORE_ROOMACOUSTICSCLUSTERSCHEDULER

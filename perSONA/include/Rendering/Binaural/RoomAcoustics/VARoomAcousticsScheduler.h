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

#ifndef IW_VACORE_ROOMACOUSTICSSCHEDULER
#define IW_VACORE_ROOMACOUSTICSSCHEDULER

#if (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#include <VAPoolObject.h>

#include <R_Raven.h>

#include <ITAASCIITable.h>
#include <ITAStringUtils.h>

// 3rdParty includes
#include <tbb/concurrent_queue.h>

// STL includes
#include <assert.h>
#include <list>


//! Basisklasse für Simulationsaufgaben. Kann durch Subclassing erweitert werden (siehe TaskCreator)
class CVARoomAcousticsSimulationTask : public CRavenSimulationTask, public CVAPoolObject
{
public:
	int iSourceID;
	int iListenerID;

	int iAuralisationMode;

	inline bool Validate()
	{
		return CRavenSimulationTask::IsValid();
	};

private:
	IRavenSimulationSchedulerInterface* m_pScheduler; //!< Scheduler pointer (only one possible)
};


//! Abstrakter Erzeuger für Simulationsaufgaben
/**
  * Hiermit können eigene Unterklassen von Task erstellt werden, welche
  * zusätzliche Informationen für die Zielanwendung enthalten.
  */
class IVARoomAcousticsSimulationTaskCreator
{
public:
	virtual inline ~IVARoomAcousticsSimulationTaskCreator() {};

	//! Erzeugt einen neuen Simulationsauftrag
	virtual CVARoomAcousticsSimulationTask* CreateTask() = 0;
};

#endif // (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#endif // IW_VACORE_ROOMACOUSTICSSCHEDULER

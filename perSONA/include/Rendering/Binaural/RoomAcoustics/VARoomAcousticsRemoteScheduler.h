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

#ifndef IW_VACORE_ROOMACOUSTICS_REMOTE_SCHEDULER
#define IW_VACORE_ROOMACOUSTICS_REMOTE_SCHEDULER

#if (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#include <R_Raven.h>

// ITA includes
#include <ITASampleFrame.h>
#include <ITAAtomicPrimitives.h>

// Vista includes
#include <VistaInterProcComm/Concurrency/VistaThreadEvent.h>
#include <VistaInterProcComm/Concurrency/VistaThreadLoop.h>
#include <VistaInterProcComm/Concurrency/VistaPriority.h>

// 3rdParty includes
#include <tbb/concurrent_queue.h>

// STL includes
#include <assert.h>
#include <list>

// VA includes
#include <VAObject.h>

// Forward declarations
class CVACoreImpl;
class IRavenNetClient;
class CVARoomAcousticsSimulationTask;


class CVARavenRemoteScheduler : public IRavenSimulationSchedulerInterface, public VistaThreadLoop, public CVAObject {
public:
	//! Leitet Anfragen an einen entfernten Scheduler weiter
	/**
	  * \param pCore VACore Verbindung
	  * \param sServerIP IP- oder Serveradresse
	  */
	CVARavenRemoteScheduler( CVACoreImpl* pCore, const std::string& sServerIP );
	virtual ~CVARavenRemoteScheduler();

	CVAStruct CallObject( const CVAStruct& oArgs );
	CVAObjectInfo GetObjectInfo() const;

	void Reset();
	void LoadScene( const std::string& sFileName );
	void AddTask( CRavenSimulationTask* pTask );
	bool AttachSimulationResultHandler( IRavenSimulationSchedulerResultHandler* );
	bool DetachSimulationResultHandler( IRavenSimulationSchedulerResultHandler* );

	bool GetProfilerStatus( CProfiler& oStatus );

	bool LoopBody();

private:
	CVACoreImpl* m_pCore; //!< Zeiger auf erzeugenden VACore
	
	IRavenSimulationSchedulerInterface* m_pRemoteScheduler;

	IRavenNetClient* m_pRavenNetClient;

	std::list<IRavenSimulationSchedulerResultHandler*> m_lpTaskHandler; //!< Liste der Task-Handler

	typedef std::list< CVARoomAcousticsSimulationTask* > TaskList;
	typedef TaskList::iterator TaskListIt;
	typedef TaskList::const_iterator TaskListCit;

	tbb::concurrent_queue<CVARoomAcousticsSimulationTask*> m_qpNewTasks; // Übergabestruktur (non-blocking)
	TaskList m_lDSTasks;	// Interne Aufgabenlisten Direktschall
	TaskList m_lISTasks;	// Interne Aufgabenlisten Image Sources
	TaskList m_lRTTasks;	// Interne Aufgabenlisten Ray Tracing
	TaskList m_lARTasks;	//!< Interal representation artificial reverb

	class CRavenSchedulerProfile;
	CRavenSchedulerProfile* m_pProfile; // Zeiger auf Profiler des Schedulers
	
	VistaThreadEvent m_evTrigger;

	ITAAtomicBool m_bStop, m_bIndicateReset, m_bResetAck;

	void FilterAndReplaceTasks( TaskList& , CVARoomAcousticsSimulationTask* );
};

#endif // (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#endif // IW_VACORE_ROOMACOUSTICS_REMOTE_SCHEDULER

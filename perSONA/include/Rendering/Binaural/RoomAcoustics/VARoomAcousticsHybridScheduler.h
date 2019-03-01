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

#ifndef IW_VACORE_ROOMACOUSTICS_HYBRID_SCHEDULER
#define IW_VACORE_ROOMACOUSTICS_HYBRID_SCHEDULER

#if (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#include <R_Raven.h>
#include <R_RavenLocalScheduler.h>

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


class CVARavenHybridScheduler : public IRavenSimulationSchedulerInterface, public VistaThreadLoop, public CVAObject {
public:
	//! Konstruktor
	CVARavenHybridScheduler( CVACoreImpl*, const std::string& sRavenDataBasePath, const std::string& sServerIP );

	//! Destruktor
	virtual ~CVARavenHybridScheduler();

	CVAStruct CallObject( const CVAStruct& oArgs );
	CVAObjectInfo GetObjectInfo() const;

	void Reset();
	void LoadScene( const std::string& sFileName );
	void AddTask( CRavenSimulationTask* pTask );
	bool AttachSimulationResultHandler( IRavenSimulationSchedulerResultHandler* );
	bool DetachSimulationResultHandler( IRavenSimulationSchedulerResultHandler* );

	bool GetProfilerStatus( CProfiler& oStatus );

	bool LoopBody();

	//! Bestimmt die Tasks, die durch den lokalen Scheduler abgearbeitet werden sollen
	/**
	  * \param viTaskDuties Siehe CRavenSimulationTask->Simulationstyp
	  */
	void SetLocalFieldOfDuties( const std::vector< int >& viTaskDuties );
	
	//! Bestimmt die Tasks, die durch den entfernten Scheduler abgearbeitet werden sollen
	/**
	  * \param viTaskDuties Siehe CRavenSimulationTask->Simulationstyp
	  */
	void SetRemoteFieldOfDuties( const std::vector< int >& viTaskDuties );

	IRavenSimulationSchedulerInterface* GetLocalScheduler() const;
	IRavenSimulationSchedulerInterface* GetRemoteScheduler() const;
	
private:
	CVACoreImpl* m_pCore;

	IRavenSimulationSchedulerInterface* m_pLocalScheduler;
	IRavenSimulationSchedulerInterface* m_pRemoteScheduler;

	IRavenNetClient* m_pRavenNetClient;

	std::vector< int > m_viLocalFieldOfDuties;
	std::vector< int > m_viRemoteFieldOfDuties;

	std::list< IRavenSimulationSchedulerResultHandler* > m_lpTaskHandler; //!< Liste der Task-Handler

	typedef std::list< CVARoomAcousticsSimulationTask* > TaskList;
	typedef TaskList::iterator TaskListIt;
	typedef TaskList::const_iterator TaskListCit;

	tbb::concurrent_queue< CVARoomAcousticsSimulationTask* > m_qpNewTasks; // Übergabestruktur (non-blocking)
	TaskList m_lDSTasks;	// Interne Aufgabenlisten Direktschall
	TaskList m_lISTasks;	// Interne Aufgabenlisten Image Sources
	TaskList m_lRTTasks;	//!< Internal list of tasks for diffuse decay using ray tracing
	TaskList m_lARTasks;	//!< Internal list of tasks for diffuse decay using artificial reverb

	class CRavenSchedulerProfile;
	CRavenSchedulerProfile* m_pProfile; // Zeiger auf Profiler des Schedulers

	VistaThreadEvent m_evTrigger;

	ITAAtomicBool m_bStop, m_bIndicateReset;

	//! Filtert und weist Tasks ab, die gar nicht erst zu den Schedulern geleitet werden müssen
	void FilterAndReplaceTasks( TaskList& , CVARoomAcousticsSimulationTask* );

	//! Weist einen Task sofort ab, ohne die Scheduler zu informieren
	/*
	 * Über diesen Task werden weder lokaler noch entfernter Scheduler informiert,
	 * z.B. wenn ein neuerer Task einen internen verdrängt, der noch gar nicht an
	 * die Scheduler geleitet wurden, oder wenn ein Task während des Reset hinzugefügt
	 * wird, oder wenn das 'Field of Duty' geändert wird, etc.
	 *
	 */
	void ImmediatelyDiscard( CVARoomAcousticsSimulationTask* );
};

#endif // (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#endif // IW_VACORE_ROOMACOUSTICS_HYBRID_SCHEDULER

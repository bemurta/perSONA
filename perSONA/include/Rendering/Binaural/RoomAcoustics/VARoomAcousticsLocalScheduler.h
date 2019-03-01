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

#ifndef IW_VACORE_ROOMACOUSTICS_LOCAL_SCHEDULER
#define IW_VACORE_ROOMACOUSTICS_LOCAL_SCHEDULER

#if (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

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

class CVARavenLocalScheduler : public CRavenLocalScheduler, public CVAObject {
public:
	CVARavenLocalScheduler(CVACoreImpl*, const CRavenLocalScheduler::CConfiguration& );
	virtual ~CVARavenLocalScheduler() {};

	//! Call-Hook für Objekt-Aufruf
	CVAStruct CallObject( const CVAStruct& oArgs );

	//! Informationsrückgabe VAObject
	CVAObjectInfo GetObjectInfo() const;
	
private:
	CVACoreImpl* m_pCore; //!< Zeiger auf erzeugenden VACore
};

#endif // (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#endif // IW_VACORE_ROOMACOUSTICS_LOCAL_SCHEDULER

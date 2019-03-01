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

#ifndef IW_VACORE_COREEVENTMANAGER
#define IW_VACORE_COREEVENTMANAGER

#include <VAEvent.h>
#include "VACoreEventHandlerRegistry.h"

#include <ITAAtomicPrimitives.h>
#include <ITACriticalSection.h>

#include <VistaInterProcComm/Concurrency/VistaThreadLoop.h>
#include <VistaInterProcComm/Concurrency/VistaThreadEvent.h>

#include <deque>
#include <set>

/**
 * Diese Klasse realisiert den Mechanismus für das Senden
 * von Core-Ereignissen mittels eines (niedrig priorisierten)
 * Threads und sorgt somit für eine saubere Entkopplung des
 * Core-Thread von den Event-Handlern.
 */

class CVACoreEventManager : public VistaThreadLoop
{
public:
	CVACoreEventManager();
	~CVACoreEventManager();

	void AttachHandler( IVAEventHandler* pHandler );
	void DetachHandler( IVAEventHandler* pHandler );

	// --= Realisierung der Handler-Methode =-----------------------------------

	// Event in die Queue legen
	void EnqueueEvent( const CVAEvent& evCoreEvent );

	// Die Queue von Events broadcasten
	void BroadcastEvents();

	// Ein Event direkt broadcasten
	void BroadcastEvent( const CVAEvent& evCoreEvent );

	// --= Redefinition der Methoden in VistaThreadLoop =----------------------

	bool LoopBody();

private:
	CVACoreEventHandlerRegistry m_oReg;
	std::deque< CVAEvent > m_qOuterEventQueue;
	std::deque< CVAEvent > m_qInnerEventQueue;
	VistaThreadEvent m_evInnerEventQueue;
	ITAAtomicBool m_bStop;
	ITACriticalSection m_csInnerEventQueue;
	ITACriticalSection m_csOuterEventQueue;
	int m_iEventIDCounter;
};

#endif // IW_VACORE_COREEVENTMANAGER

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

#ifndef IW_VACORE_COREEVENTHANDLERREGISTRY
#define IW_VACORE_COREEVENTHANDLERREGISTRY

#include <ITACriticalSection.h>
#include <set>

class CVAEvent;
class IVAEventHandler;

/**
 * Diese Klasse regelt die Registrierung von Empfängern für Core-Ereignisse.
 * Handler können sich an-/abmelden und es wird eine Funktion für das Senden
 * eines Ereignissen an alle registrierten Handler bereitgestellt.
 * Alle Methoden der Klasse sind thread-safe.
 */

class CVACoreEventHandlerRegistry
{
public:
	CVACoreEventHandlerRegistry();
	~CVACoreEventHandlerRegistry();

	void AttachHandler( IVAEventHandler* pHandler );
	void DetachHandler( IVAEventHandler* pHandler );

	// Alle Handler entfernen
	void DetachAllHandlers();

	// Ein Event an alle Handler senden
	void BroadcastEvent( const CVAEvent* evCoreEvent );

private:
	ITACriticalSection m_csHandlers;
	std::set< IVAEventHandler* > m_spHandlers;
};

#endif // IW_VACORE_COREEVENTHANDLERREGISTRY

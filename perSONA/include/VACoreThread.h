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

#ifndef IW_VACORE_CORETHREAD
#define IW_VACORE_CORETHREAD

#include <ITAAtomicPrimitives.h>
#include <VistaInterProcComm/Concurrency/VistaThreadEvent.h>
#include <VistaInterProcComm/Concurrency/VistaThreadLoop.h>

class CVACoreImpl;

class CVACoreThread : public VistaThreadLoop {
public:
	CVACoreThread(CVACoreImpl* pParent);
	~CVACoreThread();

	// Thread anschubsen (nach Aktion)
	void Trigger();

	// Versucht den Thread anzuhalten
	bool TryBreak();

	// Thread anhalten
	void Break();

	// Thread weiter fortsetzen
	void Continue();

	// -= Definition der virtuellen Methoden in VistaThread =-

	//void PreLoop();
	//void PostLoop();
	bool LoopBody();

private:
	CVACoreImpl* m_pParent;
	VistaThreadEvent m_evTrigger;
	VistaThreadEvent m_evPaused;
	ITAAtomicInt m_iTriggerCnt;
	ITAAtomicBool m_bPaused;
	ITAAtomicBool m_bStop;
	ITAAtomicBool m_bReadyForPause;
};

#endif // IW_VACORE_CORETHREAD

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

#ifndef IW_VACORE_PORTAUDIOBACKEND
#define IW_VACORE_PORTAUDIOBACKEND

#include "VAAudioDriverBackend.h"
#include "VAAudioDriverConfig.h"

#include <ITAAtomicPrimitives.h>
#include <ITAException.h>
#include <ITAStreamProperties.h>
#include <VistaInterProcComm/Concurrency/VistaThreadLoop.h>
#include <VistaInterProcComm/Concurrency/VistaThreadEvent.h>

class ITAPortaudioInterface;

//! VA Portaudio Backend Klasse
/**
  * Diese Klasse implementiert die Schnittstelle IVAAudioDriverBackend
  * für Portaudio mit ITAPortaudioInterface
  */
class CVAPortaudioBackend : public IVAAudioDriverBackend
{
public:
	CVAPortaudioBackend( CVAAudioDriverConfig* pConfig );
	~CVAPortaudioBackend();

	std::string getDriverName() const;
	std::string getDeviceName() const;
	int getNumberOfInputs() const;
	const ITAStreamProperties* getOutputStreamProperties() const;
	ITADatasource* getInputStreamDatasource() const;
	void setOutputStreamDatasource( ITADatasource* pDatasource );

	void initialize();
	void finalize();
	bool isStreaming();
	void startStreaming();
	void stopStreaming();

	// Threaded-entkoppelte Methoden
	void threadInitialize();
	void threadFinalize();
	void threadStartStreaming();
	void threadStopStreaming();

private:

	class MediatorThread : public VistaThreadLoop
	{
	public:
		MediatorThread( CVAPortaudioBackend* pParent );
		~MediatorThread();

		int doInitialize();
		int doFinalize();
		int doStartStreaming();
		int doStopStreaming();

		ITAException getException() const;

	private:
		enum
		{
			OPERATION_NOTHING = -1,
			OPERATION_STOP_THREAD = 0,
			OPERATION_INITIALIZE,
			OPERATION_FINALIZE,
			OPERATION_START_STREAMING,
			OPERATION_STOP_STREAMING
		};

		CVAPortaudioBackend* m_pParent;
		ITAPortaudioInterface* m_pPA;
		ITAAtomicInt m_iOperation;
		int m_iResult;
		ITAException m_oException;
		VistaThreadEvent m_evStart;
		VistaThreadEvent m_evFinish;

		int doOperation( int iOperation );

		// --= Redefinition der Methoden in VistaThreadLoop =--

		bool LoopBody();
	};

	MediatorThread* m_pMediator;
	ITAPortaudioInterface* m_pITAPA;
	CVAAudioDriverConfig* m_pConfig;
	ITAStreamProperties m_oOutputStreamProps;
	ITAAtomicBool m_bStreaming;
};

#endif // IW_VACORE_PORTAUDIOBACKEND

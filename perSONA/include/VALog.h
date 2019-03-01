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

#ifndef IW_VACORE_LOG
#define IW_VACORE_LOG

#include <VACoreDefinitions.h>

#include <VA.h>

#include <ITAClock.h>
#include <ITAAtomicPrimitives.h>

#include <tbb/concurrent_queue.h>

#include <VistaInterProcComm/Concurrency/VistaMutex.h>
#include <VistaInterProcComm/Concurrency/VistaThreadEvent.h>
#include <VistaInterProcComm/Concurrency/VistaThreadLoop.h>

#include <iostream>
#include <iomanip>
#include <list>
#include <stdarg.h>
#include <stdio.h>

extern VACORE_API std::ostream* VA_STDOUT;
extern VACORE_API std::ostream* VA_STDERR;

void VACORE_API VALog_setOutputStream( std::ostream* os );
void VACORE_API VALog_setErrorStream( std::ostream* os );
int VACORE_API VALog_GetLogLevel();
void VACORE_API VALog_SetLogLevel( int );

VistaMutex& VALog_getOutputStreamMutex();

#define VA_PRINT( expr )			{ if (VA_STDOUT && (VALog_GetLogLevel() >= IVAInterface::VA_LOG_LEVEL_QUIET ) )		{ VistaMutexLock oLock(VALog_getOutputStreamMutex() ); (*VA_STDOUT) << "[ VAPrint   ] " << expr << std::endl; } }
#define VA_ERROR( module, expr )	{ if (VA_STDOUT && (VALog_GetLogLevel() >= IVAInterface::VA_LOG_LEVEL_QUIET ) )		{ VistaMutexLock oLock(VALog_getOutputStreamMutex() ); (*VA_STDERR) << "[ VAError   ][ " << std::right << std::setw( 20 ) << module << " ] " << expr << std::endl; } }
#define VA_WARN( module, expr )		{ if (VA_STDOUT && (VALog_GetLogLevel() >= IVAInterface::VA_LOG_LEVEL_ERROR ) )		{ VistaMutexLock oLock(VALog_getOutputStreamMutex() ); (*VA_STDOUT) << "[ VAWarning ][ " << std::right << std::setw( 20 ) << module << " ] " << expr << std::endl; } }
#define VA_INFO( module, expr )		{ if (VA_STDOUT && (VALog_GetLogLevel() >= IVAInterface::VA_LOG_LEVEL_INFO ) )		{ VistaMutexLock oLock(VALog_getOutputStreamMutex() ); (*VA_STDOUT) << "[ VAInfo    ][ " << std::right << std::setw( 20 ) << module << " ] " << expr << std::endl; } }
#define VA_VERBOSE( module,expr )	{ if (VA_STDOUT && (VALog_GetLogLevel() >= IVAInterface::VA_LOG_LEVEL_VERBOSE ) )	{ VistaMutexLock oLock(VALog_getOutputStreamMutex() ); (*VA_STDOUT) << "[ VAVerbose ][ " << std::right << std::setw( 20 ) << module << " ] " << expr << std::endl; } }
#define VA_TRACE( module, expr )	{ if (VA_STDOUT && (VALog_GetLogLevel() >= IVAInterface::VA_LOG_LEVEL_TRACE ) )		{ VistaMutexLock oLock(VALog_getOutputStreamMutex() ); (*VA_STDOUT) << "[ VATrace   ][ " << std::right << std::setw( 20 ) << module << " ] " << expr << std::endl; } }

class CVALogItem
{
public:
	double dTimestamp;
	std::string sLogger;
	std::string sMsg;

	inline CVALogItem()
		: dTimestamp( 0 )
	{
	};

	inline CVALogItem( double dTheTimestamp, const std::string sTheLogger, const std::string sTheMsg )
		: dTimestamp( dTheTimestamp )
		, sLogger( sTheLogger )
		, sMsg( sTheMsg )
	{
	};

	//! Größer-Operator zur Verwendung der sort() Funktion
	bool operator> ( CVALogItem const& rhs );

	//! Kleiner-Operation
	bool operator< ( CVALogItem const& rhs );

};

class CVARealtimeLogStream;

//! Realtime Logger entkoppelt die Ausgabe von Logger-Streams auf einem niederpriorisierten Thread
class CVARealtimeLogger : public VistaThreadLoop
{
public:
	CVARealtimeLogger();
	~CVARealtimeLogger();

	static CVARealtimeLogger* GetInstance();

	//! Streams registrieren
	void Register( CVARealtimeLogStream* pStream );

	//! Streams deregistrieren
	void Unregister( CVARealtimeLogStream* pStream );

	//! Thread anschubsen (nach Aktion)
	void Trigger();

	//! Ausgabe
	bool LoopBody();

	inline void PreLoop() {};
	inline void PostLoop() {};

private:
	VistaThreadEvent m_evTrigger;
	ITAAtomicInt m_iTriggerCnt;
	ITAAtomicBool m_bStop;
	std::list< CVARealtimeLogStream* > m_lpStreams;
	VistaMutex m_mxRegistration;
};

//! Implementierung des Echtzeit-Logger-Streams
/**
  * Nutzt den RealtimeLogger, um Stream-Informationen mittels
  * niederpriorisiertem Thread auf die Konsole oder Visual Studio Ausgabe
  * auszugeben.
  */
class CVARealtimeLogStream
{
public:
	inline CVARealtimeLogStream( ITAClock* pClock = ITAClock::getDefaultClock() )
		: m_pClock( pClock )
	{
		// Bei Ausgabe-Thread registrieren (Singleton)
		CVARealtimeLogger::GetInstance()->Register( this );
	}

	virtual ~CVARealtimeLogStream()
	{
		// Bei Thread deregistieren
		CVARealtimeLogger::GetInstance()->Unregister( this );
	}

	std::string GetName() const
	{
		return m_sName;
	}

	void SetName( const std::string& sName )
	{
		m_sName = sName;
	}

	// Ausgabe
	void Printf( const char * format, ... )
	{
		char buf[ 16384 ];
		va_list args;
		va_start( args, format );
		vsprintf( buf, format, args );
		va_end( args );

		m_qLog.push( CVALogItem( m_pClock->getTime(), m_sName, buf ) );
		// TODO: Thread zur Ausgabe anwerfen ggf.
	}

private:
	ITAClock* m_pClock;
	std::string m_sName;
	tbb::strict_ppl::concurrent_queue<CVALogItem> m_qLog;

	friend class CVARealtimeLogger;
};
#endif // IW_VACORE_LOG

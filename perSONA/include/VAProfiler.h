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

#ifndef IW_VACORE_PROFILER
#define IW_VACORE_PROFILER

#include <VACoreDefinitions.h>

#include <ITAClock.h>
#include <ITAStringUtils.h>
#include <ITATimeSeriesAnalyzer.h>
#include <cassert>
#include <iostream>

static ITAClock* g_pDefaultClock = ITAClock::getDefaultClock();

class CVAProfilerMeasure
{
public:
	CVAProfilerMeasure( const std::string& sMeasureName )
		: m_sMeasureName( sMeasureName )
		, m_dStartTime( 0 )
	{
		assert( !sMeasureName.empty() );
	}

	// Clears all values
	void reset()
	{
		m_dAnalyzer.reset();
		m_dStartTime = 0;
	}

	// Start time measurement
	inline void start()
	{
		assert( m_dStartTime == 0 );
		m_dStartTime = g_pDefaultClock->getTime();
	}

	// Stop time measurement and handle measured timespan
	// Returns measured time [s]
	inline double stop()
	{
		assert( m_dStartTime > 0 );
		double dStopTime = g_pDefaultClock->getTime();
		double dElapsedTime = dStopTime - m_dStartTime;
		m_dAnalyzer.handle( dElapsedTime );
		m_dStartTime = 0;
		return dElapsedTime;
	}

	// Cancel started time measurement
	inline void cancel()
	{
		m_dStartTime = 0;
	}

	inline void print() const
	{
		std::cout << ToString() << std::endl;
	}

	inline std::string ToString() const
	{
		std::stringstream ss;
		ss << m_sMeasureName << ": min=" << timeToString( m_dAnalyzer.minimum() )
			<< ", avg=" << timeToString( m_dAnalyzer.mean() )
			<< ", max=" << timeToString( m_dAnalyzer.maximum() )
			<< ", std=" << timeToString( m_dAnalyzer.std_deviation() );
		return ss.str();
	}

private:
	std::string m_sMeasureName;
	double m_dStartTime;
	ITATimeseriesAnalyzer<double> m_dAnalyzer;
};

// Profiler measures definition
enum
{
	VAPROF_STREAM_PROCESSING_TIME,
	VAPROF_DSP_LOAD_PERCENT,

	// Tail enty
	VAPROF_LAST
};


#endif // IW_VACORE_PROFILER

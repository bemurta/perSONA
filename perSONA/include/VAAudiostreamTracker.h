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

#ifndef IW_VACORE_AUDIOSTREAMTRACKER
#define IW_VACORE_AUDIOSTREAMTRACKER

#include <VABase.h>

#include "Audiosignals/VAAudioSignalSourceManager.h"
#include "Utils/VADebug.h"
#include "VALog.h"

#include <ITAClock.h>
#include <ITADataSource.h>
#include <ITAStopWatch.h>
#include <ITAStreamInfo.h>

#include <string>

//! Audio stream state implementation
/**
  * Connects the ITA stream info and VA audio stream state.
  */
class CVAAudiostreamStateImpl : public CVAAudiostreamState, public ITAStreamInfo
{
public:
	inline CVAAudiostreamStateImpl() : ITAStreamInfo() {};
	inline CVAAudiostreamStateImpl( const ITAStreamInfo& base ) : ITAStreamInfo( base )
	{
		i64Sample = base.nSamples;
	};
};


//! Stream tracker of an audio stream (carrying media information)
/**
  * This class provides further information on the stream by
  * holding track of samples and stream time.
  *
  */
class CVAAudiostreamTracker : public ITADatasource
{
public:
	inline CVAAudiostreamTracker( ITADatasource* pSource, ITAClock* pClock, ITAAtomicFloat* pfClockOffset, ITAAtomicLong* plSyncModOwner, CVAAudioSignalSourceManager* pSignalSourceMan )
		: m_pSource( pSource )
		, m_pClock( pClock )
		, m_pfClockOffset( pfClockOffset )
		, m_plSyncModOwner( plSyncModOwner )
		, m_pSignalSourceMan( pSignalSourceMan )
	{
		m_oState.i64Sample = -1;
		m_dSamplerate = pSource->GetSampleRate();
		m_uiBlocklength = pSource->GetBlocklength();
		m_bGBPFirst = true;
	}

	inline ~CVAAudiostreamTracker()
	{
		VA_VERBOSE( "AudioStreaming", "Processing time: " << m_swProcessingTime.ToString() << ", avg DSP load = " << m_swProcessingTime.mean() * m_dSamplerate / m_uiBlocklength * 1e2 << "%" );
	}

	// Not thread safe, not reentrant safe
	inline const CVAAudiostreamState& GetStreamState() const
	{
		return m_oState;
	}

	inline unsigned int GetBlocklength() const
	{
		return m_pSource->GetBlocklength();
	}

	inline unsigned int GetNumberOfChannels() const 
	{
		return m_pSource->GetNumberOfChannels();
	}

	inline double GetSampleRate() const 
	{
		return m_pSource->GetSampleRate();
	}

	inline const float* GetBlockPointer( unsigned int uiChannel, const ITAStreamInfo* pStreamInfo )
	{
		m_oState = *pStreamInfo;

		if( m_bGBPFirst )
		{
			m_swProcessingTime.start();

			float fNewClockOffset = *m_pfClockOffset;

			if( m_oState.i64Sample == 0 )
			{
				// Start
				m_dStreamStartTime = m_pClock->getTime();
				m_fCurrentClockOffset = fNewClockOffset;
				m_oState.bTimeReset = false;
			}
			else
			{
				m_oState.bTimeReset = ( m_fCurrentClockOffset != fNewClockOffset );
				m_fCurrentClockOffset = fNewClockOffset;
			}

			// Clock drift compensation factor for RME Hammerfall - via manual measurement @todo remove
			m_oState.dSysTime = m_dStreamStartTime + ( double ) ( m_oState.i64Sample + m_uiBlocklength ) / m_dSamplerate * 0.999978;
			m_oState.dCoreTime = m_oState.dSysTime - ( double ) m_fCurrentClockOffset;
			m_oState.bSyncMod = ( ( *m_plSyncModOwner ) != -1 );

			m_bGBPFirst = false;

			m_pSignalSourceMan->FetchInputData( &m_oState );
		}

		return m_pSource->GetBlockPointer( uiChannel, &m_oState );
	}

	inline void IncrementBlockPointer()
	{
		m_pSource->IncrementBlockPointer();
		m_swProcessingTime.stop();
		m_bGBPFirst = true;
	}

private:
	CVAAudioSignalSourceManager* m_pSignalSourceMan;
	ITADatasource* m_pSource;
	ITAClock* m_pClock;
	ITAAtomicFloat* m_pfClockOffset;
	ITAAtomicLong* m_plSyncModOwner;
	double m_dStreamStartTime;
	float m_fCurrentClockOffset;
	CVAAudiostreamStateImpl m_oState;
	double m_dSamplerate;
	unsigned int m_uiBlocklength;
	bool m_bGBPFirst;
	ITAStopWatch m_swProcessingTime;
};

#endif // IW_VACORE_AUDIOSTREAMTRACKER

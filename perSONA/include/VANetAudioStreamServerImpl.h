/*
 *  --------------------------------------------------------------------------------------------
 *
 *    VVV        VVV A           Virtual Acoustics (VA) | http://www.virtualacoustics.org
 *     VVV      VVV AAA          Licensed under the Apache License, Version 2.0
 *      VVV    VVV   AAA
 *       VVV  VVV     AAA        Copyright 2015-2017
 *        VVVVVV       AAA       Institute of Technical Acoustics (ITA)
 *         VVVV         AAA      RWTH Aachen University
 *
 *  --------------------------------------------------------------------------------------------
 */

#ifndef IW_VANET_AUDIO_STREAM_SERVER_IMPL
#define IW_VANET_AUDIO_STREAM_SERVER_IMPL

#include <VABaseDefinitions.h>
#include <VABase.h>
#include <VASamples.h>

#include <ITANetAudioSampleServer.h>

#include <string>

class CVANetAudioStreamServer;
class ITAStreamInfo;

class CVANetAudioStreamServerImpl : CITASampleProcessor
{
public:
	CVANetAudioStreamServerImpl( CVANetAudioStreamServer* pParent, const double, const int );
	~CVANetAudioStreamServerImpl();
	bool InitializeInternal( const std::string& sBindInterface, const int iBindPort );	
	void Process( const ITAStreamInfo* pStreamInfo );

private:
	CVASampleBuffer m_sfStreamTransmitBuffer;
	CVANetAudioStreamServer* m_pParent;
	CITANetAudioSampleServer* m_pSampleServer;
	CVAAudiostreamState m_oStreamInfo;
};

#endif // IW_VANET_AUDIO_STREAM_SERVER_IMPL

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

#ifndef IW_VACORE_NETWORK_STREAM_AUDIO_SIGNAL_SOURCE
#define IW_VACORE_NETWORK_STREAM_AUDIO_SIGNAL_SOURCE

#include <VACoreDefinitions.h>
#include <VASamples.h>
#include <VAStruct.h>

class VistaConnectionIP;
class VistaTCPServer;
class VistaTCPSocket;


//! Helper class for server-side network audio signal sources
/**
  * Provides network functionality for an audio signal source.
  * Transmit method has to be executed by user, status information
  * for the queued samples on client side can be received.
  */
class VACORE_API CVANetworkStreamAudioSignalSource
{
public:

	//! Create a network signal source (not connected)
	CVANetworkStreamAudioSignalSource();

	//! Destructor
	virtual ~CVANetworkStreamAudioSignalSource();

	//! Start to listen on server socket
	/**
	  * @param[in] sServerBindAddress Server name or IP
	  * @param[in] iServerBindPort Server port number
	  *
	  * @note This method is blocking
	  */
	bool Start( const std::string& sServerBindAddress = "localhost", const int iServerBindPort = 12480 );

	//! Stop server and close connection
	void Stop();

	//! Connection status getter
	bool GetIsConnected() const;

	//! Transmit samples of arbitrary length
	/**
	  * Transmit a frame of samples (buffer for one or more channels) over the network
	  * connection. The entire length of the buffer(s) will be send and execution is blocked 
	  * until finished.
	  *
	  * @param[in] oFrame Vector of samples in a buffer with same length
	  *
	  * @note Will raise VAException if problem with connection occurs
	  */
	int Transmit( const std::vector< CVASampleBuffer >& oFrame );

	//! Returns the internal number of queued samples at client side (last known state)
	/**
	  * At client side, a ring buffer with at least the number of samples for one processing block
	  * is used.
	  *
	  * @return Number of queued samples at client side
	  *
	  * @note thread-safe
	  */
  int GetNumQueuedSamples();

private:
	int m_iNumQueuedSamples;  //!< Client-side number of queued samples

	VistaTCPServer* m_pServer; //!< TCP server
	VistaTCPSocket* m_pSocket; //!< TCP socket
	VistaConnectionIP* m_pConnection; //!< IP connection
};

#endif // IW_VACORE_NETWORK_STREAM_AUDIO_SIGNAL_SOURCE

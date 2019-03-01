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

#ifndef IW_VACORE_NETSTREAMSIGNALSOURCE
#define IW_VACORE_NETSTREAMSIGNALSOURCE

#include "VAAudioSignalSource.h"
#include <string>

class IVAInterface;
class CITANetAudioStream;
class ITADatasource;

/** Network audio stream class
 *
 * Can be used i.e. for complex physical sound synthesis on another processing unit.
 */
class CVANetstreamSignalSource : public IVAAudioSignalSource
{
public:
	//! Create a signal source that connects to a network streaming server
	/**
	 * @param[in] dDestinationSampleRate	Match sampling rate
	 * @param[in] iBlockLength				Matching streaming block length
	 * @param[in] sBindAddress				Network bind address
	 * @param[in] iRecvPort					Network bind port number
	 *
	 * @note Throws CVAException!
	 *
	 */
	CVANetstreamSignalSource(  const double dDestinationSampleRate, const int iBlockLength, const std::string& sBindAddress, const int iRecvPort );

	virtual ~CVANetstreamSignalSource();

	std::string GetTypeString() const;
	std::string GetTypeMnemonic() const;
	std::string GetDesc() const;
	std::string GetStateString() const;
	IVAInterface* GetAssociatedCore() const;
	ITADatasource* GetStreamingDatasource() const;
	inline int GetType() const { return VA_SS_NETSTREAM; };
	const float* GetStreamBlock( const CVAAudiostreamState* pStreamInfo );

	inline CVAStruct GetParameters( const CVAStruct& ) const { return CVAStruct(); };
	inline virtual void SetParameters( const CVAStruct& ) {};

private:
	IVAInterface* m_pAssociatedCore;
	CITANetAudioStream* m_pSourceStream;

	void HandleRegistration( IVAInterface* pParentCore );
	void HandleUnregistration( IVAInterface* pParentCore );
};

#endif // IW_VACORE_NETSTREAMSIGNALSOURCE

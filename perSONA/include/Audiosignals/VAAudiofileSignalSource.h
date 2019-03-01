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

#ifndef IW_VACORE_AUDIOFILESIGNALSOURCE
#define IW_VACORE_AUDIOFILESIGNALSOURCE

#include <VAAudioSignalSource.h>

#include <ITAAtomicPrimitives.h>
#include <ITADataSourceDelegator.h>
#include <ITASampleBuffer.h>
#include <ITASampleFrame.h>

#include <string>

class ITABufferDatasource;

class CVAAudiofileSignalSource : public IVAAudioSignalSource, ITADatasourceDelegator
{
public:
	CVAAudiofileSignalSource( const std::string& sFileName, double dRequiredSampleRate,int iBlockLength );
	CVAAudiofileSignalSource( int iBufferLength, double dSampleRate, int iBlockLength );

	virtual ~CVAAudiofileSignalSource();

	int GetPlaybackState() const;
	void SetPlaybackAction( int iPlayStateTransission );

	void SetCursorSeconds( double dPos );

	bool GetIsLooping() const;
	void SetIsLooping( bool bLooping );

	// --= Schnittstelle IVAAudioSignalSource =-------------------------

	int GetType() const;
	std::string GetTypeString() const;
	std::string GetDesc() const;
	std::string GetStateString() const;
	std::string GetStateString( int iPlayState ) const;
	IVAInterface* GetAssociatedCore() const;
	const float* GetStreamBlock(const CVAAudiostreamState* pStreamInfo);

	void SetParameters( const CVAStruct& ) {};
	CVAStruct GetParameters( const CVAStruct& ) const { return CVAStruct(); };

private:
	IVAInterface* m_pAssociatedCore;
	ITABufferDatasource* m_pBufferDataSource;
	ITAAtomicInt m_iCurrentPlayState;
	ITAAtomicInt m_iRequestedPlaybackAction; //!< User-triggered playback action
	ITASampleBuffer m_sbOutBuffer;
	ITASampleFrame m_sfAudioBuffer; //!< Audio buffer with multiple channels for smooth switching on update
	int m_iActiveAudioBufferChannel;
	bool m_bCrossfadeChannelSwitch;

	void HandleRegistration( IVAInterface* pParentCore );
	void HandleUnregistration( IVAInterface* pParentCore );
};

#endif // IW_VACORE_AUDIOFILESIGNALSOURCE

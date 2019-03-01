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

#ifndef IW_VACORE_SEQUENCERSIGNALSOURCE
#define IW_VACORE_SEQUENCERSIGNALSOURCE

#include <VAAudioSignalSource.h>
#include <string>

class ITASoundSampler;
class ITASoundSamplePool;

/**
 * Diese Klasse realisiert Audiosignalquellen, welche Audiosignale
 * für Klangobjekte mittels eines Sequencers generieren.
 */

class CVASequencerSignalSource : public IVAAudioSignalSource
{
public:
	CVASequencerSignalSource( const double dSamplerate, const int iBlocklength );
	virtual ~CVASequencerSignalSource();

	int AddSoundPlayback( int iSoundID, int iFlags, double dTimecode = 0 );

	// --= Schnittstelle IVAAudioSignalSource =-------------------------

	int GetType() const;
	std::string GetTypeString() const;
	std::string GetTypeMnemonic() const;
	std::string GetDesc() const;
	std::string GetStateString() const;
	IVAInterface* GetAssociatedCore() const;
	const float* GetStreamBlock( const CVAAudiostreamState* pStreamInfo );
	void SetParameters( const CVAStruct& ) {};
	CVAStruct GetParameters( const CVAStruct& ) const { return CVAStruct(); };

private:
	IVAInterface* m_pAssociatedCore;
	ITASoundSampler* m_pSampler;
	ITASoundSamplePool* m_pSamplePool;
	int m_iTrackID;

	void HandleRegistration( IVAInterface* pParentCore );
	void HandleUnregistration( IVAInterface* pParentCore );
};

#endif // IW_VACORE_SEQUENCERSIGNALSOURCE

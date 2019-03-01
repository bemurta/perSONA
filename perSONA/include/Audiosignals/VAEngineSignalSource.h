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

#ifndef IW_VACORE_ENGINESIGNALSOURCE
#define IW_VACORE_ENGINESIGNALSOURCE

#include <VAAudioSignalSource.h>
#include <VAObject.h>

#include <ITADataSourceRealization.h>
#include <ITASampleBuffer.h>
#include <ITAAtomicPrimitives.h>

#include <map>

class CVACoreImpl;

/** Engine signal source
  * 
  * Engine sinosoidal signal generator using rpm input parameter
  */

class CVAEngineSignalSource : public IVAAudioSignalSource, public ITADatasourceRealization
{
public:
	class Config
	{
	public:
		std::map< double, double > lFreqModesSpectrum; //!< Mode spectrum [Hz], Amplitude
		CVACoreImpl* pCore;
	};

	CVAEngineSignalSource( const Config& );
	virtual ~CVAEngineSignalSource();
		
	int GetType() const;
	std::string GetTypeString() const;
	std::string GetDesc() const;
	IVAInterface* GetAssociatedCore() const;
	const float* GetStreamBlock( const CVAAudiostreamState* );
	void HandleRegistration( IVAInterface* );
	void HandleUnregistration( IVAInterface* );
	std::string GetStateString() const { return "running engine"; };

	void SetParameters( const CVAStruct& );
	CVAStruct GetParameters( const CVAStruct& ) const;

	void SetEngineNumber( const double& dK );

private:
	IVAInterface* m_pAssociatedCore;
	Config m_oConfig;
	ITASampleBuffer m_sbBuffer;
	std::map< double, double > m_lFreqModesPhase;
	ITAAtomicFloat m_fK; // engine number
};

#endif // IW_VACORE_ENGINESIGNALSOURCE

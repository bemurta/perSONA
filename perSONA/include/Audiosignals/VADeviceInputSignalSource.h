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

#ifndef IW_VACORE_DEVICEINPUTSIGNALSOURCE
#define IW_VACORE_DEVICEINPUTSIGNALSOURCE

#include <VAAudioSignalSource.h>
#include <string>

class ITASampleBuffer;

/**
 * Diese Klasse realisiert Audiosignalquellen, welche die Samples
 * eines Eingangs der Audio-Hardware wiedergeben. Sie ist nur eine
 * Hülse, welche einen externen Puffer abspielt in dies in die
 * Schnittstelle "IVAAudioSignalSource" verpackt.
 */

class CVADeviceInputSignalSource : public IVAAudioSignalSource {
public:
	CVADeviceInputSignalSource(ITASampleBuffer* psbInputData, const std::string& sDesc);
	virtual ~CVADeviceInputSignalSource();

	// --= Schnittstelle IVAAudioSignalSource =-------------------------

	int GetType() const;
	std::string GetTypeString() const;
	std::string GetDesc() const;
	std::string GetStateString() const;
	IVAInterface* GetAssociatedCore() const;
	const float* GetStreamBlock(const CVAAudiostreamState* pStreamInfo);
	void SetParameters( const CVAStruct& ) {};
	CVAStruct GetParameters( const CVAStruct& ) const { return CVAStruct(); };

private:
	IVAInterface* m_pAssociatedCore;
	std::string m_sDesc;
	ITASampleBuffer* m_psbInputData;

	void HandleRegistration(IVAInterface* pParentCore);
	void HandleUnregistration(IVAInterface* pParentCore);
};

#endif // IW_VACORE_DEVICEINPUTSIGNALSOURCE

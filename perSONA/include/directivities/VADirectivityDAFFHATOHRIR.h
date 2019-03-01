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

#ifndef IW_VACORE_DIRECTIVITY_DAFF_HATO_HRIR
#define IW_VACORE_DIRECTIVITY_DAFF_HATO_HRIR

#include "VADirectivity.h"
#include "VADirectivityDAFFHRIR.h"
#include <string>

class DAFFReader;
class DAFFContentIR;
class DAFFMetadata;
class ITASampleFrame;

//! Head-above-torso head-related impulse response class
/**
  * DAFF class should contain all HRIR HATO data sets as specific channels.
  *
  *  - odd channels are always LEFT ear
  *  - even channels are always RIGHT ear
  *
  *  - the minimum number of channels is 6.
  *  - the number of HATO directions has to be symmetric
  *
  *  - first two channels correspond to natural HAT orientation (0 degree, frontal direction, for downwards-compatibility)
  *  - beginning from channels 3&4, lowest HAT orientation (HATOStartDeg)
  *  - frontal direction is skipped and will not be included (is defined as channel 1&2)
  *  - last two channels correspond to highest HAT orientation (HATOEndDeg)
  *
  * example for a HATO range: -40 .. +40
  * orientation is right-handed around torso-head-axis, e.g. defined from "looking to the right .. looking to the left"
  *
  */
class CVADirectivityDAFFHATOHRIR : public IVADirectivity
{
public:
	//! Loads HATO HRIR from DAFF file, throws CVAException
	CVADirectivityDAFFHATOHRIR( const std::string& sFilePath, const std::string& sName, const double dDesiredSamplerate );

	virtual ~CVADirectivityDAFFHATOHRIR();
	std::string GetFilename() const;
	std::string GetName() const;
	std::string GetDesc() const;

	//! Returns read-only pointer access to the internal HRIR properties
	const CVAHRIRProperties* GetProperties() const;

	//! Direct access to DAFF content, use with care.
	DAFFContentIR* GetDAFFContent() const;

	//! Queries DAFF content and constructs an index based on spherical direction
	void GetNearestSpatialNeighbourIndex( const float fAzimuthDeg, const float fElevationDeg, int* piIndex, bool* pbOutOfBounds = nullptr ) const;
	
	//! Returns the two-channel HRIR that corresponsd to the direction and hat orientation
	/**
	  * @param[out] psfDest Two-channel destination sample frame (HRIR is copied here)
	  * @param[in] iIndex DAFF direction index based on nearest neighbour search
	  * @param[in] dHATODeg HATO in degrees
	  */
	void GetHRIRByIndexAndHATO( ITASampleFrame* psfDest, const int iIndex, const double dHATODeg ) const;

private:
	CVAHRIRProperties m_oProps;
	DAFFReader* m_pReader;
	mutable DAFFContentIR* m_pContent;
	const DAFFMetadata* m_pMetadata;
	std::string m_sName;
	float m_fLatency;
	int m_iMinOffset;
	int m_iFilterLength;
	int m_iNumHATODirections;
	double m_dHATOStartDeg;
	double m_dHATOEndDeg;

	//! Returns the left ear DAFF channel index for given HAT orientation
	/**
	  * @return Odd channel index depending on HATO direction
	  */
	int GetHATOChannelIndexLeft( const double dHATODeg ) const;

	//! Returns the right ear DAFF channel index for given HAT orientation
	/**
	  * @return Even channel index depending on HATO direction
	  */
	int GetHATOChannelIndexRight( const double dHATODeg ) const;
};

#endif // IW_VACORE_DIRECTIVITY_DAFF_HATO_HRIR

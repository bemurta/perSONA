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

#ifndef IW_VACORE_DIRECTIVITY_DAFF_ENERGETIC
#define IW_VACORE_DIRECTIVITY_DAFF_ENERGETIC

#include "VADirectivity.h"

#include <string>

class DAFFReader;
class DAFFContentMS;
class DAFFMetadata;

class ITASampleFrame;

/**
 * Diese Klasse ist eine Facade für zwei-dimensionale
 * HRIR-Datensätze und stellt eine (mehr) applikationsbezogene
 * Schnittstelle zu solchen Daten bereit.
 * Zwei-dimensional bedeutet, dass nur Datensätze für eine
 * einzige Entfernung enthalten sind (Kugelschale).
 *
 * Die HRIRs enthalten NICHT die Laufzeit, die bei
 * der Messung vorhanden war. Sie werden alle auf die minimale
 * Größe gekürzt (nur falls tatsächlich Nullen vorhanden sind).
 * Was übrig bleibt, ist der Rest an Verzögerung im Filter,
 * welcher bis zur ursprünglichen Entfernung fehlt.
 * Diese Verzögerung kann abgefragt werden.
 * Alle Lautstärken werden automatisch auf 1m nomiert.
 *
 * Beispiel:
 *
 * - Messdistanz 2.0m
 * - Impulseantworten enthalten nur Koeffizienten von 200 bis 400
 * - Ursprünglich liegt Laufzeit bei 256 Samples.  ( 2.0m/(344m/s) ) *(44.1k Samples/s) = 256 Samples )
 * - Dann ist die Latenz 256-200 = 56 Samples.
 * - Also: Beim 56ten Samples ist man quasi beim Mittelpunkt des Filters.
 */
class CVADirectivityDAFFEnergetic : public IVADirectivity
{
public:
	//! Throws exception
	CVADirectivityDAFFEnergetic( const std::string& sFilePath, const std::string& sName );

	virtual ~CVADirectivityDAFFEnergetic();

	std::string GetFilename() const;
	std::string GetName() const;
	std::string GetDesc() const;

	void GetNearestNeighbour( const float fAzimuthDeg, const float fElevationDeg, int* piIndex, bool* pbOutOfBounds = nullptr ) const;

	void GetHRIRByIndex( ITASampleFrame* psfDest, const int iIndex, const float fDistanceMeters ) const;

	void GetHRIR( ITASampleFrame* psfDest, const float fAzimuthDeg, const float fElevationDeg, const float fDistanceMeters, int* piIndex = nullptr, bool* pbOutOfBounds = nullptr ) const;
	
	DAFFContentMS* GetDAFFContent() const;

private:
	DAFFReader* m_pReader;
	mutable DAFFContentMS* m_pContent;
	const DAFFMetadata* m_pMetadata;
	std::string m_sName;
	float m_fLatency;
	int m_iMinOffset;
	int m_iFilterLength;
};

#endif // IW_VACORE_DIRECTIVITY_DAFF_ENERGETIC

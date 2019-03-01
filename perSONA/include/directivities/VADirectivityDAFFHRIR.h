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

#ifndef IW_VACORE_DIRECTIVITY_DAFF_HRIR
#define IW_VACORE_DIRECTIVITY_DAFF_HRIR

#include "VADirectivity.h"
#include <string>

class DAFFReader;
class DAFFContentIR;
class DAFFMetadata;
class ITASampleFrame;

//! Data class with properties of an HRIR data set
class CVAHRIRProperties
{
public:
	std::string sFilename;		// Dateiname
	std::string sName;			// Anzeigename
	int iFilterLength;			// Filterlänge [Samples]
	float fFilterLatency;		// Latenz [Samples]
	bool bFullSphere;			// true => Alle Richtungen abgedeckt, false => Eingeschränkter Bereich
	bool bSpaceDiscrete;		// true => Diskretes Gitter, false => kontinuierliche Darstellung
	bool bDistanceDependent;	// Distanzabhängige Daten (3D)? (false => nur 2D)

	struct AnthropometricParameters
	{
		inline AnthropometricParameters()
		{
			dHeadWidth = 0.12f; //!@ Width of head (ear to ear)
			dHeadHeight = 0.10f; //!@ todo rename (chin to top)
			dHeadDepth = 0.15f; //!@ Depth of head (nose to back)
		};

		double dHeadWidth; //!@ Width of head (ear to ear) [m]
		double dHeadHeight; //!@ Depth of head (nose to back) [m]
		double dHeadDepth; //!@ todo rename (chin to top) [m]
	} oAnthroParams;
};

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
class CVADirectivityDAFFHRIR : public IVADirectivity
{
public:
	//! Lade-Konstruktor. Wirft CVAException im Fehlerfall.
	CVADirectivityDAFFHRIR( const std::string& sFilePath, const std::string& sName, const double dDesiredSamplerate );

	//! Destruktor
	virtual ~CVADirectivityDAFFHRIR();

	// --= Schnittstelle "IVAHRIRDataset" =-----------------------

	std::string GetFilename() const;
	std::string GetName() const;
	std::string GetDesc() const;
	const CVAHRIRProperties* GetProperties() const;

	void GetNearestNeighbour( const float fAzimuthDeg, const float fElevationDeg, int* piIndex, bool* pbOutOfBounds = nullptr ) const;

	void GetHRIRByIndex( ITASampleFrame* psfDest, const int iIndex, const float fDistanceMeters ) const;

	void GetHRIR( ITASampleFrame* psfDest, const float fAzimuthDeg, const float fElevationDeg, const float fDistanceMeters, int* piIndex = nullptr, bool* pbOutOfBounds = nullptr ) const;
	
	DAFFContentIR* GetDAFFContent() const;

private:
	CVAHRIRProperties m_oProps;
	DAFFReader* m_pReader;
	mutable DAFFContentIR* m_pContent;
	const DAFFMetadata* m_pMetadata;
	std::string m_sName;
	float m_fLatency;
	int m_iMinOffset;
	int m_iFilterLength;
};

#endif // IW_VACORE_DIRECTIVITY_DAFF_HRIR

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

#ifndef IW_VACORE_SURFACESTATE
#define IW_VACORE_SURFACESTATE

#include "VASceneStateBase.h"

// Diese Klasse bescheibt den Zustand einer Oberfläche
class CVASurfaceState : public CVASceneStateBase
{
public:
	// Initialisieren (in Grundzustand versetzen)
	// (Zustand danach: Nicht-finialisiert, Referenzzähler = 0)
	void Initialize( double dModificationTime );

	// Daten eines anderen Zustand übernehmen
	void Copy( const CVASurfaceState* pSrc, double dModificationTime );

	// Fixieren
	void Fix();

	// Getter
	int GetMaterial() const;

	// Setter
	void SetMaterial( int iMaterialID );

	// In Zeichenkette konvertieren
	//std::string ToString() const;

protected:
	// Destruktion vor Pool-Release
	void PreRelease();

private:
	struct
	{
		// Oberflächen-Material
		int iMaterial;
	} data;
};

#endif // IW_VACORE_PORTALSTATE

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

#ifndef IW_VACORE_PORTALSTATE
#define IW_VACORE_PORTALSTATE

#include "VASceneStateBase.h"

// Diese Klasse bescheibt den Zustand eines Portals
class CVAPortalState : public CVASceneStateBase
{
public:
	// Initialisieren (in Grundzustand versetzen)
	// (Zustand danach: Nicht-finialisiert, Referenzzähler = 0)
	void Initialize( double dModificationTime );

	// Daten eines anderen Zustand übernehmen
	void Copy( const CVAPortalState* pSrc, double dModificationTime );

	// Fixieren
	void Fix();

	// Getter
	float GetState() const;

	// Setter
	void SetState( float fState );

	// In Zeichenkette konvertieren
	//std::string ToString() const;

protected:
	// Destruktion vor Pool-Release
	void PreRelease();

private:
	struct
	{
		// Öffnungszustand (0 = vollständig geschlossen, 1 = vollständig geöffnet)
		float fState;
	} data;
};

#endif // IW_VACORE_PORTALSTATE

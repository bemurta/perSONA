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

#ifndef IW_VA_SCENE_STATE
#define IW_VA_SCENE_STATE

#include "VASceneStateBase.h"
#include <VA.h>

#include <ITAAtomicPrimitives.h>

#include <list>
#include <set>
#include <vector>

class CVASceneStateDiff;
class CVAContainerState;
class CVASoundSourceState;
class CVAReceiverState;
class CVAPortalState;
class CVASurfaceState;

class CVASceneState : public CVASceneStateBase
{
public:
	//! Initialized empty scene state
	void Initialize( const int iSceneStateID, const double dModificationTime );

	//! Copy state from other state
	void Copy( const int iNewSceneStateID, const double dModificationTime, const CVASceneState* pBase );

	//! Scene state ID getter
	int GetID() const;

	// Fixate state and make it read-only
	void Fix();
	
	//! Returns number of sound sources
	int GetNumSoundSources() const;

	// Stellt eine Liste mit den IDs aller Schallquellen zusammen
	// und speichert die im angegebenen Container
	void GetSoundSourceIDs( std::vector< int >* pviDest ) const;
	void GetSoundSourceIDs( std::list< int >* pliDest ) const;
	void GetSoundSourceIDs( std::set< int >* psiDest ) const;

	// Zustand eines Schallquelle zurückgeben [read-only]
	// (Rückgabewert: nullptr falls ungültige ID)
	CVASoundSourceState* GetSoundSourceState( const int iSourceID ) const;

	// Zustand eines Schallquelle verändern [write]
	// (Hinweis: Erzeugt einen autonomen Zustand der betreffenden Schallquelle)
	// (Rückgabewert: nullptr falls ungültige ID)
	CVASoundSourceState* AlterSoundSourceState( const int iSourceID );

	// Neue Schallquelle hinzufügen (Rückgabe: Fehlercode, ID => okay, -1 => Fehler)
	int AddSoundSource();

	// Existierende Schallquelle entfernen (Ausnahme falls ungültige ID)
	void RemoveSoundSource( const int iSourceID );

	// --= Hörer =--

	// Anzahl Hörer zurückgeben
	int GetNumListeners() const;

	// Stellt eine Liste mit den IDs aller Hörer zusammen
	// und speichert die im angegebenen Container
	void GetListenerIDs( std::vector<int>* pviDest ) const;
	void GetListenerIDs( std::list<int>* pliDest ) const;
	void GetListenerIDs( std::set<int>* psiDest ) const;

	// Zustand eines Hörers zurückgeben [read-only]
	CVAReceiverState* GetReceiverState( int iListenerID ) const;

	// Zustand eines Hörers verändern [write]
	// (Hinweis: Erzeugt einen autonomen Zustand der betreffenden Schallquelle)
	CVAReceiverState* AlterListenerState( int iListenerID );

	// Neuen Hörer hinzufügen (Rückgabe: Fehlercode, ID => okay, -1 => Fehler)
	int AddListener();

	// Existierende Schallquelle entfernen (Ausnahme falls ungültige ID)
	void RemoveListener( int iListenerID );

	// --= Portale =--

	// Stellt eine Liste mit den IDs aller Portale zusammen
	// und speichert die im angegebenen Vektor
	void GetPortalIDs( std::vector<int>* pviDest ) const;
	void GetPortalIDs( std::list<int>* pliDest ) const;
	void GetPortalIDs( std::set<int>* psiDest ) const;

	// Zustand eines Portals zurückgeben [read-only]
	CVAPortalState* GetPortalState( int iPortalID ) const;

	// Zustand eines Portals verändern [write]
	// (Hinweis: Erzeugt einen autonomen Zustand der betreffenden Schallquelle)
	CVAPortalState* AlterPortalState( int iPortalID );

	// Neues Portal hinzufügen (Rückgabe: Fehlercode, ID => okay, -1 => Fehler)
	int AddPortal();

	// Existierende Schallquelle entfernen (Ausnahme falls ungültige ID)
	void RemovePortal( int iPortalID );

	// --= Oberflächen =--

	// Stellt eine Liste mit den IDs aller Oberflächen zusammen
	// und speichert die im angegebenen Vektor
	void GetSurfaceIDs( std::vector<int>* pviDest ) const;

	// Zustand einer Oberfläche zurückgeben [read-only]
	CVASurfaceState* GetSurfaceState( int iSurfaceID ) const;

	// Zustand einer Oberfläche verändern [write]
	// (Hinweis: Erzeugt einen autonomen Zustand der betreffenden Schallquelle)
	CVASurfaceState* AlterSurfaceState( int iSurfaceID, double dModificationTime );

	// Neues Oberfläche hinzufügen (Rückgabe: Fehlercode, ID => okay, -1 => Fehler)
	int AddSurface();

	// Existierende Oberfläche entfernen (Ausnahme falls ungültige ID)
	void RemoveSurface( int iSurfaceID );

	// --= Vergleiche =--

	//! Differenzinformation erzeugen
	/**
	  * Vergleicht den eigenen Szenezustand mit einem anderen Szenezustand
	  * auf Unterschiede.
	  *
	  * Semantik: Von diesem Zustand (Basis) zum Ziel-Zustand (Zukunft)
	  * Rückgabewert: Unterschiede in den Zuständen (ja/nein)?
	  *
	  * \param pState Der Status, dessen Änderungen von dem eigenen Status überprüft werden (darf nullptr-Pointer sein)
	  * \param pDiff Die Differenzinformation
	  *
	  */
	void Diff( const CVASceneState* pState, CVASceneStateDiff* pDiff ) const;

	//! Als Zeichenkette zurückgeben
	std::string ToString() const;

protected:
	// Destruktion
	void PreRelease();

private:
	int m_iSceneStateID;

	struct
	{
		CVAContainerState* m_pSources;
		CVAContainerState* m_pListeners;
		CVAContainerState* m_pPortals;
		CVAContainerState* m_pSurfaces;
	} data;

	CVAContainerState* AlterSoundSourceListState();
	CVAContainerState* AlterListenerListState();
	CVAContainerState* AlterPortalListState();
	CVAContainerState* AlterSurfaceListState();
};

// Data class with differences based on entity IDs
class CVASceneStateDiff
{
public:
	// Hinweis: Alle IDs sind jeweils auf den Basiszustand des Vergleichs
	// bezogen und nicht auf den Zustand mit dem verglichen wurde!

	std::vector< int > viNewSoundSourceIDs;		// IDs neu erzeugter Schallquellen
	std::vector< int > viDelSoundSourceIDs;		// IDs gelöschter Schallquellen
	std::vector< int > viComSoundSourceIDs;		// IDs erhaltener Schallquellen

	std::vector< int > viNewReceiverIDs;			// IDs neu erzeugter Hörer
	std::vector< int > viDelReceiverIDs;			// IDs gelöschter Hörer
	std::vector< int > viComReceiverIDs;			// IDs erhaltener Hörer

	std::vector< int > viNewPortalIDs;			// IDs neu erzeugter Portale
	std::vector< int > viDelPortalIDs;			// IDs gelöschter Portale
	std::vector< int > viComPortalIDs;			// IDs erhaltener Portale

	std::vector< int > viNewSurfaceIDs;			// IDs neu erzeugter Oberflächen
	std::vector< int > viDelSurfaceIDs;			// IDs gelöschter Oberflächen
	std::vector< int > viComSurfaceIDs;			// IDs erhaltener Oberflächen
};

#endif // IW_VA_SCENE_STATE

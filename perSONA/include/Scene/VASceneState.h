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

	// Zustand eines Schallquelle zur�ckgeben [read-only]
	// (R�ckgabewert: nullptr falls ung�ltige ID)
	CVASoundSourceState* GetSoundSourceState( const int iSourceID ) const;

	// Zustand eines Schallquelle ver�ndern [write]
	// (Hinweis: Erzeugt einen autonomen Zustand der betreffenden Schallquelle)
	// (R�ckgabewert: nullptr falls ung�ltige ID)
	CVASoundSourceState* AlterSoundSourceState( const int iSourceID );

	// Neue Schallquelle hinzuf�gen (R�ckgabe: Fehlercode, ID => okay, -1 => Fehler)
	int AddSoundSource();

	// Existierende Schallquelle entfernen (Ausnahme falls ung�ltige ID)
	void RemoveSoundSource( const int iSourceID );

	// --= H�rer =--

	// Anzahl H�rer zur�ckgeben
	int GetNumListeners() const;

	// Stellt eine Liste mit den IDs aller H�rer zusammen
	// und speichert die im angegebenen Container
	void GetListenerIDs( std::vector<int>* pviDest ) const;
	void GetListenerIDs( std::list<int>* pliDest ) const;
	void GetListenerIDs( std::set<int>* psiDest ) const;

	// Zustand eines H�rers zur�ckgeben [read-only]
	CVAReceiverState* GetReceiverState( int iListenerID ) const;

	// Zustand eines H�rers ver�ndern [write]
	// (Hinweis: Erzeugt einen autonomen Zustand der betreffenden Schallquelle)
	CVAReceiverState* AlterListenerState( int iListenerID );

	// Neuen H�rer hinzuf�gen (R�ckgabe: Fehlercode, ID => okay, -1 => Fehler)
	int AddListener();

	// Existierende Schallquelle entfernen (Ausnahme falls ung�ltige ID)
	void RemoveListener( int iListenerID );

	// --= Portale =--

	// Stellt eine Liste mit den IDs aller Portale zusammen
	// und speichert die im angegebenen Vektor
	void GetPortalIDs( std::vector<int>* pviDest ) const;
	void GetPortalIDs( std::list<int>* pliDest ) const;
	void GetPortalIDs( std::set<int>* psiDest ) const;

	// Zustand eines Portals zur�ckgeben [read-only]
	CVAPortalState* GetPortalState( int iPortalID ) const;

	// Zustand eines Portals ver�ndern [write]
	// (Hinweis: Erzeugt einen autonomen Zustand der betreffenden Schallquelle)
	CVAPortalState* AlterPortalState( int iPortalID );

	// Neues Portal hinzuf�gen (R�ckgabe: Fehlercode, ID => okay, -1 => Fehler)
	int AddPortal();

	// Existierende Schallquelle entfernen (Ausnahme falls ung�ltige ID)
	void RemovePortal( int iPortalID );

	// --= Oberfl�chen =--

	// Stellt eine Liste mit den IDs aller Oberfl�chen zusammen
	// und speichert die im angegebenen Vektor
	void GetSurfaceIDs( std::vector<int>* pviDest ) const;

	// Zustand einer Oberfl�che zur�ckgeben [read-only]
	CVASurfaceState* GetSurfaceState( int iSurfaceID ) const;

	// Zustand einer Oberfl�che ver�ndern [write]
	// (Hinweis: Erzeugt einen autonomen Zustand der betreffenden Schallquelle)
	CVASurfaceState* AlterSurfaceState( int iSurfaceID, double dModificationTime );

	// Neues Oberfl�che hinzuf�gen (R�ckgabe: Fehlercode, ID => okay, -1 => Fehler)
	int AddSurface();

	// Existierende Oberfl�che entfernen (Ausnahme falls ung�ltige ID)
	void RemoveSurface( int iSurfaceID );

	// --= Vergleiche =--

	//! Differenzinformation erzeugen
	/**
	  * Vergleicht den eigenen Szenezustand mit einem anderen Szenezustand
	  * auf Unterschiede.
	  *
	  * Semantik: Von diesem Zustand (Basis) zum Ziel-Zustand (Zukunft)
	  * R�ckgabewert: Unterschiede in den Zust�nden (ja/nein)?
	  *
	  * \param pState Der Status, dessen �nderungen von dem eigenen Status �berpr�ft werden (darf nullptr-Pointer sein)
	  * \param pDiff Die Differenzinformation
	  *
	  */
	void Diff( const CVASceneState* pState, CVASceneStateDiff* pDiff ) const;

	//! Als Zeichenkette zur�ckgeben
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
	std::vector< int > viDelSoundSourceIDs;		// IDs gel�schter Schallquellen
	std::vector< int > viComSoundSourceIDs;		// IDs erhaltener Schallquellen

	std::vector< int > viNewReceiverIDs;			// IDs neu erzeugter H�rer
	std::vector< int > viDelReceiverIDs;			// IDs gel�schter H�rer
	std::vector< int > viComReceiverIDs;			// IDs erhaltener H�rer

	std::vector< int > viNewPortalIDs;			// IDs neu erzeugter Portale
	std::vector< int > viDelPortalIDs;			// IDs gel�schter Portale
	std::vector< int > viComPortalIDs;			// IDs erhaltener Portale

	std::vector< int > viNewSurfaceIDs;			// IDs neu erzeugter Oberfl�chen
	std::vector< int > viDelSurfaceIDs;			// IDs gel�schter Oberfl�chen
	std::vector< int > viComSurfaceIDs;			// IDs erhaltener Oberfl�chen
};

#endif // IW_VA_SCENE_STATE

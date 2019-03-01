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

#ifndef IW_VACORE_SCENEMANAGER
#define IW_VACORE_SCENEMANAGER

#include "../VAObjectContainer.h"

#include <ITAAtomicPrimitives.h>
#include <ITACriticalSection.h>


// Forward declarations
class CVASceneState;
class CVASoundSourceDesc;
class CVASoundSourceState;
class CVAListenerDesc;
class CVAReceiverState;
class CVAContainerState;
class CVAMotionState;
class CVAPortalDesc;
class CVAPortalState;
class CVASurfaceState;
class IVAObjectPool;
class ITAClock;


//! Central manager of the auditory scene
/**
  * This class is the central manager of the auditory scene. It stores any auditory object
  * in pool-based lists for rapid creation/deletion using individual reference counter.
  * The class provides access to the head state of the scene as well as to any other scene
  * in the past using IDs. It also provides mechanism to derive a scene from a base scene.
  */
class CVASceneManager
{
public:

	//! Constructor with high performance timer clock
	CVASceneManager( ITAClock* pClock );

	//! Destructor
	virtual ~CVASceneManager();

	//! Initialize scene manager
	/**
	  * Create pools for objects like sources, receivers, portals, etc.
	  *
	  * \note Performs a \Reset(), too
	  */
	void Initialize();

	//! Finalize scene manager
	/**
	  * Delete pools for objects like sources, receivers, portals, etc.
	  */
	void Finalize();

	//! Reset scene manager
	/**
	  * Resets pools for objects like sources, receivers, portals, etc.
	  */
	void Reset();

	//! ID der Wurzelkonfiguration zur�ckgeben
	int GetRootSceneStateID() const;

	//! ID der Prim�rkonfiguration zur�ckgeben
	int GetHeadSceneStateID() const;

	//! ID der Prim�rkonfiguration setzen
	void SetHeadSceneState( const int iSceneStateID );

	//! Prim�rkonfiguration zur�ckgeben
	CVASceneState* GetHeadSceneState() const;

	//! Konfiguration einer bestimmten ID zur�ckgeben
	CVASceneState* GetSceneState( const int iSceneStateID ) const;

	//! Create a new scene state based on the state given
	/**
	  * \param iSceneStateBaseID ID der Basisszene, von der Abgeleitet wird
	  * \param dModificationTime Zeitpunkt, an dem der Ableitungszustand erzeugt wird
	  *
	  * \note Ist die ID der Basiskonfiguration 0  => unabh�ngigen Zustand erzeugen
	  * \note Wichtig: Der Referenzz�hler wird direkt auf 1 gesetzt!
	  */
	CVASceneState* CreateDerivedSceneState( const int iSceneStateBaseID, const double dModificationTime );

	//! Neue Konfiguration erzeugen durch Ableitung der �bergebenen Konfiguration
	/**
	  * \param pBaseState Basisszene, von der Abgeleitet wird
	  * \param dModificationTime Zeitpunkt, an dem der Ableitungszustand erzeugt wird
	  *
	  * \note Wichtig: Der Referenzz�hler wird direkt auf 1 gesetzt!
	  */
	CVASceneState* CreateDerivedSceneState( const CVASceneState* pBaseState, const double dModificationTime );

	//! Freien Schallquellenzustand erzeugen
	CVASoundSourceState* RequestSoundSourceState();

	//! Freien H�rerzustand erzeugen
	CVAReceiverState* RequestListenerState();

	//! Freien Portalzustand erzeugen
	CVAPortalState* RequestPortalState();

	//! Freien Oberfl�chenzustand erzeugen
	CVASurfaceState* RequestSurfaceState();

	//! Freien Bewegungszustand erzeugen
	CVAMotionState* RequestMotionState();

	//! Freien Listenzustand erzeugen
	CVAContainerState* RequestContainerState();

	//! ID f�r eine Schallquelle anfordern
	int GenerateSoundSourceID();

	//! ID f�r einen H�rer anfordern
	int GenerateListenerID();

	//! ID f�r ein Portal anfordern
	int GeneratePortalID();

	//! ID f�r ein Portal anfordern
	int GenerateSurfaceID();

	// --= Unversionierte Szenedaten =--

	CVASoundSourceDesc* CreateSoundSourceDesc( int iSoundSourceID );
	void DeleteSoundSourceDesc( int iSoundSourceID );
	CVASoundSourceDesc* GetSoundSourceDesc( int iSoundSourceID ) const;

	CVAListenerDesc* CreateListenerDesc( int iListenerID );
	void DeleteListenerDesc( int iListenerID );
	CVAListenerDesc* GetListenerDesc( int iListenerID ) const;

	CVAPortalDesc* CreatePortalDesc( int iPortalID );
	void DeletePortalDesc( int iPortalID );
	CVAPortalDesc* GetPortalDesc( int iPortalID ) const;

	// Getter/Setter f�r die Namen der Szeneobjekte

	std::string GetSoundSourceName( int iSoundSourceID ) const;
	void SetSoundSourceName( int iSoundSourceID, const std::string& sName );
	std::string GetListenerName( int iListenerID ) const;
	void SetListenerName( int iListenerID, const std::string& sName );
	std::string GetPortalName( int iPortalID ) const;
	void SetPortalName( int iPortalID, const std::string& sName );

private:
	typedef std::map<int, CVASceneState*> SSMap;
	typedef std::pair<int, CVASceneState*> SSMapItem;

	ITAClock* m_pClock;
	ITAAtomicInt m_iHeadState;
	SSMap m_mSceneStates;		//!< Map: ID => Szenezustand

	int m_iIDCounterScene;		//!< ID-Counter: Szenezustand
	int m_iIDCounterSources;	//!< ID-Counter: Schallquellen
	int m_iIDCounterListeners;	//!< ID-Counter: H�rer
	int m_iIDCounterPortals;	//!< ID-Counter: Portale
	int m_iIDCounterSurfaces;	//!< ID-Counter: Oberfl�chen

	// Schallquellen

	//CVASoundSourceStateFactory* m_pFactorySS;
	IVAObjectPool* m_pPoolSceneStates;		//!< State-Pool: Szenen-Zust�nde
	IVAObjectPool* m_pPoolSourceStates;		//!< State-Pool: Schallquellen-Zust�nde
	IVAObjectPool* m_pPoolListenerStates;	//!< State-Pool: H�rer-Zust�nde
	IVAObjectPool* m_pPoolPortalStates;		//!< State-Pool: Portal-Zust�nde
	IVAObjectPool* m_pPoolSurfaceStates;	//!< State-Pool: Oberfl�chen-Zust�nde
	IVAObjectPool* m_pPoolMotionStates;		//!< State-Pool: Bewegungs-Zust�nde
	IVAObjectPool* m_pPoolContainerStates;	//!< State-Pool: Container-Zust�nde

	// --= Unversionierte Szenedaten =--

	typedef std::map<int, CVASoundSourceDesc*> SourceDescMap;
	SourceDescMap m_mSourceDesc;
	IVAObjectPool* m_pPoolSourceDesc;

	typedef std::map<int, CVAListenerDesc*> ListenerDescMap;
	ListenerDescMap m_mListenerDesc;
	IVAObjectPool* m_pPoolListenerDesc;

	typedef std::map<int, CVAPortalDesc*> PortalDescMap;
	PortalDescMap m_mPortalDesc;
	IVAObjectPool* m_pPoolPortalDesc;

	// Diese Daten m�ssen synchronisiert werden,
	// da sowohl der CoreThread als auch externe Aufrufer zugreifen
	ITACriticalSection m_csDesc;

	// States m�ssen sicher sein
	ITACriticalSection m_csStates;
};

#endif // IW_VACORE_SCENEMANAGER

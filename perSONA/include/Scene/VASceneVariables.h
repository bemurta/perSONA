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

#ifndef IW_VACORE_SCENEVARIABLES
#define IW_VACORE_SCENEVARIABLES

#include <ITATypes.h>

#include <cassert>
#include <deque>

//! Indextyp für Konfigurationsversionen
typedef int64_t VASceneConfigIndex;

//! Konfigurationsraum
/**
 * Ein Konfigurationsraum versioniert eine Menge von Variablen. 
 * Er verwaltet einen Versionszähler, welchen in ihm enthaltenen
 * Variablen verwenden. Jede Änderung einer einzelnen Variablen
 * bzw. die synchronisierte Änderung mehrerer Variablen führt zu
 * einer neuen Versionsnummer. Versionsnummer sind stets für die
 * Gesamtheit aller Variablen definiert und nicht für einzelne.
 */

class CVASceneConfigSpace {
public:
	CVASceneConfigSpace() : m_iConfigCount(0), m_bLocked(false) {}

	// Aktuelle Konfiguration zurückgeben
	VASceneConfigIndex CurrentConfig() const {
		return m_iConfigCount;
	}

	// Neue Konfiguration beginnen (gibt deren Index zurück)
	/**
	 * Falls die Klasse für atomare Operationen gesperrt ist, wird nicht erhöht
	 */
	VASceneConfigIndex NewConfig() {
		if (m_bLocked) {
			if (m_bFirst) {
				// Zähler erstmalig erhöhen
				m_bFirst = false;
				return ++m_iConfigCount;
			}

			// Alle weiteren Male Zähler belassen
			return m_iConfigCount;
		} else
			return ++m_iConfigCount;
	}

	// Atomaren Block von Operationen beginnen
	void BeginAtomic() {
		assert( !m_bLocked );
		m_bLocked = true;
		m_bFirst = true;
	}

	// Atomaren Block von Operationen beenden und Änderungen speichern
	VASceneConfigIndex EndAtomic() {
		assert( m_bLocked );
		m_bLocked = false;
		return m_iConfigCount;
	}

	/*
	// Exklusiv Sperren (Mutex->Lock)
	void Lock() const {
		// TODO!
	}

	// Exklusiven Zugriff beenden (Mutex->Unlock)
	void Unlock() const {
		// TODO!
	}
	*/

private:
	VASceneConfigIndex m_iConfigCount;
	bool m_bLocked;
	bool m_bFirst;
};

/**
 * Klassentemplate für Szenevariablen.
 * 
 * Jede Variable hat einen Lebenszyklus.
 * Dieser beginnt mit ihrer Erzeugung und Einfügung in einen Konfigurationsraum.
 * Er endet falls das übergeordnete Objekt (z.B. Schallquelle) aus der Szene
 * gelöscht wird. Allerdings bleibt die Variable trotzdem erhalten, hat aber dann
 * den Zustand 'dead'.
 */
//template <class T>
//class CVASceneVariable {
//public:
//	CVASceneVariable(CVASceneConfigSpace* pSpace, const T& initialValue) : m_pSpace(pSpace), m_iDeath(-1) {
//		// Nächste Konfiguration eröffnen
//		VASceneConfigIndex iConfig = m_pSpace->NewConfig();
//
//		// Neuen Wert speichern
//		m_vHistory.push_back( ConfigValuePair(iConfig, initialValue) );
//	}
//
//	//! Übergeordenete Szene zurückgeben
//	CVASceneConfigSpace* GetSpace() const { return m_pSpace; }
//
//	//! Älteste Konfiguration der Variable zurückgeben
//	VASceneConfigIndex GetEldestConfig() const {
//		return m_vHistory.front().first;
//	}
//
//	//! Konfiguration zurückgeben bis zu der die Variable existierte
//	VASceneConfigIndex GetLatestConfig() const {
//		return (m_iDeath == -1 ? m_pSpace->CurrentConfig() : m_iDeath);
//	}
//
//	//! Konfiguration zurückgeben zu der die Variable zuletzt modifiziert wurde
//	VASceneConfigIndex GetLastModificationConfig() const {
//		return m_vHistory.back().first;
//	}
//
//	//! Wert der Variable in einer bestimmten Konfiguration abrufen
//	void GetValue(VASceneConfigIndex iConfig, T& dest) const {
//		// Gültigkeit der Konfiguration prüfen
//		assert(iConfig >= GetEldestConfig());
//		if (m_iDeath != -1) assert(iConfig <= m_iDeath);
//
//		// Rückwärtssuche
//		std::deque<ConfigValuePair>::const_iterator cit = m_vHistory.end();
//		do {
//			--cit;
//			if (cit->first <= iConfig) {
//				dest = cit->second;
//				return;
//			}
//		} while (cit != m_vHistory.begin());
//
//		// Dies hier kann niemals erreicht werden ...
//		// Es gibt immer mindestens eine erste Konfiguration
//		assert( false );
//	}
//
//	//! Neusten Wert der Variable abrufen
//	void GetLatestValue(T& dest) const {
//		return m_vHistory.back().second;
//	}
//
//	//! Neuen Wert der Variable setzen
//	VASceneConfigIndex SetValue(const T& value) {
//		assert( Exists() );
//
//		// Nächste Konfiguration eröffnen
//		VASceneConfigIndex iConfig = m_pSpace->NewConfig();
//
//		// Prüfen ob der Wert sich zur vorherigen Konfiguration ändert
//		if (value != GetLatestValue()) {
//			// Neuen Wert speichern
//			m_vHistory.push_back( ConfigValuePair(iConfig, iValue) );
//		} else {
//			// Bisheriger Wert gilt nun auch für die neue Konfiguration
//		}
//
//		return iConfig;
//	}
//
//	//! Existiert die Variable in der aktuellen Konfiguration noch?
//	bool Exists() const {
//		return (m_iDeath == -1);
//	}
//
//	//! Lebenszeit der Variable beenden 
//	VASceneConfigIndex Destroy() {
//		assert( Exists() );
//		m_iDeath = m_pSpace->CurrentConfig();
//		return m_iDeath;
//	};
//		
//private:
//	typedef std::pair<VASceneConfigIndex, int> ConfigValuePair;
//	std::deque<ConfigValuePair> m_vHistory;
//	CVASceneConfigSpace* m_pSpace;
//	VASceneConfigIndex m_iDeath;
//};

class CVASceneVariableState {
public:
	VASceneConfigIndex m_iConfig;		// Konfigurationsindex
	CVASceneVariableState* m_pState;	// Abhängiger Zustand
	float m_fValue;						// Eigener Wert

	//! Initialisierungskonstruktor: Eigenständige Variable erzeugen
	CVASceneVariableState(VASceneConfigIndex iConfig, const float& fValue);
	
	//! Kopierkonstruktur: Abhängige Variable erzeugen
	CVASceneVariableState(VASceneConfigIndex iConfig, const CVASceneVariableState* base);

};

#endif // IW_VACORE_SCENEVARIABLES

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

//! Indextyp f�r Konfigurationsversionen
typedef int64_t VASceneConfigIndex;

//! Konfigurationsraum
/**
 * Ein Konfigurationsraum versioniert eine Menge von Variablen. 
 * Er verwaltet einen Versionsz�hler, welchen in ihm enthaltenen
 * Variablen verwenden. Jede �nderung einer einzelnen Variablen
 * bzw. die synchronisierte �nderung mehrerer Variablen f�hrt zu
 * einer neuen Versionsnummer. Versionsnummer sind stets f�r die
 * Gesamtheit aller Variablen definiert und nicht f�r einzelne.
 */

class CVASceneConfigSpace {
public:
	CVASceneConfigSpace() : m_iConfigCount(0), m_bLocked(false) {}

	// Aktuelle Konfiguration zur�ckgeben
	VASceneConfigIndex CurrentConfig() const {
		return m_iConfigCount;
	}

	// Neue Konfiguration beginnen (gibt deren Index zur�ck)
	/**
	 * Falls die Klasse f�r atomare Operationen gesperrt ist, wird nicht erh�ht
	 */
	VASceneConfigIndex NewConfig() {
		if (m_bLocked) {
			if (m_bFirst) {
				// Z�hler erstmalig erh�hen
				m_bFirst = false;
				return ++m_iConfigCount;
			}

			// Alle weiteren Male Z�hler belassen
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

	// Atomaren Block von Operationen beenden und �nderungen speichern
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
 * Klassentemplate f�r Szenevariablen.
 * 
 * Jede Variable hat einen Lebenszyklus.
 * Dieser beginnt mit ihrer Erzeugung und Einf�gung in einen Konfigurationsraum.
 * Er endet falls das �bergeordnete Objekt (z.B. Schallquelle) aus der Szene
 * gel�scht wird. Allerdings bleibt die Variable trotzdem erhalten, hat aber dann
 * den Zustand 'dead'.
 */
//template <class T>
//class CVASceneVariable {
//public:
//	CVASceneVariable(CVASceneConfigSpace* pSpace, const T& initialValue) : m_pSpace(pSpace), m_iDeath(-1) {
//		// N�chste Konfiguration er�ffnen
//		VASceneConfigIndex iConfig = m_pSpace->NewConfig();
//
//		// Neuen Wert speichern
//		m_vHistory.push_back( ConfigValuePair(iConfig, initialValue) );
//	}
//
//	//! �bergeordenete Szene zur�ckgeben
//	CVASceneConfigSpace* GetSpace() const { return m_pSpace; }
//
//	//! �lteste Konfiguration der Variable zur�ckgeben
//	VASceneConfigIndex GetEldestConfig() const {
//		return m_vHistory.front().first;
//	}
//
//	//! Konfiguration zur�ckgeben bis zu der die Variable existierte
//	VASceneConfigIndex GetLatestConfig() const {
//		return (m_iDeath == -1 ? m_pSpace->CurrentConfig() : m_iDeath);
//	}
//
//	//! Konfiguration zur�ckgeben zu der die Variable zuletzt modifiziert wurde
//	VASceneConfigIndex GetLastModificationConfig() const {
//		return m_vHistory.back().first;
//	}
//
//	//! Wert der Variable in einer bestimmten Konfiguration abrufen
//	void GetValue(VASceneConfigIndex iConfig, T& dest) const {
//		// G�ltigkeit der Konfiguration pr�fen
//		assert(iConfig >= GetEldestConfig());
//		if (m_iDeath != -1) assert(iConfig <= m_iDeath);
//
//		// R�ckw�rtssuche
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
//		// N�chste Konfiguration er�ffnen
//		VASceneConfigIndex iConfig = m_pSpace->NewConfig();
//
//		// Pr�fen ob der Wert sich zur vorherigen Konfiguration �ndert
//		if (value != GetLatestValue()) {
//			// Neuen Wert speichern
//			m_vHistory.push_back( ConfigValuePair(iConfig, iValue) );
//		} else {
//			// Bisheriger Wert gilt nun auch f�r die neue Konfiguration
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
	CVASceneVariableState* m_pState;	// Abh�ngiger Zustand
	float m_fValue;						// Eigener Wert

	//! Initialisierungskonstruktor: Eigenst�ndige Variable erzeugen
	CVASceneVariableState(VASceneConfigIndex iConfig, const float& fValue);
	
	//! Kopierkonstruktur: Abh�ngige Variable erzeugen
	CVASceneVariableState(VASceneConfigIndex iConfig, const CVASceneVariableState* base);

};

#endif // IW_VACORE_SCENEVARIABLES

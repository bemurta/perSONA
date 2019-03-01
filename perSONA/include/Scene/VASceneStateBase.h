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

#ifndef IW_VA_SCENE_STATE_BASE
#define IW_VA_SCENE_STATE_BASE

#include <VAPoolObject.h>

class CVASceneManager;

/*
 *  Diese Klasse vereint gemeinsame Eigenschaften aller Zust�nde von
 *  Objekten in einer Szene, sowie einer Szenezustandes selbst.
 *  Dies sind:
 *
 *  - Modus: Editierbar ---> Finalisiert (Fix)
 *  - Referenzz�hler f�r Garbage Collection (GC)
 */

//! Scene state base class
/**
  * This class incorporates all common properties of states of objects in a scene and
  * the scene state itself.
  *
  * @note: Object is modifieable until fixated, then read-only.
  *
  */
class CVASceneStateBase : public CVAPoolObject
{
public:
	// Konstruktor
	CVASceneStateBase();

	// Destruktor
	virtual ~CVASceneStateBase();

	// Autonome Kopie des Objektes erzeugen
	// (Diese neue Instanz ist nicht fixiert und kann ver�ndert werden,
	//  die Instanz selbst

	// Manager zur�ckgeben der diesen Zustand verwaltet
	CVASceneManager* GetManager() const;

	// Erzeugungszeitpunkt des Zustands zur�ckgeben [s]
	double GetModificationTime() const;

	// Gibt zur�ck ob das Objekt finalisiert wurde
	bool IsFixed() const;

	// Objekt finialisieren
	virtual void Fix();

protected:
	// Event-Handler: Wird Aufgerufen, bevor ein Objekt nach Benutzung wieder in frei ist (Release)
	virtual void PreRelease();

	// Manager festlegen
	virtual void SetManager( CVASceneManager* pSceneManager );

	// Erzeugungszeit setzen [s]
	virtual void SetModificationTime( const double dModificationTime );

	// Fixierung setzen
	virtual void SetFixed( const bool bFixed );

private:
	CVASceneManager* m_pManager;
	double m_dModificationTime;
	bool m_bFixed;

	friend class CVASceneManager;
};

#endif // IW_VA_SCENE_STATE_BASE

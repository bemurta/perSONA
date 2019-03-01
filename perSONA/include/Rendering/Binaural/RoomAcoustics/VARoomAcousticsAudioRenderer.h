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

#ifndef IW_VACORE_ROOMACOUSTICSLAUDIORENDERER
#define IW_VACORE_ROOMACOUSTICSLAUDIORENDERER

#if (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

// VA includes
#include <VABase.h>
#include <VAInterface.h>
#include <VAObject.h>
#include "../../VAAudioRenderer.h"
#include "../../VAAudioRendererRegistry.h"
#include "../../../Scene/VAScene.h"
#include <VAStruct.h>

// ITA includes
#include <ITADataSourceRealization.h>
#include <ITACriticalSection.h>
#include <ITASampleBuffer.h>
#include <ITAStopWatch.h>
#include <ITAThirdOctaveMagnitudeSpectrum.h>

// STL includes
#include <list>
#include <set>
#include <string.h>

// Raven includes
#include <R_RavenLocalScheduler.h>
#include <R_RavenUtils.h>

// 3rdParty includes
#include <tbb/concurrent_queue.h>

// Externe Vorwärtsdeklarationen
class CVACoreImpl;
class CVALockfreeObjectPool;
class CVASceneState;
class CVASceneStateDiff;
class CVASignalSourceManager;
class CITAThirdOctaveFIRFilterGenerator;

// Interne Vorwärtsdeklarationen
class CVARavenLocalScheduler;
class CVARoomAcousticsSimulationTaskCreator;
class ComplexSoundPath;
class ComplexSoundPathFactory;
class ITAUPFilterPool;
class Listener;
//class Portal;
class Source;


//! Binaural Room Acoustics Audio Renderer
/**
  * Verwaltet Schallausbreitungspfade mit binauraler Synthese und Raumakustik
  * für Kopfhörer oder CTC. 
  *
  * Implementiert:
  *
  * 1. Direktschall
  *		- Richtcharakteristik der Quelle
  *		- Dämpfung des Mediums
  *		- 1/r Abstandsgesetz der Kugelwellenausbreitung
  *		- Doppler-Effekt durch Relativbewegungen und endlicher Mediumsgeschwindigkeit
  *		- Kopfbezogene Übertragungsfunktion des Hörers
  *
  * 2. Frühe Reflexionen
  *		- Spiegelschallquellen u.A. mit Richtcharakteristik, Laufzeit, Mediumsdämpfung, Wandreflexionen, HRIR
  *
  * 3. Diffusen Nachhall
  *		- Ray Tracing u.A. mit Richtcharakteristik, Laufzeit, Mediumsdämpfung, Wandreflexionen, HRIR
  *
  */
class CVARoomAcousticsAudioRenderer : public CRavenSimulationSchedulerTaskHandler, public IVAAudioRenderer, public ITADatasourceRealization, public CVAObject 
{
public:
	
	//!< Setups of Raven simulation scheduler
	enum
	{
		SETUP_LOCAL		= 1, //!< Use a local scheduler on the same machine
		SETUP_REMOTE	= 2, //!< Use a remote scheduler on another machine
		SETUP_HYBRID	= 3, //!< Use both a local and a remote simulation server
	};

	CVARoomAcousticsAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVARoomAcousticsAudioRenderer();


	//! Reset renderer
	void Reset();

	//! Load a scene from file
	void LoadScene( const std::string& );
	
	//! Handle a scene state change
	/**
	  * This method updates the internal representation of the VA Scene
	  * by setting up or deleting the sound path entities as well as
	  * modifying existing ones that have changed their state, i.e.
	  * pose or dataset
	  */
	void UpdateScene( CVASceneState* pNewSceneState );

	//! Handle a state change in global auralisation mode
	/**
	  * This method updates internal settings for the global auralisation
	  * mode affecting the activation/deactivation of certain components
	  * of the sound path entities
	  */
	void UpdateGlobalAuralizationMode( int iGlobalAuralizationMode );
	
	//! Render output sample blocks
	/**
	  * This method renders the sound propagation based on the binaural approach
	  * by evaluating motion and events that are retarded in time, i.e. it switches
	  * filter parts and magnitudes of the HRIR or Directivity. It also considers
	  * the effective auralisation mode.
	  */
	void ProcessStream( const ITAStreamInfo* pStreamInfo );

	//! Returns the renderers output stream datasource
	ITADatasource* GetOutputDatasource();

	int PreTaskStart( IRavenSimulationSchedulerInterface*, CRavenSimulationTask* );

	//! Ein Simulationsergebnis ist da und muss verarbeitet werden
	/**
	  * \note Nicht reentrant!
	  *
	  * Kommt ein Simulationsauftrag mit einem Ergebnis zum Renderer zurück, muss entsprechend
	  * der Task der Teil der IR ausgetauscht werden, der durch den neuen Datensatz ersetzt wird.
	  * Hierzu wird überprüft, zu welchem Simulationsanteil das Resultat gehört (DS, IS, RT) und
	  * dann wird die gesamte RIR neu erzeugt und in die Faltungsmaschine übergeben.
	  */
	int PostTaskFinished( IRavenSimulationSchedulerInterface*, const CRavenSimulationTask*, CRavenSimulationResult* );

	//! Ein Task wurde verworfen
	/** 
	  * \note Nicht reentrant!
	  *
	  */
	int PostTaskDiscarded( IRavenSimulationSchedulerInterface*, const CRavenSimulationTask* );

	//!
	CVAObjectInfo GetObjectInfo() const;

	CVAStruct CallObject( const CVAStruct& oArgs );

	//! Aus einem Terzbandspektrum eine Impulsantwort bauen
	/**
	  * Die länge der Impulsantwort entspricht der Länge des ITASampleFrame. 
	  * \note Reentrant (blocking wait)
	  */
	void GenerateIRFromTOMags( const ITABase::CThirdOctaveGainMagnitudeSpectrum& oMags, ITASampleBuffer& sbFilter );

	//! Gibt die Schallmediumsgeschwindigkeit zurück [m/s]
	float GetSpeedOfSound() const;

protected:

	CRavenConfig m_oRavenConfig; //!< Current configuration of Renderer

private:

	const CVAAudioRendererInitParams m_oParams; //!< Create a const copy of the init params

	CVACoreImpl* m_pCore;				//!< Pointer to the core

	IRavenSimulationSchedulerInterface* m_pVARaven;		//!< Scheduler für Simulationsaufträge (Raven)

	int m_iSetup;							//!< Scheduler setup (Local, Remote, Hybrid) see \SetupModes

	CVASceneState* m_pCurSceneState;		//!< Zeiger auf aktuell benutzte Scene im Renderer
	CVASceneState* m_pNewSceneState;		//!< Zeiger auf neue Scene für den Renderer
	
	IVAObjectPool* m_pComplexSoundPathPool;					//!< Speichermanagement für komplexe Schallpfade
	ComplexSoundPathFactory* m_pComplexSoundPathFactory;	//!< Erzeuger für komplexe Schallpfade als Pool-Objekte

	IVAObjectPool* m_pSourcePool;			//!< Speichermanagement für Quellen
	IVAObjectPool* m_pListenerPool;			//!< Speichermanagement für Hörer
	ITAUPFilterPool* m_pRIRFilterPool;		//!< Speichermanagement für Raumimpulsantworten/Simulationsergebnisse
	CITAThirdOctaveFIRFilterGenerator* m_pThirdOctaveFIRFilterGenerator; //!< Filter-Generator für Impulsantworten aus Terzbandsprektren

	ITACriticalSection m_csFilterGenerator; //!< Geteilte Resource "Filter-Generator" vor reentrance schützen

	//CVARoomAcousticsSimulationTaskCreator* m_pTaskCreator;	//!< Erzeuger für individualisierte Simulationsaufgaben
	CVALockfreeObjectPool* m_pTaskPool;						//!< Speichermanagement für Simulationsaufgaben
	unsigned long int m_uiUniqueTaskNumber;						//!< Zähler/Generator für eindeutige Identifier

	std::map< int, Source* > m_mSources;		//!< Interne Abbildung der verfügbaren Quellen
	std::map< int, Listener* > m_mListener;		//!< Interne Abbildung der verfügbaren Hörer

	std::list< ComplexSoundPath* > m_lComplexSoundPaths;	//!< Liste aller komplexen Schallpfade (im Thread-Kontext: VACore)
	unsigned long int m_uiUniquePathNumber;					//!< Unique-Erzeuger für Pfadnummern
	
	// ... Evtl. mal in eine Config-Datenklasse zusammenfassen
	int m_iCurGlobalAuralizationMode;	//!< Der aktuelle Auralisierungsmodues, mit dem der Renderer arbeitet
	float m_fSpeedOfSound;				//!< Schallgeschwindigkeit zur Berechnung von Verzögerungen  [m/s]

	ITASampleBuffer m_psbVDLTempOut;	//!< Zwischenspeicher für Ausgabesamples der VDL

	double m_dDirectSoundPowerCorrectionFactor; //!< Anpassung der Direktschallenergie vs. Nachhallfilter
	
	ITAStopWatch m_swProcessStream;		//!< Zeitmessung
	ITAStopWatch m_swConvolveStream;	//!< Zeitmessung
	ITAStopWatch m_swUpdateFilter;		//!< Zeitmessung

	unsigned int m_uiProcNum;

	std::vector< ComplexSoundPath* > m_vpDropoutBlameList; //!< Profiling der Schallpfade, welche eine Dropout ausgelöst haben

	//! Daten welche nur im Kontext des Audiothreads genutzt werden
	struct {
		std::list< ComplexSoundPath* > m_lComplexSoundPathsInternal;					//!< Liste aller Schallpfade (im Thread-Kontext: Audiostreaming)
		tbb::concurrent_queue< ComplexSoundPath* > m_qpNewComplexSoundPathsInternal;	//!< Lock-free queue: Neue Schallpfade
		tbb::concurrent_queue< ComplexSoundPath* > m_qpDelComplexSoundPathsInternal;	//!< Lock-free queue: Entfernte Schallpfade
	} ctxAudio;

	//! Interne Listen der Quellen, Hörer und Schallpfade aktualisieren
	/**
	  * Bildet Änderungen an der Anzahl von Objekten in der VA Szene intern ab:
	  *
	  *  - sämtliche neuen Schallpfade werden hinzugefügt
	  *  - zu löschende Schallpfade werden markiert
	  *  - neue Hörer werden erstellt
	  *  - neue Quellen werden erstellt
	  *  - zu löschende Hörer werden markiert
	  *  - zu löschende Quellen werden markiert
	  */
	void ManageComplexSoundPaths( const CVASceneStateDiff* );

	//! Hörer erzeugen
	Listener* CreateListener( const int iID );

	//! Hörer löschen
	void DeleteListener( const int iID );

	//! Quelle erzeugen
	Source* CreateSource( const int iID );

	//! Quelle löschen
	void DeleteSource( const int iID );

	//! Create a new sound path
	ComplexSoundPath* CreateComplexSoundPath( Source* pSource, Listener* pListener);

	// Mark a non-existing sound path as deleted and move to delete liste
	void DeleteComplexSoundPath( ComplexSoundPath* pPath );

	// Aktualisiert die internen Daten für den Audio-Threadkontext (u.A. Liste der Pfade)
	void SyncInternalData();

	//! Update der Trajektorien für Quellen und Empfänger
	void UpdateTrajectories();

	//! Trajektorien abtasten
	void SampleTrajectories( double dTime );

	//! Erneuern der Pfadelemente (non-reentrant!)
	void UpdateComplexSoundPaths();

	//! Calls auf das Object mit Command "config" und "set" (key, value)
	void CallObjectConfigSet( const std::string& sKey, const CVAStructValue* pValue );
	CVAStruct CallObjectConfigGet( const std::string& sKey ) const;

	friend class ComplexSoundPath;

	//! Not for use, avoid C4512
	inline CVARoomAcousticsAudioRenderer operator=( const CVARoomAcousticsAudioRenderer & ) { VA_EXCEPT_NOT_IMPLEMENTED; };
};

#endif // (VACORE_WITH_RENDERER_BINAURAL_ROOM_ACOUSTICS==1)

#endif // IW_VACORE_ROOMACOUSTICSLAUDIORENDERER

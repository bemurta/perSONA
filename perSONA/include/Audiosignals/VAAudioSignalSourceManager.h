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

#ifndef IW_VACORE_AUDIOSIGNALSOURCEMANAGER
#define IW_VACORE_AUDIOSIGNALSOURCEMANAGER

#include <VABase.h>

#include "../Drivers/Audio/VAAudioDriverConfig.h"
#include <VACoreDefinitions.h>

#include <VAAudioSignalSource.h>

#include <ITACriticalSection.h>
#include <ITASampleBuffer.h>
#include <ITAAtomicPrimitives.h>

#include <VistaInterProcComm/Concurrency/VistaThreadEvent.h>

#include <tbb/concurrent_queue.h>

#include <map>
#include <string>
#include <vector>
#include <set>


class ITADatasource;
class IVAInterface;
class CVAAudiofileSignalSource;
class CVACoreImpl;

/*
- Signalquellen wie Datei und Sampler verwaltet der Manager (Erzeugung, Freigabe)
- Diesen Signalquellen weißt er auch ihre IDs zu
- Manche Signalquellen werden nicht vom Manager besessen (z.B. die vom Audiogerät)
- Es gibt dynamische und statische Signalquellen:
- Eingänge vom Audiogerät sind immer statisch (werden beim Start automatisch erzeugt und können nie entfernt werden)
- Alle anderen sind dynamisch: Sie können beliebig zur Laufzeit erzeugt und entfernt werden

- Warum Mnemonics (af001) => Weil einfacher anzusprechen

- Fetch Synchronisieren mit Register/Unreg.
*/

class CVAAudioSignalSourceManager
{
public:
	CVAAudioSignalSourceManager( CVACoreImpl* pParentCore, const CVAAudioDriverConfig& oAudioDriverConfig, ITADatasource* pDeviceInputSource );
	virtual ~CVAAudioSignalSourceManager();

	//! Alle Quellen entfernen
	void Reset();

	//! Registriert eine Audiosignalquelle
	/**
	 * Diese Methode fügt dem Manager eine bestehende Audiosignalquelle hinzu.
	 * Hierbei wird der Audiosignalquelle auf Basis ihres Mnemonics eine ID zugeteilt.
	 * Diese Methode u.A. kann benutzt werden, um externe Signalquellen anzubinden.
	 *
	 * bManaged = true -> Manager verwirft die Datenquelle wenn er freigegeben wird
	 * bDynamic = true -> Quelle kann zur Laufzeit aus dem Manager entfernt werden
	 *
	 * Allgemeine AddMethode.
	 * Damit können auch eigene Typen (Unterklassen von IVAAudioSignalSource) registriert werden
	 * - IDs (Zahlen) werden immer erhöht, auch wenn Zahl davor freigegeben wurde
	 *
	 * \return ID der hinzugefügten Quelle
	 */
	std::string RegisterSignalSource( IVAAudioSignalSource* pSource, const std::string& sName, bool bManaged, bool bDynamic );

	//! Deregistriert eine Audiosignalquelle, gibt diese aber nicht frei
	/**
	 * Wichtig: Nur dynamische, ungemanagete Quellen können entfernt werden
	 */
	void UnregisterSignalSource( const std::string& sID );

	//! ID einer Geräte-Eingang-Signalquelle zurückgeben
	std::string GetAudioDeviceInputSignalSource( int iInput ) const;

	//! Audiodatei-Signalquelle hinzufügen
	std::string CreateAudiofileSignalSource( const std::string& sFilename, const std::string& sName );

	std::string CreateTextToSpeechSignalSource( const std::string& sName );

	//! Sampler-Signalquelle hinzufügen
	std::string CreateSequencerSignalSource( const std::string& sName );

	//! Netzwerkstream-Signalquelle hinzufügen
	std::string CreateNetstreamSignalSource( const std::string& sBindAddress,		int iRecvPort,		const std::string& sName );

	std::string CreateEngineSignalSource( const std::string& sName );

	std::string CreateMachineSignalSource( const std::string& sName );

	//! Erwirkt das Löschen eine Audiosignalquelle (d.h. gibt diese frei)
	/**
	 * Wichtig: Nur dynamische, gemanagete Quellen können gelöscht werden
	 */
	void DeleteSignalSource( const std::string& sID );

	//! Datenquelle anfordern (für Benutzung durch eine Schallquelle) und Referenzzähler erhöhen
	// Zusätzlich wird der Puffer mit den Eingangsdaten der Quelle zurückgeben. Dies bleibt immer der selbe Zeiger!
	IVAAudioSignalSource* RequestSignalSource( const std::string& sID, const ITASampleBuffer** ppsbInputBuf = nullptr );

	//! Datenquelle freigeben (von Schallquelle) und deren Referenzzähler vermindern
	void ReleaseSignalSource( IVAAudioSignalSource* pSource );

	//! Eingabensamples einer Datenquelle abrufen
	//ITASampleBuffer* GetSignalSourceData(IVAAudioSignalSource* pSourc);

	//! Nächste Eingabedaten aller Datenquelle in die Samplepuffer laden
	/**
	 * Hinweis: Diese Methode wird direkt aus dem Audio-Streaming-Kontrollfluss
	 *          aufrufen.
	 */
	void FetchInputData( const CVAAudiostreamState* pStreamInfo );

	// Informationen zu einer Signalquelle zurückgeben
	CVASignalSourceInfo GetSignalSourceInfo( const std::string& sSignalSourceID ) const;

	//! Lister aller (momentan) verfügbaren Signalquellen zurückgeben (Liste derer IDs)
	void GetSignalSourceInfos( std::vector<CVASignalSourceInfo>& vssiDest ) const;

	//! ID zu einer Signalquellen-Instanz suchen
	std::string GetSignalSourceID( IVAAudioSignalSource* pSource ) const;

	//! Stille zurückgeben
	const ITASampleBuffer* GetSilenceBuffer() const;

private:
	// Hilfsklasse welche registrierte Audiosignalquellen beschreibt
	class CAudioSignalSource
	{
	public:
		IVAAudioSignalSource* pSource;	// Audiosignalquelle
		ITASampleBuffer sbSampleBuffer;	// Puffer für nächsten Block Audiosamples
		std::string sName;				// Name
		int iRefCount;					//!< Reference count of associated sound sources
		bool bManaged;					// Ist der Manager Besitzer der Quelle?
		bool bDynamic;					// Kann die Quelle zur Laufzeit entfernt werden?

		inline CAudioSignalSource( IVAAudioSignalSource* pSource, const std::string& sName, int iBlockLength, bool bManaged, bool bDynamic )
			: sbSampleBuffer( iBlockLength, false )
			, pSource( pSource )
			, sName( sName )
			, iRefCount( 0 )
			, bManaged( bManaged )
			, bDynamic( bDynamic )
		{};
	};

	typedef std::map<std::string, CAudioSignalSource> SignalSourceMap;
	typedef std::pair<std::string, CAudioSignalSource> SignalSourceItem;
	typedef SignalSourceMap::iterator SignalSourceMapIt;
	typedef SignalSourceMap::const_iterator SignalSourceMapCit;

	SignalSourceMap m_mSignalSources;							// Assoziationen ID -> Eintrag
	ITACriticalSection m_csSignalSourceAccess;					// Lock für die Quellen
	std::map<std::string, int> m_mMnemonicCount;	// Zähler für Mnemonics (für Zahl in den IDs)

	CVACoreImpl* m_pParentCore;
	double m_dSamplerate;
	int m_iBlocklength;
	int m_iDeviceInputs;
	ITADatasource* m_pDeviceInputSource;
	ITASampleBuffer m_sbSilence;
	std::vector<ITASampleBuffer> m_vsbDeviceInputBuffers;	// Samplepuffer für die Audio-Eingänge
	std::vector< std::string > m_vsDeviceInputSourceIDs;	// Eingangsnummer -> Datenquellen-ID

	std::set<CAudioSignalSource*> m_spIntSources;	// Audiostream-interne Liste der Quellen
	tbb::concurrent_queue<CAudioSignalSource*> m_qpNewSources; //!< Lock-free queue: New sources
	tbb::concurrent_queue<CAudioSignalSource*> m_qpDelReqSources; //!< Lock-free queue: Request delete sources

	ITAAtomicInt m_iStreamCounter;	//!< Counts the number of processed stream blocks
	VistaThreadEvent m_evStreamCounterInc;	//!< Event: Stream counter incremented

	//! Signal source find by ID
	SignalSourceMapIt FindSignalSource( const std::string& sID );

	//! Signal source find by ID (const)
	SignalSourceMapCit FindSignalSource( const std::string& sID ) const;

	//! Signal source find by pointer
	SignalSourceMapIt FindSignalSource( IVAAudioSignalSource* pSource );

	//! Warten bis im Streaming-Kontext die Änderung an den Signalquellen übernommen wurde [blocking]
	void SyncSignalSources();
};

#endif // IW_VACORE_AUDIOSIGNALSOURCEMANAGER

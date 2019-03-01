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

#ifndef IW_VACORE_AUDIODRIVERBACKEND
#define IW_VACORE_AUDIODRIVERBACKEND

// Includes
#include <string>

// Vorwärtsdeklarationen
class ITAStreamProperties;
class ITADatasource;

/**
 * Diese rein abstrakte Klasse definiert für Virtual Acoustics die Schnittstelle 
 * zur Benutzung verschiedener Audiotreiber-Architekturen (ASIO, DirectSound, ALSA, JACK, ...).
 *
 * Jedes Backend:
 *
 *   - Stellt gibt Daten auf einer festen Anzahl Ausgabekanäle wieder
 *   - Kann eine feste Anzahl Eingabekanäle bereitstellen 
 *
 *   - Die Ausgabekanäle werden aus einer Datenquelle gespeist
 *   - Die Eingabekanäle werden als Mono-Datenquellen vom Back bereitgestellt
 *
 * 'Fest' bedeutet in diesem Kontext, das sich die Parameter nicht mehr zur Laufzeit ändern.
 * Diese Parameter werden üblicherweise aus einer Konfigurationsdatei eingelesen.
 *
 * - Alle Fehler werden mittels VAExceptions behandelt.
 * - Nebenläufigkeit: Keine! Alle Funktionen seriell. Keine Parallel.
 */

class IVAAudioDriverBackend {
public:
	virtual ~IVAAudioDriverBackend() {};

	//! Namen des Treiber-Architektur zurückgeben (z.B. "ASIO")
	virtual std::string getDriverName() const=0;

	//! Namen der gewählten Audio-Gerätes zurückgeben (z.B. "RME Hammerfall DSP")
	virtual std::string getDeviceName() const=0;

	//! Anzahl der Eingänge (Mono) zurückgeben
	virtual int getNumberOfInputs() const=0;

	//! Eigenschaften (Anzahl Ausgabekanäle, Abtastrate, Blocklänge) des Ausgabe-Streams zurückgeben
	virtual const ITAStreamProperties* getOutputStreamProperties() const=0;

	//! Datenquelle für die Eingabe zurückgeben
	virtual ITADatasource* getInputStreamDatasource() const=0;

	//! Datenquelle für die Ausgabe setzen
	virtual void setOutputStreamDatasource(ITADatasource* pDatasource)=0;

	//! Backend initialisieren (bereitmachen für das Streaming)
	virtual void initialize()=0;

	//! Backend freigeben
	virtual void finalize()=0;

	//! Gibt zurück, ob das Streaming gestartet ist
	virtual bool isStreaming()=0;

	//! Audio-Streaming starten
	virtual void startStreaming()=0;

	//! Audio-Streaming starten
	/**
	 * Wichtig: Diese Methode kommt erst zurück wenn keine Calls mehr auf die Datenquelle gehen.
	 */
	virtual void stopStreaming()=0;
};

#endif // IW_VACORE_AUDIODRIVERBACKEND

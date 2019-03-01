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

// Vorw�rtsdeklarationen
class ITAStreamProperties;
class ITADatasource;

/**
 * Diese rein abstrakte Klasse definiert f�r Virtual Acoustics die Schnittstelle 
 * zur Benutzung verschiedener Audiotreiber-Architekturen (ASIO, DirectSound, ALSA, JACK, ...).
 *
 * Jedes Backend:
 *
 *   - Stellt gibt Daten auf einer festen Anzahl Ausgabekan�le wieder
 *   - Kann eine feste Anzahl Eingabekan�le bereitstellen 
 *
 *   - Die Ausgabekan�le werden aus einer Datenquelle gespeist
 *   - Die Eingabekan�le werden als Mono-Datenquellen vom Back bereitgestellt
 *
 * 'Fest' bedeutet in diesem Kontext, das sich die Parameter nicht mehr zur Laufzeit �ndern.
 * Diese Parameter werden �blicherweise aus einer Konfigurationsdatei eingelesen.
 *
 * - Alle Fehler werden mittels VAExceptions behandelt.
 * - Nebenl�ufigkeit: Keine! Alle Funktionen seriell. Keine Parallel.
 */

class IVAAudioDriverBackend {
public:
	virtual ~IVAAudioDriverBackend() {};

	//! Namen des Treiber-Architektur zur�ckgeben (z.B. "ASIO")
	virtual std::string getDriverName() const=0;

	//! Namen der gew�hlten Audio-Ger�tes zur�ckgeben (z.B. "RME Hammerfall DSP")
	virtual std::string getDeviceName() const=0;

	//! Anzahl der Eing�nge (Mono) zur�ckgeben
	virtual int getNumberOfInputs() const=0;

	//! Eigenschaften (Anzahl Ausgabekan�le, Abtastrate, Blockl�nge) des Ausgabe-Streams zur�ckgeben
	virtual const ITAStreamProperties* getOutputStreamProperties() const=0;

	//! Datenquelle f�r die Eingabe zur�ckgeben
	virtual ITADatasource* getInputStreamDatasource() const=0;

	//! Datenquelle f�r die Ausgabe setzen
	virtual void setOutputStreamDatasource(ITADatasource* pDatasource)=0;

	//! Backend initialisieren (bereitmachen f�r das Streaming)
	virtual void initialize()=0;

	//! Backend freigeben
	virtual void finalize()=0;

	//! Gibt zur�ck, ob das Streaming gestartet ist
	virtual bool isStreaming()=0;

	//! Audio-Streaming starten
	virtual void startStreaming()=0;

	//! Audio-Streaming starten
	/**
	 * Wichtig: Diese Methode kommt erst zur�ck wenn keine Calls mehr auf die Datenquelle gehen.
	 */
	virtual void stopStreaming()=0;
};

#endif // IW_VACORE_AUDIODRIVERBACKEND

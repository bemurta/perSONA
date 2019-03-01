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

#ifndef IW_VACORE_DIRECTIVITY_MANAGER
#define IW_VACORE_DIRECTIVITY_MANAGER

#include <VABase.h>

#include "VADirectivity.h"
#include "../VAObjectContainer.h"

#include <ITACriticalSection.h>

#include <vector>

class IVAInterface;

class CVADirectivityManager
{
public:
	CVADirectivityManager( IVAInterface* pAssociatedCore, const double dDesiredSamplerate );
	virtual ~CVADirectivityManager();

	void Initialize();
	void Finalize();
	void Reset();

	//! L�dt einen HRIR-Datensatz aus einer Datei und gibt dessen ID zur�ck
	/**
	 * AUFRUFER: Nur User-CF
	 */
	int CreateDirectivity( const CVAStruct& oParams, const std::string& sName = "" );

	//! Gibt einen geladenen HRIR-Datensatz wieder frei
	/**
	 * AUFRUFER: Nur User-CF
	 *
	 * Hinweis: Der HRIR-Datensatz wird gel�scht, falls er nicht in Benutzung ist
	 *
	 * \return true  HRIR-Datensatz wurde gel�scht und deren Speicher freigegeben
	 *         false HRIR-Datensatz ist noch in Benutzung und wurde nicht freigegeben
	 */
	bool DeleteDirectivity( const int iID );

	//! Gibt einen HRIR-Datensatz zur�ck und erh�ht deren Referenzz�hler
	/**
	 * AUFRUFER: Nur Core-CF
	 */
	IVADirectivity* RequestDirectivity( const int iID );

	//! Dekrementiert den Referenzz�hler eines HRIR-Datensatzes (kein automatisches L�schen)
	/**
	 * AUFRUFER: Nur Core-CF
	 * \return Anzahl der verbleibenden Referenzen auf die HRIR-Datensatz
	 */
	int ReleaseDirectivity( const int iID );

	//! Gibt die Anzahl an Referenzen (Anzahl benutzende H�rer) zur�ck
	int GetDirectivityRefCount( const int iID ) const;

	//! Gibt Informationen �ber eine geladenen HRIR-Datensatz zur�ck
	/**
	 * AUFRUFER: Nur User-CF
	 */
	CVADirectivityInfo GetDirectivityInfo( const int iID );

	//! Gibt Informationen �ber alle geladenen HRIR-Datens�tze zur�ck
	/**
	 * AUFRUFER: Nur User-CF
	 */
	void GetDirectivityInfos( std::vector< CVADirectivityInfo >& voInfos );

	//! Gibt Informationen �ber alle geladenen HRIR-Datens�tze auf der Konsole aus
	void PrintInfos();

private:
	CVAObjectContainer<IVADirectivity> m_oDirectivities;
	double m_dSampleRate;
	IVAInterface* m_pAssociatedCore;
};

#endif // IW_VACORE_DIRECTIVITY_MANAGER

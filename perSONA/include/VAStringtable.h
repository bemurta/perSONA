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

#ifndef IW_VACORE_STRINGTABLE
#define IW_VACORE_STRINGTABLE

#include <map>
#include <string>

/**
 * Diese Klasse realisiert einen einfachen Stringtable, welcher als
 * assoziativer Container agiert, welcher IDs Zeichenketten zuweist.
 * Er wird u.A. benutzt um Szeneobjekten die Namen zuzuweisen.
 * Die Klasse ist nicht thread-safe.
 */
class CVAStringtable
{
public:
	virtual inline ~CVAStringtable() {};

	// String abfragen (falls ID nicht gefunden, wird leerer String zurückgegeben)
	inline std::string GetString( int iID ) const
	{
		ID2StrMap::const_iterator cit = m_mData.find( iID );
		return ( cit != m_mData.end() ? cit->second : "" );
	};

	// String setzen (falls unter dieser ID schon vorhanden, wird überschrieben)
	inline void SetString( int iID, const std::string& sString )
	{
		ID2StrMap::iterator it = m_mData.find( iID );
		if( it == m_mData.end() )
			m_mData.insert( ID2StrMapItem( iID, sString ) );
		else
			it->second = sString;
	}

private:
	typedef std::map< int, std::string > ID2StrMap;
	typedef std::pair< int, std::string > ID2StrMapItem;

	ID2StrMap m_mData;
};

#endif // IW_VACORE_STRINGTABLE

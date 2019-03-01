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

#ifndef IW_VACORE_MACROMAP
#define IW_VACORE_MACROMAP

#include <map>
#include <string>

class CVAMacroMap
{
public:
	// Alle Makros löschen
	void Clear();

	// Makro-Eintrag hinzufügen
	void AddMacro( const std::string& sName, const std::string& sValue );

	// Makros in einem String durch deren Werte ersetzen
	std::string SubstituteMacros( const std::string& sStr ) const;

	std::map< std::string, std::string > GetMacroMapCopy() const;

private:
	// Macros $(...) = ...
	std::map< std::string, std::string > m_mMacroMap;
};

#endif // IW_VACORE_MACROMAP

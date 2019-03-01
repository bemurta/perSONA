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

#ifndef IW_VACORE_PORTALDESC
#define IW_VACORE_PORTALDESC

// Diese Klasse beschreibt die statischen (unversionierten) Daten einer Schallquelle
class CVAPortalDesc : public CVAPoolObject
{
public:
	int iID;					// Schallquellen-ID
	std::string sName;			// Angezeigter Name

	// TODO: TPDAG-Node

	inline void PreRequest()
	{
		iID = -1;
		sName = "";
	};
};

#endif // IW_VACORE_PORTALDESC

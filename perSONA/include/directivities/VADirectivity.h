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

#ifndef IW_VACORE_DIRECTIVITY
#define IW_VACORE_DIRECTIVITY

#include <string>

//! Abstract directivity base class
/**
 * This class pointer is stored in the directivity manager. Use casting to convert
 * to the required specialized class you want to employ.
 */
class IVADirectivity
{
public:
	enum DirectivityType
	{
		UNSPECIFIED = -1,
		DAFF_HRIR = 1,
		DAFF_ENERGETIC,
		MULTIPOLE,
		SPHERICAL_HARMONICS,
		DAFF_HATO_HRIR
	};

	inline IVADirectivity()
		: m_iType( UNSPECIFIED )
	{};

	virtual inline ~IVADirectivity() {};
	virtual std::string GetName() const = 0;
	virtual std::string GetDesc() const = 0;

	//! Returns type of class for casting
	virtual inline DirectivityType GetType() const
	{
		return m_iType;
	};

protected:
	DirectivityType m_iType;
};

#endif // IW_VACORE_DIRECTIVITY

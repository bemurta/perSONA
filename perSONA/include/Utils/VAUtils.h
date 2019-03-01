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

#ifndef IW_VACORE_UTILS
#define IW_VACORE_UTILS

#include <VA.h>

#include <ITAException.h>
#include <ITAStringUtils.h>

#include <VistaBase/VistaExceptionBase.h>

#include <cassert>
#include <string>
#include <map>

static const CVAStruct g_oEmptyStruct;
class CVAMotionState;

//! Sleep function
void VASleep( int iMilliseconds );

//! Identität (ID) des aktuellen Threads zurückgeben
long getCurrentThreadID();

//! Standard CVAException für alle unbekannten Fehler generieren
CVAException getDefaultUnexpectedVAException();

//! Systemauslastung durch den aktuellen Prozess ermitteln
float getCurrentProcessSystemLoad();

//! ITAException in eine CVAException konvertieren
CVAException convert2VAException( const ITAException& e );

//! VistaExceptionBase in eine CVAException konvertieren
CVAException convert2VAException( const VistaExceptionBase& e );

void ConvertQuaternionToViewUp( const VAQuat& qOrient, VAVec3& vView, VAVec3& vUp );

void ConvertViewUpToQuaternion( const VAVec3& vView, const VAVec3& vUp, VAQuat& qOrient );

//! Get azimuthal angle from a reference coordinate system defined by origin and orientation to a target position
double GetAzimuthOnTarget_DEG( const VAVec3& vOriginPos, const VAVec3& vView, const VAVec3& vUp, const VAVec3& vTargetPos );

//! Get elevation angle from a reference coordinate system defined by origin and orientation to a target position
double GetElevationOnTarget_DEG( const VAVec3& vOriginPos, const VAVec3& vView, const VAVec3& vUp, const VAVec3& vTargetPos );

//! Ersetzt Backslashes (bzw. PATH_SEPARATOR) durch doppelte Backslashes (bzw. PATH_SEPARATOR)
std::string correctPathForLUA( const std::string& sPath );

//! Loads an INI file and converts it to a VA struct
void LoadStructFromINIFIle( const std::string& sFilePath, CVAStruct& oData );

//! Will only write top level values and first level structs as sections with simple types
/**
  * @param[in] sFilePath File path
  * @param[in] oData Struct data
  */
void StoreStructToINIFile( const std::string& sFilePath, const CVAStruct& oData );

//! Sets all motion related parameters in a core event based on a motion event
void SetCoreEventParams( CVAEvent& oEvent, const CVAMotionState* pMotionState );

//! Case-insensitive literals
template< class T > class CVALiterals
{
public:
	inline void Add( const std::string& sLiteral, const T& value )
	{
		m_mLiterals.insert( std::pair< std::string, T >( toUppercase( sLiteral ), value ) );
	};

	inline bool Translate( const std::string& s, T& dest ) const
	{
		std::string t = toUppercase( stripSpaces( s ) );
		typename std::map< std::string, T >::const_iterator it = m_mLiterals.find( t );
		if( it == m_mLiterals.end() )
			return false;
		dest = it->second;
		return true;
	};

private:
	std::map<std::string, T> m_mLiterals;
};

//! Utility class helping to interpret structs as configuration specifications
class CVAConfigInterpreter
{
public:
	inline CVAConfigInterpreter( const CVAStruct& oStruct )
		: m_oStruct( oStruct )
	{
	};

	// Returns the prefix (string) for error outputs
	inline std::string GetErrorPrefix() const
	{
		return m_sErrorPrefix;
	};

	// Sets a prefix (string) for error outputs
	// Helpful for meaningful error messages...
	// Customize to your needs
	inline virtual void SetErrorPrefix( const std::string& sPrefix )
	{
		if( sPrefix.empty() )
			m_sErrorPrefix = "";
		else
			m_sErrorPrefix = sPrefix + ": ";
	};

	// Hook: Error handler
	// Customize to your needs
	inline virtual void Error( const std::string& sErrorMessage ) const
	{
		VA_EXCEPT1( m_sErrorPrefix + sErrorMessage );
	};

	// --= Error messages =--

	inline void ErrorMissKey( const std::string& sKey ) const
	{
		Error( std::string( "Missing a key \"" ) + sKey + std::string( "\"" ) );
	};

	inline void ErrorWrongType( const std::string& sKey, const std::string& sType ) const
	{
		Error( std::string( "Key \"" ) + sKey + std::string( "\" must be " ) + sType );
	};

	inline void ErrorEmptyKey( const std::string& sKey ) const
	{
		Error( std::string( "Key \"" ) + sKey + std::string( "\" must not be empty" ) );
	};

	// --= Sub groups =--

	// Semantic: check HasKey(s) && IsStruct(s)
	inline const CVAStruct& ReqStruct( const std::string& sStructName ) const
	{
		if( !m_oStruct.HasKey( sStructName ) ) ErrorMissKey( sStructName );

		const CVAStructValue& oKey = m_oStruct[ sStructName ];
		if( !oKey.IsStruct() ) ErrorWrongType( sStructName, "a struct" );

		return oKey;
	};

	// Semantic: if HasKey(s) then check IsStruct(s)
	// Info: If no key exists, an empty struct is returned
	inline const CVAStruct& OptStruct( const std::string& sStructName ) const
	{
		if( m_oStruct.HasKey( sStructName ) )
		{
			const CVAStructValue& oValue = m_oStruct[ sStructName ];
			if( !oValue.IsStruct() )
				ErrorWrongType( sStructName, "a struct" );
			return oValue;
		}
		else
		{
			// Group does not exist => return empty struct
			return g_oEmptyStruct;
		}
	};

	// --= Bools =--

	inline void ReqBool( const std::string& sKey, bool& bValue, const CVALiterals<bool>* pLiterals = nullptr ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );

		if( !pValue ) ErrorMissKey( sKey );

		if( pValue->IsBool() ) {
			bValue = *pValue;
			return;
		}

		if( pValue->IsString() ) {
			// Try to translate literals
			if( pLiterals && pLiterals->Translate( *pValue, bValue ) ) return;

			// Try to convert implicitly
			try {
				bValue = *pValue;
				return;
			}
			catch( ... ) {}
		}

		ErrorWrongType( sKey, "boolean" );
	};

	inline bool OptBool( const std::string& sKey, bool& bValue, bool bDefault, const CVALiterals<bool>* pLiterals = nullptr ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );

		if( !pValue ) {
			bValue = bDefault;
			return false;
		}

		if( pValue->IsBool() ) {
			bValue = *pValue;
			return true;
		}

		if( pValue->IsString() ) {
			// Try to translate literals
			if( pLiterals && pLiterals->Translate( *pValue, bValue ) ) return true;

			// Try to convert implicitly
			try {
				bValue = *pValue;
				return true;
			}
			catch( ... ) {
			}
		}

		ErrorWrongType( sKey, "boolean" );
		return false;
	};

	// --= Strings =--

	// Gets a mandatory string value. An exception is thrown if it does not exist 
	inline void ReqString( const std::string& sKey, std::string& sValue ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );
		if( !pValue ) ErrorMissKey( sKey );

		// Allows implicit type conversions
		if( pValue->IsBool() || pValue->IsNumeric() || pValue->IsString() )
		{
			sValue = std::string( *pValue );
			return;
		}

		ErrorWrongType( sKey, "a string" );
	};

	// Gets an Optional string value. A default value set if it does not exist
	// Return value: Key exists?
	inline bool OptString( const std::string& sKey, std::string& sValue, const std::string& sDefault = "" ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );
		if( !pValue ) {
			sValue = sDefault;
			return false;
		}

		// Allows implicit type conversions
		if( pValue->IsBool() || pValue->IsNumeric() || pValue->IsString() )
		{
			sValue = std::string( *pValue );
			return true;
		}

		ErrorWrongType( sKey, "a string" );
		return false;
	};

	// Like ReqString(...), but checks that the string is non-empty
	inline void ReqNonEmptyString( const std::string& sKey, std::string& sValue ) const
	{
		ReqString( sKey, sValue );
		if( sValue.empty() ) ErrorEmptyKey( sKey );
	};

	// Like OptString(...), but checks that the string is non-empty
	inline bool OptNonEmptyString( const std::string& sKey, std::string& sValue ) const
	{
		bool bExist = OptString( sKey, sValue );
		if( sValue.empty() )
			ErrorEmptyKey( sKey );
		return bExist;
	};

	// --= Integers =--

	inline void ReqInteger( const std::string& sKey, int& iValue, const CVALiterals<int>* pLiterals = nullptr ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );

		if( !pValue ) ErrorMissKey( sKey );

		if( pValue->IsInt() ) {
			iValue = *pValue;
			return;
		}

		if( pValue->IsString() ) {
			// Try to translate literals
			if( pLiterals && pLiterals->Translate( *pValue, iValue ) ) return;

			// Try to convert implicitly
			try {
				iValue = *pValue;
				return;
			}
			catch( ... ) {}
		}

		ErrorWrongType( sKey, "an integer number" );
	};

	inline bool OptInteger( const std::string& sKey, int& iValue, int iDefault, const CVALiterals<int>* pLiterals = nullptr ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );

		if( !pValue ) {
			iValue = iDefault;
			return false;
		}

		if( pValue->IsBool() )
		{
			iValue = *pValue ? 1 : 0;
			return true;
		}

		if( pValue->IsInt() ) {
			iValue = *pValue;
			return true;
		}

		if( pValue->IsString() ) {
			// Try to translate literals
			if( pLiterals && pLiterals->Translate( *pValue, iValue ) ) return true;

			// Try to convert implicitly
			try {
				iValue = *pValue;
				return true;
			}
			catch( ... ) {}
		}

		ErrorWrongType( sKey, "an integer number" );

		return false;
	};

	// --= Numbers (floating point) =--

	inline void ReqNumber( const std::string& sKey, double& dValue, const CVALiterals<double>* pLiterals = nullptr ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );

		if( !pValue ) ErrorMissKey( sKey );

		if( pValue->IsNumeric() ) {
			dValue = *pValue;
			return;
		}

		if( pValue->IsString() ) {
			// Try to translate literals
			if( pLiterals && pLiterals->Translate( *pValue, dValue ) ) return;

			// Try to convert implicitly
			try {
				dValue = *pValue;
				return;
			}
			catch( ... ) {}
		}

		ErrorWrongType( sKey, "a number" );
	};

	inline bool OptNumber( const std::string& sKey, double& dValue, double dDefault, const CVALiterals<double>* pLiterals = nullptr ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );

		if( !pValue ) {
			dValue = dDefault;
			return false;
		}

		if( pValue->IsNumeric() ) {
			dValue = *pValue;
			return true;
		}

		if( pValue->IsString() ) {
			// Try to translate literals
			if( pLiterals && pLiterals->Translate( *pValue, dValue ) ) return true;

			// Try to convert implicitly
			try {
				dValue = *pValue;
				return true;
			}
			catch( ... ) {}
		}

		ErrorWrongType( sKey, "a number" );

		return false;
	};

	// --= Strings =--

	// Gets a mandatory list of strings value. An exception is thrown if it does not exist 
	inline void ReqStringList( const std::string& sKey, std::vector< std::string >& vList, const std::string& sSeparator = "," ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );

		if( !pValue )
			ErrorMissKey( sKey );

		// Default case
		if( pValue->IsString() ) {
			std::string sValue = std::string( *pValue );
			vList = splitString( sValue, sSeparator );
			return;
		}

		// Allows implicit type conversions
		if( pValue->IsBool() || pValue->IsNumeric() )
		{
			std::string sValue = std::string( *pValue );
			vList.clear();
			vList.push_back( sValue );
			return;
		}

		ErrorWrongType( sKey, "a string" );
	};

	// Gets a mandatory list of strings value. An exception is thrown if it does not exist 
	inline void ReqStringListRegex( const std::string& sKey, std::vector< std::string >& vList, const std::string& sRegex ) const
	{
		const CVAStructValue* pValue = m_oStruct.GetValue( sKey );

		if( !pValue ) ErrorMissKey( sKey );

		// Default case
		if( pValue->IsString() )
		{
			std::string sValue = std::string( *pValue );
			regexSplitString( sValue, vList, sRegex );
			return;
		}

		// Allows implicit type conversions
		if( pValue->IsBool() || pValue->IsNumeric() )
		{
			std::string sValue = std::string( *pValue );
			vList.clear();
			vList.push_back( sValue );
			return;
		}

		ErrorWrongType( sKey, "a string" );
	};

private:
	const CVAStruct& m_oStruct;
	std::string m_sErrorPrefix;

	inline CVAConfigInterpreter& operator=( CVAConfigInterpreter& ) { return *this; };
};

#endif // IW_VACORE_UTILS

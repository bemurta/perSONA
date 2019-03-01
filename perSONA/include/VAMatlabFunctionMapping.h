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

#ifndef IW_FUNCTION_MAPPINGS
#define IW_FUNCTION_MAPPINGS

#include <assert.h>
#include <algorithm>
#include <map>
#include <string>
#include <vector>

#include <mex.h>

typedef void ( CommandFunction )( int, mxArray**, int, const mxArray** );

class FunctionMapping;
void AddFunctionMapping( FunctionMapping* pFuncMapping );

class FunctionInputArgument;
void AddFunctionInputArgument( const std::string& sFuncName, FunctionInputArgument* pArg );

class FunctionOutputArgument;
void AddFunctionOutputArgument( const std::string& sFuncName, FunctionOutputArgument* pArg );


class FunctionInputArgument
{
public:
	std::string sName;
	std::string sType;
	std::string sDesc;
	bool bOptional;
	std::string sDefaultValue;	// Default value expression in Matlab

	inline FunctionInputArgument( const std::string& sFunctionName, const std::string& sArgName, const std::string& sArgType, const std::string& sArgDesc, bool bArgOptional, const std::string& sArgDefaultValue )
		: sName( sArgName )
		, sType( sArgType )
		, sDesc( sArgDesc )
		, bOptional( bArgOptional )
		, sDefaultValue( sArgDefaultValue )
	{
		// Self register the argument globally
		AddFunctionInputArgument( sFunctionName, this );
	};
};

class FunctionOutputArgument
{
public:
	std::string sName;
	std::string sType;
	std::string sDesc;

	inline FunctionOutputArgument( const std::string& sFunctionName, const std::string& sArgName, const std::string& sArgType, const std::string& sArgDesc )
		: sName( sArgName )
		, sType( sArgType )
		, sDesc( sArgDesc )
	{
		// Self register the argument globally
		AddFunctionOutputArgument( sFunctionName, this );
	};
};

class FunctionMapping
{
public:
	std::string sName;
	CommandFunction* pAddr;
	bool bPublic;
	std::string sDesc;
	std::string sDoc;
	std::vector< FunctionInputArgument > vInputArgs;
	std::vector< FunctionOutputArgument > vOutputArgs;

	FunctionMapping( const std::string& sFunctionName, CommandFunction* pFunctionAddr, bool bPublicFunc, const std::string& sFunctionDesc, const std::string& sFunctionDoc )
		: sName( sFunctionName )
		, pAddr( pFunctionAddr )
		, bPublic( bPublicFunc )
		, sDesc( sFunctionDesc )
		, sDoc( sFunctionDoc )
	{
		// Self register the function globally
		AddFunctionMapping( this );
	};
};

// Function list (map used for O(log N) lookups via function name)
typedef std::map< std::string, FunctionMapping >::iterator FunctionMapIterator;
std::map< std::string, FunctionMapping > g_mFunctionMap;

void AddFunctionMapping( FunctionMapping* pFuncMapping )
{
	std::string sUpperName( pFuncMapping->sName );
	std::transform( sUpperName.begin(), sUpperName.end(), sUpperName.begin(), toupper );
	g_mFunctionMap.insert( std::pair< std::string, FunctionMapping >( sUpperName, *pFuncMapping ) );
};

void AddFunctionInputArgument( const std::string& sFuncName, FunctionInputArgument* pArg )
{
	std::string sUpperName( sFuncName );
	std::transform( sUpperName.begin(), sUpperName.end(), sUpperName.begin(), toupper );

	FunctionMapIterator it = g_mFunctionMap.find( sUpperName );
	assert( it != g_mFunctionMap.end() );
	it->second.vInputArgs.push_back( *pArg );
};

inline void AddFunctionOutputArgument( const std::string& sFuncName, FunctionOutputArgument* pArg )
{
	std::string sUpperName( sFuncName );
	std::transform( sUpperName.begin(), sUpperName.end(), sUpperName.begin(), toupper );

	FunctionMapIterator it = g_mFunctionMap.find( sUpperName );
	assert( it != g_mFunctionMap.end() );
	it->second.vOutputArgs.push_back( *pArg );
};

// INFO: Forward declare the function first. Allows to put documentation above the function body...
#define REGISTER_PUBLIC_FUNCTION(NAME, DESC, DOC) void NAME(int nlhs, mxArray *plhs[], int nrhs, const mxArray *prhs[]);\
FunctionMapping g_oFuncMapping_##NAME(#NAME, &NAME, true, DESC, DOC)

#define REGISTER_PRIVATE_FUNCTION(NAME) void NAME(int nlhs, mxArray *plhs[], int nrhs, const mxArray *prhs[]);\
FunctionMapping g_oFuncMapping_##NAME(#NAME, &NAME, false, "", "")

#define DECLARE_FUNCTION_REQUIRED_INARG(FUNCNAME, ARGNAME, ARGTYPE, ARGDESC) FunctionInputArgument g_oFuncInArg_##FUNCNAME##_##ARGNAME##(#FUNCNAME, #ARGNAME, ARGTYPE, ARGDESC, false, "")
#define DECLARE_FUNCTION_OPTIONAL_INARG(FUNCNAME, ARGNAME, ARGTYPE, ARGDESC, ARGDEF) FunctionInputArgument g_oFuncInArg_##FUNCNAME##_##ARGNAME##(#FUNCNAME, #ARGNAME, ARGTYPE, ARGDESC, true, ARGDEF)

#define DECLARE_FUNCTION_OUTARG(FUNCNAME, ARGNAME, ARGTYPE, ARGDESC) FunctionOutputArgument g_oFuncOutArg_##FUNCNAME##_##ARGNAME##(#FUNCNAME, #ARGNAME, ARGTYPE, ARGDESC)


inline mxArray* CreateFunctionInputArgumentStruct( std::vector<FunctionInputArgument>& vInputArgs )
{
	const mwSize nFields = 5;
	const char* ppszFieldNames[] = { "name", "type", "desc", "optional", "default" };

	mxArray* pStruct = mxCreateStructMatrix( 1, int( vInputArgs.size() ), nFields, ppszFieldNames );
	for( size_t i = 0; i < vInputArgs.size(); i++ ) {
		mxSetField( pStruct, ( mwIndex ) i, ppszFieldNames[ 0 ], mxCreateString( vInputArgs[ i ].sName.c_str() ) );
		mxSetField( pStruct, ( mwIndex ) i, ppszFieldNames[ 1 ], mxCreateString( vInputArgs[ i ].sType.c_str() ) );
		mxSetField( pStruct, ( mwIndex ) i, ppszFieldNames[ 2 ], mxCreateString( vInputArgs[ i ].sDesc.c_str() ) );
		mxSetField( pStruct, ( mwIndex ) i, ppszFieldNames[ 3 ], mxCreateLogicalScalar( vInputArgs[ i ].bOptional ) );
		mxSetField( pStruct, ( mwIndex ) i, ppszFieldNames[ 4 ], mxCreateString( vInputArgs[ i ].sDefaultValue.c_str() ) );
	}

	return pStruct;
};

inline mxArray* CreateFunctionOutputArgumentStruct( std::vector<FunctionOutputArgument>& vOutputArgs )
{
	const mwSize nFields = 3;
	const char* ppszFieldNames[] = { "name", "type", "desc" };

	mxArray* pStruct = mxCreateStructMatrix( 1, int( vOutputArgs.size() ), nFields, ppszFieldNames );
	for( size_t i = 0; i < vOutputArgs.size(); i++ ) {
		mxSetField( pStruct, ( mwIndex ) i, ppszFieldNames[ 0 ], mxCreateString( vOutputArgs[ i ].sName.c_str() ) );
		mxSetField( pStruct, ( mwIndex ) i, ppszFieldNames[ 1 ], mxCreateString( vOutputArgs[ i ].sType.c_str() ) );
		mxSetField( pStruct, ( mwIndex ) i, ppszFieldNames[ 2 ], mxCreateString( vOutputArgs[ i ].sDesc.c_str() ) );
	}

	return pStruct;
};

#endif IW_FUNCTION_MAPPINGS

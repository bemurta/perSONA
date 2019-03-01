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

#ifndef IW_VA_MATLAB_HELPERS
#define IW_VA_MATLAB_HELPERS

#include <VABase.h>
#include <VAStruct.h>

#include <string>
#include <vector>
#include <mex.h>

// Type checking functions
bool matlabIsScalar( const mxArray *A );
bool matlabIsVector( const mxArray *A );
bool matlabIsRowVector( const mxArray *A );
bool matlabIsColumnVector( const mxArray *A );

/*
 * [fwe] Validating getters
 *
 * These: Are provided a argument name and raise
 * a Matlab error themselves, if validation fails.
 */

// Boolean scalar
// No strict types: Can be provided int/float/double/string value as well {0,1}
bool matlabGetBoolScalar( const mxArray* arg, const char* argname );

// Integer scalar
// No strict types: Can be provided int/float/double value as well, if this is an integer number
int matlabGetIntegerScalar( const mxArray* arg, const char* argname );

// Real-valued scalar
double matlabGetRealScalar( const mxArray* arg, const char* argname );

// Real-valued 3-element vector (row|column)
void matlabGetRealVector3( const mxArray* arg, const char* argname, VAVec3& );

// Real-valued quaternion values as 1x4 vector
void matlabGetQuaternion( const mxArray* arg, const char* argname, VAQuat& );

// String
std::string matlabGetString( const mxArray* arg, const char* argname );

CVAStruct matlabGetStruct( const mxArray*, const char* );

/*
 *  Return value creation helpers
 */

mxArray* matlabCreateQuaternion( const VAQuat& );

// 3-Elemente Vektor erzeugen
mxArray* matlabCreateRealVector3( const VAVec3& v3Vec );

// Creates an ID in Matlab
mxArray* matlabCreateID( int iID );

// Creates an list (vector) of IDs in Matlab
mxArray* matlabCreateIDList( const std::vector<int>& viID );

// CVADirectivityInfo in Matlab struct konvertieren
mxArray* matlabCreateDirectivityInfo( const CVADirectivityInfo& di );

// CVASignalSourceInfo in Matlab struct konvertieren
mxArray* matlabCreateSignalSourceInfo( const CVASignalSourceInfo& ssi );

// CVASceneInfo in Matlab struct konvertieren
mxArray* matlabCreateSceneInfo( const CVASceneInfo& sci );

mxArray* matlabCreateStruct( const CVAStruct& oStruct );

#endif // IW_VA_MATLAB_HELPERS

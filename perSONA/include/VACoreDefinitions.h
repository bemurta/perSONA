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

#ifndef IW_VACORE_DEFINITIONS
#define IW_VACORE_DEFINITIONS

#include <string>
#include <vector>

#if ( defined WIN32 ) && !( ( defined VACORE_STATIC ) || ( defined VA_STATIC ) )
	#ifdef VACORE_EXPORTS
		#define VACORE_API __declspec( dllexport )
	#else
		#define VACORE_API __declspec( dllimport )
	#endif
#else
	#define VACORE_API
#endif

// Disable STL template-instantiiation warning with DLLs for Visual C++
#if defined( _MSC_VER )
#pragma warning( disable: 4251 )
#endif

#endif // IW_VACORE_DEFINITIONS

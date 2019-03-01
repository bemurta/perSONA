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

#ifndef IW_VACORE_DEBUG
#define IW_VACORE_DEBUG

// Debuggable Modules
enum
{
	VA_MODULE_CORE = 1,
	VA_MODULE_AUDIO_DRIVER_BACKEND = 2,
};

// TODO: Stream weiterleitung
void VA_DEBUG_PRINTF( const char * format, ... );
void VA_DEBUG_PRINTF( int module, int level, const char * format, ... );

#endif // IW_VACORE_DEBUG

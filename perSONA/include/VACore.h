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

/*!

@page VACore

@section core_intro_sec Introduction

VACore provides the entire functionality of real-time auralization and reproduction. It includes scene management and a lot of extra functionality to display and export audio.

@section core_getting_started_sec Getting started

If you are a C++ developer and you want to integrate VA into your application, the first contact point should be the interface represented by the #IVAInterface class in the VABase project.
If you are planning to create a new binding for any other programming and/or scripting language, also have a look at VANet and the #IVANetClient class.

@section core_developer_sec Developers

If you want to create your own auralization module, you will have to
 - implement a rendering module that is compatible with #IVAAudioRenderer
 - implement a reproduction module that is compatible with #IVAAudioReproduction

 Also, add a code line that registers your module at the corresponding registry.

*/

#ifndef IW_VA_CORE
#define IW_VA_CORE

#include "VACoreDefinitions.h"
#include "VACoreFactory.h"
#include "VANetworkStreamAudioSignalSource.h"
#include "VAObjectPool.h"
#include "VAPoolObject.h"
#include "VAReferenceableObject.h"
#include "VAUncopyable.h"

#endif // IW_VA_CORE

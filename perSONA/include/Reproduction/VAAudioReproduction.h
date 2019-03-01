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

#ifndef IW_VACORE_REPRODUCTIONMODULE
#define IW_VACORE_REPRODUCTIONMODULE

#include "../VAHardwareSetup.h"

#include <string>
#include <vector>

class CVACoreImpl;
class CVASceneState;
class CVAStruct;
class ITADatasource;

class CVAAudioReproductionInitParams
{
public:
	std::string sID;									//!< ID (right-hand side of declaration CLASS:ID)
	std::string sClass;									//!< Module class
	CVACoreImpl* pCore;									//!< Core configuration
	const CVAStruct* pConfig;							//!< Module configuration (direct link)
	std::vector<const CVAHardwareOutput*> vpOutputs;	//!< Designated hardware outputs
	bool bInputDetectorEnabled;
	bool bOutputDetectorEnabled;
	bool bRecordInputEnabled;
	bool bRecordOutputEnabled;
	std::string sRecordInputInputFilePath;
	std::string sRecordOutputInputFilePath;
};

/**
 * Diese rein abstrakte Klasse definiert die Schnittstelle für
 * die Wiedergabe binauraler Signale (z.B. Kopfhörer und virtueller Kopfhörer).
 *
 * Wiedergabe-Module bekommen als Eingabe eine Stereo-Audiosignalquelle
 * zugewiesen.
 */

//! Audio reproduction module interface
/**
  * The audio reproduction modules take a rendering stream as input
  * and reproduce the sound field (at least at the eardrums) for a
  * real world listener. They can react upon scene changes, i.e.
  * if the user moves in real world coordinates (in the lab).
  *
  */
class IVAAudioReproduction
{
public:
	virtual ~IVAAudioReproduction() {};

	virtual void SetInputDatasource( ITADatasource* pdsInput )=0;
	virtual ITADatasource* GetOutputDatasource()=0;
	virtual int GetNumInputChannels() const=0;

	//! Receive scene updates from core
	virtual void UpdateScene( CVASceneState* pNewState )=0;

	virtual inline void SetParameters( const CVAStruct& ) {};
	virtual inline CVAStruct GetParameters( const CVAStruct& ) const { return CVAStruct();  };

protected:
	IVAAudioReproduction() {};
};

#endif // IW_VACORE_AUDIO_REPRODUCTION

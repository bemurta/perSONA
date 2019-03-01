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

#ifndef IW_VACORE_VIRTUAL_AUDIO_DRIVER_BACKEND
#define IW_VACORE_VIRTUAL_AUDIO_DRIVER_BACKEND

#include "VAAudioDriverBackend.h"
#include "VAAudioDriverConfig.h"

#include <VAObject.h>

#include <ITAStreamProperties.h>
#include <ITADatasource.h>
#include <ITACriticalSection.h>
#include <ITAClock.h>
#include <ITAStreamInfo.h>

#pragma warning( disable : 4512 ) // yep no copy constructor

class CVAVirtualAudioDriverBackend : public IVAAudioDriverBackend, public CVAObject
{
public:
	CVAVirtualAudioDriverBackend( const CVAAudioDriverConfig* pConfig );
	~CVAVirtualAudioDriverBackend();

	std::string getDriverName() const;
	std::string getDeviceName() const;
	int getNumberOfInputs() const;
	const ITAStreamProperties* getOutputStreamProperties() const;
	void setOutputStreamDatasource( ITADatasource* pDatasource );
	ITADatasource* getInputStreamDatasource() const;

	void initialize();
	void finalize();
	void startStreaming();
	bool isStreaming();
	void stopStreaming();

	CVAStruct CallObject( const CVAStruct& oArgs );

	class ManualClock : public ITAClock, public CVAObject
	{
	public:
		ManualClock();
		~ManualClock();
		inline std::string getName() const { return "ManualClock"; };
		inline double getResolution() const { return -1.0f; };
		double getFrequency() const { return -1.0f; };
		inline double getTime();
		inline void SetTime( const double dManualNow );
		CVAStruct CallObject( const CVAStruct& oArgs );
	private:
		ITACriticalSection m_csTime;
		double m_dTime;
	};

private:
	const CVAAudioDriverConfig m_oConfig;
	ITAStreamProperties m_oOutputStreamProps;
	ITAStreamInfo m_oStreamInfo;
	ITADatasource* m_pDataSource;
	bool m_bStarted;
};

#endif // IW_VACORE_VIRTUAL_AUDIO_DRIVER_BACKEND

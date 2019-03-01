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

#ifndef IW_VACORE_SAMPLEANDHOLDMOTIONMODEL
#define IW_VACORE_SAMPLEANDHOLDMOTIONMODEL

#include "VAMotionModel.h"
#include "../Scene/VAMotionState.h"

#include <ITADataLog.h>

//! Implementierung eines Bewegungsmodells mit Sample-and-Hold Funktionalität
class CVASampleAndHoldMotionModel : public IVAMotionModel
{
public:
	inline CVASampleAndHoldMotionModel()
		: m_pLastState( nullptr )
		, m_dStartTime( 0 )
	{
		char buf1[ 255 ];
		sprintf( buf1, "SampleAndHoldMotionModel_Estimation_0x%08Xh.log", int( this ) );
		m_oEstimationDataLog.setOutputFile( buf1 );
		char buf2[ 255 ];
		sprintf( buf2, "SampleAndHoldMotionModel_Input_0x%08Xh.log", int( this ) );
		m_oInputDataLog.setOutputFile( buf2 );
	};

	inline ~CVASampleAndHoldMotionModel()
	{
		if( m_pLastState ) m_pLastState->RemoveReference();
	};

	inline void InputMotionKey( const CVAMotionState* pNewState )
	{
		if( m_dStartTime == 0 ) m_dStartTime = pNewState->GetModificationTime();
		if( m_pLastState ) m_pLastState->RemoveReference();
		m_pLastState = pNewState;
		if( pNewState ) pNewState->AddReference();
		MotionLogDataInput oLogItem;
		oLogItem.dTime = pNewState->GetModificationTime() - m_dStartTime; // Bessere Auflösung
		oLogItem.vPos = pNewState->GetPosition();
		m_oInputDataLog.log( oLogItem );
	};

	inline bool EstimatePosition( double dTime, VAVec3& vPos )
	{
		if( m_pLastState == nullptr )
			return false;

		vPos = m_pLastState->GetPosition();

		MotionLogDataOutput oLogItem;
		oLogItem.dTime = dTime - m_dStartTime;
		oLogItem.vPos = vPos;
		m_oEstimationDataLog.log( oLogItem );

		return true;
	};

	inline bool EstimateOrientation( double, VAVec3& vView, VAVec3& vUp )
	{
		if( m_pLastState )
		{
			vView = m_pLastState->GetView();
			vUp = m_pLastState->GetUp();
			return true;
		}
		return false;
	};

	inline bool EstimateOrientation( double, VAQuat& oOrient )
	{
		if( m_pLastState )
		{
			oOrient = m_pLastState->GetOrientation();
			return true;
		}
		return false;
	};

	inline void Reset()
	{
		m_oInputDataLog.clear();
		m_oEstimationDataLog.clear();
	};

private:
	const CVAMotionState* m_pLastState;
	double m_dStartTime;

	//! Implementierungsklasse für Logger-Datum
	class MotionLogDataOutput : ITALogDataBase
	{
	public:
		static std::ostream& outputDesc( std::ostream& os );
		std::ostream& outputData( std::ostream& os ) const;

		double dTime;
		VAVec3  vPos;
	};

	ITABufferedDataLogger<MotionLogDataOutput> m_oEstimationDataLog; //!< Logger Datum für VDL spezifische Prozess-Information

	//! Implementierungsklasse für Logger-Datum
	class MotionLogDataInput : ITALogDataBase
	{
	public:
		static std::ostream& outputDesc( std::ostream& os );
		std::ostream& outputData( std::ostream& os ) const;

		double dTime;
		VAVec3  vPos;
	};

	ITABufferedDataLogger<MotionLogDataInput> m_oInputDataLog; //!< Logger Datum für VDL spezifische Prozess-Information

};

#endif // IW_VACORE_SAMPLEANDHOLDMOTIONMODEL

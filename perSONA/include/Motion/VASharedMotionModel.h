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

#ifndef IW_VACORE_SHAREDMOTIONMODEL
#define IW_VACORE_SHAREDMOTIONMODEL

#include "VAMotionModel.h"
#include "../Scene/VAMotionState.h"

#include <tbb/concurrent_queue.h>

//! Bewegungsmodell Thread-safe Non-blocking
/**
  * Frontend zur Absicherung eines Bewegungsmodells bei Nutzung durch mehrere Threads
  * 1 Thread = Schreiber, 1 Thread = Leser
  */
class CVASharedMotionModel : IVAMotionModel
{
public:
	inline CVASharedMotionModel()
		: m_pImpl( nullptr )
		, m_bDeleteModel( false )
	{};

	inline CVASharedMotionModel( IVAMotionModel* pMotionModel, bool bDeleteModel )
		: m_pImpl( pMotionModel )
		, m_bDeleteModel( bDeleteModel )
	{};

	inline virtual ~CVASharedMotionModel()
	{
		if (m_bDeleteModel) delete m_pImpl;
	};

	// Verarbeiten/Speichern neuer Eingabewerte
	inline void InputMotionKey( const CVAMotionState* pNewState )
	{
		assert( pNewState != nullptr );
		pNewState->AddReference();
		m_qpNewStates.push(pNewState);
	};

	inline void HandleMotionKeys()
	{
		const CVAMotionState* pState;
		while (m_qpNewStates.try_pop(pState))
		{
			m_pImpl->InputMotionKey(pState);
			pState->RemoveReference();
		}
	};

	inline bool EstimatePosition( double dTime, VAVec3& vPos )
	{
		return m_pImpl->EstimatePosition( dTime, vPos );
	};

	inline bool EstimateOrientation( double dTime, VAVec3& vView, VAVec3& vUp )
	{
		return m_pImpl->EstimateOrientation( dTime, vView, vUp );
	};

	inline bool EstimateOrientation( double dTime, VAQuat& oOrient )
	{
		return m_pImpl->EstimateOrientation( dTime, oOrient );
	};

	inline IVAMotionModel* GetInstance()
	{
		return m_pImpl;
	};	

	inline void Reset()
	{
		m_pImpl->Reset();
	};

private:
	IVAMotionModel* m_pImpl; //!< Zeiger auf Implementierungsklasse des Bewegungsmodells
	bool m_bDeleteModel;
	tbb::concurrent_queue< const CVAMotionState* > m_qpNewStates;	//!< Übergabekanal für neue Bewegungsdaten
};

#endif // IW_VACORE_SHAREDMOTIONMODEL

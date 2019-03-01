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

#ifndef IW_VACORE_LOCKFREEOBJECTPOOL
#define IW_VACORE_LOCKFREEOBJECTPOOL

//#include <VACoreDefinitions.h>
#include <VAObjectPool.h>

#include <ITAAtomicPrimitives.h>

#include <tbb/concurrent_queue.h>

// ObjectPool class
class CVALockfreeObjectPool : public IVAObjectPool
{
public:
	// Dokumentation: Siehe CVAObjectPool in VABase. Identische Schnittstelle!

	CVALockfreeObjectPool( int iInitialSize, int iDelta, IVAPoolObjectFactory* pFactory, bool bDeleteFactory );
	virtual ~CVALockfreeObjectPool();

	void SetName( const std::string& sName );
	void Reset();

	int GetNumFree() const;
	int GetNumUsed() const;
	int GetSize() const;
	int Grow( const int iDelta );

	CVAPoolObject* RequestObject();

private:
	std::string m_sName;
	int m_iDelta;
	IVAPoolObjectFactory* m_pFactory;
	bool m_bDeleteFactory;

	tbb::concurrent_queue<CVAPoolObject*> m_qFree;  // Liste der freien Objekte
	tbb::concurrent_queue<CVAPoolObject*> m_qUsed;  // Liste der sich in Benutzung befindlichen Objekte
	ITAAtomicInt m_nFree; // Anzahl der freien, benutzen und gesamten Objekte
	ITAAtomicInt m_nUsed;
	ITAAtomicInt m_nTotal;

	void ReleaseObject( CVAPoolObject* pObject );

	friend class CVAPoolObject;
};

#endif // IW_VACORE_LOCKFREEOBJECTPOOL

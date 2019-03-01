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

#ifndef IW_VACORE_OBJECT_POOL
#define IW_VACORE_OBJECT_POOL

#include <VACoreDefinitions.h>

#include <VAUncopyable.h>

#include <string>

// Forward declarations
class CVAPoolObject;
class IVAPoolObjectFactory;


//! Object pool class
/**
  * This class realises pools for objects in VA (i.e. sound sources, listener, states, etc. ...)
  * A pool creates and manages a given number of referenceable objects. To the
  * user it provides an interface to request object slots that are not in use (not referenced).
  * If the object has no references it is automatically released to the pool for
  * reusage with a new object, if requested.
  *
  * Creation and deletion of objects is abstracted and is implemented in a way that
  * reusage is lightweight and effizient. Usage/Links of objects is realised by
  * reference counters. A release is triggert, if an object does not have any
  * remaining references.
  *
  * The pool creates object by using a factory that has to be provided. Therefore,
  * objects without a standard constructor can be used. Use the default factory
  * template class to create objects throughout it's standard constructor.
  *
  * @note Not thread safe, thread safety has to be provided by user
  */
class VACORE_API IVAObjectPool : public IVAUncopyable
{
public:
	//! Factory method
	/**
	  * @param[in] iInitialSize Initial size of pool / number of objects
	  * @param[in] iDelta Indicator for allocation/deallocation of objects
	  * @param[in] pFactory Factory method of pool objects
	  * @param[in] bDeleteFactory Set true if destructor available
	  *
	  * @return Pointer to the newly created pool of unused objects
	  */
	static IVAObjectPool* Create( const int iInitialSize, const int iDelta, IVAPoolObjectFactory* pFactory, const bool bDeleteFactory );

	//! Destructor
	virtual ~IVAObjectPool() {};

	//! Name setter
	/**
	  * Set name of the pool (for debugging purposes only)
	  *
	  * @param[in] sName Verbatim name
	  */
	virtual void SetName( const std::string& sName ) = 0;

	//! Reset
	/**
	  * Resets pool by quick reference removal of all objects. Does not
	  * change size of pool.
	  */
	virtual void Reset() = 0;

	//! Number of free object slots
	/**
	  * @return Number of free objects that can be requested
	  */
	virtual int GetNumFree() const = 0;

	//! Number of used object slots
	/**
	  * @return Number of used objects that can be requested
	  */
	virtual int GetNumUsed() const = 0;

	//! Pool size getter
	/**
	  * @return Number of available slots for objects
	  */
	virtual int GetSize() const = 0;

	//! Increase pool size
	/**
	  * @param[in] iDelta Number of slots to be allocated
	  *
	  * @return Returns number of created object slots (iDelta)
	  */
	virtual int Grow( const int iDelta ) = 0;

	//! Request a free object slot in pool
	/**
	  * Sets the refernce counter of the pool object to 1.
	  *
	  * @return Pointer to the new object
	  */
	virtual CVAPoolObject* RequestObject() = 0;

protected:
	//! Release-Hook (used by IVAPoolObject implementations)
	/**
	  * Releases object to the pool if no reference available anymore
	  * @param[in] pObject Object pointer
	  */
	virtual void ReleaseObject( CVAPoolObject* pObject ) = 0;

	// Access for CVAPoolObject to RemoveReference()
	friend class CVAPoolObject;
};

#endif // IW_VACORE_OBJECT_POOL

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

#ifndef IW_VA_POOL_OBJECT
#define IW_VA_POOL_OBJECT

#include <VACoreDefinitions.h>

#include <VAReferenceableObject.h>
#include <VAUncopyable.h>

class IVAObjectPool;

//! Pool objects base class
/**
  * Pool objects are referencable and linked to a superior pool. The
  * reference counter holds information on number of usages/links of
  * the specific object. In case the reference counter states that
  * no connections to the given object are available, it is marked
  * for new usage (recycling). The object releases itself to
  * the pool if no references are existent.
  */
class VACORE_API CVAPoolObject : public IVAUncopyable, public CVAReferenceableObject
{
public:
	//! Redefined method to remove reference
	/**
	  * Releases object to the pool if no reference available anymore
	  * @return Number of remaining refs
	  */
	virtual int RemoveReference() const;

protected:

	//! Protected constructor [Jonas fragt Frank: so richtig?! (hinzugefügt, damit m_pParentPool auf nullptr gesetzt werden kann]
	/**
	  * Can only be called by CVAObjectPool(), protected for users
	  */
	CVAPoolObject();

	//! Protected destructor
	/**
	  * Can only be called by CVAObjectPool(), protected for users
	  */
	virtual ~CVAPoolObject();

	//! Pre request method
	/**
	  * The PreRequest method is called before the object is used
	  */
	virtual inline void PreRequest() {};

	//! Pre release method
	/**
	  * The PreRelease method is called after the object has been released
	  */
	virtual inline void PreRelease() {};

private:
	IVAObjectPool* m_pParentPool; //!< Pointer to the pool

	friend class CVAObjectPool;
	friend class CVALockfreeObjectPool;
};


//! Pool object factory interface
/**
  * Interface for the creation of a pool object (factory method)
  */
class VACORE_API IVAPoolObjectFactory
{
public:
	//! Destructor
	inline virtual ~IVAPoolObjectFactory() {};

	//! Factory method (abstract)
	/**
	  * Interface method to create a new pool object
	  *
	  * @return Pointer to the new pool object
	  */
	virtual CVAPoolObject* CreatePoolObject() = 0;
};


//! Default factory for pool objects with standard constructor
/**
  * Pool object factory for objects using the default constructor
  */
template< class T > class CVAPoolObjectDefaultFactory : public IVAPoolObjectFactory
{
public:
	//! Factory method
	/**
	  * Creates new pool object using the standard constructor of the template object
	  *
	  * @return Pointer to the new pool object
	  */
	inline virtual CVAPoolObject* CreatePoolObject()
	{
		return new T;
	};
};

#endif // IW_VA_POOL_OBJECT

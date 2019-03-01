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

#ifndef IW_VACORE_REFERENCEABLE_OBJECT
#define IW_VACORE_REFERENCEABLE_OBJECT

#include <VACoreDefinitions.h>
#include <VAException.h>

#include <assert.h>

//! Interface for referenceable objects
/**
  * This interface class can be derived from to inherit
  * functionality that keeps track of every reference
  * an object may have.
  */
class VACORE_API IVAReferenceableObject
{
public:
	//! Destructor
	virtual inline ~IVAReferenceableObject() {};

	//! Returns true if there are references for this objects
	/**
	  * @return True, of at least one reference is set
	  */
	virtual bool HasReferences() const = 0;

	//! Number of references getter
	/**
	  * @return Number of references
	  */
	virtual int GetNumReferences() const = 0;

	//! Reset references
	/**
	  * Unclean reference reset, do not use if not explicitly required.
	  */
	virtual void ResetReferences() = 0;

	//! Adds reference
	/**
	  * @return Updated number of references
	  */
	virtual int AddReference() const = 0;
	
	//! Removes reference
	/**
	  * @return Updated number of references
	  */
	virtual int RemoveReference() const = 0;
};


//! Class for referenceable objects
/**
  * This interface class can be derived from to inherit
  * functionality that keeps track of every reference
  * an object may have. The reference functionality is
  * implemented using atomic reference counter.
  */
class VACORE_API CVAReferenceableObject :IVAReferenceableObject
{
public:
	//! Default constructor (zero references)
	CVAReferenceableObject();

	//! Destructor
	virtual ~CVAReferenceableObject();
	
	//! Returns true if there are references for this objects
	/**
	  * @return True, of at least one reference is set
	  */
	bool HasReferences() const;
	
	//! Number of references getter
	/**
	  * @return Number of references
	  */
	int GetNumReferences() const;
	
	//! Reset references
	/**
	  * Unclean reference reset, do not use if not explicitly required.
	  */
	virtual void ResetReferences();
	
	//! Adds reference
	/**
	  * @return Updated number of references
	  */
	virtual int AddReference() const;
	
	//! Removes reference
	/**
	  * @return Updated number of references
	  */
	virtual int RemoveReference() const;

private:
	mutable volatile int m_iRefCount; //!< Internal reference counter
};

//! Referenceable pointer type
/**
 * Pointer type for referenceable objects using and managing
 * reference counting. The template class has to be a subclass
 * of #CVAReferenceableObject.
 */
template< class T >
class VACORE_API CVARefPtr
{
public:
	//! Default constructor (No reference, nullptr pointer)
	inline CVARefPtr()
		: m_ptr( nullptr )
	{
	};

	//! Initialisation constructor, sets reference to the passed object pointer
	/**
	  * @param[in] ptr Object pointer
	  */
	inline CVARefPtr( T* ptr )
	{
		*this = ptr;
	};

	//! Copy constructor, sets an autonomouse reference for the copy
	/**
	  * @param[in] ref Object to copy from
	  */
	inline CVARefPtr( const CVARefPtr& ref )
	{
		*this = ref;
	};

	//! Destructor removing reference of target objects, if present
	inline ~CVARefPtr()
	{
		if( m_ptr )
			m_ptr->RemoveReference();
	};

	//! Remove reference
	/**
	  * @note Uses assertion in debug mode that reference counter is greater then 0
	  */
	inline void Clear()
	{
		if( m_ptr )
			m_ptr->RemoveReference();
		m_ptr = nullptr;
	};

	//! Assignment, creates an autonomous reference count
	/**
	  * @param[in] rhs Right hand side reference pointer object
	  * @return Reference to object that has been assigned values
	  */
	inline CVARefPtr& operator=( const CVARefPtr& rhs )
	{
		// Remove currrent reference
		if( m_ptr )
			m_ptr->RemoveReference();

		// Create new reference (note: first reference, then assignement)
		if( rhs.m_ptr )
			rhs.m_ptr->AddReference();
		m_ptr = rhs.m_ptr;

		return *this;
	};

	//! Assignment, creates an autonomous reference count
	/**
	  * @param[in] ptr Right hand side reference pointer object
	  * @return Reference to object that has been assigned values
	  */
	inline CVARefPtr& operator=( T* ptr )
	{
		// Remove currrent reference
		if( m_ptr )
			m_ptr->RemoveReference();

		// Create new reference (note: first reference, then assignement)
		if( ptr )
			ptr->AddReference();
		m_ptr = ptr;

		return *this;
	};

	//! Cast assignment to pointer type
	inline operator T*( ) const
	{
		return m_ptr;
	};

	//! Dereferenciation
	inline T& operator*( ) const
	{
		assert( m_ptr );
		if( !m_ptr )
			VA_EXCEPT1( "Attempt to dereference nullpointer" );
		return m_ptr;
	};

	//! Member dereferenciation
	inline T* operator->( ) const
	{
		assert( m_ptr );
		if( !m_ptr )
			VA_EXCEPT1( "Attempt to dereference nullpointer" );
		return m_ptr;
	};

private:
	T* m_ptr; //!< References object pointer
};

#endif // IW_VACORE_REFERENCEABLE_OBJECT

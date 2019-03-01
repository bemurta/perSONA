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

#ifndef IW_VA_CONTAINER_STATE
#define IW_VA_CONTAINER_STATE

#include "VASceneStateBase.h"

#include <list>
#include <set>
#include <vector>

//! Data class with differences between containers
/**
  * View: difference between sets A and B is defined as differences in B, e.g. deletes objects
  *       that are not available anymore in _B_.
  */
class CVAContainerDiff
{
public:
	std::vector< int > liNew;	//!< New elements (in B but not in A)
	std::vector< int > liDel;	//!< Removed elements (in A but not in B)
	std::vector< int > liCom;	//!< Mutual elements (in A and in B)
};


//! Base implementation of scene state
/**
  * This class describes a dynamic versioned list of objects handled by the element's IDs.
  * @note Set operations are implemented in an efficient way.
  */
class CVAContainerState : public CVASceneStateBase
{
public:
	//! Initialize to a non-finalized base state
	/**
	  * Sets reference counter to zero and allows for modifications until finalize is called.
	  *
	  * @param[in] dModificationTime Modification time stamp
	  */
	void Initialize( const double dModificationTime );

	//! Incorporate entire data from another state (make a copy)
	/**
	  * @param[in] pSrc Source container state (copy from)
	  * @param[in] dModificationTime Modification time stamp
	  */
	void Copy( const CVAContainerState* pSrc, const double dModificationTime );

	//! Fixate state and all inherited objects
	void Fix();

	//! Determine differences between this object and another container state
	/**
	  * @note: Objects that are included and not found in compare container state are marked _new_.
	  *
	  * @param[in] pComp Compare container, on which the diff is used (i.e. _new_ are new object for comparator)
	  * @param[out] viNew _New_ objects not include in comparator
	  * @param[out] viDel _Old_ objects only included in comparator
	  * @param[out] viCom _Modified_ objects found in both states with different data
	  */
	void Diff( const CVAContainerState* pComp, std::vector< int >& viNew, std::vector< int >& viDel, std::vector< int >& viCom ) const;

	//! Returns number of objects with individual IDs
	/**
	  * @return Number of objects
	  */
	int GetSize() const;

	//! Get IDs ov all objects as vector
	/**
	  * @param[out] pviDest Pointer to ID vector
	  */
	void GetIDs( std::vector< int >* pviDest ) const;

	//! Get IDs ov all objects as list
	/**
	  * @param[out] pviDest Pointer to ID list
	  */
	void GetIDs( std::list< int >* pliDest ) const;

	//! Get IDs ov all objects as set
	/**
	  * @param[out] pviDest Pointer to ID set
	  */
	void GetIDs( std::set< int >* psiDest ) const;

	//! Checks if an object is included
	bool HasObject( const int iID ) const;

	//! Pointer to object getter
	CVASceneStateBase* GetObject( const int iID ) const;

	//! Add an object by known ID
	void AddObject( const int iID, CVASceneStateBase* pObject );

	//! Remove an object of known ID
	void RemoveObject( const int iID );

	//! Set an object by known ID
	void SetObject( const int iID, CVASceneStateBase* pObject );

protected:
	//! Pool release
	void PreRelease();

public:
	//! Element data type (ID -> instance)
	typedef std::pair< int, CVASceneStateBase* > ElemType;

	//! Comperator
	struct ElemCompare
	{
		inline bool operator()( const ElemType& lhs, const ElemType& rhs ) const
		{
			return lhs.first < rhs.first;
		};
	};

#ifdef VA_CONTAINER_STATE_USE_STD_SET_ALGORITHM
	struct
	{
		std::set< int > sElements;
	} data;
#else
	// Implementation using arrays
	struct
	{
		mutable std::vector< ElemType > vElements;
		mutable bool bDirty;
	} data;
#endif
};

#endif // VA_CONTAINER_STATE

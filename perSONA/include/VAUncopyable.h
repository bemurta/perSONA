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

#ifndef IW_VACORE_UNCOPYABLE
#define IW_VACORE_UNCOPYABLE

#include <VACoreDefinitions.h>

// Idea: Scott Meyers

//! Functionality to suppress copying an object
/**
  * Derive your class from VAUncopyable to block any copy instruction.
  */
class VACORE_API IVAUncopyable
{
protected:
	//! For subclasses: standard constructor available
	inline IVAUncopyable() {};

	//! For subclasses: destructor available
	inline virtual ~IVAUncopyable() {};

private:
	//! Forbidden for everyone: copy constructor
	/**
	  * @param[in] oObj Uncopyable object
	  */
	inline IVAUncopyable( const IVAUncopyable& oObj ) { oObj; };

	//! Forbidden for everyone: assignment operator
	/**
	  * @param[in] oObj Unassignable object
	  * @return Uncopyable object
	  */
	IVAUncopyable& operator=( const IVAUncopyable& pObj );
};

#endif // IW_VACORE_UNCOPYABLE

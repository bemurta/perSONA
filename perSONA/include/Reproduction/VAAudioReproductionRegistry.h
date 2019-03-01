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

#ifndef IW_VACORE_AUDIOREPRODUCTIONMODULEREGISTRY
#define IW_VACORE_AUDIOREPRODUCTIONMODULEREGISTRY

#include "VAAudioReproduction.h"

#include <cassert>
#include <vector>
#include <VAStruct.h>

class CVACoreImpl;
class IVAAudioReproduction;
class IVAAudioReproductionFactory;

//! Factory method interface
class IVAAudioReproductionFactory
{
public:
	virtual std::string GetClassIdentifier() const = 0;
	virtual IVAAudioReproduction* Create( const CVAAudioReproductionInitParams& oParams ) = 0;
};

//! Default factory (template)
template < class TReproduction >
class CVAAudioReproductionModuleDefaultFactory : public IVAAudioReproductionFactory
{
public:
	inline CVAAudioReproductionModuleDefaultFactory( const std::string& sClassIdentifier )
		: m_sClassIdentifier( sClassIdentifier )
	{
	};

	inline std::string GetClassIdentifier() const
	{
		return this->m_sClassIdentifier;
	};

	inline IVAAudioReproduction* Create( const CVAAudioReproductionInitParams& oParams )
	{
		return new TReproduction( oParams );
	};

private:
	std::string m_sClassIdentifier;
};

//! Module registry (singleton)
class CVAAudioReproductionRegistry
{
public:
	static CVAAudioReproductionRegistry* GetInstance();

	CVAAudioReproductionRegistry();
	inline virtual ~CVAAudioReproductionRegistry() {};

	template< typename TReproduction >
	inline void RegisterReproductionDefaultFactory( const std::string& sClassIdentifier )
	{
		this->RegisterFactory( new CVAAudioReproductionModuleDefaultFactory< TReproduction >( sClassIdentifier ) );
	};

	void RegisterFactory( IVAAudioReproductionFactory* pFactory );
	IVAAudioReproductionFactory* FindFactory( const std::string& sClassIdentifier );

protected:
	//! Registers core-internal audio reproduction factories (only call once)
	/**
	* If you are writing your own reproduction, add your class into
	* this method implementation. If you are using a plugin, registration will
	* be done automatically.
	*/
	void RegisterInternalCoreFactoryMethods();
	friend class CVACoreImpl;

private:
	std::vector< IVAAudioReproductionFactory* > m_pFactories;
	bool m_bInternalCoreFactoriesRegistered;
};


#endif // IW_VACORE_AUDIOREPRODUCTIONMODULEREGISTRY

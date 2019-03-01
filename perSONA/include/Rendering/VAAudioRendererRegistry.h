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

#ifndef IW_VACORE_AUDIORENDERERREGISTRY
#define IW_VACORE_AUDIORENDERERREGISTRY

#include "VAAudioRenderer.h"

#include <vector>

class IVAAudioRenderer;
class IVAAudioRendererFactory;

//! Audio renderer registry (singleton)
class CVAAudioRendererRegistry
{
public:
	
	template< typename TRenderer > 
	inline void RegisterRendererDefaultFactory( const std::string& sClassIdentifier )
	{
		this->RegisterFactory( new CVAAudioRendererDefaultFactory< TRenderer >( sClassIdentifier ) );
	};

	static CVAAudioRendererRegistry* GetInstance();

	inline CVAAudioRendererRegistry() : m_bInternalCoreFactoriesRegistered( false ) {};
	inline virtual ~CVAAudioRendererRegistry() {};

	void RegisterFactory( IVAAudioRendererFactory* pFactory );
	IVAAudioRendererFactory* FindFactory( const std::string& sClassName );

protected:
	//! Registers core-internal audio rendering factories (only call once)
	/**
	  * If you are writing your own renderer, add your class into
	  * this method implementation. If you are using a plugin, registration will
	  * be done automatically.
	  */
	void RegisterInternalCoreFactoryMethods();
	friend class CVACoreImpl;

private:
	std::vector< IVAAudioRendererFactory* > m_pFactories;
	bool m_bInternalCoreFactoriesRegistered;
};

#endif // IW_VACORE_AUDIORENDERERREGISTRY

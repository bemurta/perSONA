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

#ifndef IW_VA_AUDIO_RENDERER
#define IW_VA_AUDIO_RENDERER

// Forward declarations
class IVADirectivity;
class IVADirectivity;
class ITADatasource;
class CVACoreImpl;
class CVASceneState;

#include <VA.h>

#include <string>
#include <vector>

//! Audio renderer initialization parameters
// Note: The core provides these parameters to the constructor of an audio renderer
class CVAAudioRendererInitParams
{
public:
	std::string sID;						//!< ID (right-hand side of declaration CLASS:ID)
	std::string sClass;						//!< Renderer class
	CVACoreImpl* pCore;						//!< Parent core
	const CVAStruct* pConfig;				//!< Renderer configuration
	std::vector< std::string > vsReproductions;	//!< Renderer outputs (e.g. Output:HP, Reproduction:CTC4)
	bool bRecordOutputEnabled;				//!< Renderer output recording and storing flag
	std::string sRecordOutputFilePath;		//!< Renderer output recording and storing file path
	bool bOutputLevelMeterEnabled; 			//!< Renderer output level meter will be used (uses a little bit CPU resources)
	bool bOfflineRendering;  				//!< Offline rendering indicator  (using a virtual audio device and external trigger)
};

//! Audio renderer interface
/**
 * ...
 */
class IVAAudioRenderer
{
public:
	virtual inline ~IVAAudioRenderer() {};

	virtual void Reset() = 0;

	// --= Resource hooks =--

	virtual void LoadScene( const std::string& sFileName ) = 0;

	virtual inline void PostLoadDirectivity( const IVADirectivity* ) {};
	virtual inline void PreFreeDirectivity( const IVADirectivity* ) {};

	virtual inline void PostLoadHRIRDataset( const IVADirectivity* ) {};
	virtual inline void PreFreeHRIRDataset( const IVADirectivity* ) {};

	virtual void UpdateScene( CVASceneState* pNewSceneState ) = 0;

	// @todo refactor.
	virtual void UpdateGlobalAuralizationMode( int iGlobalAuralisationMode ) = 0;

	virtual inline int GetAuralizationMode() const { return IVAInterface::VA_AURAMODE_ALL; };
	virtual inline void SetAuralizationMode( const int ) {};

	virtual inline void SetParameters( const CVAStruct& ) {};
	virtual inline CVAStruct GetParameters( const CVAStruct& ) const { return CVAStruct(); };

	virtual ITADatasource* GetOutputDatasource() = 0;
};


//! Audio renderer factory method interface
class IVAAudioRendererFactory
{
public:
	virtual inline ~IVAAudioRendererFactory() {};

	// Return the class name
	virtual std::string GetClassIdentifier() const = 0;

	// Factory method
	virtual IVAAudioRenderer* Create( const CVAAudioRendererInitParams& oParams ) = 0;
};


//! Audio renderer default factory (template)
template< class TRenderer >
class CVAAudioRendererDefaultFactory : public IVAAudioRendererFactory
{
public:
	inline CVAAudioRendererDefaultFactory( const std::string& sClassName )
		: m_sClassIdentifier( sClassName )
	{
		// drausch: no ctor side effects like registration on singleton
		//CVAAudioRendererRegistry::GetInstance()->RegisterFactory( this );
	};

	inline std::string GetClassIdentifier() const
	{
		return m_sClassIdentifier;
	};

	inline IVAAudioRenderer* Create( const CVAAudioRendererInitParams& oParams )
	{
		return new TRenderer( oParams );
	};

private:
	std::string m_sClassIdentifier;
};

#endif // IW_VA_AUDIO_RENDERER

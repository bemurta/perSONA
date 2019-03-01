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

#ifndef IW_VACORE_VBAPFREEFIELDAUDIORENDERER
#define IW_VACORE_VBAPFREEFIELDAUDIORENDERER

#ifdef VACORE_WITH_RENDERER_VBAP_FREE_FIELD

// VA includes
#include "../../../Motion/VASampleAndHoldMotionModel.h"
#include "../../../Motion/VASharedMotionModel.h"
#include "../../../Rendering/VAAudioRenderer.h"
#include "../../../Rendering/VAAudioRendererRegistry.h"
#include "../../../Scene/VAScene.h"
#include <VABase.h>
#include <VAInterface.h>
#include <VAObjectPool.h>
#include "../../../VASourceListenerMetrics.h"
#include "../../../VAHardwareSetup.h"

// ITA includes
#include <ITADataSourceRealization.h>
#include <ITASampleBuffer.h>

// Vista includes
#include <VistaBase/VistaVector3D.h>
#include <VistaMath/VistaGeometries.h>

// STL Includes
#include <list>
#include <set>

// Externe Vorwärtsdeklarationen
class CVACoreImpl;
class CVAVBAPFreeFieldAudioRendererConfig;

//! Vector-Base Amplitude Panning Audio Renderer
/**
  * Verwaltet Schallausbreitungspfade mit VBAP Synthese
  * für beliebige Lautsprecheranordnungen
  *
  * Implementiert:
  *
  * - Gain-Verteilung der Lautsprecher
  * - 1/R-Gesetz
  *
  */
class CVAVBAPFreeFieldAudioRenderer : public IVAAudioRenderer, ITADatasourceRealizationEventHandler
{
public:
	CVAVBAPFreeFieldAudioRenderer( const CVAAudioRendererInitParams& oParams );
	virtual ~CVAVBAPFreeFieldAudioRenderer();

	void Init( const CVAStruct& oArgs );

	void Reset();
	
	void LoadScene( const std::string& sFilename );
	
	//! Handle a scene state change
	/**
	  * This method updates the internal representation of the VA Scene
	  * by setting up or deleting the sound path entities as well as
	  * modifying existing ones that have changed their state, i.e.
	  * pose or dataset
	  */
	void UpdateScene( CVASceneState* pNewSceneState );

	//! Handle a state change in global auralisation mode
	/**
	  * This method updates internal settings for the global auralisation
	  * mode affecting the activation/deactivation of certain components
	  * of the sound path entities
	  */
	void UpdateGlobalAuralizationMode( int iGlobalAuralizationMode );

	void HandleProcessStream( ITADatasourceRealization* pSender, const ITAStreamInfo* pStreamInfo );
	
	ITADatasource* GetOutputDatasource();


private:
	
	const CVAAudioRendererInitParams m_oParams;		//!< Create a const copy of the init params

	VAVec3 m_vUserPosVirtualScene;				//!< Position des Hoerers in der virtuellen Umgebung 
	VAVec3 m_vUserPosRealWorld;					//!< Position des Hörers in der CAVE (oder im Reproduktionssystem)
	VAVec3 m_vReproSystemVirtualPosition;		//!< Position der CAVE (oder des Reproduktionssystems) in der virtuellen Welt. Center position of loudspeaker array.
	//VAOrientYPR m_oUserYPRRealWorldRAD;			//!< Orientierung des Hoerers in der Cave (im Reproduktionssystem), Gieren, Nicken, Rollen
	//VAOrientYPR	m_oUserYPRVirtualScene;	//!< Orientierung des Hoerers in der virtuellen Umgebung, Gieren, Nicken, Rollen
	VAVec3 m_vRotYaw, m_vRotPitch, m_vRotRoll;	//!< Hilfsvektoren zum rotieren, um diese Achsen wird rotiert
	
	enum DIMENSIONS
	{
		DIM_1D = 1, //!< One-dimensional (loudspeaker on a line, curve)
		DIM_2D = 2,	//!< Two-dimensional (loudspeaker in a circle, closed curve, square)
		DIM_3D = 3, //!< Three-dimensional (loudspeaker on tetrahedron, sphere, cube)
	};

	// Loudspeaker
	class CLoudspeaker
	{
	public:
		std::string sIdentifier;
		int iIdentifier;
		int iChannel;
		VAVec3 pos;
	};

	// Sections
	class CSection
	{
	public:
		int iIdentifier;
		std::vector <int> iLSIdentifier;
		VistaPolygon Polygon;
	};

	class SoundPath
	{
	public:
		SoundPath( const unsigned int iNumChannels )
		: m_vdNewGains( iNumChannels )
		, m_vdOldGains( iNumChannels )
		, bMarkDeleted( false )
		, iSourceID( -1 )
		{};
		std::vector< double > m_vdNewGains; //!< New gains for current process block
		std::vector< double > m_vdOldGains; //!< Old gains from prior process block (for crossfade)
		int iSourceID; //!< Source identifier
		ITAAtomicBool bMarkDeleted;
	};

	std::list< SoundPath > m_lSoundPaths; //!< List of active sound paths
	std::list< SoundPath > m_lNewSoundPaths; //!< List of added sound paths
	
	CVASceneState* m_pCurSceneState;		//!< Zeiger auf aktuell benutzte Scene im Renderer
	CVASceneState* m_pNewSceneState;		//!< Zeiger auf neue Scene für den Renderer
	bool m_bResetTrigger;					//!< Bool die eins wird falls ein Reset stattfindet
	int m_iCurGlobalAuralizationMode;		//!< Der aktuelle Auralisierungsmodues, mit dem der Renderer arbeitet
	
	VAVec3 m_vecCaveCenterPos;			//!< Mittelpunkt der CAVE Umgebung relativ zu Szene
	VAVec3 m_vecCaveCenterOrientYPR;		//!< Mittelpunkt der CAVE Umgebung relativ zu Szene

	int m_iSetupDimension;						//!< Dimension of setup, see \DIMENSIONS

	const CVAHardwareOutput* m_pOutput;		//!< Hardware output group with information on devices
	std::vector< CSection > m_voSectionList;		//!< List of all sections for the VBAP setup (DIM_3D only)
	std::vector< CLoudspeaker > m_voLoudspeaker;	//!< List of all loudspeaker for the VBAP setup
	
	ITADatasourceRealization* m_pdsOutput; //!< Output datasource
	


	//! Checks if a direction (from center point of the VBAP loudspeaker setup) is within a defined section
	//bool IsSourceDirectionWithinSection( const RG_Vector& vDirection, unsigned int uiCurrentSectionIdentifier) const;
	bool IsSourceDirectionWithinSection( const VAVec3& vDirection, const CSection& oSection ) const;
	

	//! Calculates loudspeaker gains for a 3D loudspeaker set-up
	/**
	  * Calculates loudspeaker gains using members of the class
	  *
	  * \param vSoundSource	Position of sound source
	  * \param vdLoudspeakerGains Call-by-reference loudspeaker gains, will be calculated
	  *
	  * \return False, if calculation was not possible
	  */
	bool CalculateLoudspeakerGains3D( const VAVec3& vSoundSource, std::vector< double >& vdLoudpeakerGains) const;
	
	
	//! Calculates loudspeaker gains for a 2D loudspeaker set-up
	/**
	  * Calculates loudspeaker gains using members of the class
	  *
	  * \param vSoundSource	Position of sound source
	  * \param vdLoudspeakerGains Call-by-reference loudspeaker gains, will be calculated
	  *
	  * \return False, if calculation was not possible
	  * 
	  */
	bool CalculateLoudspeakerGains2D( const VAVec3& vSoundSource, std::vector< double >& vdLoudpeakerGains) const;


	//! Helper function to retrieve real world position of a loudspeaker by identifier string
	/**
	  * \sIdentifier	Loudspeaker identifier string (has to match with hardware setup from VACore)
	  * \pSetup			VA core hardware setup pointer
	  *
	  * \return RG_Vector of loudspeaker position, zero vector if not found
	  */
	//RG_Vector GetLSPosition( const int iIdentifier, const CVAHardwareSetup* pSetup ) const;
	
	//! Helper function to retrieve channel number of a loudspeaker by identifier string
	/**
	  * \sIdentifier	Loudspeaker identifier string (has to match with hardware setup from VACore)
	  * \pSetup			VA core hardware setup pointer
	  *
	  * \return channel of loudspeaker in HardwareSetup, minus one if not found
	  */
	//int GetLSChannel( const int iIdentifier, const CVAHardwareSetup* pSetup ) const;


	//! Calculate the inverse for a three dimensional matrix
	/**
	  * \dOriginalMatrix	Original matrix array
	  * \dInverseMatrix		Target for inverse matrix
	  */
	void CalculateInverseMatrix3x3( const double *dOriginalMatrix, double *dInverseMatrix ) const;


	//! Calculate source position in relation to virtual position of CAVE
	/**
	  */
	VAVec3 GetSourcePosition( const CVAMotionState* pMotionState );


	//! Manage sound pathes
	/**
	  *
	  */
	void ManageSoundPaths( const CVASceneStateDiff* pDiff );


	void SyncInternalData();

};

#endif // VACORE_WITH_RENDERER_VBAP_FREE_FIELD

#endif // IW_VACORE_VBAPFREEFIELDAUDIORENDERER

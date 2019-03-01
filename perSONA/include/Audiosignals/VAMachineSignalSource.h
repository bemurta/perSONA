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

#ifndef IW_VACORE_MACHINE_SIGNAL_SOURCE
#define IW_VACORE_MACHINE_SIGNAL_SOURCE

#include <VAAudioSignalSource.h>
#include <VAObject.h>

#include <ITADataSourceRealization.h>
#include <ITASampleBuffer.h>
#include <ITAAtomicPrimitives.h>

class CVACoreImpl;
class IITASampleInterpolationRoutine;

/** Machine signal source
  * 
  * The machine signal source realizes a state machine,
  * that has a starting sound, an (optional) idle sound that can
  * be changed in speed (resulting into a pitch shift),
  * and a stopping sound. Transitions can be
  * triggered by actions i.e. the "start" action, see \MachineTransition.
  */

class CVAMachineSignalSource : public IVAAudioSignalSource, public ITADatasourceRealization
{
public:
	enum MachineState
	{
		MACHINE_STATE_INVALID	= 0,	//!< Machine has an invalid/undefined state
		MACHINE_STATE_STARTING,		//!< Machine ist starting (running up)
		MACHINE_STATE_IDLING,			//!< Machine is idling (at given pitch)
		MACHINE_STATE_HALTING,		//!< Machine is halting
		MACHINE_STATE_STOPPED,		//!< Machine is stopped
	};

	enum MachineTransition
	{
		MACHINE_TRANSITION_RESET = 0,	//!< Reset the machine
		MACHINE_TRANSITION_NONE,		//!< No action
		MACHINE_TRANSITION_START,		//!< Start the machine
		MACHINE_TRANSITION_SET_SPEED,	//!< Set idle speed of machine
		MACHINE_TRANSITION_HALT,		//!< Halt/stop machine
	};


	class Config
	{
	public:
		CVACoreImpl* pCore;
		bool bRequiresStartSound;
		bool bRequiresIdleSound;
		bool bRequiresStopSound;
		bool bCrossfadeSounds;			//!< Enabled crossfading of sound samples (may introduce comb filter artifacts)
		double dMaximumSpeed;			//!< Max allowed speed
		bool bDisableInterpolation;		//!< For more performance, interpolation may be disabled (no speed manipulation)
		bool bUseSplineInterpolation;	//!< If false, fallback to linear interpolation (fast)

		Config( CVACoreImpl* pCore ) : pCore( pCore )
		{
			SetDefaults();
		};

		virtual void SetDefaults()
		{
			bCrossfadeSounds = bRequiresStartSound = bRequiresIdleSound = bRequiresStopSound = true;
			dMaximumSpeed = 10.0f;
			bDisableInterpolation = false;
			bUseSplineInterpolation = false;
		};

	private:
		Config();
	};

	CVAMachineSignalSource( const Config& );
	virtual ~CVAMachineSignalSource();
		
	int GetType() const;
	std::string GetTypeString() const;
	std::string GetDesc() const;
	IVAInterface* GetAssociatedCore() const;
	const float* GetStreamBlock( const CVAAudiostreamState* );
	void HandleRegistration( IVAInterface* );
	void HandleUnregistration( IVAInterface* );
	std::string GetStateString() const;
	void SetParameters( const CVAStruct& );
	CVAStruct GetParameters( const CVAStruct& ) const;

	//! Initiate state transition
	/**
	  * \return True, if state transition was possible
	  */
	bool SetAction( MachineTransition );

	//! State getter
	MachineState GetState() const;

	//! Reset the machine
	void Reset();

	//! Speed setter (0..+inf)
	void SetSpeed( double );

	//! Speed getter (0..+inf)
	double GetSpeed() const;

	//! Load the start sound from file
	void SetStartSound( const std::string& sFilePath );

	//! Load the idle sound from file
	void SetIdleSound( const std::string& sFilePath );

	//! Load the stop sound from file
	void SetStopSound( const std::string& sFilePath );

	//! Control, if crossfading between samples is activated
	void SetCrossfadeSoundsEnabled( bool bEnabled );

	//! Crossfade sound flag getter
	bool GetCrossfadeSoundsEnabled() const;

	//! Start up machine
	void Start();

	//! Halt machine
	void Halt();

private:

	IVAInterface* m_pAssociatedCore;
	Config m_oConfig;
	ITASampleBuffer m_sbStartSound, m_sbStartSoundNew;
	ITASampleBuffer m_sbIdleSound, m_sbIdleSoundNew;
	ITASampleBuffer m_sbStopSound, m_sbStopSoundNew;
	ITASampleBuffer m_sbInterpolationSrc;
	ITAAtomicBool m_bStartSoundNew, m_bIdleSoundNew, m_bStopSoundNew;
	ITASampleBuffer m_sbOut;
	ITAAtomicInt m_iCurrentState;
	ITAAtomicInt m_iNewTransition;
	ITAAtomicFloat m_fMachineSpeed;
	ITAAtomicBool m_bHasStartSound;
	ITAAtomicBool m_bHasIdleSound;
	ITAAtomicBool m_bHasStopSound;
	ITAAtomicBool m_bCrossfadeSounds;

	int m_iStartingSoundCursor;
	int m_iIdlingSoundCursor;
	int m_iStoppingSoundCursor;

	IITASampleInterpolationRoutine* m_pInterpRoutine;

	std::string GetMachineStateString( MachineState ) const;
	void AddSamplesStarting( ITASampleBuffer&, int&, bool& );
	void AddSamplesIdling( ITASampleBuffer&, int iOutputOffset=0 );
	void AddSamplesStopping( ITASampleBuffer&, int&, bool& );
	void ResetReadCursors();
};

#endif // IW_VACORE_MACHINE_SIGNAL_SOURCE

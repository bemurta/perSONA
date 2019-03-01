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

#ifndef IW_VA_TEXT_TO_SPEECH_SIGNAL_SOURCE
#define IW_VA_TEXT_TO_SPEECH_SIGNAL_SOURCE

#include "VAAudiofileSignalSource.h"

#include <VAAudioSignalSource.h>
#include <VAObject.h>

#include <ITADataSourceRealization.h>
#include <ITASampleBuffer.h>
#include <ITAAudioSample.h>
#include <ITAAtomicPrimitives.h>

#include <unordered_set>

class ITABufferDatasource;
class CVACoreImpl;
class CPRCEN_engine;
class CPRC_abuf;


/** Text-to-speech signal source
  * 
  * The TTS signal source generates sound from text using external libraries, like TTSRelay for Windows platforms.
  *
  */
class CVATextToSpeechSignalSource : public IVAAudioSignalSource, public ITADatasourceRealization
{
public:
	CVATextToSpeechSignalSource( const double dSampleRate, const int iBlockLength );
	virtual ~CVATextToSpeechSignalSource();
		
	int GetType() const;
	std::string GetTypeString() const;
	std::string GetDesc() const;
	IVAInterface* GetAssociatedCore() const;
	const float* GetStreamBlock( const CVAAudiostreamState* );
	void HandleRegistration( IVAInterface* );
	void HandleUnregistration( IVAInterface* );
	std::string GetStateString() const;


	/*
	*	This should be used to start a prepared TTS using a CVAStruct with:
	*		["prepare_text"] = text to be spoken
	*		["id"] = identificator that will be used for playing this speech and reference it (must be unique)
	*		["voice"] = the voice to be used //if none is given or the one given cannot be found the standard voice is used (i.e. "Heather")
	*		["direct_playback"] = true/false whether the audio should directly be played (in this case no id has to be given, should not be used for lipsyncing) 

	*	This should be used to start a prepared TTS using a CVAStruct with:
	*		["play_speech"] = identificator (int) of created speech	
	*		["free_after"] = true/false, whether the resources can be freed or this sentences should be used again
	*/
	void SetParameters( const CVAStruct& );



	/*
	*	This can be used to receive the viseme data for a created speech
	*		["get_visemes_for"] = identificator as given above
	*	and returns a CVAStruct with:
	*		["visemes"] = viseme data for facial animation as xml string (empty string if something went wrong with creation)
	*
	*	This can also be used to find the available voices using a CVAStruct with:
	*		["list_voices"] = true
	*	and returns a CVAStruct with:
	*		["number"] = the number of available voices
	*		["i"] = where i is an index from 0...["number"]-1 which itself is a CVAStruct with
	*			["name"] = the name of the voice
	*			["sex"] = the sex of the voice, i.e. "male" or "female"
	*			["language"] = the language of the voice, e.g. "en" or "de"
	*			["country"] = the country of the voice, e.g. "GB" or "US"
	*/
	CVAStruct GetParameters( const CVAStruct& ) const;
	void Reset();

private:

	class TTSEngine{
		//This is a wrapper for the CereVoice TTS Engine, which has to be only initialized once and not for each TTSignalSource
	public:
		TTSEngine() { m_pTTSEngine = NULL; };
		~TTSEngine();
		static TTSEngine& getInstance(){
			static TTSEngine    instance; // Guaranteed to be destroyed.
			return instance;// Instantiated on first use.
		}
		void Init();
		void SetupPhonemeMapping();

		TTSEngine(TTSEngine const&) = delete; //to avoid copies being made etc.
		void operator=(TTSEngine const&) = delete;

		CPRCEN_engine* getEngine() const;
		float getSampleRate() const;
		std::string PhonemeToViseme(std::string phoneme);

		void SetAdditionalVoicePath(std::string _path);

	private:
		/*The engine maintains the list of
		loaded voices and makes them available to synthesis channels. */
		CPRCEN_engine* m_pTTSEngine; //you must not delete this from outside!!!!!!
		std::map<std::string, int> m_phonemeToId;
		std::map<int, std::string> m_idToViseme;
		float m_sampleRate;
		std::vector<std::string> m_VoicePaths;
		std::unordered_set<std::string> m_loadedVoices;
	};

	struct UserCallbackData{
		float lastEnd = 0.0f;
		std::string visemes = "";
		std::vector<float> floatBuffer;
	};

	static void VisemeProcessing(CPRC_abuf* abuf, void * userdata); //used as callback for the CereVoice engine
	static std::string to_string_with_precision(float a_value, const int n = 3);

	std::map<std::string, CITAAudioSample*> m_AudioSampleFrames;
	std::map<std::string, std::string> m_Visemes;

	IVAInterface* m_pAssociatedCore;
	ITASampleBuffer m_sbOut;	

	//this mutable keyword here is necessary since the inherited method GetParameters() is const, however we want to be able to change some parts (not very clean code, sorry)
	ITAAtomicInt m_iCurrentPlayState;
	ITABufferDatasource* m_pBufferDataSource;
	ITASampleFrame* m_pFrameToDelete;//this is set if the sample should be freed after playback

};

#endif // IW_VA_TEXT_TO_SPEECH_SIGNAL_SOURCE

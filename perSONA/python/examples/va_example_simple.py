# Add va module if it was not installed
import sys
sys.path.append( '../Lib/site-packages' )
sys.path.append( '../dist/Lib/site-packages' )

import os
current_exec_dir = os.getcwd()

import va

va.connect() # localhost
va.reset()
va.add_search_path( current_exec_dir ) # add current working path to find any file lying around here

signal_source_id = va.create_signal_source_buffer_from_file( 'Audiofiles/lang_short.wav' ) # Provide this file or modify file name and use your own

dir_id = va.create_directivity_from_file( '$(Trumpet)' )

sound_source_id = va.create_sound_source( 'PySoundSource' )
va.set_sound_source_signal_source( sound_source_id, signal_source_id )
va.set_sound_source_directivity( sound_source_id, dir_id )
va.set_sound_source_position( sound_source_id, ( 1, 1.2, -1 ) ) # OpenGL axes convention, direction is lower front-right from listener pos (s.b.)

hrir_id = va.create_directivity_from_file( '$(DefaultHRIR)' )

sound_receiver_id = va.create_sound_receiver( "PyListener" )
va.set_sound_receiver_directivity( sound_receiver_id, hrir_id )
va.set_sound_receiver_position( sound_receiver_id, ( 0, 1.7, 0 ) ) # Ear height at 1.7m 

va.set_signal_source_buffer_looping( signal_source_id, True )
va.set_signal_source_buffer_playback_action_str( signal_source_id, 'play' )

va.disconnect()

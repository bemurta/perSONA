set( VANETCSWRAPPER_RELATIVE_INCLUDE_DIRS "include" )
set( VANETCSWRAPPER_RELATIVE_LIBRARY_DIRS "lib" )
set( VANETCSWRAPPER_RELATIVE_SHADER_DIRS "" )
set( VANETCSWRAPPER_DEFINITIONS "" )
set( VANETCSWRAPPER_HWARCH "win32-x64.vc12" )
set( VANETCSWRAPPER_LIBRARIES
		optimized VANetCSWrapper
		debug VANetCSWrapperD )
set( VANETCSWRAPPER_DEPENDENCIES package;VACore;REQUIRED;FIND_DEPENDENCIES;package;VAMatlab;REQUIRED;FIND_DEPENDENCIES )

# msvc-project for inclusion in other solutions - always empty for installations
set( VANETCSWRAPPER_MSVC_PROJECT )

# we're getting installed to ROOT_DIR/cmake, so to get our root dir, we have to take the current dir
# and look for the lib dir, which can be one, two, or three steps up
# we check if the folder is correct by testing if the first library dir exists there
set( _TEST_DIR "${VANetCSWrapper_DIR}" )
list( GET VANETCSWRAPPER_RELATIVE_LIBRARY_DIRS 0 _TEST_SUBDIR )
foreach( _STEP RANGE 3 )
	get_filename_component( _TEST_DIR "${_TEST_DIR}" PATH ) # one dir up
	if( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
		set( VANETCSWRAPPER_ROOT_DIR "${_TEST_DIR}" )
		break()
	endif( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
endforeach( _STEP RANGE 3 )

if( NOT VANETCSWRAPPER_ROOT_DIR )
	message( SEND_ERROR "Package configfile for \"VANetCSWrapper\" found in \"${VANetCSWrapper_DIR}\", "
                       "but matching library directory is missing" )
endif( NOT VANETCSWRAPPER_ROOT_DIR )


# set include/lib dirs relative to root dir
set( VANETCSWRAPPER_INCLUDE_DIRS  )
foreach( _DIR ${VANETCSWRAPPER_RELATIVE_INCLUDE_DIRS} )
	list( APPEND VANETCSWRAPPER_INCLUDE_DIRS "${VANETCSWRAPPER_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VANETCSWRAPPER_RELATIVE_INCLUDE_DIRS} )

set( VANETCSWRAPPER_LIBRARY_DIRS  )
foreach( _DIR ${VANETCSWRAPPER_RELATIVE_LIBRARY_DIRS} )
	list( APPEND VANETCSWRAPPER_LIBRARY_DIRS "${VANETCSWRAPPER_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VANETCSWRAPPER_RELATIVE_LIBRARY_DIRS} )

set( VANETCSWRAPPER_SHADER_DIRS  )
foreach( _DIR ${VANETCSWRAPPER_RELATIVE_SHADER_DIRS} )
	list( APPEND VANETCSWRAPPER_SHADER_DIRS "${VANETCSWRAPPER_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VANETCSWRAPPER_RELATIVE_SHADER_DIRS} )

set( VANETCSWRAPPER_FOUND TRUE )

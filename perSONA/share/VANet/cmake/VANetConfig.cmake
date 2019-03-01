set( VANET_RELATIVE_INCLUDE_DIRS "include" )
set( VANET_RELATIVE_LIBRARY_DIRS "lib" )
set( VANET_RELATIVE_SHADER_DIRS "" )
set( VANET_DEFINITIONS "" )
set( VANET_HWARCH "win32-x64.vc12" )
set( VANET_LIBRARIES
		optimized VANet
		debug VANetD )
set( VANET_DEPENDENCIES package;VistaCoreLibs;REQUIRED;COMPONENTS;VistaBase;VistaInterProcComm;FIND_DEPENDENCIES;package;VABase;REQUIRED )

# msvc-project for inclusion in other solutions - always empty for installations
set( VANET_MSVC_PROJECT )

# we're getting installed to ROOT_DIR/cmake, so to get our root dir, we have to take the current dir
# and look for the lib dir, which can be one, two, or three steps up
# we check if the folder is correct by testing if the first library dir exists there
set( _TEST_DIR "${VANet_DIR}" )
list( GET VANET_RELATIVE_LIBRARY_DIRS 0 _TEST_SUBDIR )
foreach( _STEP RANGE 3 )
	get_filename_component( _TEST_DIR "${_TEST_DIR}" PATH ) # one dir up
	if( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
		set( VANET_ROOT_DIR "${_TEST_DIR}" )
		break()
	endif( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
endforeach( _STEP RANGE 3 )

if( NOT VANET_ROOT_DIR )
	message( SEND_ERROR "Package configfile for \"VANet\" found in \"${VANet_DIR}\", "
                       "but matching library directory is missing" )
endif( NOT VANET_ROOT_DIR )


# set include/lib dirs relative to root dir
set( VANET_INCLUDE_DIRS  )
foreach( _DIR ${VANET_RELATIVE_INCLUDE_DIRS} )
	list( APPEND VANET_INCLUDE_DIRS "${VANET_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VANET_RELATIVE_INCLUDE_DIRS} )

set( VANET_LIBRARY_DIRS  )
foreach( _DIR ${VANET_RELATIVE_LIBRARY_DIRS} )
	list( APPEND VANET_LIBRARY_DIRS "${VANET_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VANET_RELATIVE_LIBRARY_DIRS} )

set( VANET_SHADER_DIRS  )
foreach( _DIR ${VANET_RELATIVE_SHADER_DIRS} )
	list( APPEND VANET_SHADER_DIRS "${VANET_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VANET_RELATIVE_SHADER_DIRS} )

set( VANET_FOUND TRUE )

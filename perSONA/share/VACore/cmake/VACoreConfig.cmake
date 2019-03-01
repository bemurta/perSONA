set( VACORE_RELATIVE_INCLUDE_DIRS "include" )
set( VACORE_RELATIVE_LIBRARY_DIRS "lib" )
set( VACORE_RELATIVE_SHADER_DIRS "" )
set( VACORE_DEFINITIONS "" )
set( VACORE_HWARCH "win32-x64.vc12" )
set( VACORE_LIBRARIES
		optimized VACore
		debug VACoreD )
set( VACORE_DEPENDENCIES package;VistaCoreLibs;REQUIRED;COMPONENTS;VistaInterProcComm;FIND_DEPENDENCIES;package;VABase;REQUIRED;FIND_DEPENDENCIES;package;ITABase;REQUIRED;FIND_DEPENDENCIES;package;ITADSP;REQUIRED;FIND_DEPENDENCIES;package;ITADataSources;REQUIRED;FIND_DEPENDENCIES;package;ITACTC;REQUIRED;FIND_DEPENDENCIES;package;ITASampler;REQUIRED;FIND_DEPENDENCIES;package;Eigen;REQUIRED )

# msvc-project for inclusion in other solutions - always empty for installations
set( VACORE_MSVC_PROJECT )

# we're getting installed to ROOT_DIR/cmake, so to get our root dir, we have to take the current dir
# and look for the lib dir, which can be one, two, or three steps up
# we check if the folder is correct by testing if the first library dir exists there
set( _TEST_DIR "${VACore_DIR}" )
list( GET VACORE_RELATIVE_LIBRARY_DIRS 0 _TEST_SUBDIR )
foreach( _STEP RANGE 3 )
	get_filename_component( _TEST_DIR "${_TEST_DIR}" PATH ) # one dir up
	if( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
		set( VACORE_ROOT_DIR "${_TEST_DIR}" )
		break()
	endif( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
endforeach( _STEP RANGE 3 )

if( NOT VACORE_ROOT_DIR )
	message( SEND_ERROR "Package configfile for \"VACore\" found in \"${VACore_DIR}\", "
                       "but matching library directory is missing" )
endif( NOT VACORE_ROOT_DIR )


# set include/lib dirs relative to root dir
set( VACORE_INCLUDE_DIRS  )
foreach( _DIR ${VACORE_RELATIVE_INCLUDE_DIRS} )
	list( APPEND VACORE_INCLUDE_DIRS "${VACORE_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VACORE_RELATIVE_INCLUDE_DIRS} )

set( VACORE_LIBRARY_DIRS  )
foreach( _DIR ${VACORE_RELATIVE_LIBRARY_DIRS} )
	list( APPEND VACORE_LIBRARY_DIRS "${VACORE_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VACORE_RELATIVE_LIBRARY_DIRS} )

set( VACORE_SHADER_DIRS  )
foreach( _DIR ${VACORE_RELATIVE_SHADER_DIRS} )
	list( APPEND VACORE_SHADER_DIRS "${VACORE_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VACORE_RELATIVE_SHADER_DIRS} )

set( VACORE_FOUND TRUE )

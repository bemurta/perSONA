set( VAMATLAB_RELATIVE_INCLUDE_DIRS "include" )
set( VAMATLAB_RELATIVE_LIBRARY_DIRS "lib" )
set( VAMATLAB_RELATIVE_SHADER_DIRS "" )
set( VAMATLAB_DEFINITIONS "" )
set( VAMATLAB_HWARCH "win32-x64.vc12" )
set( VAMATLAB_LIBRARIES
		optimized VAMatlab
		debug VAMatlabD )
set( VAMATLAB_DEPENDENCIES package;VACore;REQUIRED;FIND_DEPENDENCIES;package;VANet;REQUIRED;FIND_DEPENDENCIES;package;Matlab;REQUIRED;FIND_DEPENDENCIES;package;NatNetSDK;REQUIRED;FIND_DEPENDENCIES )

# msvc-project for inclusion in other solutions - always empty for installations
set( VAMATLAB_MSVC_PROJECT )

# we're getting installed to ROOT_DIR/cmake, so to get our root dir, we have to take the current dir
# and look for the lib dir, which can be one, two, or three steps up
# we check if the folder is correct by testing if the first library dir exists there
set( _TEST_DIR "${VAMatlab_DIR}" )
list( GET VAMATLAB_RELATIVE_LIBRARY_DIRS 0 _TEST_SUBDIR )
foreach( _STEP RANGE 3 )
	get_filename_component( _TEST_DIR "${_TEST_DIR}" PATH ) # one dir up
	if( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
		set( VAMATLAB_ROOT_DIR "${_TEST_DIR}" )
		break()
	endif( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
endforeach( _STEP RANGE 3 )

if( NOT VAMATLAB_ROOT_DIR )
	message( SEND_ERROR "Package configfile for \"VAMatlab\" found in \"${VAMatlab_DIR}\", "
                       "but matching library directory is missing" )
endif( NOT VAMATLAB_ROOT_DIR )


# set include/lib dirs relative to root dir
set( VAMATLAB_INCLUDE_DIRS  )
foreach( _DIR ${VAMATLAB_RELATIVE_INCLUDE_DIRS} )
	list( APPEND VAMATLAB_INCLUDE_DIRS "${VAMATLAB_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VAMATLAB_RELATIVE_INCLUDE_DIRS} )

set( VAMATLAB_LIBRARY_DIRS  )
foreach( _DIR ${VAMATLAB_RELATIVE_LIBRARY_DIRS} )
	list( APPEND VAMATLAB_LIBRARY_DIRS "${VAMATLAB_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VAMATLAB_RELATIVE_LIBRARY_DIRS} )

set( VAMATLAB_SHADER_DIRS  )
foreach( _DIR ${VAMATLAB_RELATIVE_SHADER_DIRS} )
	list( APPEND VAMATLAB_SHADER_DIRS "${VAMATLAB_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VAMATLAB_RELATIVE_SHADER_DIRS} )

set( VAMATLAB_FOUND TRUE )

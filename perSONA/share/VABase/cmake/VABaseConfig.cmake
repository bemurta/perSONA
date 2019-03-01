set( VABASE_RELATIVE_INCLUDE_DIRS "include" )
set( VABASE_RELATIVE_LIBRARY_DIRS "lib" )
set( VABASE_RELATIVE_SHADER_DIRS "" )
set( VABASE_DEFINITIONS "" )
set( VABASE_HWARCH "win32-x64.vc12" )
set( VABASE_LIBRARIES
		optimized VABase
		debug VABaseD )
set( VABASE_DEPENDENCIES  )

# msvc-project for inclusion in other solutions - always empty for installations
set( VABASE_MSVC_PROJECT )

# we're getting installed to ROOT_DIR/cmake, so to get our root dir, we have to take the current dir
# and look for the lib dir, which can be one, two, or three steps up
# we check if the folder is correct by testing if the first library dir exists there
set( _TEST_DIR "${VABase_DIR}" )
list( GET VABASE_RELATIVE_LIBRARY_DIRS 0 _TEST_SUBDIR )
foreach( _STEP RANGE 3 )
	get_filename_component( _TEST_DIR "${_TEST_DIR}" PATH ) # one dir up
	if( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
		set( VABASE_ROOT_DIR "${_TEST_DIR}" )
		break()
	endif( EXISTS "${_TEST_DIR}/${_TEST_SUBDIR}" )
endforeach( _STEP RANGE 3 )

if( NOT VABASE_ROOT_DIR )
	message( SEND_ERROR "Package configfile for \"VABase\" found in \"${VABase_DIR}\", "
                       "but matching library directory is missing" )
endif( NOT VABASE_ROOT_DIR )


# set include/lib dirs relative to root dir
set( VABASE_INCLUDE_DIRS  )
foreach( _DIR ${VABASE_RELATIVE_INCLUDE_DIRS} )
	list( APPEND VABASE_INCLUDE_DIRS "${VABASE_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VABASE_RELATIVE_INCLUDE_DIRS} )

set( VABASE_LIBRARY_DIRS  )
foreach( _DIR ${VABASE_RELATIVE_LIBRARY_DIRS} )
	list( APPEND VABASE_LIBRARY_DIRS "${VABASE_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VABASE_RELATIVE_LIBRARY_DIRS} )

set( VABASE_SHADER_DIRS  )
foreach( _DIR ${VABASE_RELATIVE_SHADER_DIRS} )
	list( APPEND VABASE_SHADER_DIRS "${VABASE_ROOT_DIR}/${_DIR}" )
endforeach( _DIR ${VABASE_RELATIVE_SHADER_DIRS} )

set( VABASE_FOUND TRUE )

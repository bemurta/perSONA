# VA is used as a singleton.
# You can access va in every script, function and method.

# Add va module if it was not installed
import sys
sys.path.append( '../Lib/site-packages' ) # deploy structure

import va

print( "Testing va extension connection methods." )

if va.connect() :
	print( "Successfully connected to local server without arguments" )
	va.disconnect() # direct disconnect
else :
	print( "Connection failed" )

if va.connect( "localhost" ) :
	print( "Successfully connected to local server with localhost argument" )
else :
	print( "Connection failed" )

# sensitive disconnect
if va.is_connected() :
	va.disconnect()

if va.connect( "localhost", 12340 ) :
	print( "Successfully connected to local server with localhost and port 12340 argument" )
else :
	print( "Connection failed" )

print( "Disconnect." )
va.disconnect()

import time

import warnings

with warnings.catch_warnings() :
	warnings.simplefilter( "always" )
	
	time.sleep( 1 )
	print( "Double disconnect:" )
	va.disconnect() # double disconnect should raise warning

	va.connect()

	time.sleep( 1 )
	print( "Double connect:" )
	va.connect() # double connect should raise forced disconnection warning

va.disconnect()
print( "Test done." )

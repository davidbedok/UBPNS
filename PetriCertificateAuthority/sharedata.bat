REM Passwords
set ROOTCA_KEY_PASSWORD=changeitroot
set ROOTCA_CERTSTORE_PASSWORD=changeit
set SUBCA_KEY_PASSWORD=changeitsub
set SUBCA_CERTSTORE_PASSWORD=changeit
set END_KEYSTORE_PASSWORD=changeitstore
set END_PRIVATEKEY_PASSWORD=petripass
set END_PKCS12_PASSWORD=changeitstore
set END_CERTSTORE_PASSWORD=changeit

REM Alias
set ROOTCA_ALIAS=rootca
set SUBCA_ALIAS=subca
set END_ALIAS=petri

REM Files Prefix
set ROOTCA_FILE_PREFIX=petrirootca
set SUBCA_FILE_PREFIX=petrisubca
REM set END_FILE_PREFIX=uniobuda
REM set END_FILE_PREFIX=davidbedok
set END_FILE_PREFIX=ericsson

REM Info

REM ericsson (PFX pass: ericsson123)
set CERT_CN=Ericsson Hungary Ltd.
set CERT_OU=ETH
set CERT_O=Ericsson
set CERT_L=Budapest
set CERT_S=Hungary
set CERT_C=HU
set CERT_E=david.bedok@ericsson.com

set KEYALG=RSA
set KEYSIZE=1024
set SIGALG=SHA1withRSA
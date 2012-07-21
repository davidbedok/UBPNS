@echo off
call .\..\sharedata.bat
call .\..\sharecommon.bat

ECHO ------------------- DELETE AND CREATE DATABASE
del /Q database.txt
del /Q serial.txt
ECHO. 2> database.txt
ECHO 01 > serial.txt

ECHO ------------------- CREATE PASSWORD FILE
del /Q %ROOTCA_KEY_PASSWORD_TXT%
echo %ROOTCA_KEY_PASSWORD% > %ROOTCA_KEY_PASSWORD_TXT%

ECHO ------------------- GENERATE PRIVATE KEY
del /Q %ROOTCA_KEY_FILE%
%OPENSSL_HOME%\bin\openssl genrsa -des3 -out %ROOTCA_KEY_FILE% 4096

ECHO ------------------- CREATE SELF-SIGN ROOT CERTIFICATE
del /Q %ROOTCA_CERT_PEM_FILE%
%OPENSSL_HOME%\bin\openssl req -config %OSSL_CONFIG% -new -x509 -sha384 -days 1826 -key %ROOTCA_KEY_FILE% -passin pass:%ROOTCA_KEY_PASSWORD% -extensions x509v3_extensions -out %ROOTCA_CERT_PEM_FILE%

ECHO ------------------- CONVERT ROOT CA PEM TO DER
%OPENSSL_HOME%\bin\openssl x509 -in %ROOTCA_CERT_PEM_FILE% -inform PEM -out %ROOTCA_CERT_DER_FILE% -outform DER

ECHO ------------------- CREATE PASSWORD FILE
del /Q %ROOTCA_CERTSTORE_PASSWORD_TXT%
echo %ROOTCA_CERTSTORE_PASSWORD% > %ROOTCA_CERTSTORE_PASSWORD_TXT%

ECHO ------------------- CREATE CERTSTORE FOR ROOTCA
del /Q %ROOTCA_CERTSTORE%
%KEYTOOL_HOME%\bin\keytool -importcert -keystore %ROOTCA_CERTSTORE% -storepass %ROOTCA_CERTSTORE_PASSWORD% -alias %ROOTCA_ALIAS% -file %ROOTCA_CERT_PEM_FILE% -noprompt -trustcacerts

ECHO ------------------- PRINT CERTSTORE
del /Q %ROOTCA_CERTSTORE_LIST_TXT%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %ROOTCA_CERTSTORE% -storepass %ROOTCA_CERTSTORE_PASSWORD% > %ROOTCA_CERTSTORE_LIST_TXT%
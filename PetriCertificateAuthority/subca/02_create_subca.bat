@echo off
call .\..\sharedata.bat
call .\..\sharecommon.bat

ECHO ------------------- DELETE AND CREATE DATABASE
del /Q database.txt
del /Q serial.txt
ECHO. 2> database.txt
ECHO 01 > serial.txt

ECHO ------------------- CREATE PASSWORD FILE
del /Q %SUBCA_KEY_PASSWORD_TXT%
echo %SUBCA_KEY_PASSWORD% > %SUBCA_KEY_PASSWORD_TXT%

ECHO ------------------- GENERATE PRIVATE KEY
del /Q %SUBCA_KEY_FILE%
%OPENSSL_HOME%\bin\openssl genrsa -des3 -out %SUBCA_KEY_FILE% 4096

ECHO ------------------- CREATE PKCS10 CERTIFICATE REQUEST (CSR)
del /Q %SUBCA_CSR_FILE%
%OPENSSL_HOME%\bin\openssl req -config %OSSL_CONFIG% -new -days 1826 -sha384 -key %SUBCA_KEY_FILE% -passin pass:%SUBCA_KEY_PASSWORD% -extensions x509v3_extensions -out %SUBCA_CSR_FILE%
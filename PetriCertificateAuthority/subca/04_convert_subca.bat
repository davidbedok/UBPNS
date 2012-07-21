@echo off
call .\..\sharedata.bat
call .\..\sharecommon.bat

ECHO ------------------- CONVERT SUBCA CERTIFICATE
%OPENSSL_HOME%\bin\openssl x509 -in %SUBCA_CERT_PEM_FILE% -inform PEM -out %SUBCA_CERT_DER_FILE% -outform DER

ECHO ------------------- CREATE PASSWORD FILE
del /Q %SUBCA_CERTSTORE_PASSWORD_TXT%
echo %SUBCA_CERTSTORE_PASSWORD% > %SUBCA_CERTSTORE_PASSWORD_TXT%

ECHO ------------------- CREATE CERTSTORE FOR SUBCA
del /Q %SUBCA_CERTSTORE%
%KEYTOOL_HOME%\bin\keytool -importcert -keystore %SUBCA_CERTSTORE% -storepass %SUBCA_CERTSTORE_PASSWORD% -alias %SUBCA_ALIAS% -file %SUBCA_CERT_DER_FILE% -noprompt -trustcacerts

ECHO ------------------- PRINT CERTSTORE
del /Q %SUBCA_CERTSTORE_LIST_TXT%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %SUBCA_CERTSTORE% -storepass %SUBCA_CERTSTORE_PASSWORD% > %SUBCA_CERTSTORE_LIST_TXT%

ECHO ------------------- CREATE PASSWORD FILE
del /Q %SUBCA_CERTPATHSTORE_PASSWORD_TXT%
echo %SUBCA_CERTSTORE_PASSWORD% > %SUBCA_CERTPATHSTORE_PASSWORD_TXT%

ECHO ------------------- CREATE CERTPATHSTORE FOR SUBCA AND ROOTCA
del /Q %SUBCA_CERTPATHSTORE%
%KEYTOOL_HOME%\bin\keytool -importcert -keystore %SUBCA_CERTPATHSTORE% -storepass %SUBCA_CERTSTORE_PASSWORD% -alias %ROOTCA_ALIAS% -file %ROOTCA_CERT_PEM_FILE% -noprompt -trustcacerts
%KEYTOOL_HOME%\bin\keytool -importcert -keystore %SUBCA_CERTPATHSTORE% -storepass %SUBCA_CERTSTORE_PASSWORD% -alias %SUBCA_ALIAS% -file %SUBCA_CERT_DER_FILE% -noprompt -trustcacerts

ECHO ------------------- PRINT CERTPATHSTORE
del /Q %SUBCA_CERTPATHSTORE_LIST_TXT%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %SUBCA_CERTPATHSTORE% -storepass %SUBCA_CERTSTORE_PASSWORD% > %SUBCA_CERTPATHSTORE_LIST_TXT%


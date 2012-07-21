@echo off
call .\..\sharedata.bat
call .\..\sharecommon.bat

ECHO ------------------- CREATE PASSWORD FILES
del /Q %END_PKCS12_PASSWORD_TXT%
del /Q %END_CERTSTORE_PASSWORD_TXT%
del /Q %END_TRUSTSTORE_PASSWORD_TXT%
echo %END_PKCS12_PASSWORD%>%END_PKCS12_PASSWORD_TXT%
echo %END_CERTSTORE_PASSWORD%>%END_CERTSTORE_PASSWORD_TXT%
echo %END_CERTSTORE_PASSWORD%>%END_TRUSTSTORE_PASSWORD_TXT%

ECHO ------------------- CONVERT CERTIFICATE PEM2PEM
del /Q %END_TMP_CERT_PEM_FILE%
%OPENSSL_HOME%\bin\openssl x509 -in %END_LONG_CERT_PEM_FILE% -out %END_TMP_CERT_PEM_FILE%

ECHO ------------------- IMPORT SIGNED CERTIFICATE
%KEYTOOL_HOME%\bin\keytool -importcert -alias %END_ALIAS% -file %END_TMP_CERT_PEM_FILE% -keystore %END_KEYSTORE% -storepass %END_KEYSTORE_PASSWORD% -keypass %END_PRIVATEKEY_PASSWORD%

ECHO ------------------- PRINT KEYSTORE
del /Q %END_KEYSTORE_LIST_TXT%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %END_KEYSTORE% -storepass %END_KEYSTORE_PASSWORD% > %END_KEYSTORE_LIST_TXT%

ECHO ------------------- EXPORT CERT FROM KEYSTORE
del /Q %END_CERT_DER_FILE%
%KEYTOOL_HOME%\bin\keytool -exportcert -keystore %END_KEYSTORE% -storepass %END_KEYSTORE_PASSWORD% -alias %END_ALIAS% -file %END_CERT_DER_FILE%

ECHO ------------------- CONVERT JKS 2 PKCS12
del /Q %END_PKCS12%
%KEYTOOL_HOME%\bin\keytool -importkeystore -srckeystore %END_KEYSTORE% -destkeystore %END_PKCS12% -srcstoretype JKS -deststoretype PKCS12 -srcstorepass %END_KEYSTORE_PASSWORD% -deststorepass %END_PKCS12_PASSWORD% -srckeypass %END_PRIVATEKEY_PASSWORD% -srcalias %END_ALIAS% -destalias %END_ALIAS% -noprompt

ECHO ------------------- LIST PKCS12 STORE
del /Q %END_PKCS12_LIST_TXT%
%KEYTOOL_HOME%\bin\keytool -list -keystore %END_PKCS12% -storetype PKCS12 -storepass %END_KEYSTORE_PASSWORD% -v > %END_PKCS12_LIST_TXT%

ECHO ------------------- CONVERT ENDCERT DER TO PEM
del /Q %END_CERT_PEM_FILE%
%OPENSSL_HOME%\bin\openssl x509 -in %END_CERT_DER_FILE% -inform DER -out %END_CERT_PEM_FILE% -outform PEM

ECHO ------------------- CREATE CERTSTORE
del /Q %END_CERTSTORE%
%KEYTOOL_HOME%\bin\keytool -importcert -keystore %END_CERTSTORE% -storepass %END_CERTSTORE_PASSWORD% -alias %END_ALIAS% -file %END_CERT_DER_FILE% -noprompt -trustcacerts

ECHO ------------------- PRINT CERTSTORE
del /Q %END_CERTSTORE_LIST_TXT%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %END_CERTSTORE% -storepass %END_CERTSTORE_PASSWORD% > %END_CERTSTORE_LIST_TXT%

ECHO ------------------- CREATE TRUSTSTORE FOR ENDENTITY, SUBCA AND ROOTCA
del /Q %END_TRUSTSTORE%
%KEYTOOL_HOME%\bin\keytool -importcert -keystore %END_TRUSTSTORE% -storepass %END_CERTSTORE_PASSWORD% -alias %ROOTCA_ALIAS% -file %ROOTCA_CERT_DER_FILE% -noprompt -trustcacerts
%KEYTOOL_HOME%\bin\keytool -importcert -keystore %END_TRUSTSTORE% -storepass %END_CERTSTORE_PASSWORD% -alias %SUBCA_ALIAS% -file %SUBCA_CERT_DER_FILE% -noprompt -trustcacerts
%KEYTOOL_HOME%\bin\keytool -importcert -keystore %END_TRUSTSTORE% -storepass %END_CERTSTORE_PASSWORD% -alias %END_ALIAS% -file %END_CERT_DER_FILE% -noprompt -trustcacerts

ECHO ------------------- PRINT CERTFULLSTORE
del /Q %END_TRUSTSTORE_LIST_TXT%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %END_TRUSTSTORE% -storepass %END_CERTSTORE_PASSWORD% > %END_TRUSTSTORE_LIST_TXT%

REM ECHO ------------------- EXPORT PRIVATE KEY
REM del /Q %END_PRIVATEKEY_PEM_FILE%
REM %OPENSSL_HOME%\bin\openssl pkcs12 -in %END_PKCS12% -nocerts -out %END_PRIVATEKEY_PEM_FILE% -passin pass:%END_PKCS12_PASSWORD% -passout pass:%END_PRIVATEKEY_PASSWORD%

REM ECHO ------------------- REMOVE PASSWORD FROM PRIVATE KEY
REM del /Q %END_PRIVATEKEY_PEM_NOPASS_FILE%
REM %OPENSSL_HOME%\bin\openssl rsa -in %END_PRIVATEKEY_PEM_FILE% -out %END_PRIVATEKEY_PEM_NOPASS_FILE% -passin pass:%END_PKCS12_PASSWORD%
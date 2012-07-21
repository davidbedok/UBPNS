@echo off
call .\..\sharedata.bat
call .\..\sharecommon.bat

ECHO ------------------- CREATE OUTPUTDIR
md %OUTPUTDIR%

ECHO ------------------- CREATE PASSWORD AND ALIAS FILE
del /Q %END_KEY_PASSWORD_TXT%
del /Q %END_KEYSTORE_PASSWORD_TXT%
del /Q %END_PRIVATEKEY_ALIAS_TXT%
echo %END_PRIVATEKEY_PASSWORD%>%END_KEY_PASSWORD_TXT%
echo %END_KEYSTORE_PASSWORD%>%END_KEYSTORE_PASSWORD_TXT%
echo %END_ALIAS%>%END_PRIVATEKEY_ALIAS_TXT%

ECHO ------------------- GENERATE KEY PAIR
del /Q %END_KEYSTORE%
%KEYTOOL_HOME%\bin\keytool -genkeypair -keyalg %KEYALG% -keysize %KEYSIZE% -sigalg %SIGALG% -alias %END_ALIAS% -keystore %END_KEYSTORE% -dname "CN=%CERT_CN%, OU=%CERT_OU%, O=%CERT_O%, L=%CERT_L%, S=%CERT_S%, C=%CERT_C%, EmailAddress=%CERT_E%" -storepass %END_KEYSTORE_PASSWORD% -keypass %END_PRIVATEKEY_PASSWORD%

ECHO ------------------- PRINT KEYSTORE
del /Q %END_KEYSTORE_LIST_TXT_AFTER_CREATE%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %END_KEYSTORE% -storepass %END_KEYSTORE_PASSWORD% > %END_KEYSTORE_LIST_TXT_AFTER_CREATE%

ECHO ------------------- CREATE CERTIFICATE REQUEST
del /Q %END_CSR_FILE%
%KEYTOOL_HOME%\bin\keytool -certreq -alias %END_ALIAS% -keystore %END_KEYSTORE% -file %END_CSR_FILE% -storepass %END_KEYSTORE_PASSWORD% -keypass %END_PRIVATEKEY_PASSWORD%

ECHO ------------------- IMPORT ROOTCA CERTIFICATE
%KEYTOOL_HOME%\bin\keytool -importcert -alias %ROOTCA_ALIAS% -file %ROOTCA_CERT_PEM_FILE% -keystore %END_KEYSTORE% -storepass %END_KEYSTORE_PASSWORD% -noprompt

ECHO ------------------- PRINT KEYSTORE
del /Q %END_KEYSTORE_LIST_TXT_AFTER_ADD_ROOTCA%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %END_KEYSTORE% -storepass %END_KEYSTORE_PASSWORD% > %END_KEYSTORE_LIST_TXT_AFTER_ADD_ROOTCA%

ECHO ------------------- IMPORT SUBCA CERTIFICATE
%KEYTOOL_HOME%\bin\keytool -importcert -alias %SUBCA_ALIAS% -file %SUBCA_CERT_DER_FILE% -keystore %END_KEYSTORE% -storepass %END_KEYSTORE_PASSWORD% -noprompt

ECHO ------------------- PRINT KEYSTORE
del /Q %END_KEYSTORE_LIST_TXT_AFTER_ADD_SUBCA%
%KEYTOOL_HOME%\bin\keytool -list -v -keystore %END_KEYSTORE% -storepass %END_KEYSTORE_PASSWORD% > %END_KEYSTORE_LIST_TXT_AFTER_ADD_SUBCA%

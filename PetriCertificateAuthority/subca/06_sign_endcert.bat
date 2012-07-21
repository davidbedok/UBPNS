@echo off
call .\..\sharedata.bat
call .\..\sharecommon.bat

ECHO ------------------- SIGN END WITH SUBCA
del /Q %END_LONG_CERT_PEM_FILE%
%OPENSSL_HOME%\bin\openssl ca -policy %SUBCA_POLICY% -config %OSSL_CONFIG% -cert %SUBCA_CERT_PEM_FILE% -in %END_CSR_FILE% -keyfile %SUBCA_KEY_FILE% -days 720 -md sha384 -passin pass:%SUBCA_KEY_PASSWORD% -out %END_LONG_CERT_PEM_FILE%
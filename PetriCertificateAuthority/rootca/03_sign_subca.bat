@echo off
call .\..\sharedata.bat
call .\..\sharecommon.bat

ECHO ------------------- SIGN SUBCA WITH ROOTCA
del /Q %SUBCA_CERT_PEM_FILE%
%OPENSSL_HOME%\bin\openssl ca -config %OSSL_CONFIG% -keyfile %ROOTCA_KEY_FILE% -passin pass:%ROOTCA_KEY_PASSWORD% -cert %ROOTCA_CERT_PEM_FILE% -in %SUBCA_CSR_FILE%  -extensions x509v3_extensions -md sha384 -out %SUBCA_CERT_PEM_FILE%
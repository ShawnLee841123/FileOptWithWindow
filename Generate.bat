@echo on
::@set ClientLogic=LogicModule
::@set ServerLogic=Server
::@set ServerBase=ServerBase

::@set VSCLDir=VS_Client
::@set VSSLDir=VS_Server
::@set VSSBDir=VS_ServerBase


@set ProRoot=E:\Works\GitWork\FileOptWithWindow

::md %VSCLDir%
::cd %VSCLDir%
::cmake -G"Visual Studio 15 2017 Win64" %ProRoot%/%ClientLogic% -DBINARY_DIR=%ProRoot%/%VSCLDir% -DCMAKE_CONFIGURATION_TYPES=RelWithDebInfo
::cmake -G"Visual Studio 15 2017 Win64" %ClientLogic% -DBINARY_DIR=%VSCLDir% -DCMAKE_CONFIGURATION_TYPES=RelWithDebInfo

::@echo ---------------------generate client project completed---------------------

::cd ..
::md %VSSLDir%
::cd %VSSLDir%
::cmake -G"Visual Studio 15 2017" %ProRoot%\%ServerLogic% -DBINARY_DIR=%ProRoot%\%VSSLDir% -DCMAKE_CONFIGURATION_TYPES=Debug -A x64
::cmake -G"Visual Studio 15 2017 Win64" %ProRoot%\%ServerLogic% -DBINARY_DIR=%ProRoot%\%VSSLDir% -DCMAKE_CONFIGURATION_TYPES=Debug

::@echo ---------------------generate server project completed---------------------
::cd ..

::md %VSSBDir%
::cd %VSSBDir%

::cmake -G"Visual Studio 15 2017 Win64" %ProRoot%\%ServerBase% -DBINARY_DIR=%ProRoot%\%VSSBDir% -DCMAKE_CONFIGURATION_TYPES=Debug
::cmake -G"Visual Studio 15 2017 Win64" %ProRoot%\%ServerBase% -DCMAKE_CONFIGURATION_TYPES=Debug

::@echo ---------------------generate server base project completed---------------------
::cd ..



@set ProPath=FileOp
@set BuildPath=VSSln

md %BuildPath%
cd %BuildPath%
cmake -G"Visual Studio 16 2019" %ProRoot%\%ProPath% -DBINARY_DIR=%ProRoot%\%BuildPath% -A x64

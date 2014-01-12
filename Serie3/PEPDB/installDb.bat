@echo off

if [%1] == [/?] goto :usage
if [%1] == [-?] goto :usage

set server=%1
if [%server%] NEQ [] goto :serverSet
set server=localhost
echo Servidor não especificado, utilizando localhost por omissão.

:serverSet
SET /P ANSWER=Será criada a base de dados PEPDB no servidor %server%. Deseja continuar (S/N)?
if /i {%ANSWER%}=={s} (goto :yes)
goto :end
:yes 

echo ##########################################################
echo #                    B U I L D I N G                     #
echo ##########################################################

for %%f in (
buildTables.sql
) do (
	@echo on 
	echo executing %%f 
	echo off
	SQLCMD -b -S %server% -d PEPDB -i ".\"%%f 
	if ERRORLEVEL 1 exit /b 1
	@echo off
)

echo ############         Building Ended        ###############
echo ##########################################################
echo #                  P O P U L A T I N G                   #
echo ##########################################################

for %%f in (
populateTables.sql
) do (
	@echo on 
	echo executing %%f 
	echo off
	SQLCMD -b -S %server% -d PEPDB -i ".\"%%f 
	if ERRORLEVEL 1 exit /b 1
	@echo off
)

echo ############       Populating Ended        ###############
goto end

:usage
echo usage: "installDb.bat [server]"
echo 	server - defaults to localhost
pause
:end
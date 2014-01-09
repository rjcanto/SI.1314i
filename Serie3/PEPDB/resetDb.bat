@echo off
if [%1] == [] goto usage
if [%2] == [] goto usage
if [%3] == [] goto usage
if [%4] == [] goto usage
if [%5] == [] goto usage

echo ##########################################################
echo #                    B U I L D I N G                     #
echo ##########################################################

for %%f in (
buildTables.sql
) do (
	@echo on 
	echo executing %%f 
	echo off
	SQLCMD -b -S %4 -U %1 -P %2 -d %3 -i ".\"%f 
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
	SQLCMD -b -S %4 -U %1 -P %2 -d %3 -i ".\"%f 
	if ERRORLEVEL 1 exit /b 1
	@echo off
)

echo ############       Populating Ended        ###############
goto end

:usage
echo usage: "DBInstaller.bat <dbUserName> <dbPassword> <dbName> <server>"
pause
:end
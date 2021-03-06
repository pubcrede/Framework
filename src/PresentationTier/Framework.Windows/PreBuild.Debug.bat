ECHO Starting PreBuild.bat
REM Usage: Call "$(MSBuildProjectDirectory)\PreBuild.$(ConfigurationName).bat" "$(MSBuildProjectDirectory)" "$(ConfigurationName)"
REM Vars:  $(TargetPath) = output file, $(TargetDir) = full bin path , $(OutDir) = bin\debug, $(ConfigurationName) = "Debug"

REM Locals
SET FullPath=%1
SET FullPath=%FullPath:"=%
SET ProductFolder="genesys.framework.windows"

ECHO ** PreBuild.bat **
ECHO FullPath: %FullPath%
SET Configuration=%2
ECHO Configuration: %Configuration%

if "%Configuration%"=="" SET Configuration="Debug"

exit 0
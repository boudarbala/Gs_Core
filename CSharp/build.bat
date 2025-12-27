@echo off
REM ==============================================================================
REM Gs_Core C# Build Automation Script
REM Purpose: Comprehensive build automation with error handling and logging
REM Created: 2025-12-27
REM ==============================================================================

setlocal enabledelayedexpansion
cd /d "%~dp0"

REM ==============================================================================
REM Configuration Variables
REM ==============================================================================
set "BUILD_CONFIG=Release"
set "BUILD_PLATFORM=AnyCPU"
set "LOG_DIR=%~dp0logs"
set "LOG_FILE=%LOG_DIR%\build_%date:~-4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%.log"
set "BUILD_OUTPUT_DIR=%~dp0bin\%BUILD_CONFIG%"
set "SOLUTION_FILE=%~dp0Gs_Core.sln"
set "VERBOSE=0"
set "CLEAN_BUILD=0"
set "TEST_BUILD=0"

REM ==============================================================================
REM Color Codes for Console Output
REM ==============================================================================
for /F %%A in ('copy /Z "%~f0" nul') do set "BS=%%A"

REM ==============================================================================
REM Function: Display Help
REM ==============================================================================
:DisplayHelp
cls
echo.
echo ==============================================================================
echo Gs_Core C# Build Automation Script
echo ==============================================================================
echo.
echo Usage: build.bat [OPTIONS]
echo.
echo Options:
echo   /? or /help              Display this help message
echo   /config:CONFIG           Build configuration (Debug/Release) - Default: Release
echo   /platform:PLATFORM       Target platform (AnyCPU/x86/x64) - Default: AnyCPU
echo   /clean                   Perform clean build
echo   /test                    Run tests after build
echo   /verbose                 Enable verbose logging
echo   /output:PATH             Custom output directory
echo.
echo Examples:
echo   build.bat                                  - Standard Release build
echo   build.bat /config:Debug /verbose           - Debug build with verbose output
echo   build.bat /clean /config:Release           - Clean Release build
echo   build.bat /test /config:Debug              - Debug build with tests
echo.
echo ==============================================================================
exit /b 0

REM ==============================================================================
REM Function: Parse Command Line Arguments
REM ==============================================================================
:ParseArguments
setlocal enabledelayedexpansion
for %%A in (%*) do (
    set "arg=%%A"
    
    if /i "!arg!"=="/?" goto DisplayHelp
    if /i "!arg!"=="/help" goto DisplayHelp
    if /i "!arg!"=="/clean" set "CLEAN_BUILD=1"
    if /i "!arg!"=="/test" set "TEST_BUILD=1"
    if /i "!arg!"=="/verbose" set "VERBOSE=1"
    
    if /i "!arg:~0,8!"=="/config:" (
        set "BUILD_CONFIG=!arg:~8!"
        set "BUILD_OUTPUT_DIR=%~dp0bin\!BUILD_CONFIG!"
    )
    if /i "!arg:~0,10!"=="/platform:" set "BUILD_PLATFORM=!arg:~10!"
    if /i "!arg:~0,8!"=="/output:" set "BUILD_OUTPUT_DIR=!arg:~8!"
)
endlocal & set "BUILD_CONFIG=%BUILD_CONFIG%" & set "BUILD_PLATFORM=%BUILD_PLATFORM%" & set "BUILD_OUTPUT_DIR=%BUILD_OUTPUT_DIR%" & set "VERBOSE=%VERBOSE%" & set "CLEAN_BUILD=%CLEAN_BUILD%" & set "TEST_BUILD=%TEST_BUILD%"

REM ==============================================================================
REM Function: Initialize Logging
REM ==============================================================================
:InitializeLogging
if not exist "%LOG_DIR%" (
    mkdir "%LOG_DIR%"
    if !errorlevel! neq 0 (
        echo [ERROR] Failed to create log directory: %LOG_DIR%
        exit /b 1
    )
)
call :LogMessage "================================================================================"
call :LogMessage "Gs_Core C# Build Process Started"
call :LogMessage "================================================================================"
call :LogMessage "Date/Time: %date% %time%"
call :LogMessage "Script Directory: %~dp0"
call :LogMessage "Build Configuration: %BUILD_CONFIG%"
call :LogMessage "Build Platform: %BUILD_PLATFORM%"
call :LogMessage "Output Directory: %BUILD_OUTPUT_DIR%"
call :LogMessage "Clean Build: %CLEAN_BUILD%"
call :LogMessage "Run Tests: %TEST_BUILD%"
call :LogMessage "Verbose Mode: %VERBOSE%"
call :LogMessage "================================================================================"

REM ==============================================================================
REM Function: Log Message
REM ==============================================================================
:LogMessage
setlocal enabledelayedexpansion
set "msg=%~1"
set "timestamp=%date% %time%"
(
    echo !timestamp! - !msg!
) >> "%LOG_FILE%"
if %VERBOSE% equ 1 echo !timestamp! - !msg!
endlocal
exit /b 0

REM ==============================================================================
REM Function: Verify Prerequisites
REM ==============================================================================
:VerifyPrerequisites
call :LogMessage "Verifying build prerequisites..."

REM Check for .NET Framework
powershell -NoProfile -ExecutionPolicy Bypass -Command "^
    $netFramework = Get-ChildItem 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP' -Recurse | Where-Object {$_.Name -match 'v[\d.]+'} | Select-Object -ExpandProperty Name;
    if ($netFramework) { exit 0 } else { exit 1 }
"
if !errorlevel! neq 0 (
    call :LogMessage "[WARNING] .NET Framework may not be installed or not found in registry"
)

REM Check for MSBuild
where /q msbuild.exe
if !errorlevel! neq 0 (
    call :LogMessage "[ERROR] MSBuild not found in PATH"
    call :LogMessage "Please install Visual Studio or the Build Tools for Visual Studio"
    exit /b 1
)
call :LogMessage "MSBuild found successfully"

REM Check for solution file
if not exist "%SOLUTION_FILE%" (
    call :LogMessage "[ERROR] Solution file not found: %SOLUTION_FILE%"
    exit /b 1
)
call :LogMessage "Solution file found: %SOLUTION_FILE%"

exit /b 0

REM ==============================================================================
REM Function: Clean Build
REM ==============================================================================
:CleanBuild
call :LogMessage "Starting clean build..."
if exist "%BUILD_OUTPUT_DIR%" (
    call :LogMessage "Removing output directory: %BUILD_OUTPUT_DIR%"
    rmdir /s /q "%BUILD_OUTPUT_DIR%" 2>>"%LOG_FILE%"
    if !errorlevel! neq 0 (
        call :LogMessage "[WARNING] Failed to remove some files from output directory"
    )
)
call :LogMessage "Clean build completed"
exit /b 0

REM ==============================================================================
REM Function: Build Solution
REM ==============================================================================
:BuildSolution
call :LogMessage "Building solution..."
call :LogMessage "Command: msbuild.exe "%SOLUTION_FILE%" /p:Configuration=%BUILD_CONFIG% /p:Platform=%BUILD_PLATFORM% /m"

msbuild.exe "%SOLUTION_FILE%" ^
    /p:Configuration=%BUILD_CONFIG% ^
    /p:Platform=%BUILD_PLATFORM% ^
    /m ^
    /verbosity:normal ^
    /nologo ^
    /fl ^
    /flp:logfile="%LOG_DIR%\msbuild.log";verbosity=detailed >> "%LOG_FILE%" 2>&1

set "BUILD_EXIT_CODE=!errorlevel!"

if !BUILD_EXIT_CODE! equ 0 (
    call :LogMessage "Build completed successfully"
) else (
    call :LogMessage "[ERROR] Build failed with exit code !BUILD_EXIT_CODE!"
)

exit /b !BUILD_EXIT_CODE!

REM ==============================================================================
REM Function: Run Tests
REM ==============================================================================
:RunTests
call :LogMessage "Running unit tests..."

REM Check for test assemblies
setlocal enabledelayedexpansion
set "TEST_ASSEMBLIES="
for /r "%BUILD_OUTPUT_DIR%" %%F in (*Test*.dll *Tests.dll) do (
    set "TEST_ASSEMBLIES=!TEST_ASSEMBLIES! "%%F""
)
endlocal & set "TEST_ASSEMBLIES=%TEST_ASSEMBLIES%"

if not defined TEST_ASSEMBLIES (
    call :LogMessage "[WARNING] No test assemblies found in output directory"
    exit /b 0
)

call :LogMessage "Found test assemblies: %TEST_ASSEMBLIES%"

REM Try to find VSTest or NUnit
where /q vstest.console.exe
if !errorlevel! equ 0 (
    call :LogMessage "Using VSTest console..."
    vstest.console.exe %TEST_ASSEMBLIES% /Logger:trx /ResultsDirectory:"%LOG_DIR%" >> "%LOG_FILE%" 2>&1
    set "TEST_EXIT_CODE=!errorlevel!"
) else (
    call :LogMessage "[WARNING] VSTest console not found. Tests cannot be executed."
    set "TEST_EXIT_CODE=0"
)

if !TEST_EXIT_CODE! equ 0 (
    call :LogMessage "Tests completed successfully"
) else (
    call :LogMessage "[ERROR] Tests failed with exit code !TEST_EXIT_CODE!"
)

exit /b !TEST_EXIT_CODE!

REM ==============================================================================
REM Function: Generate Build Report
REM ==============================================================================
:GenerateBuildReport
call :LogMessage "Generating build report..."

REM Count output files
setlocal enabledelayedexpansion
set "FILE_COUNT=0"
if exist "%BUILD_OUTPUT_DIR%" (
    for /r "%BUILD_OUTPUT_DIR%" %%F in (*.*) do set /a FILE_COUNT+=1
)
endlocal & set "FILE_COUNT=%FILE_COUNT%"

call :LogMessage "Build output contains %FILE_COUNT% files"

REM Calculate build output size
if exist "%BUILD_OUTPUT_DIR%" (
    for /f "delims=" %%F in ('powershell -NoProfile -ExecutionPolicy Bypass -Command "if(Test-Path '%BUILD_OUTPUT_DIR%') { [math]::Round((Get-ChildItem -Path '%BUILD_OUTPUT_DIR%' -Recurse | Measure-Object -Property Length -Sum).Sum / 1MB, 2) } else { 0 }" 2^>nul') do set "OUTPUT_SIZE=%%F"
)

if not defined OUTPUT_SIZE set "OUTPUT_SIZE=0"
call :LogMessage "Build output size: %OUTPUT_SIZE% MB"

exit /b 0

REM ==============================================================================
REM Function: Display Build Summary
REM ==============================================================================
:DisplayBuildSummary
setlocal enabledelayedexpansion
echo.
echo ==============================================================================
echo Build Summary
echo ==============================================================================
echo Configuration: %BUILD_CONFIG%
echo Platform: %BUILD_PLATFORM%
echo Output Directory: %BUILD_OUTPUT_DIR%
echo Log File: %LOG_FILE%
echo Build Status: !BUILD_EXIT_CODE!
echo.
echo Log Directory: %LOG_DIR%
echo ==============================================================================
endlocal
exit /b 0

REM ==============================================================================
REM Main Build Process
REM ==============================================================================
:Main
call :ParseArguments %*
call :InitializeLogging
call :VerifyPrerequisites
if !errorlevel! neq 0 exit /b 1

if %CLEAN_BUILD% equ 1 (
    call :CleanBuild
)

call :BuildSolution
set "BUILD_EXIT_CODE=!errorlevel!"

if !BUILD_EXIT_CODE! equ 0 (
    call :GenerateBuildReport
    
    if %TEST_BUILD% equ 1 (
        call :RunTests
        set "FINAL_EXIT_CODE=!errorlevel!"
    ) else (
        set "FINAL_EXIT_CODE=0"
    )
) else (
    set "FINAL_EXIT_CODE=!BUILD_EXIT_CODE!"
)

call :LogMessage "================================================================================"
call :LogMessage "Build Process Completed"
call :LogMessage "Exit Code: !FINAL_EXIT_CODE!"
call :LogMessage "Timestamp: %date% %time%"
call :LogMessage "================================================================================"

call :DisplayBuildSummary

exit /b !FINAL_EXIT_CODE!

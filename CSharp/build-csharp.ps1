<#
.SYNOPSIS
    Build script for C# projects in Gs_Core repository
.DESCRIPTION
    This script builds the C# project with proper error handling, logging, and diagnostics.
    It supports clean builds, incremental builds, and various logging levels.
.PARAMETER Configuration
    Build configuration: Debug or Release (default: Debug)
.PARAMETER Clean
    Perform a clean build by removing previous build artifacts
.PARAMETER Verbose
    Enable verbose logging output
.PARAMETER LogPath
    Path to log file (default: ./build.log)
.EXAMPLE
    .\build-csharp.ps1 -Configuration Release
    .\build-csharp.ps1 -Clean -Verbose
#>

param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug',
    
    [switch]$Clean,
    
    [switch]$Verbose,
    
    [string]$LogPath = './build.log'
)

# Enable strict error handling
$ErrorActionPreference = 'Stop'
$WarningPreference = 'Continue'

# Constants
$SCRIPT_DIR = Split-Path -Parent $MyInvocation.MyCommand.Path
$SOLUTION_FILE = Get-ChildItem -Path $SCRIPT_DIR -Filter "*.sln" -Depth 0 | Select-Object -First 1
$BUILD_TIMESTAMP = Get-Date -Format 'yyyy-MM-dd HH:mm:ss'
$LOG_FILE = Join-Path -Path $SCRIPT_DIR -ChildPath $LogPath

# Logging function
function Write-Log {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Message,
        
        [ValidateSet('Info', 'Warning', 'Error', 'Success')]
        [string]$Level = 'Info'
    )
    
    $timestamp = Get-Date -Format 'yyyy-MM-dd HH:mm:ss'
    $logMessage = "[$timestamp] [$Level] $Message"
    
    # Write to console with color
    switch ($Level) {
        'Info'    { Write-Host $logMessage -ForegroundColor Cyan }
        'Warning' { Write-Host $logMessage -ForegroundColor Yellow }
        'Error'   { Write-Host $logMessage -ForegroundColor Red }
        'Success' { Write-Host $logMessage -ForegroundColor Green }
    }
    
    # Write to log file
    Add-Content -Path $LOG_FILE -Value $logMessage -ErrorAction SilentlyContinue
}

# Initialize logging
function Initialize-Logging {
    try {
        $logDir = Split-Path -Parent $LOG_FILE
        if (-not (Test-Path $logDir)) {
            New-Item -ItemType Directory -Path $logDir -Force | Out-Null
        }
        
        # Clear previous log file
        if (Test-Path $LOG_FILE) {
            Clear-Content -Path $LOG_FILE
        }
        
        Write-Log "Build script started at $BUILD_TIMESTAMP" 'Info'
        Write-Log "Configuration: $Configuration" 'Info'
        Write-Log "Script directory: $SCRIPT_DIR" 'Info'
    }
    catch {
        Write-Error "Failed to initialize logging: $_"
        exit 1
    }
}

# Check prerequisites
function Test-Prerequisites {
    Write-Log "Checking prerequisites..." 'Info'
    
    # Check if solution file exists
    if ($null -eq $SOLUTION_FILE) {
        Write-Log "No solution file (.sln) found in $SCRIPT_DIR" 'Error'
        exit 1
    }
    
    Write-Log "Found solution: $($SOLUTION_FILE.Name)" 'Info'
    
    # Check if dotnet CLI is installed
    try {
        $dotnetVersion = dotnet --version
        Write-Log "dotnet CLI version: $dotnetVersion" 'Info'
    }
    catch {
        Write-Log "dotnet CLI not found. Please install .NET SDK." 'Error'
        exit 1
    }
    
    # Check if NuGet is accessible
    try {
        dotnet nuget locals all --list | Out-Null
        Write-Log "NuGet configuration verified" 'Info'
    }
    catch {
        Write-Log "Warning: NuGet configuration issue detected" 'Warning'
    }
}

# Clean build artifacts
function Invoke-CleanBuild {
    Write-Log "Cleaning previous build artifacts..." 'Info'
    
    try {
        # Remove bin and obj directories
        $patterns = @('bin', 'obj')
        Get-ChildItem -Path $SCRIPT_DIR -Include $patterns -Recurse -Directory | 
            ForEach-Object {
                Write-Log "Removing directory: $($_.FullName)" 'Info'
                Remove-Item -Path $_.FullName -Recurse -Force
            }
        
        # Clean using dotnet
        $solutionPath = $SOLUTION_FILE.FullName
        & dotnet clean $solutionPath --configuration $Configuration --verbosity minimal
        
        Write-Log "Clean operation completed successfully" 'Success'
    }
    catch {
        Write-Log "Clean operation failed: $_" 'Error'
        exit 1
    }
}

# Restore NuGet packages
function Restore-NuGetPackages {
    Write-Log "Restoring NuGet packages..." 'Info'
    
    try {
        $solutionPath = $SOLUTION_FILE.FullName
        & dotnet restore $solutionPath --verbosity $(if ($Verbose) { 'detailed' } else { 'minimal' })
        
        Write-Log "NuGet package restoration completed successfully" 'Success'
    }
    catch {
        Write-Log "Package restoration failed: $_" 'Error'
        exit 1
    }
}

# Build the project
function Invoke-Build {
    Write-Log "Starting build process for configuration: $Configuration" 'Info'
    
    try {
        $solutionPath = $SOLUTION_FILE.FullName
        $verbosity = if ($Verbose) { 'detailed' } else { 'minimal' }
        
        $buildArgs = @(
            'build',
            $solutionPath,
            '--configuration', $Configuration,
            '--verbosity', $verbosity,
            '--no-restore'
        )
        
        Write-Log "Executing: dotnet $($buildArgs -join ' ')" 'Info'
        & dotnet @buildArgs
        
        if ($LASTEXITCODE -ne 0) {
            Write-Log "Build failed with exit code: $LASTEXITCODE" 'Error'
            exit 1
        }
        
        Write-Log "Build completed successfully" 'Success'
    }
    catch {
        Write-Log "Build process failed: $_" 'Error'
        exit 1
    }
}

# Verify build output
function Test-BuildOutput {
    Write-Log "Verifying build output..." 'Info'
    
    try {
        $binDirs = Get-ChildItem -Path $SCRIPT_DIR -Filter "bin" -Recurse -Directory
        
        if ($binDirs.Count -eq 0) {
            Write-Log "No bin directories found after build" 'Warning'
            return $false
        }
        
        $configPath = Join-Path -Path $binDirs[0] -ChildPath $Configuration
        
        if (Test-Path $configPath) {
            $files = Get-ChildItem -Path $configPath -Recurse
            Write-Log "Build output verified. Found $($files.Count) files in output directory" 'Success'
            return $true
        }
        else {
            Write-Log "Configuration directory not found: $configPath" 'Warning'
            return $false
        }
    }
    catch {
        Write-Log "Error verifying build output: $_" 'Warning'
        return $false
    }
}

# Main execution
function Main {
    try {
        Initialize-Logging
        
        Write-Log "======================================" 'Info'
        Write-Log "C# Build Script Execution Started" 'Info'
        Write-Log "======================================" 'Info'
        
        Test-Prerequisites
        
        if ($Clean) {
            Invoke-CleanBuild
        }
        
        Restore-NuGetPackages
        Invoke-Build
        Test-BuildOutput
        
        Write-Log "======================================" 'Info'
        Write-Log "Build process completed successfully" 'Success'
        Write-Log "======================================" 'Info'
        Write-Log "Log file: $LOG_FILE" 'Info'
        
        exit 0
    }
    catch {
        Write-Log "Unexpected error occurred: $_" 'Error'
        Write-Log "Stack trace: $($_.ScriptStackTrace)" 'Error'
        exit 1
    }
}

# Execute main function
Main

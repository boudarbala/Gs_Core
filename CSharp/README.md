# C# Project Build Guide

This README provides comprehensive instructions for building and developing the C# project within Gs_Core.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Building the Project](#building-the-project)
- [Running Tests](#running-tests)
- [Troubleshooting](#troubleshooting)
- [Development Setup](#development-setup)

## Prerequisites

Before you begin, ensure you have the following installed on your system:

### Required Software

- **.NET SDK** (version 6.0 or later)
  - Download from: https://dotnet.microsoft.com/download
  - Verify installation: `dotnet --version`

- **Visual Studio 2022** or **Visual Studio Code**
  - Visual Studio 2022 (Community, Professional, or Enterprise edition)
  - OR Visual Studio Code with C# Dev Kit extension

- **Git** (for version control)
  - Download from: https://git-scm.com/

### Optional Tools

- **Visual Studio Build Tools** (for CI/CD environments)
- **NuGet** (usually included with .NET SDK)
- **ReSharper** (for advanced code analysis in Visual Studio)

## Project Structure

```
CSharp/
├── src/                    # Source code
├── tests/                  # Unit tests
├── *.csproj               # Project files
├── *.sln                  # Solution file
└── packages.config        # NuGet dependencies (if applicable)
```

## Building the Project

### 1. Clone the Repository

```bash
git clone https://github.com/boudarbala/Gs_Core.git
cd Gs_Core/CSharp
```

### 2. Restore Dependencies

```bash
dotnet restore
```

This command restores all NuGet packages required by the project.

### 3. Build the Project

#### Development Build (with debugging symbols)

```bash
dotnet build
```

#### Release Build (optimized for production)

```bash
dotnet build --configuration Release
```

#### Specific Project Build

If your solution contains multiple projects:

```bash
dotnet build --project <ProjectName>.csproj
```

### 4. Expected Output

Successful builds will display:
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

## Running Tests

### Execute All Tests

```bash
dotnet test
```

### Run Tests with Verbose Output

```bash
dotnet test --verbosity normal
```

### Run Specific Test Class

```bash
dotnet test --filter "FullyQualifiedName~TestClassName"
```

### Generate Code Coverage Report

```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

## Building for Different Configurations

### Target Specific .NET Framework

```bash
dotnet build --framework net6.0
```

### Build with Custom Properties

```bash
dotnet build /p:Version=1.0.0 /p:AssemblyVersion=1.0.0.0
```

## Troubleshooting

### Issue 1: ".NET SDK not found"

**Solution:**
- Verify .NET SDK installation: `dotnet --version`
- Ensure the global.json file (if present) specifies a compatible SDK version
- Reinstall .NET SDK from: https://dotnet.microsoft.com/download

### Issue 2: "NuGet restore fails"

**Solution:**
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Try restore again
dotnet restore
```

### Issue 3: Build fails with "Package not found"

**Solution:**
- Check internet connectivity
- Verify NuGet.config is correctly configured
- Try restoring with a specific source:
```bash
dotnet restore --source https://api.nuget.org/v3/index.json
```

### Issue 4: "Project file not found"

**Solution:**
```bash
# List available projects
ls -la *.csproj

# Verify you're in the correct directory
pwd
```

### Issue 5: Build succeeds but tests fail

**Solution:**
- Ensure all test dependencies are installed
- Check test configuration files
- Review test output for detailed error messages
```bash
dotnet test --verbosity detailed
```

### Issue 6: "The type or namespace name 'X' does not exist"

**Solution:**
- Verify all project references are correctly configured
- Ensure using statements are present in source files
- Rebuild the solution: `dotnet clean && dotnet build`

### Issue 7: Port already in use (if running services)

**Solution:**
```bash
# Kill process on specific port (Linux/macOS)
lsof -i :PORT_NUMBER | grep LISTEN | awk '{print $2}' | xargs kill -9

# Or (Windows)
netstat -ano | findstr :PORT_NUMBER
taskkill /PID <PID> /F
```

## Development Setup

### Visual Studio Code Setup

1. Install extensions:
   - C# (powered by OmniSharp)
   - C# Dev Kit
   - NUnit Test Adapter (optional)

2. Create launch configuration (`.vscode/launch.json`):
```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (console)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/bin/Debug/net6.0/YourApp.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false,
            "console": "internalConsole"
        }
    ]
}
```

### Visual Studio Setup

1. Open the solution file (`.sln`) in Visual Studio 2022
2. Solution will automatically restore NuGet packages
3. Build the solution: `Build > Build Solution` or `Ctrl+Shift+B`
4. Run tests: `Test > Run All Tests` or `Ctrl+R, A`

### Code Style & Analysis

Ensure code follows project conventions:

```bash
# Run code analysis (if configured)
dotnet analyzers run

# Format code
dotnet format
```

## Clean Build

To perform a clean rebuild (removing previous build artifacts):

```bash
dotnet clean
dotnet build
```

Or combine in one command:

```bash
dotnet clean && dotnet build
```

## Continuous Integration

For CI/CD pipelines, use:

```bash
dotnet build --configuration Release
dotnet test --configuration Release --no-build --verbosity normal
```

## Additional Resources

- [Microsoft .NET Documentation](https://docs.microsoft.com/dotnet/)
- [NuGet Package Management](https://www.nuget.org/)
- [C# Language Reference](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Visual Studio Documentation](https://docs.microsoft.com/en-us/visualstudio/)

## Support

If you encounter issues not covered in this guide:

1. Check the main repository README
2. Review existing GitHub issues
3. Create a new issue with:
   - Detailed error messages
   - .NET SDK version output
   - OS and development environment information
   - Steps to reproduce the problem

---

**Last Updated:** 2025-12-27

# C# Quick Start Guide

Welcome to the C# development environment for Gs_Core! This guide will help you get started with building and running C# projects quickly.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Project Setup](#project-setup)
- [Building the Project](#building-the-project)
- [Running the Project](#running-the-project)
- [Running Tests](#running-tests)
- [Common Commands](#common-commands)
- [Troubleshooting](#troubleshooting)
- [Project Structure](#project-structure)
- [Best Practices](#best-practices)

## Prerequisites

Before you begin, ensure you have the following installed:

### Required

- **.NET SDK** (version 6.0 or higher recommended)
  - Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)
  - Verify installation: `dotnet --version`

- **Git**
  - Download from [git-scm.com](https://git-scm.com)
  - Verify installation: `git --version`

### Recommended

- **Visual Studio Code** or **Visual Studio Community** (free)
  - [Visual Studio Code](https://code.visualstudio.com)
  - [Visual Studio Community](https://visualstudio.microsoft.com/community)
  
- **C# Extension for VS Code** (if using VS Code)
  - Install from Extensions marketplace

## Project Setup

### 1. Clone the Repository

```bash
git clone https://github.com/boudarbala/Gs_Core.git
cd Gs_Core
cd CSharp
```

### 2. Restore Dependencies

Navigate to your project directory and restore NuGet packages:

```bash
dotnet restore
```

This command downloads and installs all required dependencies specified in the project file.

### 3. Verify Setup

Check that everything is configured correctly:

```bash
dotnet --version
dotnet list package
```

## Building the Project

### Debug Build

Create a debug build with debugging symbols (useful for development):

```bash
dotnet build
```

Output will be in the `bin/Debug/` directory.

### Release Build

Create an optimized release build:

```bash
dotnet build --configuration Release
```

Output will be in the `bin/Release/` directory.

### Clean Build

Remove previous builds and start fresh:

```bash
dotnet clean
dotnet build
```

### Build Specific Project (in multi-project solutions)

```bash
dotnet build -p:ProjectName=YourProjectName
```

## Running the Project

### Run Console Application

For console applications:

```bash
dotnet run
```

Or with arguments:

```bash
dotnet run -- arg1 arg2
```

### Run with Configuration

```bash
dotnet run --configuration Release
```

### Run Without Building

If already built:

```bash
dotnet run --no-build
```

### Run Specific Project (multi-project solution)

```bash
dotnet run --project ./ProjectFolder/ProjectName.csproj
```

## Running Tests

### Run All Tests

```bash
dotnet test
```

### Run Tests with Verbose Output

```bash
dotnet test --verbosity normal
```

### Run Specific Test Class

```bash
dotnet test --filter ClassName=YourTestClassName
```

### Run Tests with Code Coverage

```bash
dotnet test /p:CollectCoverage=true
```

### Generate Test Report

```bash
dotnet test --logger "html" --results-directory "TestResults"
```

## Common Commands

| Command | Purpose |
|---------|---------|
| `dotnet new console` | Create new console application |
| `dotnet new classlib` | Create new class library |
| `dotnet add package <package>` | Add NuGet package |
| `dotnet remove package <package>` | Remove NuGet package |
| `dotnet list package --outdated` | Check for outdated packages |
| `dotnet publish` | Publish application |
| `dotnet tool list -g` | List global tools |
| `dotnet format` | Format code (if available) |

## Troubleshooting

### Issue: "dotnet: command not found"

**Solution:** Ensure .NET SDK is installed and in your PATH. Reinstall from [dotnet.microsoft.com](https://dotnet.microsoft.com/download).

### Issue: NuGet Package Resolution Error

**Solution:** 
```bash
dotnet nuget locals all --clear
dotnet restore
```

### Issue: Build Fails with Version Mismatch

**Solution:** Check the `global.json` file (if present) for SDK version requirements:
```bash
dotnet --list-sdks
```

### Issue: Port Already in Use (for web applications)

**Solution:** 
```bash
# Change port in launchSettings.json or:
dotnet run -- --urls "http://localhost:5001"
```

### Issue: Tests Not Found

**Solution:** Ensure test project references the testing framework (xUnit, NUnit, or MSTest):
```bash
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
```

## Project Structure

Typical C# project structure:

```
Gs_Core/
├── CSharp/
│   ├── src/
│   │   ├── YourProject/
│   │   │   ├── Program.cs
│   │   │   ├── YourProject.csproj
│   │   │   └── ...
│   │   └── ...
│   ├── tests/
│   │   ├── YourProject.Tests/
│   │   │   ├── YourProject.Tests.csproj
│   │   │   └── ...
│   │   └── ...
│   ├── YourProject.sln (Solution file)
│   └── QUICKSTART.md (This file)
├── README.md
└── ...
```

## Best Practices

### 1. Use Solution Files

Organize multiple projects in a solution file (`.sln`):

```bash
dotnet new sln -n YourSolution
dotnet sln add src/Project1/Project1.csproj
dotnet sln add tests/Project1.Tests/Project1.Tests.csproj
```

### 2. Follow Naming Conventions

- **Classes:** PascalCase (e.g., `MyClass`)
- **Properties:** PascalCase (e.g., `MyProperty`)
- **Methods:** PascalCase (e.g., `MyMethod`)
- **Variables:** camelCase (e.g., `myVariable`)
- **Constants:** UPPER_CASE (e.g., `MAX_SIZE`)

### 3. Use Async/Await for I/O Operations

```csharp
public async Task<string> FetchDataAsync()
{
    // Implementation
}
```

### 4. Write Unit Tests

Keep test files in a separate `tests` directory with the same namespace structure.

### 5. Use Dependency Injection

For .NET 6+, use the built-in DI container:

```csharp
var services = new ServiceCollection();
services.AddScoped<IMyService, MyService>();
```

### 6. Keep Dependencies Updated

Regularly check and update packages:

```bash
dotnet list package --outdated
dotnet add package <PackageName> --version <NewVersion>
```

### 7. Use Code Formatting

Apply consistent formatting:

```bash
dotnet format
```

### 8. Enable Nullable Reference Types

Add to your `.csproj` for better null safety:

```xml
<PropertyGroup>
  <Nullable>enable</Nullable>
</PropertyGroup>
```

## Additional Resources

- [Official .NET Documentation](https://docs.microsoft.com/dotnet)
- [C# Language Guide](https://docs.microsoft.com/en-us/dotnet/csharp)
- [.NET CLI Reference](https://docs.microsoft.com/en-us/dotnet/core/tools)
- [NuGet Package Manager](https://www.nuget.org)
- [Microsoft Learn - C# Path](https://docs.microsoft.com/learn/paths/csharp-first-steps)

## Getting Help

If you encounter issues:

1. Check the [Troubleshooting](#troubleshooting) section
2. Review project-specific documentation
3. Search GitHub issues in this repository
4. Consult official .NET documentation

---

**Happy coding!** 🚀

For more information about Gs_Core, see the main [README.md](../README.md).

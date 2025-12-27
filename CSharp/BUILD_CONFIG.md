# BUILD_CONFIG.md - C# Build Configuration and Optimization Guide

**Last Updated:** 2025-12-27 15:41:37 UTC

---

## Table of Contents

1. [Overview](#overview)
2. [Prerequisites](#prerequisites)
3. [Build Configurations](#build-configurations)
4. [Compiler Settings](#compiler-settings)
5. [Optimization Strategies](#optimization-strategies)
6. [Performance Tuning](#performance-tuning)
7. [Code Analysis and Quality](#code-analysis-and-quality)
8. [Troubleshooting](#troubleshooting)
9. [Best Practices](#best-practices)

---

## Overview

This document provides comprehensive guidance for building, configuring, and optimizing the Gs_Core C# project. It covers build configurations, compiler settings, optimization techniques, and performance tuning strategies to ensure efficient development and production deployments.

**Key Goals:**
- Maintain consistent build quality across development and production environments
- Optimize build times and application performance
- Enforce code quality standards
- Enable seamless CI/CD integration

---

## Prerequisites

### System Requirements
- **.NET Framework/Core Version:** .NET 6.0 or higher (recommended: .NET 8.0+)
- **Visual Studio:** Visual Studio 2022 or later
- **Alternative IDEs:** JetBrains Rider, Visual Studio Code with C# extensions
- **Build Tools:** MSBuild 17.0+

### Required Tools
```bash
# Install .NET SDK
dotnet --version

# Verify MSBuild
msbuild --version

# NuGet CLI (optional but recommended)
nuget help
```

### Environment Setup
```bash
# Set environment variables for build optimization
SET DOTNET_ENABLE_TIERED_COMPILATION=1
SET DOTNET_TieredCompilation=1
SET DOTNET_SKIP_FIRST_TIME_SOURCE_GEN=0
```

---

## Build Configurations

### Debug Configuration
**Purpose:** Development and debugging with full diagnostic information

**Properties:**
- Optimization Level: Debug
- Define Constants: `DEBUG;TRACE`
- Debug Info: Full
- Symbols: Embedded

**Configuration File (`.csproj` snippet):**
```xml
<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
  <DebugSymbols>true</DebugSymbols>
  <DebugType>full</DebugType>
  <Optimize>false</Optimize>
  <DefineConstants>DEBUG;TRACE</DefineConstants>
  <ErrorReport>prompt</ErrorReport>
  <WarningLevel>4</WarningLevel>
</PropertyGroup>
```

**Build Command:**
```bash
dotnet build --configuration Debug
```

### Release Configuration
**Purpose:** Production builds with optimizations and minimal overhead

**Properties:**
- Optimization Level: Release
- Define Constants: `RELEASE;TRACE`
- Debug Info: PDBOnly
- Deterministic Build: Enabled

**Configuration File (`.csproj` snippet):**
```xml
<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
  <DebugSymbols>true</DebugSymbols>
  <DebugType>pdbonly</DebugType>
  <Optimize>true</Optimize>
  <DefineConstants>RELEASE;TRACE</DefineConstants>
  <ErrorReport>prompt</ErrorReport>
  <WarningLevel>4</WarningLevel>
  <Deterministic>true</Deterministic>
</PropertyGroup>
```

**Build Command:**
```bash
dotnet build --configuration Release
```

### Custom Configurations
Create platform-specific configurations (x86, x64, ARM64) as needed:

```xml
<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|x64'">
  <PlatformTarget>x64</PlatformTarget>
  <Optimize>true</Optimize>
</PropertyGroup>
```

---

## Compiler Settings

### Target Framework Configuration
```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <LangVersion>latest</LangVersion>
  <Nullable>enable</Nullable>
</PropertyGroup>
```

### Compiler Warnings and Errors
```xml
<PropertyGroup>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  <WarningLevel>4</WarningLevel>
  <NoWarn>0618</NoWarn>
  <NullableReferenceTypes>enable</NullableReferenceTypes>
</PropertyGroup>
```

### IL Configuration
```xml
<PropertyGroup>
  <PublishTrimmed>true</PublishTrimmed>
  <PublishReadyToRun>true</PublishReadyToRun>
  <PublishSingleFile>false</PublishSingleFile>
  <SelfContained>true</SelfContained>
</PropertyGroup>
```

### Source Generators
```xml
<PropertyGroup>
  <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

---

## Optimization Strategies

### 1. Compilation Optimization

#### Incremental Compilation
```bash
# Enable incremental compilation for faster builds
dotnet build --incremental
```

#### Parallel Build
```bash
# Build with multiple cores
dotnet build -p:BuildInParallel=true -m
```

#### Deterministic Builds
```xml
<PropertyGroup>
  <Deterministic>true</Deterministic>
  <ContinuousIntegrationBuild>true</ContinuousIntegrationBuild>
</PropertyGroup>
```

### 2. Runtime Optimization

#### Tiered JIT Compilation
```xml
<PropertyGroup>
  <TieredCompilation>true</TieredCompilation>
  <TieredCompilationQuickJit>true</TieredCompilationQuickJit>
</PropertyGroup>
```

**Environment Variable:**
```bash
DOTNET_TieredCompilation=1
DOTNET_TieredCompilationQuickJit=1
```

#### Ahead-of-Time (AOT) Compilation
```bash
dotnet publish -c Release -r win-x64 /p:PublishAot=true
```

#### Ready-to-Run (R2R) Compilation
```xml
<PropertyGroup>
  <PublishReadyToRun>true</PublishReadyToRun>
</PropertyGroup>
```

### 3. Trimming and Size Optimization

#### Enable Trimming
```xml
<PropertyGroup>
  <PublishTrimmed>true</PublishTrimmed>
</PropertyGroup>
```

#### Trim Configuration
```xml
<ItemGroup>
  <TrimmerRootAssembly Include="Gs_Core" />
  <TrimmerRootAssembly Include="YourMainAssembly" />
</ItemGroup>
```

### 4. Assembly Optimization

#### Inlining Hints
```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int GetValue() => _value;
```

#### Loop Unrolling
```xml
<PropertyGroup>
  <TieredCompilationQuickJitForLoops>true</TieredCompilationQuickJitForLoops>
</PropertyGroup>
```

---

## Performance Tuning

### Memory Optimization

#### Garbage Collection Settings
```xml
<PropertyGroup>
  <ServerGarbageCollection>true</ServerGarbageCollection>
  <ConcurrentGarbageCollection>true</ConcurrentGarbageCollection>
  <RetainVMMemory>true</RetainVMMemory>
</PropertyGroup>
```

**Environment Variables:**
```bash
DOTNET_GCServer=1
DOTNET_GCConcurrent=1
DOTNET_GCRetainVM=1
DOTNET_GCGEN0SIZE=262144
```

#### Object Pooling
Implement object pooling for frequently allocated objects:

```csharp
using System.Collections.Generic;

public class ObjectPool<T> where T : class, new()
{
    private readonly Stack<T> _pool = new();
    private readonly int _maxSize;

    public ObjectPool(int maxSize = 100)
    {
        _maxSize = maxSize;
    }

    public T Rent()
    {
        return _pool.Count > 0 ? _pool.Pop() : new T();
    }

    public void Return(T item)
    {
        if (_pool.Count < _maxSize)
            _pool.Push(item);
    }
}
```

### CPU Optimization

#### CPU Affinity
```csharp
// Pin threads to specific CPU cores
System.Diagnostics.ProcessThread thread = System.Diagnostics.Process.GetCurrentProcess().Threads[0];
thread.ProcessorAffinity = new IntPtr(0x1); // CPU 0
```

#### Thread Pool Configuration
```csharp
ThreadPool.GetMinThreads(out int workerThreads, out int ioThreads);
ThreadPool.SetMinThreads(Environment.ProcessorCount, ioThreads);
```

### I/O Optimization

#### Async/Await Best Practices
```csharp
// Good: Use async operations
public async Task<string> ReadFileAsync(string path)
{
    using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
    using var reader = new StreamReader(stream);
    return await reader.ReadToEndAsync();
}

// Avoid: Sync-over-Async anti-pattern
public string ReadFile(string path)
{
    return ReadFileAsync(path).Result; // WRONG!
}
```

#### Stream Buffering
```csharp
const int bufferSize = 65536; // 64KB
using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize);
```

---

## Code Analysis and Quality

### Static Code Analysis

#### Enable Roslyn Analyzers
```xml
<PropertyGroup>
  <EnableNETAnalyzers>true</EnableNETAnalyzers>
  <AnalysisLevel>latest</AnalysisLevel>
  <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
</PropertyGroup>
```

#### Custom Analyzer Rules
Create `.editorconfig` file:
```ini
[*.cs]
# Code style rules
csharp_indent_case_contents = true
csharp_space_after_keywords_in_control_flow_statements = true

# Naming conventions
dotnet_naming_rule.interfaces_should_be_begins_with_i.severity = suggestion
dotnet_naming_rule.interfaces_should_be_begins_with_i.symbols = interface
dotnet_naming_rule.interfaces_should_be_begins_with_i.style = begins_with_i

dotnet_naming_symbols.interface.applicable_kinds = interface
dotnet_naming_style.begins_with_i.required_prefix = I
dotnet_naming_style.begins_with_i.capitalization = pascal_case
```

### Code Coverage

#### Configure Code Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

#### Coverage Reports
```xml
<PropertyGroup>
  <CollectCoverage>true</CollectCoverage>
  <CoverletOutputFormat>opencover</CoverletOutputFormat>
  <Exclude>[Gs_Core.Tests]*</Exclude>
</PropertyGroup>
```

### Testing Configuration

#### Unit Test Framework
```xml
<ItemGroup>
  <PackageReference Include="xunit" Version="2.6.3" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.5.1" />
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
</ItemGroup>
```

#### Test Run Configuration
```bash
dotnet test --configuration Release --parallel --logger "console;verbosity=minimal"
```

---

## Troubleshooting

### Common Build Issues

#### Issue: Build Timeout
**Solution:**
```bash
dotnet build --tl:off  # Disable multi-core compilation
dotnet build -p:BuildInParallel=false
```

#### Issue: Out of Memory During Build
**Solution:**
```bash
# Reduce parallel build threads
dotnet build -p:BuildInParallel=false -m:1

# Clear NuGet cache
dotnet nuget locals all --clear
```

#### Issue: Incremental Build Problems
**Solution:**
```bash
# Full rebuild
dotnet clean
dotnet build --configuration Release
```

#### Issue: Conflicting Package Versions
**Solution:**
```bash
# Check dependencies
dotnet list package --vulnerable

# Update packages
dotnet package update --interactive
```

### Debugging Build Process

#### Enable Verbose Logging
```bash
dotnet build --verbosity diagnostic
msbuild /v:diag
```

#### Binary Log Analysis
```bash
dotnet build /bl:build.binlog
# Analyze with MSBuild Structured Log Viewer
```

### Performance Diagnostics

#### Profile Build Time
```bash
# Using dotnet-build-times tool
dotnet tool install -g dotnet-build-times
dotnet build-times
```

#### Memory Usage Analysis
```bash
dotnet publish -c Release --self-contained
dotnet PerfView collect -MaxCollectSec 30 -Rundown GC
```

---

## Best Practices

### 1. Build Configuration Management

✅ **DO:**
- Keep separate Debug and Release configurations
- Use conditional compilation for platform-specific code
- Document custom build properties
- Automate build scripts in CI/CD pipeline

❌ **DON'T:**
- Modify global NuGet config in production builds
- Mix Debug and Release optimizations
- Ignore compiler warnings
- Commit temporary build artifacts

### 2. Dependency Management

✅ **DO:**
```xml
<PropertyGroup>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  <AnalysisLevel>latest</AnalysisLevel>
</PropertyGroup>

<ItemGroup>
  <PackageReference Include="PackageName" Version="1.0.0" />
  <!-- Use explicit versions -->
</ItemGroup>
```

❌ **DON'T:**
```xml
<PackageReference Include="PackageName" Version="*" />
<!-- Avoid wildcard versions -->
```

### 3. Code Quality

✅ **DO:**
- Use nullable reference types
- Enable strict compiler warnings
- Implement static code analysis
- Run tests before commit
- Use source generators for boilerplate

❌ **DON'T:**
- Suppress warnings without justification
- Use dynamic when static typing is possible
- Ignore security warnings from analyzers
- Skip code reviews

### 4. Performance

✅ **DO:**
```csharp
// Use ValueTask for async operations with low-latency requirements
public ValueTask<int> GetValueAsync() => new(_value);

// Use StringBuilder for string concatenation in loops
var sb = new StringBuilder();
foreach (var item in items)
{
    sb.Append(item);
}

// Use spans for zero-copy operations
Span<byte> buffer = stackalloc byte[256];
```

❌ **DON'T:**
```csharp
// Avoid boxing value types
object value = 42; // Boxing!

// Avoid string concatenation in loops
string result = "";
foreach (var item in items)
{
    result += item; // Inefficient!
}
```

### 5. Documentation

✅ **DO:**
- Document all public APIs with XML comments
- Keep README synchronized with build process
- Maintain changelog of optimization changes
- Include performance benchmarks

❌ **DON'T:**
- Leave code undocumented
- Ignore breaking changes in updates
- Make optimizations without benchmarking

---

## Build Automation Scripts

### PowerShell Build Script (Windows)
```powershell
param(
    [string]$Configuration = "Release",
    [switch]$Clean,
    [switch]$Test,
    [switch]$Publish
)

$ErrorActionPreference = "Stop"

Write-Host "Starting build process..." -ForegroundColor Green

if ($Clean) {
    Write-Host "Cleaning solution..." -ForegroundColor Yellow
    dotnet clean --configuration $Configuration
}

Write-Host "Restoring dependencies..." -ForegroundColor Yellow
dotnet restore

Write-Host "Building project..." -ForegroundColor Yellow
dotnet build --configuration $Configuration --no-restore

if ($Test) {
    Write-Host "Running tests..." -ForegroundColor Yellow
    dotnet test --configuration $Configuration --no-build
}

if ($Publish) {
    Write-Host "Publishing project..." -ForegroundColor Yellow
    dotnet publish --configuration $Configuration --no-build
}

Write-Host "Build completed successfully!" -ForegroundColor Green
```

### Bash Build Script (Linux/macOS)
```bash
#!/bin/bash
set -e

CONFIGURATION=${1:-Release}
CLEAN=${2:-false}
TEST=${3:-false}

echo "Starting build process..."

if [ "$CLEAN" = "true" ]; then
    echo "Cleaning solution..."
    dotnet clean --configuration "$CONFIGURATION"
fi

echo "Restoring dependencies..."
dotnet restore

echo "Building project..."
dotnet build --configuration "$CONFIGURATION" --no-restore

if [ "$TEST" = "true" ]; then
    echo "Running tests..."
    dotnet test --configuration "$CONFIGURATION" --no-build
fi

echo "Build completed successfully!"
```

---

## CI/CD Integration

### GitHub Actions Example
```yaml
name: Build and Test

on: [push, pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    strategy:
      matrix:
        configuration: [Debug, Release]

    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '8.0.x'
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --configuration ${{ matrix.configuration }} --no-restore
    
    - name: Test
      run: dotnet test --configuration ${{ matrix.configuration }} --no-build --verbosity normal
    
    - name: Publish
      if: matrix.configuration == 'Release' && github.event_name == 'push'
      run: dotnet publish --configuration Release --output ./publish
```

---

## References and Resources

- [Microsoft .NET Documentation](https://docs.microsoft.com/dotnet)
- [Performance Best Practices](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/performance)
- [MSBuild Reference](https://docs.microsoft.com/visualstudio/msbuild/msbuild-reference)
- [C# Language Reference](https://docs.microsoft.com/dotnet/csharp/)
- [.NET Runtime Documentation](https://github.com/dotnet/runtime)

---

## Version History

| Date | Version | Author | Changes |
|------|---------|--------|---------|
| 2025-12-27 | 1.0.0 | Gs_Core Team | Initial comprehensive build configuration guide |

---

**Last Reviewed:** 2025-12-27 15:41:37 UTC

For questions or contributions, please open an issue or contact the development team.

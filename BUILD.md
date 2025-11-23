# Build Instructions

## Prerequisites

### Install .NET 8.0 SDK

#### Windows
```powershell
# Using winget
winget install Microsoft.DotNet.SDK.8

# Or download from
# https://dotnet.microsoft.com/download/dotnet/8.0
```

#### Linux (Ubuntu/Debian)
```bash
# Add Microsoft repository
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Install SDK
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

#### Linux (Fedora/RHEL/CentOS)
```bash
sudo dnf install dotnet-sdk-8.0
```

#### macOS
```bash
# Using Homebrew
brew install dotnet-sdk

# Or download from
# https://dotnet.microsoft.com/download/dotnet/8.0
```

### Verify Installation
```bash
dotnet --version
# Should output: 8.0.x
```

## Building the Project

### Quick Build
```bash
# From the Shadow-Labyrinth directory
dotnet build ShadowLabyrinth.sln
```

### Release Build (Optimized)
```bash
dotnet build ShadowLabyrinth.sln -c Release
```

### Clean and Rebuild
```bash
dotnet clean
dotnet build
```

## Running the Server

### Development Mode
```bash
dotnet run --project Server
```

### Production Mode
```bash
# Build first
dotnet build -c Release

# Run the compiled executable
cd Server/bin/Release/net8.0
dotnet ShadowLabyrinth.dll
```

### With Custom Configuration
```bash
# Edit config.json first
dotnet run --project Server
```

## Build Output

After building, you'll find:

```
Server/bin/Debug/net8.0/
├── ShadowLabyrinth.dll       # Main executable
├── ShadowLabyrinth.pdb       # Debug symbols
├── Scripts.dll               # Scripts assembly
├── Scripts.pdb               # Scripts debug symbols
└── config.json               # Auto-generated config
```

## Common Build Issues

### Issue: "SDK not found"
**Solution**: Install .NET 8.0 SDK (see Prerequisites)

### Issue: "Project not found"
**Solution**: Ensure you're in the Shadow-Labyrinth directory
```bash
cd /path/to/Shadow-Labyrinth
dotnet build
```

### Issue: Build errors in Scripts
**Solution**: Build Server project first
```bash
dotnet build Server/Server.csproj
dotnet build Scripts/Scripts.csproj
```

### Issue: Missing dependencies
**Solution**: Restore NuGet packages
```bash
dotnet restore
dotnet build
```

## Advanced Build Options

### Self-Contained Deployment
```bash
# Windows x64
dotnet publish -c Release -r win-x64 --self-contained

# Linux x64
dotnet publish -c Release -r linux-x64 --self-contained

# macOS ARM64
dotnet publish -c Release -r osx-arm64 --self-contained
```

### Single File Executable
```bash
dotnet publish -c Release -r linux-x64 \
  --self-contained \
  -p:PublishSingleFile=true \
  -p:EnableCompressionInSingleFile=true
```

### Trimmed Build (Smaller Size)
```bash
dotnet publish -c Release -r linux-x64 \
  --self-contained \
  -p:PublishTrimmed=true \
  -p:TrimMode=link
```

## Performance Profiling

### Using dotnet-counters
```bash
# Install
dotnet tool install --global dotnet-counters

# Monitor running server
dotnet-counters monitor --process-id <PID>
```

### Using dotnet-trace
```bash
# Install
dotnet tool install --global dotnet-trace

# Collect trace
dotnet-trace collect --process-id <PID>
```

## Testing

### Run Unit Tests (when implemented)
```bash
dotnet test
```

### Run with Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Continuous Integration

### GitHub Actions Example
```yaml
name: Build

on: [push, pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x
    - name: Restore
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build
```

## Docker Build

### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build -c Release

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /src/Server/bin/Release/net8.0 .
COPY --from=build /src/Scripts/bin/Release/net8.0/Scripts.dll .
ENTRYPOINT ["dotnet", "ShadowLabyrinth.dll"]
```

### Build and Run
```bash
docker build -t shadow-labyrinth .
docker run -p 2593:2593 shadow-labyrinth
```

## Development Workflow

### Recommended VS Code Extensions
- C# Dev Kit
- .NET Extension Pack
- C# XML Documentation

### Recommended Visual Studio Setup
- Visual Studio 2022 or later
- .NET Desktop Development workload
- C# and .NET development tools

### Recommended JetBrains Rider Setup
- Rider 2023.3 or later
- Default C# settings

## Build Verification

After building, verify everything works:

```bash
# Check assemblies
ls -lh Server/bin/Debug/net8.0/
ls -lh Scripts/bin/Debug/net8.0/

# Verify dependencies
dotnet list package

# Check for outdated packages
dotnet list package --outdated
```

## Updating Dependencies

```bash
# Update all packages
dotnet list package --outdated
dotnet add package <PackageName>

# Or update all at once
dotnet outdated -u
```

## Build Performance Tips

1. **Use NuGet Package Cache**: Shared across projects
2. **Incremental Builds**: Only changed files rebuild
3. **Parallel Builds**: `-m` flag (automatic in .NET 8)
4. **Binary Log**: `-bl` for build analysis

```bash
dotnet build -bl:build.binlog
# Analyze with https://msbuildlog.com/
```

---

For issues, see [Troubleshooting](#troubleshooting) in README.md

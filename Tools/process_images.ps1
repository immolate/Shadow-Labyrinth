# Shadow Labyrinth - Image Post-Processor
# Processes AI-generated images for UO import

param(
    [string]$InputDir = "./generated",
    [string]$OutputDir = "./processed",
    [int]$TargetSize = 44,
    [switch]$KeepAspect = $true
)

$ErrorActionPreference = "Stop"

Write-Host "=" * 60 -ForegroundColor Cyan
Write-Host "Shadow Labyrinth - Image Post-Processor" -ForegroundColor Cyan
Write-Host "=" * 60 -ForegroundColor Cyan
Write-Host ""

# Check for ImageMagick
$magick = Get-Command magick -ErrorAction SilentlyContinue
if (-not $magick) {
    Write-Host "ERROR: ImageMagick not found!" -ForegroundColor Red
    Write-Host "Please install ImageMagick from: https://imagemagick.org/script/download.php" -ForegroundColor Yellow
    Write-Host "Or use: winget install ImageMagick.ImageMagick" -ForegroundColor Yellow
    exit 1
}

Write-Host "[OK] ImageMagick found: $($magick.Source)" -ForegroundColor Green
Write-Host ""

# Create output directory
if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null
    Write-Host "[CREATED] Output directory: $OutputDir" -ForegroundColor Green
}

# Get all PNG files
$files = Get-ChildItem $InputDir -Filter "*.png" -ErrorAction SilentlyContinue

if ($files.Count -eq 0) {
    Write-Host "WARNING: No PNG files found in $InputDir" -ForegroundColor Yellow
    Write-Host "Please place AI-generated images in: $InputDir" -ForegroundColor Yellow
    exit 1
}

Write-Host "Found $($files.Count) images to process" -ForegroundColor Cyan
Write-Host ""

$processed = 0
$failed = 0

foreach ($file in $files) {
    $inputFile = $file.FullName
    $outputFile = Join-Path $OutputDir $file.Name

    Write-Host "Processing: " -NoNewline
    Write-Host $file.Name -ForegroundColor Yellow

    try {
        # Process image
        $args = @(
            "convert"
            $inputFile
            "-background", "none"
            "-alpha", "set"
        )

        if ($KeepAspect) {
            $args += "-resize", "${TargetSize}x${TargetSize}>"
            $args += "-gravity", "center"
            $args += "-extent", "${TargetSize}x${TargetSize}"
        } else {
            $args += "-resize", "${TargetSize}x${TargetSize}!"
        }

        $args += @(
            "-colors", "256"
            "-depth", "8"
            $outputFile
        )

        & magick @args 2>&1 | Out-Null

        if ($LASTEXITCODE -eq 0) {
            Write-Host "  ✓ Success" -ForegroundColor Green
            $processed++
        } else {
            Write-Host "  ✗ Failed (exit code: $LASTEXITCODE)" -ForegroundColor Red
            $failed++
        }
    }
    catch {
        Write-Host "  ✗ Error: $($_.Exception.Message)" -ForegroundColor Red
        $failed++
    }
}

Write-Host ""
Write-Host "=" * 60 -ForegroundColor Cyan
Write-Host "Processing Complete!" -ForegroundColor Green
Write-Host "  Processed: $processed" -ForegroundColor Green
Write-Host "  Failed: $failed" -ForegroundColor $(if ($failed -eq 0) { "Green" } else { "Red" })
Write-Host "  Output: $OutputDir" -ForegroundColor Cyan
Write-Host "=" * 60 -ForegroundColor Cyan

# Shadow Labyrinth - Graphics Pipeline Guide

Complete guide for creating and importing 250 item graphics into UO.

## Overview

This pipeline integrates:
1. **AI Image Generation** (DALL-E, Midjourney, Stable Diffusion)
2. **Image Processing** (resizing, formatting)
3. **MUL File Import** (custom C# tools)
4. **UO Fiddler Integration** (optional manual editing)

---

## Pipeline Architecture

```
[AI Image Generator]
        ↓
    [PNG Files]
        ↓
  [Post-Processing]
        ↓
    [MUL Tools]
        ↓
  [art.mul/artidx.mul]
        ↓
  [UO Client/Server]
```

---

## Step-by-Step Process

### Phase 1: AI Image Generation

#### Option A: Using DALL-E 3 (Recommended for Quality)

1. **Open AI_IMAGE_PROMPTS.md**
2. **Copy prompts one by one**
3. **Generate images:**
   ```
   Prompt: [Copy from AI_IMAGE_PROMPTS.md]
   Size: 1024x1024 (will downscale)
   Style: Pixel art, isometric
   ```

4. **Download all generated images**
5. **Name files:** `item_13FD.png`, `item_0F62.png`, etc.

#### Option B: Using Midjourney

```bash
/imagine [prompt from AI_IMAGE_PROMPTS.md] --ar 1:1 --v 6 --style raw --quality 2
```

Download and rename files to match item IDs.

#### Option C: Using Stable Diffusion (Local/Free)

1. **Install Stable Diffusion WebUI**
2. **Use model:** "Pixel Art XL" or "IsometricGame"
3. **Settings:**
   - Size: 512x512
   - Steps: 50
   - CFG Scale: 7
   - Sampler: DPM++ 2M Karras

4. **Batch generate** using prompts from AI_IMAGE_PROMPTS.md

---

### Phase 2: Post-Processing

#### Automated Batch Processing (PowerShell Script)

Create `process_images.ps1`:

```powershell
# Shadow Labyrinth Image Post-Processor
param(
    [string]$InputDir = "./generated",
    [string]$OutputDir = "./processed"
)

# Requires ImageMagick installed
$ErrorActionPreference = "Stop"

Write-Host "Processing images..." -ForegroundColor Cyan

New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null

Get-ChildItem $InputDir -Filter "*.png" | ForEach-Object {
    $inputFile = $_.FullName
    $outputFile = Join-Path $OutputDir $_.Name

    Write-Host "Processing: $($_.Name)" -ForegroundColor Yellow

    # Resize to 44x44, maintain aspect ratio, add transparency
    magick convert $inputFile `
        -resize 44x44 `
        -background none `
        -gravity center `
        -extent 44x44 `
        -colors 256 `
        $outputFile

    Write-Host "  ✓ Processed: $outputFile" -ForegroundColor Green
}

Write-Host "`nProcessing complete!" -ForegroundColor Green
Write-Host "Output directory: $OutputDir"
```

#### Manual Processing (GIMP/Photoshop)

For each image:
1. **Open** in image editor
2. **Resize** to 44x44 pixels
3. **Ensure** transparent background
4. **Convert** to indexed color (256 colors)
5. **Apply** isometric perspective if needed
6. **Add** top-left lighting
7. **Export** as PNG with transparency

---

### Phase 3: Import to MUL Files

#### Method 1: Automated Import (Recommended)

```bash
# Build the MulTools
cd Tools/MulTools
dotnet build -c Release

# Backup existing files
./bin/Release/net8.0/MulTools backup "C:\UO"

# Batch import all processed images
./bin/Release/net8.0/MulTools batch-import "C:\UO" "./processed"

# Verify imports
./bin/Release/net8.0/MulTools verify "C:\UO" "shadow_items_manifest.json"
```

#### Method 2: Individual Import

```bash
# Import specific item
./MulTools import "C:\UO" 0x13FD "./processed/item_13FD.png"

# Import range
for ($i=0x13FD; $i -le 0x1400; $i++) {
    $hex = "{0:X4}" -f $i
    ./MulTools import "C:\UO" "0x$hex" "./processed/item_$hex.png"
}
```

#### Method 3: Using UO Fiddler (Manual)

1. **Open UO Fiddler**
2. **Go to Items tab**
3. **Find item ID** (e.g., 0x13FD)
4. **Right-click → Replace**
5. **Select PNG file**
6. **Save art.mul**

---

### Phase 4: Testing

#### Test in UO Client

1. **Copy updated art.mul and artidx.mul** to UO client directory
2. **Launch UO**
3. **Use [add command** to spawn items
4. **Verify graphics** display correctly

#### Test in Shadow Labyrinth Server

```bash
cd /home/user/Shadow-Labyrinth
dotnet run --project Server

# In-game:
[add PhotonRifle
[add LaserPistol
[add PulseHammer
```

---

## Automation Scripts

### Full Pipeline Automation

Create `generate_all_graphics.sh`:

```bash
#!/bin/bash
set -e

echo "Shadow Labyrinth - Full Graphics Pipeline"
echo "=========================================="

# Step 1: Verify AI prompts exist
if [ ! -f "AI_IMAGE_PROMPTS.md" ]; then
    echo "Error: AI_IMAGE_PROMPTS.md not found"
    exit 1
fi

echo "✓ AI prompts ready"

# Step 2: Wait for AI generation (manual step)
echo ""
echo "MANUAL STEP REQUIRED:"
echo "1. Use AI_IMAGE_PROMPTS.md to generate images"
echo "2. Place generated images in ./generated directory"
echo "3. Press ENTER when ready..."
read

# Step 3: Post-process images
echo ""
echo "Step 2: Post-processing images..."
pwsh ./process_images.ps1

# Step 4: Build MUL tools
echo ""
echo "Step 3: Building MUL tools..."
cd Tools/MulTools
dotnet build -c Release
cd ../..

# Step 5: Backup
echo ""
echo "Step 4: Creating backup..."
./Tools/MulTools/bin/Release/net8.0/MulTools backup "$UO_PATH"

# Step 6: Import
echo ""
echo "Step 5: Importing to MUL files..."
./Tools/MulTools/bin/Release/net8.0/MulTools batch-import "$UO_PATH" "./processed"

# Step 7: Verify
echo ""
echo "Step 6: Verifying imports..."
./Tools/MulTools/bin/Release/net8.0/MulTools verify "$UO_PATH" "./Tools/shadow_items_manifest.json"

echo ""
echo "=========================================="
echo "Pipeline complete!"
echo "Graphics have been imported to UO MUL files"
```

### Windows Batch Version

Create `generate_all_graphics.bat`:

```batch
@echo off
echo Shadow Labyrinth - Full Graphics Pipeline
echo ==========================================

REM Step 1: Check prompts
if not exist "AI_IMAGE_PROMPTS.md" (
    echo Error: AI_IMAGE_PROMPTS.md not found
    exit /b 1
)
echo [OK] AI prompts ready

REM Step 2: Manual generation notice
echo.
echo MANUAL STEP REQUIRED:
echo 1. Use AI_IMAGE_PROMPTS.md to generate images
echo 2. Place generated images in .\generated directory
echo 3. Press any key when ready...
pause > nul

REM Step 3: Post-process
echo.
echo [STEP 2] Post-processing images...
powershell -ExecutionPolicy Bypass -File .\process_images.ps1

REM Step 4: Build tools
echo.
echo [STEP 3] Building MUL tools...
cd Tools\MulTools
dotnet build -c Release
cd ..\..

REM Step 5: Backup
echo.
echo [STEP 4] Creating backup...
Tools\MulTools\bin\Release\net8.0\MulTools.exe backup "%UO_PATH%"

REM Step 6: Import
echo.
echo [STEP 5] Importing to MUL files...
Tools\MulTools\bin\Release\net8.0\MulTools.exe batch-import "%UO_PATH%" ".\processed"

REM Step 7: Verify
echo.
echo [STEP 6] Verifying imports...
Tools\MulTools\bin\Release\net8.0\MulTools.exe verify "%UO_PATH%" ".\Tools\shadow_items_manifest.json"

echo.
echo ==========================================
echo Pipeline complete!
echo Graphics have been imported to UO MUL files
pause
```

---

## Advanced: Hue Application

After importing base graphics, apply hues:

### Using UO Fiddler

1. **Open Items tab**
2. **Select item**
3. **Click "Show Hues" button**
4. **Select hue ID** (1266 for Cyan, 1358 for Purple, etc.)
5. **Apply and save**

### Using MUL Tools (Future Enhancement)

```bash
# Apply hue to imported item
./MulTools apply-hue "C:\UO" 0x13FD 1266
```

---

## Troubleshooting

### Images Don't Appear in UO

**Solution:**
1. Verify item ID is correct (0x4000+ for items)
2. Check art.mul and artidx.mul are in client directory
3. Restart UO client
4. Verify file permissions

### Graphics Look Wrong

**Solution:**
1. Check image size (should be 44x44 or smaller)
2. Verify transparent background
3. Ensure 16-bit color depth
4. Check isometric perspective

### Import Fails

**Solution:**
1. Verify PNG format is valid
2. Check file permissions
3. Ensure backup was created
4. Try manual import with UO Fiddler

### Colors Don't Match

**Solution:**
1. Apply correct hue in UO Fiddler
2. Adjust source image colors
3. Use UO color palette

---

## Quality Checklist

Before finalizing each item:

- [ ] Correct size (44x44 pixels)
- [ ] Transparent background
- [ ] Isometric perspective
- [ ] Top-left lighting
- [ ] Appropriate colors for category
- [ ] Matches item description
- [ ] No artifacts or noise
- [ ] Properly centered
- [ ] Hue applied if needed
- [ ] Tested in-game

---

## Performance Notes

### Batch Processing Times

- **AI Generation:** 2-5 seconds per image
- **Post-Processing:** <1 second per image
- **MUL Import:** <0.1 second per image

**Total Time for 250 Items:**
- AI Generation: ~20 minutes (parallelized)
- Post-Processing: ~5 minutes
- Import: ~1 minute
- **Total: ~30 minutes** (excluding manual review)

---

## Next Steps

1. ✅ Read AI_IMAGE_PROMPTS.md
2. ✅ Generate images with AI tool
3. ✅ Run post-processing script
4. ✅ Build and run MUL tools
5. ✅ Test in UO client
6. ✅ Verify all 250 items

---

## Support Files

- `AI_IMAGE_PROMPTS.md` - Detailed prompts for AI generation
- `shadow_items_manifest.json` - Item ID mapping
- `process_images.ps1` - Batch processing script
- `MulReader.cs` - Read MUL files
- `MulWriter.cs` - Write MUL files
- `Program.cs` - CLI tool

---

**Happy graphics creation!** 🎨

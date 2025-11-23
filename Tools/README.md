# Shadow Labyrinth - Graphics Tools

Complete toolkit for creating, processing, and importing 250 modern item graphics into Ultima Online.

## Quick Start

### Windows

```powershell
# 1. Generate images using AI (see AI_IMAGE_PROMPTS.md)
# 2. Place images in ./generated directory
# 3. Run automated pipeline:

.\Tools\generate_all_graphics.bat
```

### Linux/macOS

```bash
# 1. Generate images using AI (see AI_IMAGE_PROMPTS.md)
# 2. Place images in ./generated directory
# 3. Run automated pipeline:

export UO_PATH="/path/to/uo"
./Tools/generate_all_graphics.sh
```

---

## Tools Overview

### 1. AI Image Generation

**File:** `AI_IMAGE_PROMPTS.md`

Contains detailed prompts for all 250 items, optimized for:
- DALL-E 3
- Midjourney
- Stable Diffusion

**Usage:**
1. Open AI_IMAGE_PROMPTS.md
2. Copy prompts to your AI image generator
3. Download generated images
4. Name files: `item_XXXX.png` (e.g., `item_13FD.png`)

### 2. Image Post-Processing

**File:** `process_images.ps1`

Automatically processes AI-generated images:
- Resizes to 44x44 pixels
- Adds transparent background
- Converts to UO-compatible format
- Applies proper color depth

**Requirements:**
- PowerShell (Windows/Linux/macOS)
- ImageMagick

**Usage:**
```powershell
pwsh ./Tools/process_images.ps1 -InputDir ./generated -OutputDir ./processed
```

### 3. MUL File Tools

**Files:** `MulTools/`
- `MulReader.cs` - Read UO MUL files
- `MulWriter.cs` - Write UO MUL files
- `Program.cs` - CLI interface

**Features:**
- Read item graphics from art.mul
- Export items to PNG
- Import PNG to art.mul
- Batch operations
- Automatic backups
- Verification

**Build:**
```bash
cd Tools/MulTools
dotnet build -c Release
```

**Usage:**
```bash
# Export item
./MulTools export "C:\UO" 0x13FD photon_rifle.png

# Import item
./MulTools import "C:\UO" 0x13FD ./graphics/photon_rifle.png

# Batch import
./MulTools batch-import "C:\UO" ./processed

# Verify imports
./MulTools verify "C:\UO" shadow_items_manifest.json

# Create backup
./MulTools backup "C:\UO"
```

### 4. Automated Pipeline

**Files:**
- `generate_all_graphics.sh` (Linux/macOS)
- `generate_all_graphics.bat` (Windows)

Complete end-to-end automation:
1. AI generation (manual with prompts)
2. Image post-processing
3. MUL tools build
4. Backup creation
5. Batch import
6. Verification

**Usage:**
```bash
# Set UO path
export UO_PATH="/path/to/uo"  # Linux/macOS
set UO_PATH=C:\UO             # Windows

# Run pipeline
./Tools/generate_all_graphics.sh   # Linux/macOS
.\Tools\generate_all_graphics.bat  # Windows
```

---

## File Structure

```
Tools/
├── AI_IMAGE_PROMPTS.md          # 250 AI prompts
├── GRAPHICS_PIPELINE_GUIDE.md   # Complete guide
├── README.md                     # This file
│
├── process_images.ps1            # Image processor
├── generate_all_graphics.sh      # Linux/macOS pipeline
├── generate_all_graphics.bat     # Windows pipeline
│
├── shadow_items_manifest.json    # Item ID mapping
│
└── MulTools/                     # C# MUL tools
    ├── MulTools.csproj
    ├── Program.cs               # CLI interface
    ├── MulReader.cs             # Read MUL files
    └── MulWriter.cs             # Write MUL files
```

---

## Workflow

### Phase 1: AI Generation

1. Open `AI_IMAGE_PROMPTS.md`
2. Choose AI platform:
   - **DALL-E 3** (best quality, paid)
   - **Midjourney** (high quality, paid)
   - **Stable Diffusion** (free, local)

3. Generate images using prompts
4. Download and name files: `item_XXXX.png`
5. Place in `./generated` directory

### Phase 2: Post-Processing

**Automated:**
```powershell
pwsh ./Tools/process_images.ps1
```

**Manual (GIMP/Photoshop):**
1. Resize to 44x44 pixels
2. Ensure transparent background
3. Convert to indexed color (256 colors)
4. Export as PNG

### Phase 3: Import to UO

**Automated:**
```bash
cd Tools/MulTools
dotnet run -- batch-import "$UO_PATH" "../../processed"
```

**Manual (UO Fiddler):**
1. Open UO Fiddler
2. Items tab → Find item ID
3. Right-click → Replace
4. Select PNG file
5. Save art.mul

### Phase 4: Testing

1. Copy art.mul and artidx.mul to UO client
2. Launch UO
3. Test items in-game
4. Verify graphics display correctly

---

## Requirements

### Software

**Required:**
- .NET 8.0 SDK (for MUL tools)
- AI image generator account (DALL-E/Midjourney/SD)

**Optional:**
- ImageMagick (for automated processing)
- PowerShell 7+ (cross-platform)
- UO Fiddler (for manual editing)

### Installation

**ImageMagick:**
```bash
# Windows
winget install ImageMagick.ImageMagick

# macOS
brew install imagemagick

# Ubuntu/Debian
sudo apt install imagemagick
```

**.NET 8.0 SDK:**
```bash
# Download from:
# https://dotnet.microsoft.com/download/dotnet/8.0
```

---

## Configuration

### Item IDs

Items use standard UO item IDs:
- Format: 0xXXXX (hexadecimal)
- Range: 0x0000 - 0xFFFF
- Custom items: Use available IDs (check with UO Fiddler)

### Hues

Hue IDs for color variants:
- 1266 = Cyan (Quantum/Plasma)
- 1358 = Purple (Psionic/Neural)
- 1152 = Blue (Technology)
- 1161 = Silver (Metal/Tech)
- 1150 = Steel Blue (Titanium)

Apply hues in UO Fiddler after import.

---

## Troubleshooting

### "ImageMagick not found"

**Solution:** Install ImageMagick or process images manually

### "art.mul not found"

**Solution:** Set UO_PATH environment variable correctly

### "Import failed"

**Solution:**
1. Check PNG format (44x44, transparent background)
2. Verify file permissions
3. Ensure backup was created
4. Try manual import with UO Fiddler

### Graphics don't appear in UO

**Solution:**
1. Verify art.mul and artidx.mul are in client directory
2. Restart UO client
3. Check item ID is correct
4. Use UO Fiddler to verify import

---

## Performance

### Processing Times

| Task | Time (250 items) |
|------|------------------|
| AI Generation | ~20 minutes |
| Post-Processing | ~5 minutes |
| MUL Import | ~1 minute |
| **Total** | **~30 minutes** |

### Optimization

- Use batch operations
- Process images in parallel
- Use SSD for faster I/O
- Keep UO_PATH on local drive

---

## Advanced Usage

### Custom Item IDs

To use custom item ID range:

```bash
# Modify manifest
# Edit shadow_items_manifest.json

# Import specific range
./MulTools batch-import "$UO_PATH" ./processed 0x5000 0x5100
```

### Multiple UO Installations

```bash
# Separate environments
./MulTools import "$UO_PATH_TEST" 0x13FD test.png
./MulTools import "$UO_PATH_LIVE" 0x13FD final.png
```

### Hue Variations

Create multiple hue variants:

```bash
# Base import
./MulTools import "$UO_PATH" 0x13FD base.png

# Then in UO Fiddler:
# - Select item 0x13FD
# - Apply different hues
# - Save as new item IDs
```

---

## Support

For issues or questions:

1. Check `GRAPHICS_PIPELINE_GUIDE.md` for detailed instructions
2. Review `AI_IMAGE_PROMPTS.md` for prompt examples
3. Verify all prerequisites are installed
4. Check file permissions and paths

---

## Credits

**Tools Created:**
- MulReader/MulWriter - Custom C# implementation
- Image processing - PowerShell + ImageMagick
- Automation - Bash/Batch scripts

**Formats:**
- UO MUL format - Electronic Arts / Origin Systems
- Image processing - ImageMagick Project

---

**Total Items:** 250
**Total Prompts:** 250
**Total Tools:** 7

Ready to create amazing UO graphics! 🎨

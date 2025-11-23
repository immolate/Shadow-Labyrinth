#!/bin/bash

# Shadow Labyrinth - Full Graphics Pipeline
# Automated workflow for creating and importing item graphics

set -e

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

echo -e "${CYAN}============================================================${NC}"
echo -e "${CYAN}Shadow Labyrinth - Full Graphics Pipeline${NC}"
echo -e "${CYAN}============================================================${NC}"
echo ""

# Configuration
UO_PATH="${UO_PATH:-/path/to/uo}"
GENERATED_DIR="./generated"
PROCESSED_DIR="./processed"
TOOLS_DIR="./Tools/MulTools"

# Step 0: Check prerequisites
echo -e "${BLUE}[STEP 0]${NC} Checking prerequisites..."

if [ ! -f "AI_IMAGE_PROMPTS.md" ]; then
    echo -e "${RED}Error: AI_IMAGE_PROMPTS.md not found${NC}"
    exit 1
fi
echo -e "${GREEN}  ✓ AI prompts ready${NC}"

if ! command -v convert &> /dev/null; then
    echo -e "${YELLOW}  ⚠ ImageMagick not found (optional for automation)${NC}"
else
    echo -e "${GREEN}  ✓ ImageMagick installed${NC}"
fi

if ! command -v dotnet &> /dev/null; then
    echo -e "${RED}  ✗ .NET SDK not found${NC}"
    echo -e "${YELLOW}  Please install .NET 8.0 SDK${NC}"
    exit 1
fi
echo -e "${GREEN}  ✓ .NET SDK installed${NC}"

echo ""

# Step 1: AI Generation (manual)
echo -e "${BLUE}[STEP 1]${NC} AI Image Generation (Manual Step)"
echo -e "${YELLOW}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${NC}"
echo ""
echo -e "  1. Open ${CYAN}AI_IMAGE_PROMPTS.md${NC}"
echo -e "  2. Use prompts with your preferred AI image generator:"
echo -e "     • DALL-E 3 (https://openai.com/dall-e-3)"
echo -e "     • Midjourney (https://midjourney.com)"
echo -e "     • Stable Diffusion (local)"
echo -e "  3. Download generated images"
echo -e "  4. Place them in: ${CYAN}$GENERATED_DIR${NC}"
echo -e "  5. Name files as: ${CYAN}item_XXXX.png${NC} (e.g., item_13FD.png)"
echo ""
echo -e "${YELLOW}Press ENTER when images are ready...${NC}"
read -r

# Check if images exist
if [ ! -d "$GENERATED_DIR" ] || [ -z "$(ls -A $GENERATED_DIR/*.png 2>/dev/null)" ]; then
    echo -e "${RED}Error: No PNG files found in $GENERATED_DIR${NC}"
    exit 1
fi

IMAGE_COUNT=$(ls -1 $GENERATED_DIR/*.png 2>/dev/null | wc -l)
echo -e "${GREEN}  ✓ Found $IMAGE_COUNT images${NC}"
echo ""

# Step 2: Post-processing
echo -e "${BLUE}[STEP 2]${NC} Post-processing images..."

if command -v pwsh &> /dev/null; then
    pwsh -File ./Tools/process_images.ps1 -InputDir "$GENERATED_DIR" -OutputDir "$PROCESSED_DIR"
elif command -v convert &> /dev/null; then
    # Fallback to bash + ImageMagick
    mkdir -p "$PROCESSED_DIR"

    for file in $GENERATED_DIR/*.png; do
        filename=$(basename "$file")
        echo -e "  Processing: ${YELLOW}$filename${NC}"

        convert "$file" \
            -background none \
            -alpha set \
            -resize 44x44\> \
            -gravity center \
            -extent 44x44 \
            -colors 256 \
            "$PROCESSED_DIR/$filename"

        echo -e "${GREEN}    ✓ Processed${NC}"
    done
else
    echo -e "${YELLOW}  ⚠ Skipping automated processing (no ImageMagick)${NC}"
    echo -e "${YELLOW}  Please manually resize images to 44x44 pixels${NC}"
    echo -e "${YELLOW}Press ENTER to continue...${NC}"
    read -r
fi

echo ""

# Step 3: Build MUL tools
echo -e "${BLUE}[STEP 3]${NC} Building MUL tools..."

cd "$TOOLS_DIR"
dotnet build -c Release > /dev/null 2>&1

if [ $? -eq 0 ]; then
    echo -e "${GREEN}  ✓ MUL tools built successfully${NC}"
else
    echo -e "${RED}  ✗ Build failed${NC}"
    exit 1
fi

cd - > /dev/null
echo ""

# Step 4: Backup
echo -e "${BLUE}[STEP 4]${NC} Creating backup of MUL files..."

if [ ! -d "$UO_PATH" ]; then
    echo -e "${YELLOW}  ⚠ UO_PATH not set or invalid: $UO_PATH${NC}"
    echo -e "${YELLOW}  Enter UO installation path:${NC}"
    read -r UO_PATH
fi

$TOOLS_DIR/bin/Release/net8.0/MulTools backup "$UO_PATH"

if [ $? -eq 0 ]; then
    echo -e "${GREEN}  ✓ Backup created${NC}"
else
    echo -e "${RED}  ✗ Backup failed${NC}"
    exit 1
fi

echo ""

# Step 5: Import
echo -e "${BLUE}[STEP 5]${NC} Importing graphics to MUL files..."

$TOOLS_DIR/bin/Release/net8.0/MulTools batch-import "$UO_PATH" "$PROCESSED_DIR"

if [ $? -eq 0 ]; then
    echo -e "${GREEN}  ✓ Import successful${NC}"
else
    echo -e "${RED}  ✗ Import failed${NC}"
    exit 1
fi

echo ""

# Step 6: Verify
echo -e "${BLUE}[STEP 6]${NC} Verifying imports..."

$TOOLS_DIR/bin/Release/net8.0/MulTools verify "$UO_PATH" "./Tools/shadow_items_manifest.json"

echo ""

# Complete
echo -e "${CYAN}============================================================${NC}"
echo -e "${GREEN}Pipeline Complete!${NC}"
echo -e "${CYAN}============================================================${NC}"
echo ""
echo -e "  ${GREEN}✓${NC} AI images generated"
echo -e "  ${GREEN}✓${NC} Images post-processed"
echo -e "  ${GREEN}✓${NC} MUL tools built"
echo -e "  ${GREEN}✓${NC} Backup created"
echo -e "  ${GREEN}✓${NC} Graphics imported"
echo -e "  ${GREEN}✓${NC} Verification complete"
echo ""
echo -e "${YELLOW}Next Steps:${NC}"
echo -e "  1. Copy updated art.mul and artidx.mul to UO client"
echo -e "  2. Launch UO and test items"
echo -e "  3. Apply hues in UO Fiddler if needed"
echo ""
echo -e "${CYAN}============================================================${NC}"

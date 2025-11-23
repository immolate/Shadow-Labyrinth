using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ShadowLabyrinth.Tools.MulTools;

/// <summary>
/// UO MUL file format reader for art.mul and artidx.mul
/// Compatible with UO Fiddler format
/// </summary>
public class MulReader
{
    private readonly string _artMulPath;
    private readonly string _artIdxPath;

    public MulReader(string uoDataPath)
    {
        _artMulPath = Path.Combine(uoDataPath, "art.mul");
        _artIdxPath = Path.Combine(uoDataPath, "artidx.mul");

        if (!File.Exists(_artMulPath))
            throw new FileNotFoundException($"art.mul not found at {_artMulPath}");
        if (!File.Exists(_artIdxPath))
            throw new FileNotFoundException($"artidx.mul not found at {_artIdxPath}");
    }

    /// <summary>
    /// Read an item graphic from art.mul
    /// </summary>
    public Bitmap? ReadItemGraphic(int itemId)
    {
        try
        {
            using var idx = new FileStream(_artIdxPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var mul = new FileStream(_artMulPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var idxReader = new BinaryReader(idx);
            using var mulReader = new BinaryReader(mul);

            // Items start at index 0x4000 in art files
            int index = itemId + 0x4000;

            // Read index entry (12 bytes: lookup, length, extra)
            idx.Seek(index * 12, SeekOrigin.Begin);
            int lookup = idxReader.ReadInt32();
            int length = idxReader.ReadInt32();
            int extra = idxReader.ReadInt32();

            if (lookup < 0 || length <= 0)
                return null; // Invalid entry

            // Read graphic data from mul file
            mul.Seek(lookup, SeekOrigin.Begin);

            // Read header (4 bytes: always 0x0001)
            int flag = mulReader.ReadInt32();
            if (flag != 0x0001)
                return null;

            // Read dimensions
            int width = mulReader.ReadInt16();
            int height = mulReader.ReadInt16();

            if (width <= 0 || height <= 0 || width > 1024 || height > 1024)
                return null;

            // Create bitmap
            var bitmap = new Bitmap(width, height, PixelFormat.Format16bppArgb1555);

            // Read lookup table for scanlines
            int[] lookupTable = new int[height];
            for (int i = 0; i < height; i++)
            {
                lookupTable[i] = mulReader.ReadInt16();
            }

            // Read pixel data
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format16bppArgb1555);

            try
            {
                unsafe
                {
                    ushort* line = (ushort*)bitmapData.Scan0;
                    int delta = bitmapData.Stride >> 1;

                    for (int y = 0; y < height; y++)
                    {
                        // Seek to scanline
                        mul.Seek(lookup + lookupTable[y], SeekOrigin.Begin);

                        ushort* cur = line + (y * delta);
                        int x = 0;

                        while (x < width)
                        {
                            ushort xOffset = mulReader.ReadUInt16();
                            ushort xRun = mulReader.ReadUInt16();

                            if (xOffset + xRun > width)
                                break;

                            // Skip transparent pixels
                            cur += xOffset;
                            x += xOffset;

                            // Read colored pixels
                            for (int j = 0; j < xRun; j++)
                            {
                                ushort color = mulReader.ReadUInt16();
                                *cur++ = (ushort)(color | 0x8000); // Set alpha bit
                                x++;
                            }
                        }
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            return bitmap;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading item {itemId}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Get total number of item graphics
    /// </summary>
    public int GetItemCount()
    {
        try
        {
            var info = new FileInfo(_artIdxPath);
            return (int)(info.Length / 12) - 0x4000;
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// Check if an item graphic exists
    /// </summary>
    public bool ItemExists(int itemId)
    {
        try
        {
            using var idx = new FileStream(_artIdxPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new BinaryReader(idx);

            int index = itemId + 0x4000;
            idx.Seek(index * 12, SeekOrigin.Begin);

            int lookup = reader.ReadInt32();
            int length = reader.ReadInt32();

            return lookup >= 0 && length > 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Export item graphic to PNG
    /// </summary>
    public bool ExportToPng(int itemId, string outputPath)
    {
        try
        {
            var bitmap = ReadItemGraphic(itemId);
            if (bitmap == null)
                return false;

            bitmap.Save(outputPath, ImageFormat.Png);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting item {itemId}: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Batch export items to PNG
    /// </summary>
    public int BatchExport(int startId, int endId, string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);
        int exported = 0;

        for (int i = startId; i <= endId; i++)
        {
            if (ItemExists(i))
            {
                string filename = Path.Combine(outputDirectory, $"item_{i:X4}.png");
                if (ExportToPng(i, filename))
                {
                    exported++;
                    if (exported % 100 == 0)
                        Console.WriteLine($"Exported {exported} items...");
                }
            }
        }

        Console.WriteLine($"Export complete: {exported} items exported to {outputDirectory}");
        return exported;
    }
}

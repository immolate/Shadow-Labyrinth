using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace ShadowLabyrinth.Tools.MulTools;

/// <summary>
/// UO MUL file format writer for art.mul and artidx.mul
/// Creates UO Fiddler compatible files
/// </summary>
public class MulWriter
{
    private readonly string _artMulPath;
    private readonly string _artIdxPath;
    private readonly string _backupPath;

    public MulWriter(string uoDataPath)
    {
        _artMulPath = Path.Combine(uoDataPath, "art.mul");
        _artIdxPath = Path.Combine(uoDataPath, "artidx.mul");
        _backupPath = Path.Combine(uoDataPath, "Backups");

        Directory.CreateDirectory(_backupPath);
    }

    /// <summary>
    /// Backup existing MUL files
    /// </summary>
    public void CreateBackup()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        if (File.Exists(_artMulPath))
        {
            string backup = Path.Combine(_backupPath, $"art_{timestamp}.mul");
            File.Copy(_artMulPath, backup, true);
            Console.WriteLine($"Backed up art.mul to {backup}");
        }

        if (File.Exists(_artIdxPath))
        {
            string backup = Path.Combine(_backupPath, $"artidx_{timestamp}.mul");
            File.Copy(_artIdxPath, backup, true);
            Console.WriteLine($"Backed up artidx.mul to {backup}");
        }
    }

    /// <summary>
    /// Import PNG to item graphic slot
    /// </summary>
    public bool ImportFromPng(int itemId, string pngPath, bool createBackup = true)
    {
        try
        {
            if (createBackup)
                CreateBackup();

            // Load PNG
            using var sourceBitmap = new Bitmap(pngPath);

            // Convert to 16-bit ARGB1555 format
            var bitmap = ConvertToUOFormat(sourceBitmap);

            // Write to MUL files
            return WriteItemGraphic(itemId, bitmap);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error importing {pngPath} to item {itemId}: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Convert bitmap to UO's 16-bit ARGB1555 format
    /// </summary>
    private Bitmap ConvertToUOFormat(Bitmap source)
    {
        // Resize if needed (UO items are typically 44x44 or smaller)
        int maxSize = 128;
        if (source.Width > maxSize || source.Height > maxSize)
        {
            float scale = Math.Min((float)maxSize / source.Width, (float)maxSize / source.Height);
            int newWidth = (int)(source.Width * scale);
            int newHeight = (int)(source.Height * scale);
            source = new Bitmap(source, newWidth, newHeight);
        }

        var result = new Bitmap(source.Width, source.Height, PixelFormat.Format16bppArgb1555);

        using (var g = Graphics.FromImage(result))
        {
            g.DrawImage(source, 0, 0, source.Width, source.Height);
        }

        return result;
    }

    /// <summary>
    /// Write bitmap to MUL files
    /// </summary>
    private bool WriteItemGraphic(int itemId, Bitmap bitmap)
    {
        try
        {
            int index = itemId + 0x4000;

            // Read existing files
            byte[] mulData;
            byte[] idxData;

            if (File.Exists(_artMulPath))
                mulData = File.ReadAllBytes(_artMulPath);
            else
                mulData = new byte[0];

            if (File.Exists(_artIdxPath))
                idxData = File.ReadAllBytes(_artIdxPath);
            else
                idxData = new byte[index * 12 + 12]; // Ensure enough space

            // Ensure index file is large enough
            if (idxData.Length < (index + 1) * 12)
            {
                Array.Resize(ref idxData, (index + 1) * 12);
            }

            // Encode bitmap to UO format
            var encodedData = EncodeBitmap(bitmap);

            // Append to mul file
            int lookup = mulData.Length;
            Array.Resize(ref mulData, mulData.Length + encodedData.Length);
            Array.Copy(encodedData, 0, mulData, lookup, encodedData.Length);

            // Update index file
            using (var ms = new MemoryStream(idxData))
            using (var writer = new BinaryWriter(ms))
            {
                ms.Seek(index * 12, SeekOrigin.Begin);
                writer.Write(lookup);
                writer.Write(encodedData.Length);
                writer.Write(0); // extra
            }

            // Write files
            File.WriteAllBytes(_artMulPath, mulData);
            File.WriteAllBytes(_artIdxPath, idxData);

            Console.WriteLine($"Successfully wrote item {itemId} to MUL files");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing item {itemId}: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Encode bitmap to UO MUL format
    /// </summary>
    private byte[] EncodeBitmap(Bitmap bitmap)
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);

        int width = bitmap.Width;
        int height = bitmap.Height;

        // Write header
        writer.Write(0x0001); // Flag
        writer.Write((short)width);
        writer.Write((short)height);

        // Calculate lookup table position
        int lookupTablePos = (int)ms.Position;

        // Reserve space for lookup table
        for (int i = 0; i < height; i++)
            writer.Write((short)0);

        var bitmapData = bitmap.LockBits(
            new Rectangle(0, 0, width, height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format16bppArgb1555);

        int[] lookupTable = new int[height];

        try
        {
            unsafe
            {
                ushort* line = (ushort*)bitmapData.Scan0;
                int delta = bitmapData.Stride >> 1;

                for (int y = 0; y < height; y++)
                {
                    // Store scanline position
                    lookupTable[y] = (int)ms.Position - lookupTablePos - (height * 2) - 8;

                    ushort* cur = line + (y * delta);

                    // Encode scanline with RLE
                    int x = 0;
                    while (x < width)
                    {
                        // Find transparent run
                        int transRun = 0;
                        while (x + transRun < width && (cur[transRun] & 0x8000) == 0)
                            transRun++;

                        // Find colored run
                        int colorRun = 0;
                        while (x + transRun + colorRun < width &&
                               (cur[transRun + colorRun] & 0x8000) != 0)
                            colorRun++;

                        if (colorRun > 0)
                        {
                            // Write run header
                            writer.Write((ushort)transRun);
                            writer.Write((ushort)colorRun);

                            // Write colored pixels
                            for (int i = 0; i < colorRun; i++)
                            {
                                writer.Write(cur[transRun + i]);
                            }
                        }

                        x += transRun + colorRun;
                        cur += transRun + colorRun;

                        if (colorRun == 0)
                            break; // End of scanline
                    }

                    // Write scanline terminator
                    writer.Write((ushort)0);
                    writer.Write((ushort)0);
                }
            }
        }
        finally
        {
            bitmap.UnlockBits(bitmapData);
        }

        // Write lookup table
        long endPos = ms.Position;
        ms.Seek(lookupTablePos, SeekOrigin.Begin);
        for (int i = 0; i < height; i++)
        {
            writer.Write((short)lookupTable[i]);
        }
        ms.Seek(endPos, SeekOrigin.Begin);

        return ms.ToArray();
    }

    /// <summary>
    /// Batch import PNG files
    /// </summary>
    public int BatchImport(Dictionary<int, string> itemPaths, bool createBackup = true)
    {
        if (createBackup && itemPaths.Count > 0)
            CreateBackup();

        int imported = 0;
        foreach (var kvp in itemPaths)
        {
            if (ImportFromPng(kvp.Key, kvp.Value, false))
            {
                imported++;
                if (imported % 10 == 0)
                    Console.WriteLine($"Imported {imported}/{itemPaths.Count} items...");
            }
        }

        Console.WriteLine($"Import complete: {imported}/{itemPaths.Count} items imported");
        return imported;
    }

    /// <summary>
    /// Import from directory (expects files named item_XXXX.png)
    /// </summary>
    public int BatchImportFromDirectory(string directory, int startId, int endId)
    {
        var files = new Dictionary<int, string>();

        for (int i = startId; i <= endId; i++)
        {
            string filename = Path.Combine(directory, $"item_{i:X4}.png");
            if (File.Exists(filename))
            {
                files[i] = filename;
            }
        }

        return BatchImport(files);
    }
}

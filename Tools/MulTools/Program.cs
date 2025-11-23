using ShadowLabyrinth.Tools.MulTools;
using System.Text.Json;

Console.WriteLine("Shadow Labyrinth - MUL Tools");
Console.WriteLine("============================");
Console.WriteLine();

if (args.Length < 2)
{
    ShowHelp();
    return;
}

string command = args[0].ToLower();
string uoPath = args[1];

try
{
    switch (command)
    {
        case "export":
            ExportItems(uoPath, args);
            break;

        case "import":
            ImportItems(uoPath, args);
            break;

        case "batch-import":
            BatchImportItems(uoPath, args);
            break;

        case "verify":
            VerifyItems(uoPath, args);
            break;

        case "backup":
            BackupFiles(uoPath);
            break;

        default:
            Console.WriteLine($"Unknown command: {command}");
            ShowHelp();
            break;
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}

static void ShowHelp()
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  MulTools export <UO_PATH> <ITEM_ID> <OUTPUT.png>");
    Console.WriteLine("  MulTools export <UO_PATH> <START_ID> <END_ID> <OUTPUT_DIR>");
    Console.WriteLine("  MulTools import <UO_PATH> <ITEM_ID> <INPUT.png>");
    Console.WriteLine("  MulTools batch-import <UO_PATH> <INPUT_DIR> [START_ID] [END_ID]");
    Console.WriteLine("  MulTools verify <UO_PATH> <MANIFEST.json>");
    Console.WriteLine("  MulTools backup <UO_PATH>");
    Console.WriteLine();
    Console.WriteLine("Examples:");
    Console.WriteLine("  MulTools export \"C:\\UO\" 0x13FD photon_rifle.png");
    Console.WriteLine("  MulTools export \"C:\\UO\" 0x13FD 0x1400 ./exported");
    Console.WriteLine("  MulTools import \"C:\\UO\" 0x13FD ./graphics/photon_rifle.png");
    Console.WriteLine("  MulTools batch-import \"C:\\UO\" ./generated_graphics");
    Console.WriteLine("  MulTools verify \"C:\\UO\" shadow_items.json");
}

static void ExportItems(string uoPath, string[] args)
{
    var reader = new MulReader(uoPath);

    if (args.Length == 4)
    {
        // Single item export
        int itemId = ParseItemId(args[2]);
        string output = args[3];

        Console.WriteLine($"Exporting item 0x{itemId:X4} to {output}...");
        if (reader.ExportToPng(itemId, output))
        {
            Console.WriteLine("Export successful!");
        }
        else
        {
            Console.WriteLine("Export failed!");
        }
    }
    else if (args.Length == 5)
    {
        // Range export
        int startId = ParseItemId(args[2]);
        int endId = ParseItemId(args[3]);
        string outputDir = args[4];

        Console.WriteLine($"Exporting items 0x{startId:X4} - 0x{endId:X4} to {outputDir}...");
        int count = reader.BatchExport(startId, endId, outputDir);
        Console.WriteLine($"Exported {count} items!");
    }
    else
    {
        ShowHelp();
    }
}

static void ImportItems(string uoPath, string[] args)
{
    if (args.Length < 4)
    {
        ShowHelp();
        return;
    }

    var writer = new MulWriter(uoPath);
    int itemId = ParseItemId(args[2]);
    string input = args[3];

    Console.WriteLine($"Importing {input} to item 0x{itemId:X4}...");
    if (writer.ImportFromPng(itemId, input))
    {
        Console.WriteLine("Import successful!");
    }
    else
    {
        Console.WriteLine("Import failed!");
    }
}

static void BatchImportItems(string uoPath, string[] args)
{
    if (args.Length < 3)
    {
        ShowHelp();
        return;
    }

    var writer = new MulWriter(uoPath);
    string inputDir = args[2];

    int startId = args.Length > 3 ? ParseItemId(args[3]) : 0;
    int endId = args.Length > 4 ? ParseItemId(args[4]) : 0xFFFF;

    Console.WriteLine($"Batch importing from {inputDir}...");
    Console.WriteLine($"Range: 0x{startId:X4} - 0x{endId:X4}");

    int count = writer.BatchImportFromDirectory(inputDir, startId, endId);
    Console.WriteLine($"Imported {count} items!");
}

static void VerifyItems(string uoPath, string[] args)
{
    if (args.Length < 3)
    {
        ShowHelp();
        return;
    }

    string manifestPath = args[2];
    if (!File.Exists(manifestPath))
    {
        Console.WriteLine($"Manifest not found: {manifestPath}");
        return;
    }

    var reader = new MulReader(uoPath);
    var manifest = JsonSerializer.Deserialize<ItemManifest>(File.ReadAllText(manifestPath));

    if (manifest == null || manifest.Items == null)
    {
        Console.WriteLine("Invalid manifest file!");
        return;
    }

    Console.WriteLine($"Verifying {manifest.Items.Count} items...");

    int found = 0;
    int missing = 0;

    foreach (var item in manifest.Items)
    {
        if (reader.ItemExists(item.ItemId))
        {
            found++;
            Console.WriteLine($"✓ 0x{item.ItemId:X4} - {item.Name}");
        }
        else
        {
            missing++;
            Console.WriteLine($"✗ 0x{item.ItemId:X4} - {item.Name} (MISSING)");
        }
    }

    Console.WriteLine();
    Console.WriteLine($"Summary: {found} found, {missing} missing");
}

static void BackupFiles(string uoPath)
{
    var writer = new MulWriter(uoPath);
    Console.WriteLine("Creating backup...");
    writer.CreateBackup();
    Console.WriteLine("Backup complete!");
}

static int ParseItemId(string value)
{
    if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
    {
        return Convert.ToInt32(value.Substring(2), 16);
    }
    return int.Parse(value);
}

// Manifest classes
class ItemManifest
{
    public List<ItemEntry>? Items { get; set; }
}

class ItemEntry
{
    public int ItemId { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
}

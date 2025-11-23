namespace Server;

/// <summary>
/// Server configuration with modern settings
/// </summary>
public static class Configuration
{
    public static string ServerName { get; set; } = "Shadow Labyrinth";
    public static int ServerPort { get; set; } = 2593;
    public static string DataPath { get; set; } = "./";
    public static int MaxClients { get; set; } = 256;
    public static bool Debug { get; set; } = true;

    // Modern features
    public static bool EnableAutoSave { get; set; } = true;
    public static TimeSpan AutoSaveInterval { get; set; } = TimeSpan.FromMinutes(10);
    public static bool EnableCompression { get; set; } = true;
    public static bool EnableEncryption { get; set; } = false;

    // File paths
    public static string SavesPath { get; set; } = "./Saves";
    public static string BackupsPath { get; set; } = "./Backups";
    public static string LogsPath { get; set; } = "./Logs";

    public static void Load()
    {
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        if (!File.Exists(configPath))
        {
            Console.WriteLine("No configuration file found, using defaults.");
            Save();
            return;
        }

        try
        {
            var json = File.ReadAllText(configPath);
            var config = System.Text.Json.JsonSerializer.Deserialize<ConfigData>(json);

            if (config != null)
            {
                ServerName = config.ServerName;
                ServerPort = config.ServerPort;
                DataPath = config.DataPath;
                MaxClients = config.MaxClients;
                Debug = config.Debug;
            }

            Console.WriteLine($"Configuration loaded from {configPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configuration: {ex.Message}");
            Console.WriteLine("Using default settings.");
        }
    }

    public static void Save()
    {
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        try
        {
            var config = new ConfigData
            {
                ServerName = ServerName,
                ServerPort = ServerPort,
                DataPath = DataPath,
                MaxClients = MaxClients,
                Debug = Debug
            };

            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = System.Text.Json.JsonSerializer.Serialize(config, options);
            File.WriteAllText(configPath, json);

            Console.WriteLine($"Configuration saved to {configPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving configuration: {ex.Message}");
        }
    }

    private class ConfigData
    {
        public string ServerName { get; set; } = "Shadow Labyrinth";
        public int ServerPort { get; set; } = 2593;
        public string DataPath { get; set; } = "./";
        public int MaxClients { get; set; } = 256;
        public bool Debug { get; set; } = true;
    }
}

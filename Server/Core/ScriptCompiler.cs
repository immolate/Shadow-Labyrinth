using System.Reflection;

namespace Server;

/// <summary>
/// Script compilation system for .NET 8.0
/// </summary>
public static class ScriptCompiler
{
    private static readonly List<Assembly> _assemblies = new();
    public static IReadOnlyList<Assembly> Assemblies => _assemblies;

    public static void Compile()
    {
        Console.WriteLine("Loading scripts...");

        try
        {
            // Load Scripts assembly
            var scriptsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts.dll");

            if (File.Exists(scriptsPath))
            {
                var assembly = Assembly.LoadFrom(scriptsPath);
                _assemblies.Add(assembly);
                Console.WriteLine($"Loaded Scripts assembly: {assembly.GetName().Name}");

                // Initialize script types
                InitializeScripts(assembly);
            }
            else
            {
                Console.WriteLine("Scripts assembly not found. Server will run with core functionality only.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scripts: {ex.Message}");
        }
    }

    private static void InitializeScripts(Assembly assembly)
    {
        var types = assembly.GetTypes();
        Console.WriteLine($"Found {types.Length} types in scripts assembly.");

        // Find and execute Configure methods
        foreach (var type in types)
        {
            var configMethod = type.GetMethod("Configure",
                BindingFlags.Static | BindingFlags.Public);

            if (configMethod != null)
            {
                try
                {
                    configMethod.Invoke(null, null);
                    Console.WriteLine($"Configured: {type.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error configuring {type.Name}: {ex.Message}");
                }
            }
        }
    }
}

using Server;
using Server.Network;
using System.Diagnostics;

Console.WriteLine("Shadow Labyrinth - Modern Ultima Online Server");
Console.WriteLine("=".PadRight(50, '='));
Console.WriteLine($".NET Version: {Environment.Version}");
Console.WriteLine($"OS: {Environment.OSVersion}");
Console.WriteLine($"64-bit: {Environment.Is64BitProcess}");
Console.WriteLine("=".PadRight(50, '='));

try
{
    // Initialize core systems
    Console.WriteLine("Initializing core systems...");

    Core.Initialize();
    ScriptCompiler.Compile();
    World.Load();

    // Start network listener
    Console.WriteLine("Starting network listener...");
    MessagePump.StartListener();

    Console.WriteLine("Server is ready!");
    Console.WriteLine($"Listening on port {Configuration.ServerPort}");
    Console.WriteLine("Press Ctrl+C to shut down.");

    // Main server loop
    Core.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Fatal error: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    Environment.Exit(1);
}

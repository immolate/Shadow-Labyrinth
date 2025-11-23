using System.Diagnostics;

namespace Server;

/// <summary>
/// Core server functionality with modern async/await patterns
/// </summary>
public static class Core
{
    private static bool _running;
    private static readonly CancellationTokenSource _cts = new();
    private static Task? _mainLoop;

    public static bool IsRunning => _running;
    public static DateTime StartTime { get; private set; }
    public static TimeSpan UpTime => DateTime.UtcNow - StartTime;

    public static void Initialize()
    {
        Console.WriteLine("Initializing Core...");

        // Create necessary directories
        Directory.CreateDirectory(Configuration.SavesPath);
        Directory.CreateDirectory(Configuration.BackupsPath);
        Directory.CreateDirectory(Configuration.LogsPath);

        // Load configuration
        Configuration.Load();

        // Set up shutdown handler
        Console.CancelKeyPress += OnShutdown;
        AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

        StartTime = DateTime.UtcNow;

        Console.WriteLine("Core initialized.");
    }

    public static void Run()
    {
        _running = true;
        _mainLoop = Task.Run(MainLoop, _cts.Token);
        _mainLoop.Wait();
    }

    private static async Task MainLoop()
    {
        var sw = Stopwatch.StartNew();
        var lastAutoSave = DateTime.UtcNow;

        while (_running && !_cts.Token.IsCancellationRequested)
        {
            try
            {
                sw.Restart();

                // Process timers
                Timer.Slice();

                // Auto-save check
                if (Configuration.EnableAutoSave &&
                    DateTime.UtcNow - lastAutoSave > Configuration.AutoSaveInterval)
                {
                    Console.WriteLine("Auto-saving...");
                    World.Save();
                    lastAutoSave = DateTime.UtcNow;
                }

                // Maintain 20ms tick rate (50 TPS)
                var elapsed = sw.ElapsedMilliseconds;
                if (elapsed < 20)
                {
                    await Task.Delay(20 - (int)elapsed, _cts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in main loop: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }

    public static void Shutdown()
    {
        if (!_running)
            return;

        Console.WriteLine("Shutting down server...");
        _running = false;

        // Save world state
        Console.WriteLine("Saving world...");
        World.Save();

        // Cancel main loop
        _cts.Cancel();

        Console.WriteLine("Shutdown complete.");
    }

    private static void OnShutdown(object? sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        Shutdown();
    }

    private static void OnProcessExit(object? sender, EventArgs e)
    {
        Shutdown();
    }
}

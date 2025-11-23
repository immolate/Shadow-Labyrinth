using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Server;

/// <summary>
/// Highly optimized core server functionality with modern async/await patterns
/// </summary>
public static class Core
{
    private static bool _running;
    private static readonly CancellationTokenSource _cts = new();
    private static Task? _mainLoop;

    // Performance tracking
    private static long _tickCount;
    private static readonly Stopwatch _uptimeWatch = new();

    public static bool IsRunning => _running;
    public static DateTime StartTime { get; private set; }
    public static TimeSpan UpTime => _uptimeWatch.Elapsed;
    public static long TickCount => Volatile.Read(ref _tickCount);

    // Tick rate configuration (50 TPS = 20ms per tick)
    private const int TargetTickMs = 20;
    private const int TicksPerSecond = 50;

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
        _uptimeWatch.Start();

        Console.WriteLine("Core initialized.");
    }

    public static void Run()
    {
        _running = true;
        _mainLoop = Task.Run(MainLoop, _cts.Token);
        _mainLoop.Wait();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private static async Task MainLoop()
    {
        var lastAutoSave = DateTime.UtcNow;
        var tickTimer = Stopwatch.StartNew();
        long nextTick = TargetTickMs;

        while (_running && !_cts.Token.IsCancellationRequested)
        {
            try
            {
                var startTick = tickTimer.ElapsedMilliseconds;

                // Process timers
                Timer.Slice();

                // Process entity deltas (batched network updates)
                World.ProcessDeltas();

                // Increment tick counter
                Interlocked.Increment(ref _tickCount);

                // Auto-save check (only check every second to reduce overhead)
                if (_tickCount % TicksPerSecond == 0)
                {
                    var now = DateTime.UtcNow;
                    if (Configuration.EnableAutoSave && now - lastAutoSave > Configuration.AutoSaveInterval)
                    {
                        Console.WriteLine("Auto-saving...");
                        _ = Task.Run(World.Save); // Save async to not block main loop
                        lastAutoSave = now;
                    }
                }

                // Precise tick timing with spin-wait for last millisecond
                var elapsed = tickTimer.ElapsedMilliseconds - startTick;
                var remaining = TargetTickMs - elapsed;

                if (remaining > 1)
                {
                    // Use Task.Delay for bulk of wait
                    await Task.Delay((int)remaining - 1, _cts.Token);
                }

                // Spin-wait for precise timing on last millisecond
                while (tickTimer.ElapsedMilliseconds < nextTick)
                {
                    Thread.SpinWait(10);
                }

                nextTick += TargetTickMs;
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

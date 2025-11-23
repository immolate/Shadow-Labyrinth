using System.Runtime.CompilerServices;

namespace Server;

/// <summary>
/// Highly optimized timer system using modern data structures and minimal locking
/// </summary>
public abstract class Timer
{
    private static readonly PriorityQueue<Timer, DateTime> _queue = new();
    private static readonly List<Timer> _executing = new(256);
    private static readonly object _lock = new();

    protected Timer(TimeSpan delay, TimeSpan interval = default)
    {
        Delay = delay;
        Interval = interval;
        Next = DateTime.UtcNow + delay;
    }

    public TimeSpan Delay { get; set; }
    public TimeSpan Interval { get; set; }
    public DateTime Next { get; private set; }
    public bool Running { get; private set; }

    protected abstract void OnTick();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Start()
    {
        if (Running)
            return;

        Running = true;
        Next = DateTime.UtcNow + Delay;

        lock (_lock)
        {
            _queue.Enqueue(this, Next);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Stop()
    {
        Running = false;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static void Slice()
    {
        var now = DateTime.UtcNow;

        // Minimize lock time by extracting ready timers first
        lock (_lock)
        {
            while (_queue.Count > 0)
            {
                if (!_queue.TryPeek(out var timer, out var priority) || priority > now)
                    break;

                _queue.Dequeue();

                if (timer.Running)
                    _executing.Add(timer);
            }
        }

        // Execute timers outside of lock
        for (int i = 0; i < _executing.Count; i++)
        {
            var timer = _executing[i];

            try
            {
                timer.OnTick();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Timer error: {ex.Message}");
            }

            // Re-queue recurring timers
            if (timer.Running && timer.Interval > TimeSpan.Zero)
            {
                timer.Next = now + timer.Interval;

                lock (_lock)
                {
                    _queue.Enqueue(timer, timer.Next);
                }
            }
            else
            {
                timer.Running = false;
            }
        }

        _executing.Clear();
    }

    public static int QueuedCount
    {
        get
        {
            lock (_lock)
            {
                return _queue.Count;
            }
        }
    }
}

/// <summary>
/// Delay timer for one-time execution
/// </summary>
public class DelayTimer : Timer
{
    private readonly Action _callback;

    public DelayTimer(TimeSpan delay, Action callback) : base(delay)
    {
        _callback = callback;
    }

    protected override void OnTick()
    {
        _callback?.Invoke();
    }

    public static DelayTimer Create(TimeSpan delay, Action callback)
    {
        var timer = new DelayTimer(delay, callback);
        timer.Start();
        return timer;
    }
}

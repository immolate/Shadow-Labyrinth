namespace Server;

/// <summary>
/// Modern timer system using high-performance data structures
/// </summary>
public abstract class Timer
{
    private static readonly PriorityQueue<Timer, DateTime> _queue = new();
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

    public void Stop()
    {
        Running = false;
    }

    public static void Slice()
    {
        var now = DateTime.UtcNow;

        lock (_lock)
        {
            while (_queue.Count > 0 && _queue.Peek() is Timer timer)
            {
                if (timer.Next > now)
                    break;

                _queue.Dequeue();

                if (!timer.Running)
                    continue;

                try
                {
                    timer.OnTick();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Timer error: {ex.Message}");
                }

                if (timer.Running && timer.Interval > TimeSpan.Zero)
                {
                    timer.Next = now + timer.Interval;
                    _queue.Enqueue(timer, timer.Next);
                }
                else
                {
                    timer.Running = false;
                }
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

using System.Runtime.CompilerServices;

namespace Server;

/// <summary>
/// Highly optimized mobile (player/NPC) class with delta batching and minimal allocations
/// </summary>
public class Mobile : IEntity
{
    private Point3D _location;
    private Map _map;
    private string _name;
    private int _bodyValue;
    private int _hue;
    private Direction _direction;

    // Delta batching for network optimization
    private MobileDelta _pendingDeltas;
    private bool _deltaQueued;

    // Stats with change tracking
    private int _hits;
    private int _hitsMax;
    private int _stam;
    private int _stamMax;
    private int _mana;
    private int _manaMax;

    public Mobile()
    {
        Serial = World.NewMobileSerial();
        _name = string.Intern("Mobile"); // Intern common names
        _bodyValue = 0x190; // Human male
        _map = Map.Felucca;
        _location = Point3D.Zero;
        _direction = Direction.North;

        _hits = _hitsMax = 100;
        _stam = _stamMax = 100;
        _mana = _manaMax = 100;

        World.AddEntity(this);
    }

    public Serial Serial { get; }
    public bool Deleted { get; private set; }
    public NetState? NetState { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            var newName = string.IsNullOrEmpty(value) ? "Mobile" : value;
            if (_name != newName)
            {
                // Intern common names to reduce memory
                _name = newName.Length < 32 ? string.Intern(newName) : newName;
                QueueDelta(MobileDelta.Properties);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Point3D Location
    {
        get => _location;
        set
        {
            if (_location != value)
            {
                _location = value;
                QueueDelta(MobileDelta.Location);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Map Map
    {
        get => _map;
        set
        {
            if (_map != value)
            {
                _map = value;
                QueueDelta(MobileDelta.Location);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Body
    {
        get => _bodyValue;
        set
        {
            if (_bodyValue != value)
            {
                _bodyValue = value;
                QueueDelta(MobileDelta.Body);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Hue
    {
        get => _hue;
        set
        {
            if (_hue != value)
            {
                _hue = value;
                QueueDelta(MobileDelta.Hue);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Direction Direction
    {
        get => _direction;
        set
        {
            if (_direction != value)
            {
                _direction = value;
                QueueDelta(MobileDelta.Direction);
            }
        }
    }

    // Optimized stats with change tracking
    public int Hits
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _hits;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (_hits != value)
            {
                _hits = Math.Clamp(value, 0, _hitsMax);
                QueueDelta(MobileDelta.Hits);
            }
        }
    }

    public int HitsMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _hitsMax;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (_hitsMax != value)
            {
                _hitsMax = Math.Max(1, value);
                _hits = Math.Min(_hits, _hitsMax);
                QueueDelta(MobileDelta.Stats);
            }
        }
    }

    public int Stam
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _stam;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (_stam != value)
            {
                _stam = Math.Clamp(value, 0, _stamMax);
                QueueDelta(MobileDelta.Stam);
            }
        }
    }

    public int StamMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _stamMax;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (_stamMax != value)
            {
                _stamMax = Math.Max(1, value);
                _stam = Math.Min(_stam, _stamMax);
                QueueDelta(MobileDelta.Stats);
            }
        }
    }

    public int Mana
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _mana;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (_mana != value)
            {
                _mana = Math.Clamp(value, 0, _manaMax);
                QueueDelta(MobileDelta.Mana);
            }
        }
    }

    public int ManaMax
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _manaMax;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (_manaMax != value)
            {
                _manaMax = Math.Max(1, value);
                _mana = Math.Min(_mana, _manaMax);
                QueueDelta(MobileDelta.Stats);
            }
        }
    }

    // Skills (modern with Span<T> for zero-allocation access)
    private readonly int[] _skills = new int[58];

    public Span<int> Skills => _skills;

    // Batch delta updates to reduce network traffic
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void QueueDelta(MobileDelta delta)
    {
        _pendingDeltas |= delta;

        if (!_deltaQueued)
        {
            _deltaQueued = true;
            World.QueueMobileDelta(this);
        }
    }

    internal void ProcessDeltas()
    {
        if (_pendingDeltas != MobileDelta.None)
        {
            Delta(_pendingDeltas);
            _pendingDeltas = MobileDelta.None;
            _deltaQueued = false;
        }
    }

    public virtual void Delete()
    {
        if (Deleted)
            return;

        Deleted = true;
        World.RemoveEntity(this);

        OnDelete();
    }

    protected virtual void OnDelete()
    {
    }

    protected virtual void Delta(MobileDelta delta)
    {
        // TODO: Network delta handling
    }

    public virtual void SendMessage(string message)
    {
        // TODO: Send message packet
        if (Configuration.Debug)
            Console.WriteLine($"[{Name}] {message}");
    }

    public virtual void OnDoubleClick(Mobile from)
    {
    }

    public virtual void OnSingleClick(Mobile from)
    {
        from.SendMessage(Name);
    }

    public virtual void Resurrect()
    {
        if (Hits > 0)
            return;

        Hits = HitsMax;
        Stam = StamMax;
        Mana = ManaMax;

        SendMessage("You have been resurrected!");
    }
}

[Flags]
public enum MobileDelta
{
    None = 0x0000,
    Location = 0x0001,
    Body = 0x0002,
    Hue = 0x0004,
    Direction = 0x0008,
    Stats = 0x0010,
    Hits = 0x0020,
    Stam = 0x0040,
    Mana = 0x0080,
    Properties = 0x0100
}

public enum Direction : byte
{
    North = 0x0,
    Right = 0x1,
    East = 0x2,
    Down = 0x3,
    South = 0x4,
    Left = 0x5,
    West = 0x6,
    Up = 0x7,
    Running = 0x80,
    ValueMask = 0x07,
    FacingMask = 0x87
}

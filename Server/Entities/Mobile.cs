namespace Server;

/// <summary>
/// Base mobile (player/NPC) class with modern features
/// </summary>
public class Mobile : IEntity
{
    private Point3D _location;
    private Map _map;
    private string _name;
    private int _bodyValue;
    private int _hue;
    private Direction _direction;

    public Mobile()
    {
        Serial = World.NewMobileSerial();
        _name = "Mobile";
        _bodyValue = 0x190; // Human male
        _map = Map.Felucca;
        _location = Point3D.Zero;
        _direction = Direction.North;

        Hits = HitsMax = 100;
        Stam = StamMax = 100;
        Mana = ManaMax = 100;

        World.AddEntity(this);
    }

    public Serial Serial { get; }
    public bool Deleted { get; private set; }
    public NetState? NetState { get; set; }

    public string Name
    {
        get => _name;
        set => _name = value ?? string.Empty;
    }

    public Point3D Location
    {
        get => _location;
        set
        {
            if (_location != value)
            {
                _location = value;
                Delta(MobileDelta.Location);
            }
        }
    }

    public Map Map
    {
        get => _map;
        set
        {
            if (_map != value)
            {
                _map = value;
                Delta(MobileDelta.Location);
            }
        }
    }

    public int Body
    {
        get => _bodyValue;
        set
        {
            if (_bodyValue != value)
            {
                _bodyValue = value;
                Delta(MobileDelta.Body);
            }
        }
    }

    public int Hue
    {
        get => _hue;
        set
        {
            if (_hue != value)
            {
                _hue = value;
                Delta(MobileDelta.Hue);
            }
        }
    }

    public Direction Direction
    {
        get => _direction;
        set
        {
            if (_direction != value)
            {
                _direction = value;
                Delta(MobileDelta.Direction);
            }
        }
    }

    // Stats
    public int Hits { get; set; }
    public int HitsMax { get; set; }
    public int Stam { get; set; }
    public int StamMax { get; set; }
    public int Mana { get; set; }
    public int ManaMax { get; set; }

    // Skills (modern with Span<T> for performance)
    private readonly int[] _skills = new int[58];

    public Span<int> Skills => _skills;

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
}

[Flags]
public enum MobileDelta
{
    None = 0x00,
    Location = 0x01,
    Body = 0x02,
    Hue = 0x04,
    Direction = 0x08,
    Stats = 0x10,
    Hits = 0x20,
    Stam = 0x40,
    Mana = 0x80
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

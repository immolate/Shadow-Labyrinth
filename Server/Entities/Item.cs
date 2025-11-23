namespace Server;

/// <summary>
/// Base item class with modern C# features
/// </summary>
public class Item : IEntity
{
    private Point3D _location;
    private Map _map;
    private string _name;
    private int _amount;
    private int _hue;
    private int _itemId;

    public Item(int itemId)
    {
        Serial = World.NewItemSerial();
        _itemId = itemId;
        _amount = 1;
        _hue = 0;
        _map = Map.Felucca;
        _location = Point3D.Zero;
        _name = string.Empty;

        World.AddEntity(this);
    }

    public Serial Serial { get; }
    public bool Deleted { get; private set; }

    public int ItemId
    {
        get => _itemId;
        set
        {
            if (_itemId != value)
            {
                _itemId = value;
                Delta(ItemDelta.Properties);
            }
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value ?? string.Empty;
                Delta(ItemDelta.Properties);
            }
        }
    }

    public Point3D Location
    {
        get => _location;
        set
        {
            if (_location != value)
            {
                _location = value;
                Delta(ItemDelta.Location);
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
                Delta(ItemDelta.Location);
            }
        }
    }

    public int Amount
    {
        get => _amount;
        set
        {
            if (_amount != value)
            {
                _amount = Math.Max(0, value);
                Delta(ItemDelta.Properties);

                if (_amount == 0)
                    Delete();
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
                Delta(ItemDelta.Properties);
            }
        }
    }

    public Mobile? RootParent { get; set; }

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

    protected virtual void Delta(ItemDelta delta)
    {
        // TODO: Network delta handling
    }

    public virtual void OnDoubleClick(Mobile from)
    {
    }

    public virtual void OnSingleClick(Mobile from)
    {
        if (!string.IsNullOrEmpty(_name))
            from.SendMessage(_name);
    }
}

[Flags]
public enum ItemDelta
{
    None = 0x00,
    Location = 0x01,
    Properties = 0x02,
    Contents = 0x04
}

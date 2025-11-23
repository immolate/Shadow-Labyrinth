using System.Runtime.CompilerServices;

namespace Server;

/// <summary>
/// Highly optimized item class with delta batching and minimal allocations
/// </summary>
public class Item : IEntity
{
    private Point3D _location;
    private Map _map;
    private string _name;
    private int _amount;
    private int _hue;
    private int _itemId;

    // Delta batching for network optimization
    private ItemDelta _pendingDeltas;
    private bool _deltaQueued;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int ItemId
    {
        get => _itemId;
        set
        {
            if (_itemId != value)
            {
                _itemId = value;
                QueueDelta(ItemDelta.Properties);
            }
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            var newName = value ?? string.Empty;
            if (_name != newName)
            {
                // Intern common item names to reduce memory
                _name = newName.Length < 32 ? string.Intern(newName) : newName;
                QueueDelta(ItemDelta.Properties);
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
                QueueDelta(ItemDelta.Location);
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
                QueueDelta(ItemDelta.Location);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Amount
    {
        get => _amount;
        set
        {
            if (_amount != value)
            {
                _amount = Math.Max(0, value);
                QueueDelta(ItemDelta.Properties);

                if (_amount == 0)
                    Delete();
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
                QueueDelta(ItemDelta.Properties);
            }
        }
    }

    public Mobile? RootParent { get; set; }

    // Batch delta updates to reduce network traffic
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void QueueDelta(ItemDelta delta)
    {
        _pendingDeltas |= delta;

        if (!_deltaQueued)
        {
            _deltaQueued = true;
            World.QueueItemDelta(this);
        }
    }

    internal void ProcessDeltas()
    {
        if (_pendingDeltas != ItemDelta.None)
        {
            Delta(_pendingDeltas);
            _pendingDeltas = ItemDelta.None;
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

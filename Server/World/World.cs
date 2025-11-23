using System.Collections.Concurrent;

namespace Server;

/// <summary>
/// World management system with modern concurrent collections
/// </summary>
public static class World
{
    private static readonly ConcurrentDictionary<Serial, IEntity> _entities = new();
    private static readonly ConcurrentDictionary<Serial, Mobile> _mobiles = new();
    private static readonly ConcurrentDictionary<Serial, Item> _items = new();

    private static int _itemCount;
    private static int _mobileCount;

    public static IEnumerable<IEntity> Entities => _entities.Values;
    public static IEnumerable<Mobile> Mobiles => _mobiles.Values;
    public static IEnumerable<Item> Items => _items.Values;

    public static int EntityCount => _entities.Count;
    public static int MobileCount => _mobiles.Count;
    public static int ItemCount => _items.Count;

    public static void Load()
    {
        Console.WriteLine("Loading world...");

        var savePath = Path.Combine(Configuration.SavesPath, "world.dat");

        if (!File.Exists(savePath))
        {
            Console.WriteLine("No world save found. Creating new world...");
            CreateDefaultWorld();
            return;
        }

        try
        {
            // TODO: Implement binary serialization
            Console.WriteLine($"World loaded: {_entities.Count} entities");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading world: {ex.Message}");
            CreateDefaultWorld();
        }
    }

    public static void Save()
    {
        Console.WriteLine("Saving world...");

        try
        {
            var savePath = Path.Combine(Configuration.SavesPath, "world.dat");

            // Create backup
            if (File.Exists(savePath))
            {
                var backupPath = Path.Combine(Configuration.BackupsPath,
                    $"world_{DateTime.UtcNow:yyyyMMdd_HHmmss}.dat");
                File.Copy(savePath, backupPath, true);
            }

            // TODO: Implement binary serialization
            Console.WriteLine($"World saved: {_entities.Count} entities");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving world: {ex.Message}");
        }
    }

    private static void CreateDefaultWorld()
    {
        Console.WriteLine("Creating default world...");
        // World will be populated by scripts
    }

    public static void AddEntity(IEntity entity)
    {
        if (_entities.TryAdd(entity.Serial, entity))
        {
            if (entity is Mobile mobile)
                _mobiles.TryAdd(mobile.Serial, mobile);
            else if (entity is Item item)
                _items.TryAdd(item.Serial, item);
        }
    }

    public static void RemoveEntity(IEntity entity)
    {
        _entities.TryRemove(entity.Serial, out _);

        if (entity is Mobile)
            _mobiles.TryRemove(entity.Serial, out _);
        else if (entity is Item)
            _items.TryRemove(entity.Serial, out _);
    }

    public static IEntity? FindEntity(Serial serial)
    {
        _entities.TryGetValue(serial, out var entity);
        return entity;
    }

    public static Mobile? FindMobile(Serial serial)
    {
        _mobiles.TryGetValue(serial, out var mobile);
        return mobile;
    }

    public static Item? FindItem(Serial serial)
    {
        _items.TryGetValue(serial, out var item);
        return item;
    }

    public static Serial NewItemSerial()
    {
        return new Serial(0x40000000 + Interlocked.Increment(ref _itemCount));
    }

    public static Serial NewMobileSerial()
    {
        return new Serial(Interlocked.Increment(ref _mobileCount));
    }
}

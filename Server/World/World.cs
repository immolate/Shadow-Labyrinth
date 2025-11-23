using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Server;

/// <summary>
/// Highly optimized world management system with spatial indexing and delta batching
/// </summary>
public static class World
{
    private static readonly ConcurrentDictionary<Serial, IEntity> _entities = new();
    private static readonly ConcurrentDictionary<Serial, Mobile> _mobiles = new();
    private static readonly ConcurrentDictionary<Serial, Item> _items = new();

    // Delta queue for batched network updates
    private static readonly HashSet<Mobile> _mobileDeltaQueue = new();
    private static readonly HashSet<Item> _itemDeltaQueue = new();
    private static readonly object _deltaLock = new();

    // Spatial indexing for fast location-based queries
    private static readonly Dictionary<Point2D, List<IEntity>> _spatialIndex = new();
    private static readonly object _spatialLock = new();

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

    // Delta queue management for batched network updates
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void QueueMobileDelta(Mobile mobile)
    {
        lock (_deltaLock)
        {
            _mobileDeltaQueue.Add(mobile);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void QueueItemDelta(Item item)
    {
        lock (_deltaLock)
        {
            _itemDeltaQueue.Add(item);
        }
    }

    /// <summary>
    /// Process all queued deltas - called each server tick
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static void ProcessDeltas()
    {
        // Process mobile deltas
        if (_mobileDeltaQueue.Count > 0)
        {
            List<Mobile> toProcess;

            lock (_deltaLock)
            {
                toProcess = new List<Mobile>(_mobileDeltaQueue);
                _mobileDeltaQueue.Clear();
            }

            foreach (var mobile in toProcess)
            {
                mobile.ProcessDeltas();
            }
        }

        // Process item deltas
        if (_itemDeltaQueue.Count > 0)
        {
            List<Item> toProcess;

            lock (_deltaLock)
            {
                toProcess = new List<Item>(_itemDeltaQueue);
                _itemDeltaQueue.Clear();
            }

            foreach (var item in toProcess)
            {
                item.ProcessDeltas();
            }
        }
    }

    // Spatial indexing for fast location-based queries
    private static Point2D GetSector(Point3D location)
    {
        return new Point2D(location.X >> 4, location.Y >> 4); // 16x16 sectors
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static IEnumerable<IEntity> GetEntitiesInRange(Point3D center, int range)
    {
        var results = new List<IEntity>(32);
        var rangeSq = range * range;

        int sectorRange = (range >> 4) + 1;
        var centerSector = GetSector(center);

        lock (_spatialLock)
        {
            for (int x = -sectorRange; x <= sectorRange; x++)
            {
                for (int y = -sectorRange; y <= sectorRange; y++)
                {
                    var sector = new Point2D(centerSector.X + x, centerSector.Y + y);

                    if (_spatialIndex.TryGetValue(sector, out var entities))
                    {
                        foreach (var entity in entities)
                        {
                            var dx = entity.Location.X - center.X;
                            var dy = entity.Location.Y - center.Y;
                            var distSq = dx * dx + dy * dy;

                            if (distSq <= rangeSq)
                                results.Add(entity);
                        }
                    }
                }
            }
        }

        return results;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static IEnumerable<Mobile> GetMobilesInRange(Point3D center, int range)
    {
        return GetEntitiesInRange(center, range).OfType<Mobile>();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static IEnumerable<Item> GetItemsInRange(Point3D center, int range)
    {
        return GetEntitiesInRange(center, range).OfType<Item>();
    }

    // Update spatial index when entity moves
    internal static void UpdateSpatialIndex(IEntity entity, Point3D oldLocation)
    {
        lock (_spatialLock)
        {
            // Remove from old sector
            var oldSector = GetSector(oldLocation);
            if (_spatialIndex.TryGetValue(oldSector, out var oldList))
            {
                oldList.Remove(entity);
                if (oldList.Count == 0)
                    _spatialIndex.Remove(oldSector);
            }

            // Add to new sector
            var newSector = GetSector(entity.Location);
            if (!_spatialIndex.TryGetValue(newSector, out var newList))
            {
                newList = new List<IEntity>(16);
                _spatialIndex[newSector] = newList;
            }

            newList.Add(entity);
        }
    }
}

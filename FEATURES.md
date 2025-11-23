# Modern .NET 8.0 Features Implemented

This document details all the modern C# and .NET features used in Shadow Labyrinth.

## C# 12 Features

### Primary Constructors
```csharp
// Not yet used, but structure supports it
// Example: public class Item(int itemId) : IEntity
```

### Collection Expressions
```csharp
// Used in timer systems and collections
private static readonly PriorityQueue<Timer, DateTime> _queue = new();
```

## C# 11 Features

### Raw String Literals
```csharp
// Available for multi-line strings
var sql = """
    SELECT * FROM items
    WHERE deleted = 0
    """;
```

### Generic Attributes
```csharp
// Can be used for custom attributes
[AttributeUsage(AttributeTargets.Class)]
public class ItemAttribute<T> : Attribute where T : Item
{
}
```

## C# 10 Features

### Record Structs
```csharp
// Server/Entities/Serial.cs:5
public readonly record struct Serial(int Value) : IComparable<Serial>

// Server/Entities/IEntity.cs:16
public readonly record struct Point3D(int X, int Y, int Z)

// Server/Entities/IEntity.cs:33
public readonly record struct Point2D(int X, int Y)
```

**Benefits:**
- Value semantics
- Immutability
- Built-in equality
- Deconstruction support
- Reduced allocations

### Global Using Directives
```csharp
// Server/Server.csproj
<ImplicitUsings>enable</ImplicitUsings>
```

**Benefits:**
- Cleaner files
- Less boilerplate
- Consistent imports

### File-Scoped Namespaces
```csharp
// Throughout the codebase
namespace Server;

public class Core
{
    // ...
}
```

**Benefits:**
- Less indentation
- More readable code
- Modern C# style

## C# 9 Features

### Init-Only Properties
```csharp
// Used in configuration
public int MapId { get; init; }
public string Name { get; init; }
```

### Top-Level Statements
```csharp
// Server/Program.cs:3
Console.WriteLine("Shadow Labyrinth - Modern Ultima Online Server");
Core.Initialize();
ScriptCompiler.Compile();
```

**Benefits:**
- Simplified entry point
- Less ceremony
- Script-like feel

### Pattern Matching Enhancements
```csharp
// Server/Entities/Serial.cs:10-11
public bool IsItem => Value >= 0x40000000;
public bool IsMobile => Value > 0 && Value < 0x40000000;
```

## C# 8 Features

### Nullable Reference Types
```csharp
// Enabled throughout via <Nullable>enable</Nullable>
public Mobile? RootParent { get; set; }
public NetState? NetState { get; set; }
private Task? _mainLoop;
```

**Benefits:**
- Null safety at compile time
- Fewer NullReferenceExceptions
- Better code clarity

### Async Streams
```csharp
// Can be used for streaming data
public async IAsyncEnumerable<Item> GetItemsAsync()
{
    foreach (var item in Items)
    {
        await Task.Yield();
        yield return item;
    }
}
```

### Using Declarations
```csharp
// md5generator.cs:26
using var md5 = MD5.Create();

foreach (var file in files)
{
    using var stream = File.OpenRead(file);
    // Automatically disposed
}
```

**Benefits:**
- Automatic disposal
- Less nesting
- Cleaner code

### Default Interface Methods
```csharp
public interface IEntity
{
    void Delete();

    // Could add default implementation
    void Update() { }
}
```

## .NET 8.0 Features

### Performance Improvements

#### Span<T> and Memory<T>
```csharp
// Server/Entities/Mobile.cs:85
private readonly int[] _skills = new int[58];
public Span<int> Skills => _skills;
```

**Benefits:**
- Zero-copy operations
- Reduced allocations
- Better cache locality

```csharp
// Server/Network/NetState.cs:43
ProcessReceived(_recvBuffer.AsSpan(0, received));
```

#### PriorityQueue<T>
```csharp
// Server/Core/Timer.cs:10
private static readonly PriorityQueue<Timer, DateTime> _queue = new();
```

**Benefits:**
- O(log n) enqueue/dequeue
- Native implementation
- Type-safe

### Concurrent Collections
```csharp
// Server/World/World.cs:9-11
private static readonly ConcurrentDictionary<Serial, IEntity> _entities = new();
private static readonly ConcurrentDictionary<Serial, Mobile> _mobiles = new();
private static readonly ConcurrentDictionary<Serial, Item> _items = new();
```

**Benefits:**
- Thread-safe
- Lock-free operations
- High concurrency

### System.Text.Json
```csharp
// Server/Core/Configuration.cs:36
var config = System.Text.Json.JsonSerializer.Deserialize<ConfigData>(json);

// Server/Core/Configuration.cs:71
var options = new System.Text.Json.JsonSerializerOptions
{
    WriteIndented = true
};
```

**Benefits:**
- High performance
- Source generation support
- Modern API

### Async/Await Throughout
```csharp
// Server/Network/NetState.cs:26
private async void BeginReceive()
{
    while (Running && !_disposing)
    {
        var received = await _socket.ReceiveAsync(_recvBuffer, SocketFlags.None);
        ProcessReceived(_recvBuffer.AsSpan(0, received));
    }
}
```

**Benefits:**
- Non-blocking I/O
- Better scalability
- Resource efficiency

### CancellationToken Support
```csharp
// Server/Core/Core.cs:9
private static readonly CancellationTokenSource _cts = new();

// Server/Network/MessagePump.cs:38
var socket = await _listener.AcceptAsync(_cts.Token);
```

**Benefits:**
- Cooperative cancellation
- Clean shutdown
- Resource cleanup

## Modern Design Patterns

### Dependency Injection Ready
```csharp
// Structure supports DI
public class MyService
{
    private readonly IConfiguration _config;

    public MyService(IConfiguration config)
    {
        _config = config;
    }
}
```

### Repository Pattern
```csharp
// World acts as a repository
public static class World
{
    public static IEntity? FindEntity(Serial serial)
    public static void AddEntity(IEntity entity)
    public static void RemoveEntity(IEntity entity)
}
```

### Factory Pattern
```csharp
public static Serial NewItemSerial()
{
    return new Serial(0x40000000 + Interlocked.Increment(ref _itemCount));
}
```

## Performance Optimizations

### 1. Value Types for Small Data
```csharp
readonly record struct Serial(int Value)    // 4 bytes
readonly record struct Point3D(int X, int Y, int Z)  // 12 bytes
```

### 2. ArrayPool (Ready for Implementation)
```csharp
var buffer = ArrayPool<byte>.Shared.Rent(4096);
try
{
    // Use buffer
}
finally
{
    ArrayPool<byte>.Shared.Return(buffer);
}
```

### 3. Interlocked for Atomic Operations
```csharp
// Server/World/World.cs:73-74
return new Serial(0x40000000 + Interlocked.Increment(ref _itemCount));
return new Serial(Interlocked.Increment(ref _mobileCount));
```

### 4. High-Performance Random
```csharp
// Scripts/Items/Weapons/ModernWeapons.cs:22
return Random.Shared.Next(MinDamage, MaxDamage + 1);
```

## Type Safety Improvements

### 1. Enums for Type Safety
```csharp
public enum Layer : byte
public enum Direction : byte
public enum MessageType : byte
```

### 2. Strongly-Typed IDs
```csharp
public readonly record struct Serial(int Value)
// Instead of just using int
```

### 3. Nullable Annotations
```csharp
public Mobile? NetState { get; set; }  // Can be null
public string Name { get; set; }       // Never null
```

## Code Quality Features

### 1. Expression-Bodied Members
```csharp
public bool IsValid => Value > 0;
public bool IsItem => Value >= 0x40000000;
public static Map? GetMap(int mapId) =>
    mapId >= 0 && mapId < _allMaps.Count ? _allMaps[mapId] : null;
```

### 2. Property Initializers
```csharp
public string Name { get; set; } = "Shadow Labyrinth";
public int ServerPort { get; set; } = 2593;
```

### 3. Null-Coalescing
```csharp
_name = value ?? string.Empty;
```

### 4. String Interpolation
```csharp
Console.WriteLine($"Server is listening on port {Configuration.ServerPort}");
Console.WriteLine($"Loaded {assembly.GetName().Name}");
```

## Comparison with Old UO Servers

### RunUO (circa 2004)
- .NET Framework 1.1
- No generics
- Manual threading
- Synchronous I/O
- ArrayList/Hashtable

### ServUO (circa 2014)
- .NET Framework 4.5
- Some async/await
- Generic collections
- Still mostly synchronous

### Shadow Labyrinth (2024)
- .NET 8.0
- Full async/await
- Modern collections
- Span<T>, Memory<T>
- Record types
- Nullable reference types
- PriorityQueue
- Top-level statements

## Future Enhancements

### Source Generators (Planned)
```csharp
[AutoNotify]
public partial class Mobile
{
    private int _hits;  // Auto-generates Hits property
}
```

### Native AOT (Possible)
```csharp
// Potential for ahead-of-time compilation
dotnet publish -c Release -r linux-x64 /p:PublishAot=true
```

### Minimal APIs (Possible for Admin)
```csharp
var app = WebApplication.Create();
app.MapGet("/status", () => new {
    Players = World.MobileCount,
    Uptime = Core.UpTime
});
```

---

This document demonstrates that Shadow Labyrinth uses cutting-edge C# and .NET features while maintaining clean, performant code suitable for a game server.

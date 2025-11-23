# Shadow Labyrinth - Modern .NET 8.0 Ultima Online Server

A modernized Ultima Online server implementation built with .NET 8.0, featuring cutting-edge C# language features and high-performance architecture.

## 🚀 Features

### Modern .NET 8.0 Architecture
- ✅ **Latest C# Language Features**: Record types, pattern matching, nullable reference types, top-level statements
- ✅ **High Performance**: Async/await throughout, ArrayPool, Span<T>, PriorityQueue
- ✅ **Modern Collections**: ConcurrentDictionary, ConcurrentBag for thread-safe operations
- ✅ **Improved Serialization**: System.Text.Json for configuration
- ✅ **Clean Code**: LINQ, expression-bodied members, using declarations

### Server Features
- 🌐 **Asynchronous Networking**: Full async/await socket handling
- 🎮 **Entity Management**: Modern world system with concurrent collections
- ⏱️ **High-Performance Timers**: PriorityQueue-based timer system
- 💾 **Auto-Save**: Configurable automatic world saving
- 🔧 **Hot-Reloadable Scripts**: Modular script system

### New Content
#### Modern Weapons
- **Plasma Sword**: High-tech energy blade
- **Quantum Katana**: Reality-warping weapon
- **Cybernetic Axe**: Tech-enhanced two-handed weapon
- **Nano Dagger**: Advanced stealth weapon

#### Modern Armor
- **Nanoweave Chest**: High-tech body armor
- **Quantum Shield**: Energy shield device
- **Exosuit Helm**: Powered helmet with HUD
- **Cybernetic Gloves**: Enhanced gauntlets

#### Advanced Consumables
- **Nano Healing Injector**: Advanced healing (30-50 HP)
- **Quantum Energy Cell**: Mana restoration (25-40 MP)
- **Stamina Booster**: Full stamina refresh
- **Enhancement Chip**: Temporary stat boost

#### NPCs
- **Cybernetic Guard**: Advanced security NPC
- **Quantum Merchant**: High-tech trader
- **Rogue AI**: Hostile synthetic entity
- **Tech Sage**: Knowledge NPC

## 📋 Requirements

- **.NET 8.0 SDK** or later
- **Windows, Linux, or macOS**
- **4GB RAM** minimum (8GB recommended)
- **UO Client** (any version compatible with protocol)

## 🔨 Building

```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build ShadowLabyrinth.sln

# Or build in Release mode
dotnet build ShadowLabyrinth.sln -c Release
```

## 🏃 Running

```bash
# Run the server
cd Server/bin/Debug/net8.0
dotnet ShadowLabyrinth.dll

# Or run directly from solution
dotnet run --project Server
```

## ⚙️ Configuration

The server creates a `config.json` file on first run:

```json
{
  "ServerName": "Shadow Labyrinth",
  "ServerPort": 2593,
  "DataPath": "./",
  "MaxClients": 256,
  "Debug": true
}
```

### Configuration Options

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| ServerName | string | "Shadow Labyrinth" | Server name shown to clients |
| ServerPort | int | 2593 | Port to listen on |
| DataPath | string | "./" | Path to UO data files |
| MaxClients | int | 256 | Maximum concurrent connections |
| Debug | bool | true | Enable debug logging |

## 📁 Project Structure

```
Shadow-Labyrinth/
├── Server/                    # Core server project
│   ├── Core/                  # Core systems
│   │   ├── Core.cs           # Main server loop
│   │   ├── Configuration.cs  # Configuration management
│   │   ├── Timer.cs          # High-performance timer system
│   │   └── ScriptCompiler.cs # Script loading
│   ├── Entities/             # World entities
│   │   ├── IEntity.cs        # Entity interface
│   │   ├── Serial.cs         # Entity serial numbers
│   │   ├── Item.cs           # Base item class
│   │   ├── Mobile.cs         # Base mobile class
│   │   └── Map.cs            # Map/facet system
│   ├── Network/              # Network layer
│   │   ├── NetState.cs       # Client connection
│   │   ├── MessagePump.cs    # Network listener
│   │   └── Packet.cs         # Packet system
│   ├── World/                # World management
│   │   └── World.cs          # World state
│   └── Program.cs            # Entry point
├── Scripts/                   # Content scripts
│   ├── Items/                # Item definitions
│   │   ├── Weapons/          # Modern weapons
│   │   ├── Armor/            # Modern armor
│   │   ├── Consumables/      # Consumable items
│   │   └── Decorations/      # Decorative items
│   └── Mobiles/              # NPC definitions
│       └── ModernNPCs.cs     # Modern NPCs
└── ShadowLabyrinth.sln       # Solution file
```

## 🆕 Modern C# Features Used

### Records and Value Types
```csharp
public readonly record struct Serial(int Value);
public readonly record struct Point3D(int X, int Y, int Z);
```

### Pattern Matching
```csharp
public bool IsItem => Value >= 0x40000000;
public bool IsMobile => Value > 0 && Value < 0x40000000;
```

### Async/Await
```csharp
private async void BeginReceive()
{
    var received = await _socket.ReceiveAsync(_recvBuffer, SocketFlags.None);
    ProcessReceived(_recvBuffer.AsSpan(0, received));
}
```

### Span<T> for Performance
```csharp
private readonly int[] _skills = new int[58];
public Span<int> Skills => _skills;
```

### Using Declarations
```csharp
using var md5 = MD5.Create();
foreach (var file in files)
{
    using var stream = File.OpenRead(file);
    // ...
}
```

### Nullable Reference Types
```csharp
public Mobile? RootParent { get; set; }
public NetState? NetState { get; set; }
```

### Top-Level Statements
```csharp
Console.WriteLine("Shadow Labyrinth - Modern Ultima Online Server");
Core.Initialize();
ScriptCompiler.Compile();
World.Load();
Core.Run();
```

### PriorityQueue
```csharp
private static readonly PriorityQueue<Timer, DateTime> _queue = new();
```

### Concurrent Collections
```csharp
private static readonly ConcurrentDictionary<Serial, IEntity> _entities = new();
```

## 🎯 Performance Optimizations

1. **Async I/O**: All network operations use async/await
2. **Memory Pooling**: ArrayPool usage for packet buffers
3. **Zero-Copy**: Span<T> for buffer manipulation
4. **Concurrent Collections**: Lock-free data structures
5. **PriorityQueue Timers**: O(log n) timer operations
6. **Value Types**: Records for reduced allocations

## 🔧 Extending the Server

### Creating Custom Items

```csharp
public class MyCustomItem : Item
{
    public MyCustomItem() : base(0x1234) // Item ID
    {
        Name = "My Custom Item";
        Hue = 1266; // Color
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("You activated the custom item!");
    }
}
```

### Creating Custom NPCs

```csharp
public class MyCustomNPC : BaseNPC
{
    public MyCustomNPC()
    {
        Name = "Custom NPC";
        Body = 0x190; // Human male
        HitsMax = Hits = 100;
    }

    public override void OnDoubleClick(Mobile from)
    {
        // NPC interaction logic
    }
}
```

## 📊 System Requirements

### Minimum
- .NET 8.0 Runtime
- 2 CPU cores
- 4GB RAM
- 1GB disk space

### Recommended
- .NET 8.0 SDK
- 4+ CPU cores
- 8GB+ RAM
- SSD storage
- Linux or Windows Server

## 🐛 Troubleshooting

### Server won't start
- Ensure .NET 8.0 is installed: `dotnet --version`
- Check port 2593 is not in use
- Verify file permissions

### Can't connect
- Check firewall settings
- Verify client login.cfg points to correct IP
- Ensure server is listening (check console output)

### Performance issues
- Enable Release build: `dotnet build -c Release`
- Increase auto-save interval in config
- Monitor with `dotnet-counters`

## 📝 Development Roadmap

- [x] Core server infrastructure
- [x] Modern entity system
- [x] Async networking
- [x] Modern items and NPCs
- [ ] Full packet implementation
- [ ] Serialization system
- [ ] Map file readers
- [ ] Combat system
- [ ] Skills system
- [ ] Crafting system
- [ ] Quest system
- [ ] Admin commands

## 📄 License

GPL-3.0 License - See LICENSE file for details

## 🤝 Contributing

Contributions welcome! Please:
1. Fork the repository
2. Create a feature branch
3. Use modern C# patterns
4. Add unit tests
5. Submit a pull request

## 🙏 Credits

Built with modern .NET technologies for the Ultima Online community.

---

**Shadow Labyrinth** - Where ancient mysteries meet cutting-edge technology.

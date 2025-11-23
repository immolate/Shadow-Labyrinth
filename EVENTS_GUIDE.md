# Shadow Labyrinth - Events System

Complete competitive event system with 15 modern events designed to attract players and create competitiveness.

## Event System Features

- **Automated Leaderboards** - Real-time score tracking
- **Reward Distribution** - Gold, items, and titles
- **Event Scheduling** - Timed events with registration
- **Multiple Event Types** - PvP, PvE, Crafting, Racing, Puzzle, Economic, Mixed
- **Concurrent Support** - Thread-safe with ConcurrentDictionary
- **Modern Architecture** - .NET 8.0 with async/await

---

## All 15 Events

### 1. Quantum Battle Royale (PvP)
**Duration:** 30 minutes
**Players:** 10-100
**Type:** PvP

100 players enter, 1 survives! Battle in a shrinking quantum zone.

**Mechanics:**
- Zone shrinks every 60 seconds
- Loot drops every 2 minutes
- Players outside zone take damage
- Last player standing wins

**Rewards:**
- 1st Place: 50,000 gold, "Quantum Champion" title, Quantum Katana, Quantum Shield

---

### 2. King of the Hill (PvP)
**Duration:** 15 minutes
**Players:** 5-50
**Type:** PvP

Capture and hold the Quantum Beacon to earn points.

**Mechanics:**
- Stand within 5 meters to capture
- Need 300 seconds total hold time to win
- Multiple players = contested (no points)
- First to reach time limit wins

**Rewards:**
- 1st Place: 30,000 gold, "Hill Conqueror" title, Power Fist, Exosuit Chestplate

---

### 3. Tech Race (Racing)
**Duration:** 10 minutes
**Players:** 3-30
**Type:** Racing

Race through quantum checkpoints with tech boosters.

**Mechanics:**
- 5 checkpoints to complete
- Use speed boosters
- First to finish all checkpoints wins
- Points based on placement

**Rewards:**
- 1st Place: 25,000 gold, "Speed Demon" title, Jetpack Module, Magnetic Boots

---

### 4. Team Deathmatch (PvP)
**Duration:** 20 minutes
**Players:** 6-10 (5v5)
**Type:** PvP

5v5 tactical team combat with respawns.

**Mechanics:**
- Two teams: Red vs Blue
- First team to 50 kills wins
- 5-second respawn timer
- Tactical teamwork required

**Rewards:**
- Winning Team: 15,000 gold, "Combat Elite" title, Plasma Grenades, Shield Booster Module

---

### 5. Boss Rush (PvE)
**Duration:** 30 minutes
**Players:** 3-20
**Type:** PvE

Defeat 5 powerful bosses in sequence.

**Mechanics:**
- 5 bosses with increasing difficulty
- Bosses: Corrupted AI Core, Plasma Titan, Quantum Wraith, Neural Hivemind, Dark Matter Entity
- Highest damage dealer wins
- Team-based PvE challenge

**Rewards:**
- 1st Place: 40,000 gold, "Boss Slayer" title, Quantum Katana, Photon Rifle, Exosuit Chestplate

---

### 6. Crafting Championship (Crafting)
**Duration:** 15 minutes
**Players:** 3-30
**Type:** Crafting

Craft the highest quality items to win.

**Mechanics:**
- Quality matters more than quantity
- Legendary items: 100 points
- Exceptional items: 50 points
- High quality items: 25 points
- Normal items: 10 points

**Rewards:**
- 1st Place: 25,000 gold, "Master Crafter" title, Quantum Forge, Nano Assembler, Molecular Printer

---

### 7. Quantum Market Tycoon (Economic)
**Duration:** 20 minutes
**Players:** 5-30
**Type:** Economic

Turn 10,000 gold into a fortune through trading.

**Mechanics:**
- Start with 10,000 gold
- Trade with NPCs and players
- Buy low, sell high
- Highest profit percentage wins

**Rewards:**
- 1st Place: 50,000 gold, "Trade Baron" title, Market Data Pad, Quantum Wallet, Trade Terminal

---

### 8. Capture the Quantum Core (PvP)
**Duration:** 25 minutes
**Players:** 6-20
**Type:** PvP

Team-based capture the flag gameplay.

**Mechanics:**
- Two teams with bases
- Capture enemy cores and return to base
- First team to 3 captures wins
- Defend your own core

**Rewards:**
- Winning Team: 20,000 gold, Quantum Core, Speed Booster Module

---

### 9. Horde Survival (PvE)
**Duration:** 30 minutes
**Players:** 2-10
**Type:** PvE

Survive endless waves of enemies.

**Mechanics:**
- Waves increase in difficulty
- Each wave: 5 enemies per wave number
- Higher waves = more points
- Survive as long as possible

**Rewards:**
- 1st Place: 35,000 gold, "Horde Breaker" title, Plasma Repeater, Titanium Platemail, Shield Generator

---

### 10. Quantum Treasure Hunt (Mixed)
**Duration:** 20 minutes
**Players:** 3-50
**Type:** Mixed

Find 20 hidden quantum artifacts across the map.

**Mechanics:**
- 20 treasures scattered across map
- Use quantum detector to scan
- Each treasure: 10 points
- Most treasures found wins

**Rewards:**
- 1st Place: 30,000 gold, "Treasure Hunter" title, Quantum Detector, Teleport Beacon, Quantum Core

---

### 11. Territory Control (PvP)
**Duration:** 20 minutes
**Players:** 8-30
**Type:** PvP

Capture and hold 5 zones across the map.

**Mechanics:**
- 5 control zones
- Points awarded every 5 seconds for holding zones
- Single player in zone = capture
- Most territory points wins

**Rewards:**
- Top 10: 25,000 gold, Territory Beacon, Control Node

---

### 12. Arena Tournament (PvP)
**Duration:** 30 minutes
**Players:** 4-16 (power of 2)
**Type:** PvP

Single-elimination 1v1 bracket tournament.

**Mechanics:**
- Randomized bracket
- 1v1 matches
- Winner advances, loser eliminated
- Last fighter standing wins

**Rewards:**
- Champion: 40,000 gold, "Arena Champion" title, Champion's Belt, Victory Banner, Quantum Katana

---

### 13. Resource Rush (Mixed)
**Duration:** 15 minutes
**Players:** 3-30
**Type:** Mixed

Gather the most valuable resources.

**Mechanics:**
- Different resources worth different points
- Dark Matter Sample: 50 points each
- Quantum Crystal: 30 points each
- Plasma Core: 20 points each
- Cybernetic Component: 15 points each
- Nanotube Bundle: 10 points each

**Rewards:**
- 1st Place: 20,000 gold, "Resource Baron" title, Quantum Pickaxe, Resource Scanner, Portable Smelter

---

### 14. Quantum Puzzle Challenge (Puzzle)
**Duration:** 20 minutes
**Players:** 3-50
**Type:** Puzzle

Solve quantum physics and technology puzzles.

**Mechanics:**
- 10 puzzles with increasing difficulty
- First solver gets 2x points
- Points: 50-150 per puzzle
- Type `/answer <your answer>` to submit

**Rewards:**
- 1st Place: 25,000 gold, "Quantum Genius" title, Neural Processor, Quantum Computer, AI Assistant

---

### 15. Dungeon Speedrun (Mixed)
**Duration:** 25 minutes
**Players:** 2-20
**Type:** Mixed

Complete the quantum dungeon as fast as possible.

**Mechanics:**
- 7 checkpoints including boss room
- Fight monsters, solve puzzles, defeat boss
- Fastest completion time wins
- Score based on time (faster = more points)

**Rewards:**
- 1st Place: 35,000 gold, "Speedrun Master" title, Swiftness Boots, Teleportation Orb, Time Dilation Device

---

## Event Types

| Type | Description | Events |
|------|-------------|---------|
| **PvP** | Player vs Player combat | Battle Royale, King of Hill, Team Deathmatch, Capture Core, Territory Control, Arena |
| **PvE** | Player vs Environment | Boss Rush, Horde Survival |
| **Crafting** | Item creation competition | Crafting Championship |
| **Racing** | Speed-based challenges | Tech Race |
| **Economic** | Trading and profit | Market Tycoon |
| **Puzzle** | Mental challenges | Puzzle Challenge |
| **Mixed** | Multiple mechanics | Treasure Hunt, Resource Rush, Dungeon Speedrun |

---

## Using the Event System

### Starting an Event

```csharp
// Create event instance
var battleRoyale = new QuantumBattleRoyale();

// Register with event manager
EventManager.RegisterEvent(battleRoyale);

// Players register
battleRoyale.Register(player);

// Start when ready
EventManager.StartEvent(battleRoyale);
```

### Registration

```csharp
// Players can register during registration phase
bool success = event.Register(player);

// Event automatically starts when minimum players reached
// or when manually triggered
```

### Leaderboard

```csharp
// View current standings
foreach (var (player, score) in event.Leaderboard.Take(10))
{
    Console.WriteLine($"{player.Name}: {score} points");
}
```

### Rewards

```csharp
// Rewards automatically distributed on event end
// Based on final leaderboard rankings
// Top players receive gold, items, and titles
```

---

## Architecture

### BaseEvent Class

Core event framework with:
- Participant management (ConcurrentBag)
- Score tracking (ConcurrentDictionary)
- Leaderboard system
- Reward distribution
- Event lifecycle (Scheduled → Registration → Active → Completed)

### EventReward Class

Reward system supporting:
- Gold amounts
- Item lists
- Title awards
- Rank-based distribution (Top 1, Top 3, Top 10, etc.)

### EventManager Class

Global event coordinator:
- Event scheduling
- Active event tracking
- Server-wide broadcasts
- Recurring event scheduling

---

## Modern Features

### Thread Safety
- ConcurrentDictionary for scores
- ConcurrentBag for participants
- Atomic operations for state changes

### Modern C# Patterns
- Record structs for immutable data
- Pattern matching in switch expressions
- Nullable reference types
- LINQ for leaderboard queries

### Performance
- PriorityQueue for timer system
- Efficient concurrent collections
- Minimal allocations
- Async/await for I/O

---

## Customization

### Creating New Events

```csharp
public class MyCustomEvent : BaseEvent
{
    public MyCustomEvent() : base("Event Name", TimeSpan.FromMinutes(20))
    {
        MinPlayers = 5;
        MaxPlayers = 50;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 10000,
            Title = "Custom Champion"
        });
    }

    public override string Description => "Event description";
    public override EventType Type => EventType.Mixed;

    protected override void OnEventStart()
    {
        // Your event logic here
    }
}
```

### Event Hooks

- `OnPlayerRegistered(Mobile player)` - Player joins
- `OnEventStart()` - Event begins
- `OnEventEnd()` - Event completes

### Scoring

```csharp
// Add points
UpdateScore(player, points);

// Set exact score
SetScore(player, totalScore);

// Get current score
int score = GetScore(player);
```

---

## Statistics

| Metric | Value |
|--------|-------|
| Total Events | 15 |
| PvP Events | 6 |
| PvE Events | 2 |
| Mixed Events | 4 |
| Total Rewards | 15 unique reward sets |
| Total Gold Distributed | 470,000+ per event cycle |
| Unique Titles | 15 |
| Player Capacity | 2-100 per event |

---

## Scheduling Recommendations

### Daily Schedule Example

```
10:00 AM - Quantum Battle Royale (Prime Time)
12:00 PM - Crafting Championship (Lunch Hour)
02:00 PM - Boss Rush (Afternoon PvE)
04:00 PM - Team Deathmatch (Afternoon PvP)
06:00 PM - Arena Tournament (Evening Prime)
08:00 PM - Quantum Treasure Hunt (Evening Event)
10:00 PM - Horde Survival (Late Night PvE)
```

### Weekend Special Events

```
Saturday:
- Market Tycoon (Economic focus)
- Territory Control (Large-scale PvP)
- Dungeon Speedrun (Challenge mode)

Sunday:
- Battle Royale (Main attraction)
- Puzzle Challenge (Casual fun)
- Resource Rush (Gathering focus)
```

---

## Implementation Status

All 15 events are **fully implemented** with:
- ✓ Complete game mechanics
- ✓ Scoring systems
- ✓ Reward structures
- ✓ Timer integration
- ✓ Player notifications
- ✓ Leaderboard tracking
- ✓ Event lifecycle management

**Note:** Some events reference items/NPCs that need implementation in the base game systems.

---

## Files

- `Scripts/Events/EventSystem.cs` - Core event framework
- `Scripts/Events/BattleRoyale.cs` - All 15 event implementations
- `EVENTS_GUIDE.md` - This documentation

Total: **~1,500 lines** of modern event code

---

**Ready to attract players and create intense competition!** 🎮🏆

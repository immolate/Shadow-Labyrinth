using Server;
using Server.Items;

namespace Server.Events;

/// <summary>
/// Event 1: QUANTUM BATTLE ROYALE
/// Modern battle royale with shrinking zone and loot drops
/// </summary>
public class QuantumBattleRoyale : BaseEvent
{
    private Point3D _centerPoint = new(1000, 1000, 0);
    private int _currentRadius = 100;
    private const int _shrinkInterval = 60; // Seconds
    private readonly List<Point3D> _lootDrops = new();

    public QuantumBattleRoyale() : base("Quantum Battle Royale", TimeSpan.FromMinutes(30))
    {
        MinPlayers = 10;
        MaxPlayers = 100;

        // Rewards
        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 50000,
            Title = "Quantum Champion",
            Items = { new QuantumKatana(), new QuantumShield() }
        });

        Rewards.Add(new EventReward
        {
            MaxRank = 3,
            GoldAmount = 25000,
            Items = { new PhotonRifle() }
        });

        Rewards.Add(new EventReward
        {
            MaxRank = 10,
            GoldAmount = 10000,
            Items = { new RegenerationSerum { Amount = 10 } }
        });
    }

    public override string Description =>
        "100 players enter, 1 survives! Battle in a shrinking quantum zone. " +
        "Last player standing wins. Loot drops appear throughout the arena. " +
        "Zone shrinks every 60 seconds - stay inside or take damage!";

    public override EventType Type => EventType.PvP;

    protected override void OnEventStart()
    {
        // Teleport all players to arena
        foreach (var player in GetParticipants())
        {
            TeleportToArena(player);
            EquipStarterGear(player);
            player.SendMessage("Battle Royale started! Stay in the zone!");
        }

        // Start zone shrinking
        ScheduleZoneShrink();

        // Schedule loot drops
        ScheduleLootDrops();
    }

    private void TeleportToArena(Mobile player)
    {
        // Random position within starting radius
        int angle = Random.Shared.Next(360);
        int distance = Random.Shared.Next(_currentRadius);

        int x = _centerPoint.X + (int)(distance * Math.Cos(angle * Math.PI / 180));
        int y = _centerPoint.Y + (int)(distance * Math.Sin(angle * Math.PI / 180));

        player.Location = new Point3D(x, y, _centerPoint.Z);
        player.Map = Map.Felucca;
    }

    private void EquipStarterGear(Mobile player)
    {
        // Give basic starter items
        var weapon = new LaserPistol();
        var armor = new NanoweaveBodysuit();
        var healing = new QuickHealPatch { Amount = 5 };

        player.SendMessage("You received starter gear!");
    }

    private void ScheduleZoneShrink()
    {
        var timer = new ShrinkTimer(this);
        timer.Start();
    }

    private void ScheduleLootDrops()
    {
        // Drop loot every 2 minutes
        var timer = new LootDropTimer(this);
        timer.Start();
    }

    private void ShrinkZone()
    {
        _currentRadius = Math.Max(10, _currentRadius - 10);

        BroadcastMessage($"⚠️ ZONE SHRINKING! New radius: {_currentRadius}m");
        BroadcastMessage($"Get to the safe zone at ({_centerPoint.X}, {_centerPoint.Y})!");

        // Damage players outside zone
        foreach (var player in GetParticipants())
        {
            if (GetDistance(player.Location, _centerPoint) > _currentRadius)
            {
                player.SendMessage("⚠️ You're outside the safe zone! Taking damage!");
                player.Hits -= 10;

                if (player.Hits <= 0)
                {
                    OnPlayerEliminated(player);
                }
            }
        }

        // Check for winner
        CheckForWinner();
    }

    private void SpawnLootDrop()
    {
        int angle = Random.Shared.Next(360);
        int distance = Random.Shared.Next(_currentRadius - 10);

        int x = _centerPoint.X + (int)(distance * Math.Cos(angle * Math.PI / 180));
        int y = _centerPoint.Y + (int)(distance * Math.Sin(angle * Math.PI / 180));

        var location = new Point3D(x, y, _centerPoint.Z);
        _lootDrops.Add(location);

        BroadcastMessage($"📦 LOOT DROP at ({x}, {y})!");

        // Spawn high-tier items
        // In real implementation, create actual items at location
    }

    private void OnPlayerEliminated(Mobile player)
    {
        int placement = GetParticipants().Count(p => p.Hits > 0) + 1;
        player.SendMessage($"You were eliminated! Placement: #{placement}");

        SetScore(player, placement);
    }

    private void CheckForWinner()
    {
        var alive = GetParticipants().Where(p => p.Hits > 0).ToList();

        if (alive.Count == 1)
        {
            var winner = alive[0];
            BroadcastMessage($"🏆 WINNER: {winner.Name}!");
            SetScore(winner, 1);
            End();
        }
        else if (alive.Count == 0)
        {
            BroadcastMessage("No survivors! Draw!");
            End();
        }
    }

    private int GetDistance(Point3D p1, Point3D p2)
    {
        int dx = p1.X - p2.X;
        int dy = p1.Y - p2.Y;
        return (int)Math.Sqrt(dx * dx + dy * dy);
    }

    private class ShrinkTimer : Timer
    {
        private readonly QuantumBattleRoyale _event;

        public ShrinkTimer(QuantumBattleRoyale evt)
            : base(TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60))
        {
            _event = evt;
        }

        protected override void OnTick()
        {
            if (_event.Status == EventStatus.Active)
            {
                _event.ShrinkZone();
            }
            else
            {
                Stop();
            }
        }
    }

    private class LootDropTimer : Timer
    {
        private readonly QuantumBattleRoyale _event;

        public LootDropTimer(QuantumBattleRoyale evt)
            : base(TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(2))
        {
            _event = evt;
        }

        protected override void OnTick()
        {
            if (_event.Status == EventStatus.Active)
            {
                _event.SpawnLootDrop();
            }
            else
            {
                Stop();
            }
        }
    }
}

/// <summary>
/// Event 2: KING OF THE HILL
/// Capture and hold the quantum beacon
/// </summary>
public class KingOfTheHill : BaseEvent
{
    private Point3D _hillLocation = new(1500, 1500, 0);
    private Mobile? _currentKing;
    private int _kingHoldTime = 0;
    private const int _requiredHoldTime = 300; // 5 minutes total

    public KingOfTheHill() : base("King of the Hill", TimeSpan.FromMinutes(15))
    {
        MinPlayers = 5;
        MaxPlayers = 50;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 30000,
            Title = "Hill Conqueror",
            Items = { new PowerFist(), new ExosuitChestplate() }
        });
    }

    public override string Description =>
        "Capture and hold the Quantum Beacon! " +
        "Stand on the hill to earn points. " +
        "First to 300 seconds of control wins!";

    public override EventType Type => EventType.PvP;

    protected override void OnEventStart()
    {
        BroadcastMessage($"The Quantum Beacon is at ({_hillLocation.X}, {_hillLocation.Y})!");
        BroadcastMessage("Capture and hold the hill to earn points!");

        var timer = new HillTimer(this);
        timer.Start();
    }

    private void UpdateHillControl()
    {
        // Find players on the hill
        var playersOnHill = GetParticipants()
            .Where(p => GetDistance(p.Location, _hillLocation) <= 5)
            .ToList();

        if (playersOnHill.Count == 1)
        {
            // Single player controls hill
            var player = playersOnHill[0];

            if (_currentKing != player)
            {
                _currentKing = player;
                BroadcastMessage($"👑 {player.Name} captured the hill!");
            }

            _kingHoldTime++;
            UpdateScore(_currentKing, 1);
            _currentKing.SendMessage($"Holding hill... {_kingHoldTime}/{_requiredHoldTime}s");

            if (_kingHoldTime >= _requiredHoldTime)
            {
                BroadcastMessage($"🏆 {_currentKing.Name} wins by holding the hill!");
                End();
            }
        }
        else if (playersOnHill.Count > 1)
        {
            // Contested
            BroadcastMessage("⚔️ CONTESTED! Clear the hill to capture!");
            _currentKing = null;
        }
        else
        {
            // No one on hill
            if (_currentKing != null)
            {
                BroadcastMessage($"{_currentKing.Name} lost control of the hill!");
                _currentKing = null;
            }
        }
    }

    private int GetDistance(Point3D p1, Point3D p2)
    {
        return p1.GetDistance(p2);
    }

    private class HillTimer : Timer
    {
        private readonly KingOfTheHill _event;

        public HillTimer(KingOfTheHill evt)
            : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
        {
            _event = evt;
        }

        protected override void OnTick()
        {
            if (_event.Status == EventStatus.Active)
            {
                _event.UpdateHillControl();
            }
            else
            {
                Stop();
            }
        }
    }
}

/// <summary>
/// Event 3: TECH RACE
/// Racing event with tech boosters and obstacles
/// </summary>
public class TechRace : BaseEvent
{
    private readonly List<Point3D> _checkpoints = new();
    private readonly Dictionary<Mobile, int> _playerCheckpoints = new();

    public TechRace() : base("Tech Race", TimeSpan.FromMinutes(10))
    {
        MinPlayers = 3;
        MaxPlayers = 30;

        // Set up checkpoints
        _checkpoints.Add(new Point3D(1000, 1000, 0)); // Start
        _checkpoints.Add(new Point3D(1100, 1050, 0));
        _checkpoints.Add(new Point3D(1150, 1100, 10));
        _checkpoints.Add(new Point3D(1200, 1000, 0));
        _checkpoints.Add(new Point3D(1000, 1000, 0)); // Finish

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 25000,
            Title = "Speed Demon",
            Items = { new JetpackModule(), new MagneticBoots() }
        });
    }

    public override string Description =>
        "Race through quantum checkpoints! " +
        "Use tech boosters to gain speed. " +
        "First to complete all checkpoints wins!";

    public override EventType Type => EventType.Racing;

    protected override void OnEventStart()
    {
        foreach (var player in GetParticipants())
        {
            player.Location = _checkpoints[0];
            _playerCheckpoints[player] = 0;

            // Give speed boost items
            player.SendMessage("Race started! Head to checkpoint 1!");
        }

        var timer = new RaceTimer(this);
        timer.Start();
    }

    private void CheckPlayerProgress()
    {
        foreach (var player in GetParticipants())
        {
            if (!_playerCheckpoints.ContainsKey(player))
                continue;

            int currentCheckpoint = _playerCheckpoints[player];

            if (currentCheckpoint >= _checkpoints.Count - 1)
                continue; // Already finished

            var nextCheckpoint = _checkpoints[currentCheckpoint + 1];

            if (player.Location.GetDistance(nextCheckpoint) <= 5)
            {
                _playerCheckpoints[player]++;
                currentCheckpoint = _playerCheckpoints[player];

                if (currentCheckpoint >= _checkpoints.Count - 1)
                {
                    // Finished!
                    int placement = GetParticipants().Count(p =>
                        _playerCheckpoints.TryGetValue(p, out var cp) &&
                        cp >= _checkpoints.Count - 1) + 1;

                    SetScore(player, 1000 - placement); // Higher score for better placement
                    player.SendMessage($"🏁 FINISHED! Placement: #{placement}");
                    BroadcastMessage($"{player.Name} finished in position #{placement}!");
                }
                else
                {
                    player.SendMessage($"✓ Checkpoint {currentCheckpoint}/{_checkpoints.Count - 1}");
                }
            }
        }
    }

    private class RaceTimer : Timer
    {
        private readonly TechRace _event;

        public RaceTimer(TechRace evt)
            : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
        {
            _event = evt;
        }

        protected override void OnTick()
        {
            if (_event.Status == EventStatus.Active)
            {
                _event.CheckPlayerProgress();
            }
            else
            {
                Stop();
            }
        }
    }
}

/// <summary>
/// Event 4: TEAM DEATHMATCH
/// 5v5 tactical combat with respawns
/// </summary>
public class TeamDeathmatch : BaseEvent
{
    private readonly List<Mobile> _teamRed = new();
    private readonly List<Mobile> _teamBlue = new();
    private int _redKills = 0;
    private int _blueKills = 0;
    private const int _killsToWin = 50;

    public TeamDeathmatch() : base("Team Deathmatch", TimeSpan.FromMinutes(20))
    {
        MinPlayers = 6;
        MaxPlayers = 10; // 5v5

        Rewards.Add(new EventReward
        {
            MaxRank = 5, // Winning team
            GoldAmount = 15000,
            Title = "Combat Elite",
            Items = { new PlasmaGrenade { Amount = 10 }, new ShieldBoosterModule() }
        });
    }

    public override string Description =>
        "5v5 team combat! First team to 50 kills wins. " +
        "Respawn after death. Tactical teamwork required!";

    public override EventType Type => EventType.PvP;

    protected override void OnEventStart()
    {
        var participants = GetParticipants().ToList();

        // Assign teams
        for (int i = 0; i < participants.Count; i++)
        {
            if (i % 2 == 0)
            {
                _teamRed.Add(participants[i]);
                participants[i].SendMessage("You are on RED TEAM!");
            }
            else
            {
                _teamBlue.Add(participants[i]);
                participants[i].SendMessage("You are on BLUE TEAM!");
            }

            TeleportToSpawn(participants[i], i % 2 == 0);
        }

        BroadcastMessage("Team Deathmatch started! First to 50 kills wins!");
    }

    private void TeleportToSpawn(Mobile player, bool isRed)
    {
        if (isRed)
            player.Location = new Point3D(1000, 1000, 0);
        else
            player.Location = new Point3D(1100, 1100, 0);
    }

    public void OnPlayerKilled(Mobile victim, Mobile killer)
    {
        bool victimRed = _teamRed.Contains(victim);
        bool killerRed = _teamRed.Contains(killer);

        if (victimRed != killerRed) // Opposite teams
        {
            if (killerRed)
                _redKills++;
            else
                _blueKills++;

            UpdateScore(killer, 1);
            BroadcastMessage($"{killer.Name} killed {victim.Name}! Score: RED {_redKills} - BLUE {_blueKills}");

            // Respawn
            DelayTimer.Create(TimeSpan.FromSeconds(5), () =>
            {
                victim.Resurrect();
                TeleportToSpawn(victim, victimRed);
            });

            // Check win condition
            if (_redKills >= _killsToWin || _blueKills >= _killsToWin)
            {
                EndMatch();
            }
        }
    }

    private void EndMatch()
    {
        bool redWon = _redKills >= _killsToWin;
        var winners = redWon ? _teamRed : _teamBlue;

        BroadcastMessage($"🏆 {(redWon ? "RED" : "BLUE")} TEAM WINS! ({(redWon ? _redKills : _blueKills)} kills)");

        foreach (var winner in winners)
        {
            SetScore(winner, 1); // Mark as winner
        }

        End();
    }
}

/// <summary>
/// Event 5: BOSS RUSH
/// PvE challenge - defeat 5 bosses in sequence
/// </summary>
public class BossRush : BaseEvent
{
    private int _currentBoss = 0;
    private readonly List<string> _bossNames = new()
    {
        "Corrupted AI Core",
        "Plasma Titan",
        "Quantum Wraith",
        "Neural Hivemind",
        "Dark Matter Entity"
    };

    public BossRush() : base("Boss Rush", TimeSpan.FromMinutes(30))
    {
        MinPlayers = 3;
        MaxPlayers = 20;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 40000,
            Title = "Boss Slayer",
            Items = { new QuantumKatana(), new PhotonRifle(), new ExosuitChestplate() }
        });
    }

    public override string Description =>
        "Defeat 5 powerful bosses in sequence! " +
        "Each boss is stronger than the last. " +
        "Highest damage dealer gets bonus rewards!";

    public override EventType Type => EventType.PvE;

    protected override void OnEventStart()
    {
        foreach (var player in GetParticipants())
        {
            player.Location = new Point3D(2000, 2000, 0);
            player.SendMessage("Boss Rush started! Prepare for the first boss!");
        }

        SpawnNextBoss();
    }

    private void SpawnNextBoss()
    {
        if (_currentBoss >= _bossNames.Count)
        {
            BroadcastMessage("🏆 ALL BOSSES DEFEATED! Victory!");
            End();
            return;
        }

        var bossName = _bossNames[_currentBoss];
        BroadcastMessage($"⚔️ BOSS {_currentBoss + 1}: {bossName} has appeared!");
        BroadcastMessage($"Difficulty Level: {_currentBoss + 1}/5");

        // In real implementation, spawn actual boss NPC
        _currentBoss++;
    }

    public void OnBossKilled(Mobile killer, int damage)
    {
        BroadcastMessage($"{killer.Name} dealt the killing blow! ({damage} damage)");
        UpdateScore(killer, damage);

        SpawnNextBoss();
    }
}

/// <summary>
/// Event 6: CRAFTING CHAMPIONSHIP
/// Competitive crafting and quality competition
/// </summary>
public class CraftingChampionship : BaseEvent
{
    private readonly Dictionary<Mobile, int> _itemsCreated = new();
    private readonly Dictionary<Mobile, int> _qualityPoints = new();

    public CraftingChampionship() : base("Crafting Championship", TimeSpan.FromMinutes(15))
    {
        MinPlayers = 3;
        MaxPlayers = 30;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 25000,
            Title = "Master Crafter",
            Items = { new QuantumForge(), new NanoAssembler(), new MolecularPrinter() }
        });
    }

    public override string Description =>
        "Craft the highest quality items in 15 minutes! " +
        "Quality matters more than quantity. " +
        "Exceptional and legendary items earn bonus points!";

    public override EventType Type => EventType.Crafting;

    protected override void OnEventStart()
    {
        foreach (var player in GetParticipants())
        {
            player.Location = new Point3D(1500, 2000, 0);

            // Give crafting materials
            player.SendMessage("Crafting Championship started!");
            player.SendMessage("You have been given crafting materials!");
        }

        BroadcastMessage("Craft the highest quality items! Quality > Quantity!");
    }

    public void OnItemCrafted(Mobile crafter, int quality)
    {
        _itemsCreated.TryGetValue(crafter, out var count);
        _itemsCreated[crafter] = count + 1;

        int points = quality switch
        {
            >= 90 => 100, // Legendary
            >= 70 => 50,  // Exceptional
            >= 50 => 25,  // High Quality
            >= 30 => 10,  // Normal
            _ => 5        // Low Quality
        };

        _qualityPoints.TryGetValue(crafter, out var current);
        _qualityPoints[crafter] = current + points;

        UpdateScore(crafter, points);

        string qualityName = quality switch
        {
            >= 90 => "LEGENDARY",
            >= 70 => "EXCEPTIONAL",
            >= 50 => "HIGH QUALITY",
            _ => "NORMAL"
        };

        crafter.SendMessage($"Crafted {qualityName} item! +{points} points (Total: {current + points})");
    }

    protected override void OnEventEnd()
    {
        var topCrafter = _qualityPoints.OrderByDescending(x => x.Value).FirstOrDefault();
        if (topCrafter.Key != null)
        {
            BroadcastMessage($"🏆 {topCrafter.Key.Name} wins with {topCrafter.Value} quality points!");
        }
    }
}

/// <summary>
/// Event 7: QUANTUM MARKET TYCOON
/// Economic competition - most profitable trader wins
/// </summary>
public class QuantumMarketTycoon : BaseEvent
{
    private readonly Dictionary<Mobile, int> _startingGold = new();
    private readonly Dictionary<Mobile, int> _currentGold = new();
    private const int _startAmount = 10000;

    public QuantumMarketTycoon() : base("Quantum Market Tycoon", TimeSpan.FromMinutes(20))
    {
        MinPlayers = 5;
        MaxPlayers = 30;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 50000,
            Title = "Trade Baron",
            Items = { new MarketDataPad(), new QuantumWallet(), new TradeTerminal() }
        });
    }

    public override string Description =>
        "Start with 10,000 gold and turn it into a fortune! " +
        "Buy low, sell high. Trade with NPCs and players. " +
        "Highest profit percentage wins!";

    public override EventType Type => EventType.Economic;

    protected override void OnEventStart()
    {
        foreach (var player in GetParticipants())
        {
            _startingGold[player] = _startAmount;
            _currentGold[player] = _startAmount;

            player.SendMessage($"Trading started! You have {_startAmount} gold to trade!");
            player.SendMessage("Buy low, sell high! Profit percentage determines winner!");
        }

        // Spawn trading NPCs and items
        BroadcastMessage("Market is OPEN! Start trading!");
    }

    public void OnTrade(Mobile trader, int goldChange)
    {
        _currentGold.TryGetValue(trader, out var current);
        _currentGold[trader] = current + goldChange;

        int profit = _currentGold[trader] - _startAmount;
        float profitPercent = ((float)profit / _startAmount) * 100f;

        SetScore(trader, (int)profitPercent);

        if (goldChange > 0)
        {
            trader.SendMessage($"Profit! +{goldChange}g (Total: {_currentGold[trader]}g, {profitPercent:F1}% gain)");
        }
    }

    protected override void OnEventEnd()
    {
        var topTrader = _currentGold.OrderByDescending(x => x.Value).FirstOrDefault();
        if (topTrader.Key != null)
        {
            int profit = topTrader.Value - _startAmount;
            float percent = ((float)profit / _startAmount) * 100f;
            BroadcastMessage($"🏆 {topTrader.Key.Name} wins with {topTrader.Value}g ({percent:F1}% profit)!");
        }
    }
}

/// <summary>
/// Event 8: CAPTURE THE QUANTUM CORE
/// Team-based objective game
/// </summary>
public class CaptureTheCore : BaseEvent
{
    private Point3D _redBase = new(1000, 1000, 0);
    private Point3D _blueBase = new(1200, 1200, 0);
    private Mobile? _redCoreCarrier;
    private Mobile? _blueCoreCarrier;
    private int _redCaptures = 0;
    private int _blueCaptures = 0;
    private const int _capturesToWin = 3;

    public CaptureTheCore() : base("Capture the Quantum Core", TimeSpan.FromMinutes(25))
    {
        MinPlayers = 6;
        MaxPlayers = 20;

        Rewards.Add(new EventReward
        {
            MaxRank = 10, // Winning team
            GoldAmount = 20000,
            Items = { new QuantumCore(), new SpeedBoosterModule() }
        });
    }

    public override string Description =>
        "Capture enemy quantum cores and return to base! " +
        "First team to 3 captures wins. Defend your own core!";

    public override EventType Type => EventType.PvP;

    protected override void OnEventStart()
    {
        BroadcastMessage("Capture the Quantum Core! First to 3 captures wins!");

        // Assign teams and teleport
        var participants = GetParticipants().ToList();
        for (int i = 0; i < participants.Count; i++)
        {
            bool isRed = i % 2 == 0;
            participants[i].Location = isRed ? _redBase : _blueBase;
            participants[i].SendMessage($"You are on {(isRed ? "RED" : "BLUE")} team!");
        }
    }

    public void OnCorePickup(Mobile player, bool isRedCore)
    {
        if (isRedCore)
            _redCoreCarrier = player;
        else
            _blueCoreCarrier = player;

        BroadcastMessage($"⚠️ {player.Name} has the {(isRedCore ? "RED" : "BLUE")} core!");
        player.SendMessage("Return to your base to score!");
    }

    public void OnCoreCapture(Mobile player, bool scoredForRed)
    {
        if (scoredForRed)
            _redCaptures++;
        else
            _blueCaptures++;

        BroadcastMessage($"🎯 {(scoredForRed ? "RED" : "BLUE")} team scores! ({_redCaptures} - {_blueCaptures})");
        UpdateScore(player, 10);

        _redCoreCarrier = null;
        _blueCoreCarrier = null;

        if (_redCaptures >= _capturesToWin || _blueCaptures >= _capturesToWin)
        {
            bool redWon = _redCaptures >= _capturesToWin;
            BroadcastMessage($"🏆 {(redWon ? "RED" : "BLUE")} TEAM WINS!");
            End();
        }
    }
}

/// <summary>
/// Event 9: HORDE SURVIVAL
/// Wave-based PvE survival
/// </summary>
public class HordeSurvival : BaseEvent
{
    private int _currentWave = 0;
    private int _enemiesRemaining = 0;

    public HordeSurvival() : base("Horde Survival", TimeSpan.FromMinutes(30))
    {
        MinPlayers = 2;
        MaxPlayers = 10;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 35000,
            Title = "Horde Breaker",
            Items = { new PlasmaRepeater(), new TitaniumPlatemail(), new ShieldGenerator() }
        });
    }

    public override string Description =>
        "Survive endless waves of enemies! " +
        "Each wave gets harder. Last team standing wins!";

    public override EventType Type => EventType.PvE;

    protected override void OnEventStart()
    {
        foreach (var player in GetParticipants())
        {
            player.Location = new Point3D(2500, 2500, 0);
        }

        BroadcastMessage("Horde Survival started! Prepare for wave 1!");
        SpawnNextWave();
    }

    private void SpawnNextWave()
    {
        _currentWave++;
        _enemiesRemaining = _currentWave * 5; // 5, 10, 15, etc.

        BroadcastMessage($"🌊 WAVE {_currentWave} - {_enemiesRemaining} enemies!");

        // In real implementation, spawn enemies
    }

    public void OnEnemyKilled(Mobile killer)
    {
        _enemiesRemaining--;
        UpdateScore(killer, _currentWave); // More points for higher waves

        killer.SendMessage($"Enemy killed! {_enemiesRemaining} remaining");

        if (_enemiesRemaining <= 0)
        {
            BroadcastMessage($"✓ Wave {_currentWave} complete! Next wave in 10 seconds...");
            DelayTimer.Create(TimeSpan.FromSeconds(10), SpawnNextWave);
        }
    }

    protected override void OnEventEnd()
    {
        BroadcastMessage($"Event ended! Survived {_currentWave} waves!");
    }
}

/// <summary>
/// Event 10: QUANTUM TREASURE HUNT
/// Find hidden quantum artifacts across the map
/// </summary>
public class QuantumTreasureHunt : BaseEvent
{
    private readonly List<Point3D> _treasureLocations = new();
    private readonly HashSet<Point3D> _foundTreasures = new();
    private const int _totalTreasures = 20;

    public QuantumTreasureHunt() : base("Quantum Treasure Hunt", TimeSpan.FromMinutes(20))
    {
        MinPlayers = 3;
        MaxPlayers = 50;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 30000,
            Title = "Treasure Hunter",
            Items = { new QuantumDetector(), new TeleportBeacon(), new QuantumCore() }
        });
    }

    public override string Description =>
        "Find 20 hidden quantum artifacts across the map! " +
        "Use your detector to scan. Most treasures found wins!";

    public override EventType Type => EventType.Mixed;

    protected override void OnEventStart()
    {
        // Generate random treasure locations
        for (int i = 0; i < _totalTreasures; i++)
        {
            var loc = new Point3D(
                Random.Shared.Next(1000, 2000),
                Random.Shared.Next(1000, 2000),
                0
            );
            _treasureLocations.Add(loc);
        }

        foreach (var player in GetParticipants())
        {
            player.Location = new Point3D(1500, 1500, 0);
            player.SendMessage($"Find {_totalTreasures} quantum artifacts! Use your detector!");
        }

        BroadcastMessage($"Treasure Hunt started! {_totalTreasures} artifacts hidden!");
    }

    public void OnTreasureFound(Mobile finder, Point3D location)
    {
        if (_foundTreasures.Contains(location))
            return;

        _foundTreasures.Add(location);
        UpdateScore(finder, 10);

        int remaining = _totalTreasures - _foundTreasures.Count;
        BroadcastMessage($"💎 {finder.Name} found a treasure! {remaining} remaining!");

        if (_foundTreasures.Count >= _totalTreasures)
        {
            BroadcastMessage("All treasures found!");
            End();
        }
    }
}

/// <summary>
/// Event 11: TERRITORY CONTROL
/// Capture and hold multiple zones
/// </summary>
public class TerritoryControl : BaseEvent
{
    private readonly List<Point3D> _zones = new();
    private readonly Dictionary<Point3D, string> _zoneControl = new();
    private readonly Dictionary<string, int> _territoryPoints = new();

    public TerritoryControl() : base("Territory Control", TimeSpan.FromMinutes(20))
    {
        MinPlayers = 8;
        MaxPlayers = 30;

        // Setup 5 control zones
        _zones.Add(new Point3D(1000, 1000, 0));
        _zones.Add(new Point3D(1100, 1000, 0));
        _zones.Add(new Point3D(1000, 1100, 0));
        _zones.Add(new Point3D(1100, 1100, 0));
        _zones.Add(new Point3D(1050, 1050, 0)); // Center

        Rewards.Add(new EventReward
        {
            MaxRank = 10,
            GoldAmount = 25000,
            Items = { new TerritoryBeacon(), new ControlNode() }
        });
    }

    public override string Description =>
        "Control 5 zones across the map! " +
        "Hold zones to earn points. Most points wins!";

    public override EventType Type => EventType.PvP;

    protected override void OnEventStart()
    {
        BroadcastMessage("Territory Control! Capture and hold the zones!");

        var timer = new TerritoryTimer(this);
        timer.Start();
    }

    private void UpdateTerritories()
    {
        foreach (var zone in _zones)
        {
            var playersInZone = GetParticipants()
                .Where(p => p.Location.GetDistance(zone) <= 10)
                .ToList();

            if (playersInZone.Count == 1)
            {
                var controller = playersInZone[0].Name;
                _zoneControl[zone] = controller;

                UpdateScore(playersInZone[0], 1);
                _territoryPoints.TryGetValue(controller, out var points);
                _territoryPoints[controller] = points + 1;
            }
        }
    }

    private class TerritoryTimer : Timer
    {
        private readonly TerritoryControl _event;

        public TerritoryTimer(TerritoryControl evt)
            : base(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5))
        {
            _event = evt;
        }

        protected override void OnTick()
        {
            if (_event.Status == EventStatus.Active)
            {
                _event.UpdateTerritories();
            }
            else
            {
                Stop();
            }
        }
    }
}

/// <summary>
/// Event 12: ARENA TOURNAMENT
/// 1v1 bracket-style tournament
/// </summary>
public class ArenaTournament : BaseEvent
{
    private readonly Queue<Mobile> _bracket = new();
    private Mobile? _currentFighter1;
    private Mobile? _currentFighter2;

    public ArenaTournament() : base("Arena Tournament", TimeSpan.FromMinutes(30))
    {
        MinPlayers = 4;
        MaxPlayers = 16; // Power of 2 for bracket

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 40000,
            Title = "Arena Champion",
            Items = { new ChampionsBelt(), new VictoryBanner(), new QuantumKatana() }
        });
    }

    public override string Description =>
        "1v1 tournament bracket! " +
        "Single elimination. Last fighter standing wins!";

    public override EventType Type => EventType.PvP;

    protected override void OnEventStart()
    {
        var participants = GetParticipants().ToList();

        // Randomize bracket
        participants = participants.OrderBy(x => Random.Shared.Next()).ToList();

        foreach (var p in participants)
        {
            _bracket.Enqueue(p);
        }

        StartNextMatch();
    }

    private void StartNextMatch()
    {
        if (_bracket.Count < 2)
        {
            if (_bracket.Count == 1)
            {
                var winner = _bracket.Dequeue();
                BroadcastMessage($"🏆 {winner.Name} WINS THE TOURNAMENT!");
                SetScore(winner, 1);
                End();
            }
            return;
        }

        _currentFighter1 = _bracket.Dequeue();
        _currentFighter2 = _bracket.Dequeue();

        _currentFighter1.Location = new Point3D(1500, 1500, 0);
        _currentFighter2.Location = new Point3D(1510, 1510, 0);

        BroadcastMessage($"⚔️ MATCH: {_currentFighter1.Name} vs {_currentFighter2.Name}");
        BroadcastMessage("FIGHT!");
    }

    public void OnMatchComplete(Mobile winner, Mobile loser)
    {
        BroadcastMessage($"✓ {winner.Name} wins the match!");
        UpdateScore(winner, 1);

        // Winner advances
        _bracket.Enqueue(winner);

        // Start next match after delay
        DelayTimer.Create(TimeSpan.FromSeconds(10), StartNextMatch);
    }
}

/// <summary>
/// Event 13: RESOURCE RUSH
/// Gather the most resources in time limit
/// </summary>
public class ResourceRush : BaseEvent
{
    private readonly Dictionary<Mobile, Dictionary<string, int>> _gatheredResources = new();
    private readonly List<string> _resourceTypes = new()
    {
        "Quantum Crystal",
        "Plasma Core",
        "Nanotube Bundle",
        "Dark Matter Sample",
        "Cybernetic Component"
    };

    public ResourceRush() : base("Resource Rush", TimeSpan.FromMinutes(15))
    {
        MinPlayers = 3;
        MaxPlayers = 30;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 20000,
            Title = "Resource Baron",
            Items = { new QuantumPickaxe(), new ResourceScanner(), new PortableSmelter() }
        });
    }

    public override string Description =>
        "Gather the most valuable resources in 15 minutes! " +
        "Different resources worth different points. " +
        "Rare resources give bonus multipliers!";

    public override EventType Type => EventType.Mixed;

    protected override void OnEventStart()
    {
        foreach (var player in GetParticipants())
        {
            player.Location = new Point3D(3000, 3000, 0);
            _gatheredResources[player] = new Dictionary<string, int>();
            player.SendMessage("Resource Rush started! Gather valuable resources!");
        }

        BroadcastMessage("Resource nodes are scattered across the map!");
        BroadcastMessage("Rare resources worth more points!");

        // Spawn resource nodes
        SpawnResourceNodes();
    }

    private void SpawnResourceNodes()
    {
        // In real implementation, spawn harvestable resource nodes
        BroadcastMessage("Resource nodes spawned! Start gathering!");
    }

    public void OnResourceGathered(Mobile gatherer, string resourceType, int amount)
    {
        if (!_gatheredResources.ContainsKey(gatherer))
            _gatheredResources[gatherer] = new Dictionary<string, int>();

        var resources = _gatheredResources[gatherer];
        resources.TryGetValue(resourceType, out var current);
        resources[resourceType] = current + amount;

        // Calculate points based on rarity
        int points = resourceType switch
        {
            "Dark Matter Sample" => amount * 50,
            "Quantum Crystal" => amount * 30,
            "Plasma Core" => amount * 20,
            "Cybernetic Component" => amount * 15,
            "Nanotube Bundle" => amount * 10,
            _ => amount * 5
        };

        UpdateScore(gatherer, points);
        gatherer.SendMessage($"Gathered {amount}x {resourceType}! +{points} points");
    }

    protected override void OnEventEnd()
    {
        BroadcastMessage("═══════════════════════════");
        BroadcastMessage("RESOURCE TOTALS:");

        foreach (var (player, resources) in _gatheredResources.OrderByDescending(x => GetScore(x.Key)).Take(3))
        {
            BroadcastMessage($"{player.Name}: {GetScore(player)} points");
            foreach (var (resource, count) in resources)
            {
                BroadcastMessage($"  - {count}x {resource}");
            }
        }
    }
}

/// <summary>
/// Event 14: QUANTUM PUZZLE CHALLENGE
/// Solve puzzles and riddles for points
/// </summary>
public class QuantumPuzzleChallenge : BaseEvent
{
    private int _currentPuzzle = 0;
    private readonly List<(string Question, string Answer, int Points)> _puzzles = new()
    {
        ("What travels faster than light in quantum mechanics?", "information", 100),
        ("What particle has no mass but carries energy?", "photon", 100),
        ("What is the smallest unit of data?", "bit", 50),
        ("In binary, what is 1 + 1?", "10", 75),
        ("What does CPU stand for?", "central processing unit", 100),
        ("What is the speed of light in vacuum? (m/s)", "299792458", 150),
        ("What is Schrödinger's cat?", "both alive and dead", 100),
        ("What is quantum entanglement also called?", "spooky action", 125),
        ("What is the Planck constant symbol?", "h", 75),
        ("What is the theory of everything seeking to unify?", "forces", 100)
    };

    private readonly Dictionary<Mobile, HashSet<int>> _solvedPuzzles = new();

    public QuantumPuzzleChallenge() : base("Quantum Puzzle Challenge", TimeSpan.FromMinutes(20))
    {
        MinPlayers = 3;
        MaxPlayers = 50;

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 25000,
            Title = "Quantum Genius",
            Items = { new NeuralProcessor(), new QuantumComputer(), new AIAssistant() }
        });
    }

    public override string Description =>
        "Solve quantum physics and technology puzzles! " +
        "Harder puzzles worth more points. " +
        "First to solve gets bonus!";

    public override EventType Type => EventType.Puzzle;

    protected override void OnEventStart()
    {
        foreach (var player in GetParticipants())
        {
            _solvedPuzzles[player] = new HashSet<int>();
            player.SendMessage("Quantum Puzzle Challenge started!");
        }

        BroadcastMessage("Solve puzzles by typing your answers!");
        BroadcastMessage("Type /answer <your answer> to submit!");

        PresentNextPuzzle();
    }

    private void PresentNextPuzzle()
    {
        if (_currentPuzzle >= _puzzles.Count)
        {
            BroadcastMessage("All puzzles solved! Event ending...");
            End();
            return;
        }

        var puzzle = _puzzles[_currentPuzzle];
        BroadcastMessage("═══════════════════════════");
        BroadcastMessage($"PUZZLE #{_currentPuzzle + 1} ({puzzle.Points} points)");
        BroadcastMessage($"❓ {puzzle.Question}");
        BroadcastMessage("═══════════════════════════");
    }

    public void OnAnswerSubmitted(Mobile player, string answer)
    {
        if (_currentPuzzle >= _puzzles.Count)
            return;

        var puzzle = _puzzles[_currentPuzzle];

        if (answer.ToLower().Contains(puzzle.Answer.ToLower()))
        {
            if (!_solvedPuzzles.ContainsKey(player))
                _solvedPuzzles[player] = new HashSet<int>();

            if (_solvedPuzzles[player].Contains(_currentPuzzle))
            {
                player.SendMessage("You already solved this puzzle!");
                return;
            }

            _solvedPuzzles[player].Add(_currentPuzzle);

            // First solver gets bonus
            int solvers = _solvedPuzzles.Count(x => x.Value.Contains(_currentPuzzle));
            int points = solvers == 1 ? puzzle.Points * 2 : puzzle.Points;

            UpdateScore(player, points);
            BroadcastMessage($"✓ {player.Name} solved it! +{points} points!");

            if (solvers == 1)
            {
                _currentPuzzle++;
                DelayTimer.Create(TimeSpan.FromSeconds(5), PresentNextPuzzle);
            }
        }
        else
        {
            player.SendMessage("Incorrect! Try again...");
        }
    }
}

/// <summary>
/// Event 15: DUNGEON SPEEDRUN
/// Race to complete a dungeon fastest
/// </summary>
public class DungeonSpeedrun : BaseEvent
{
    private readonly Dictionary<Mobile, DateTime> _startTimes = new();
    private readonly Dictionary<Mobile, TimeSpan> _completionTimes = new();
    private readonly List<Point3D> _checkpoints = new();

    public DungeonSpeedrun() : base("Dungeon Speedrun", TimeSpan.FromMinutes(25))
    {
        MinPlayers = 2;
        MaxPlayers = 20;

        // Setup dungeon checkpoints
        _checkpoints.Add(new Point3D(3000, 3000, 0));  // Start
        _checkpoints.Add(new Point3D(3050, 3000, 0));  // Checkpoint 1
        _checkpoints.Add(new Point3D(3050, 3050, -1)); // Checkpoint 2 (down)
        _checkpoints.Add(new Point3D(3100, 3050, -1)); // Checkpoint 3
        _checkpoints.Add(new Point3D(3100, 3100, -2)); // Checkpoint 4 (down)
        _checkpoints.Add(new Point3D(3150, 3150, -2)); // Boss Room
        _checkpoints.Add(new Point3D(3150, 3150, 0));  // Exit Portal

        Rewards.Add(new EventReward
        {
            MaxRank = 1,
            GoldAmount = 35000,
            Title = "Speedrun Master",
            Items = { new SwiftnessBoots(), new TeleportationOrb(), new TimeDilationDevice() }
        });
    }

    public override string Description =>
        "Complete the quantum dungeon as fast as possible! " +
        "Fight monsters, solve puzzles, defeat the boss. " +
        "Fastest time wins!";

    public override EventType Type => EventType.Mixed;

    protected override void OnEventStart()
    {
        foreach (var player in GetParticipants())
        {
            player.Location = _checkpoints[0];
            _startTimes[player] = DateTime.UtcNow;
            player.SendMessage("Speedrun started! GO GO GO!");
        }

        BroadcastMessage("Race to the end of the dungeon!");
        BroadcastMessage("Defeat monsters, solve puzzles, beat the boss!");
    }

    public void OnCheckpointReached(Mobile player, int checkpointIndex)
    {
        if (checkpointIndex == _checkpoints.Count - 1)
        {
            // Finished!
            OnDungeonCompleted(player);
        }
        else
        {
            player.SendMessage($"✓ Checkpoint {checkpointIndex + 1}/{_checkpoints.Count}");
        }
    }

    private void OnDungeonCompleted(Mobile player)
    {
        if (!_startTimes.ContainsKey(player))
            return;

        var completionTime = DateTime.UtcNow - _startTimes[player];
        _completionTimes[player] = completionTime;

        int placement = _completionTimes.Count;

        // Score based on time (faster = more points)
        int points = Math.Max(1000 - (int)completionTime.TotalSeconds, 100);
        SetScore(player, points);

        BroadcastMessage($"🏁 {player.Name} finished in {completionTime.Minutes}m {completionTime.Seconds}s! (#{placement})");
        player.SendMessage($"Your time: {completionTime.Minutes}:{completionTime.Seconds:D2}");

        // Check if all players finished
        if (_completionTimes.Count >= GetParticipants().Count())
        {
            ShowFinalTimes();
            End();
        }
    }

    private void ShowFinalTimes()
    {
        BroadcastMessage("═══════════════════════════");
        BroadcastMessage("FINAL SPEEDRUN TIMES:");
        BroadcastMessage("═══════════════════════════");

        int rank = 1;
        foreach (var (player, time) in _completionTimes.OrderBy(x => x.Value))
        {
            string medal = rank switch
            {
                1 => "🥇",
                2 => "🥈",
                3 => "🥉",
                _ => $"#{rank}"
            };

            BroadcastMessage($"{medal} {player.Name}: {time.Minutes}:{time.Seconds:D2}.{time.Milliseconds:D3}");
            rank++;
        }
    }

    protected override void OnEventEnd()
    {
        var fastest = _completionTimes.OrderBy(x => x.Value).FirstOrDefault();
        if (fastest.Key != null)
        {
            BroadcastMessage($"🏆 SPEEDRUN CHAMPION: {fastest.Key.Name} ({fastest.Value.TotalSeconds:F2}s)");
        }
    }
}

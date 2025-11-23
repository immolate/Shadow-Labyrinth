using System.Diagnostics;
using System.Text;

namespace Server.Admin;

/// <summary>
/// Comprehensive admin commands for server management
/// </summary>
public static class AdminCommands
{
    public static void RegisterCommands()
    {
        // Player Management Commands
        AdminSystem.RegisterCommand("kick", "Kick a player from the server", AccessLevel.GameMaster, Kick);
        AdminSystem.RegisterCommand("ban", "Ban a player account", AccessLevel.Administrator, Ban);
        AdminSystem.RegisterCommand("jail", "Send a player to jail", AccessLevel.GameMaster, Jail);
        AdminSystem.RegisterCommand("mute", "Mute/unmute a player", AccessLevel.GameMaster, Mute);
        AdminSystem.RegisterCommand("teleport", "Teleport to coordinates or player", AccessLevel.GameMaster, Teleport);
        AdminSystem.RegisterCommand("tele", "Alias for teleport", AccessLevel.GameMaster, Teleport);
        AdminSystem.RegisterCommand("bring", "Bring a player to your location", AccessLevel.GameMaster, Bring);
        AdminSystem.RegisterCommand("setstats", "Set player stats", AccessLevel.GameMaster, SetStats);
        AdminSystem.RegisterCommand("setskill", "Set player skill", AccessLevel.GameMaster, SetSkill);
        AdminSystem.RegisterCommand("res", "Resurrect a player", AccessLevel.Counselor, Resurrect);
        AdminSystem.RegisterCommand("heal", "Fully heal a player", AccessLevel.Counselor, Heal);
        AdminSystem.RegisterCommand("freeze", "Freeze/unfreeze a player", AccessLevel.GameMaster, Freeze);

        // Server Management Commands
        AdminSystem.RegisterCommand("shutdown", "Shutdown the server", AccessLevel.Administrator, Shutdown);
        AdminSystem.RegisterCommand("restart", "Restart the server", AccessLevel.Administrator, Restart);
        AdminSystem.RegisterCommand("save", "Save the world", AccessLevel.Administrator, Save);
        AdminSystem.RegisterCommand("backup", "Create a backup", AccessLevel.Administrator, Backup);
        AdminSystem.RegisterCommand("broadcast", "Broadcast message to all players", AccessLevel.GameMaster, Broadcast);
        AdminSystem.RegisterCommand("announce", "Alias for broadcast", AccessLevel.GameMaster, Broadcast);

        // Event Management Commands
        AdminSystem.RegisterCommand("startevent", "Start an event", AccessLevel.GameMaster, StartEvent);
        AdminSystem.RegisterCommand("stopevent", "Stop an event", AccessLevel.GameMaster, StopEvent);
        AdminSystem.RegisterCommand("listevents", "List active events", AccessLevel.Counselor, ListEvents);

        // Monitoring Commands
        AdminSystem.RegisterCommand("serverinfo", "Display server statistics", AccessLevel.Counselor, ServerInfo);
        AdminSystem.RegisterCommand("info", "Alias for serverinfo", AccessLevel.Counselor, ServerInfo);
        AdminSystem.RegisterCommand("playerlist", "List all online players", AccessLevel.Counselor, PlayerList);
        AdminSystem.RegisterCommand("who", "Alias for playerlist", AccessLevel.Counselor, PlayerList);
        AdminSystem.RegisterCommand("performance", "Show performance metrics", AccessLevel.GameMaster, Performance);
        AdminSystem.RegisterCommand("perf", "Alias for performance", AccessLevel.GameMaster, Performance);
        AdminSystem.RegisterCommand("memory", "Display memory usage", AccessLevel.GameMaster, MemoryInfo);

        // World Management Commands
        AdminSystem.RegisterCommand("add", "Create an item", AccessLevel.GameMaster, CreateItem);
        AdminSystem.RegisterCommand("spawnnpc", "Spawn an NPC", AccessLevel.GameMaster, CreateMobile);
        AdminSystem.RegisterCommand("delete", "Delete targeted entity", AccessLevel.GameMaster, Delete);
        AdminSystem.RegisterCommand("go", "Teleport to coordinates", AccessLevel.GameMaster, Go);
        AdminSystem.RegisterCommand("props", "View/edit entity properties", AccessLevel.GameMaster, Properties);

        // Access Level Management
        AdminSystem.RegisterCommand("setaccess", "Set player access level", AccessLevel.Administrator, SetAccess);
        AdminSystem.RegisterCommand("getaccess", "Get player access level", AccessLevel.Counselor, GetAccess);

        // Admin Menu
        AdminSystem.RegisterCommand("admin", "Open admin menu", AccessLevel.Counselor, OpenAdminMenu);
        AdminSystem.RegisterCommand("menu", "Alias for admin", AccessLevel.Counselor, OpenAdminMenu);
        AdminSystem.RegisterCommand("help", "Display available commands", AccessLevel.Player, Help);
    }

    #region Player Management Commands

    private static void Kick(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: kick <player name>");
            return;
        }

        var playerName = string.Join(" ", args);
        var target = FindPlayer(playerName);

        if (target == null)
        {
            from.SendMessage($"Player '{playerName}' not found.");
            return;
        }

        if (AdminSystem.GetAccessLevel(target) >= AdminSystem.GetAccessLevel(from))
        {
            from.SendMessage("You cannot kick someone with equal or higher access level.");
            return;
        }

        target.SendMessage("You have been kicked from the server.");
        target.NetState?.Dispose();

        from.SendMessage($"You have kicked {target.Name}.");
        Console.WriteLine($"[Admin] {from.Name} kicked {target.Name}");
    }

    private static void Ban(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: ban <player name>");
            return;
        }

        var playerName = string.Join(" ", args);
        var target = FindPlayer(playerName);

        if (target == null)
        {
            from.SendMessage($"Player '{playerName}' not found.");
            return;
        }

        if (AdminSystem.GetAccessLevel(target) >= AdminSystem.GetAccessLevel(from))
        {
            from.SendMessage("You cannot ban someone with equal or higher access level.");
            return;
        }

        // TODO: Implement actual ban system with account tracking
        target.SendMessage("You have been banned from the server.");
        target.NetState?.Dispose();

        from.SendMessage($"You have banned {target.Name}.");
        Console.WriteLine($"[Admin] {from.Name} banned {target.Name}");
    }

    private static void Jail(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: jail <player name>");
            return;
        }

        var playerName = string.Join(" ", args);
        var target = FindPlayer(playerName);

        if (target == null)
        {
            from.SendMessage($"Player '{playerName}' not found.");
            return;
        }

        // Jail location (could be configured)
        target.Location = new Point3D(5275, 1164, 0);
        target.Map = Map.Felucca;
        target.SendMessage("You have been jailed by a staff member.");

        from.SendMessage($"You have jailed {target.Name}.");
        Console.WriteLine($"[Admin] {from.Name} jailed {target.Name}");
    }

    private static void Mute(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: mute <player name>");
            return;
        }

        var playerName = string.Join(" ", args);
        var target = FindPlayer(playerName);

        if (target == null)
        {
            from.SendMessage($"Player '{playerName}' not found.");
            return;
        }

        // TODO: Implement actual mute flag
        target.SendMessage("You have been muted by a staff member.");
        from.SendMessage($"You have muted {target.Name}.");
        Console.WriteLine($"[Admin] {from.Name} muted {target.Name}");
    }

    private static void Bring(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: bring <player name>");
            return;
        }

        var playerName = string.Join(" ", args);
        var target = FindPlayer(playerName);

        if (target == null)
        {
            from.SendMessage($"Player '{playerName}' not found.");
            return;
        }

        target.Location = from.Location;
        target.Map = from.Map;
        target.SendMessage($"You have been brought to {from.Name}.");
        from.SendMessage($"You have brought {target.Name} to your location.");
    }

    private static void Teleport(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: teleport <player name> OR teleport <x> <y> <z>");
            return;
        }

        // Check if coordinates
        if (args.Length >= 2 && int.TryParse(args[0], out var x) && int.TryParse(args[1], out var y))
        {
            var z = args.Length >= 3 && int.TryParse(args[2], out var zCoord) ? zCoord : 0;
            from.Location = new Point3D(x, y, z);
            from.SendMessage($"You have teleported to {x}, {y}, {z}.");
            return;
        }

        // Teleport to player
        var playerName = string.Join(" ", args);
        var target = FindPlayer(playerName);

        if (target == null)
        {
            from.SendMessage($"Player '{playerName}' not found.");
            return;
        }

        from.Location = target.Location;
        from.Map = target.Map;
        from.SendMessage($"You have teleported to {target.Name}.");
    }

    private static void SetStats(Mobile from, string[] args)
    {
        if (args.Length < 4)
        {
            from.SendMessage("Usage: setstats <player> <hits> <stam> <mana>");
            return;
        }

        var target = FindPlayer(args[0]);
        if (target == null)
        {
            from.SendMessage($"Player '{args[0]}' not found.");
            return;
        }

        if (int.TryParse(args[1], out var hits) && int.TryParse(args[2], out var stam) && int.TryParse(args[3], out var mana))
        {
            target.HitsMax = hits;
            target.Hits = hits;
            target.StamMax = stam;
            target.Stam = stam;
            target.ManaMax = mana;
            target.Mana = mana;

            from.SendMessage($"Set {target.Name}'s stats: {hits} HP, {stam} Stam, {mana} Mana");
            target.SendMessage($"Your stats have been set by {from.Name}.");
        }
        else
        {
            from.SendMessage("Invalid stat values.");
        }
    }

    private static void SetSkill(Mobile from, string[] args)
    {
        if (args.Length < 3)
        {
            from.SendMessage("Usage: setskill <player> <skill index> <value>");
            return;
        }

        var target = FindPlayer(args[0]);
        if (target == null)
        {
            from.SendMessage($"Player '{args[0]}' not found.");
            return;
        }

        if (int.TryParse(args[1], out var skillIndex) && int.TryParse(args[2], out var value))
        {
            if (skillIndex >= 0 && skillIndex < target.Skills.Length)
            {
                target.Skills[skillIndex] = value;
                from.SendMessage($"Set {target.Name}'s skill {skillIndex} to {value}.");
                target.SendMessage($"Your skill has been modified by {from.Name}.");
            }
            else
            {
                from.SendMessage("Invalid skill index.");
            }
        }
        else
        {
            from.SendMessage("Invalid parameters.");
        }
    }

    private static void Resurrect(Mobile from, string[] args)
    {
        Mobile target = from;

        if (args.Length > 0)
        {
            var playerName = string.Join(" ", args);
            target = FindPlayer(playerName);

            if (target == null)
            {
                from.SendMessage($"Player '{playerName}' not found.");
                return;
            }
        }

        target.Resurrect();
        from.SendMessage($"You have resurrected {target.Name}.");

        if (target != from)
            target.SendMessage($"You have been resurrected by {from.Name}.");
    }

    private static void Heal(Mobile from, string[] args)
    {
        Mobile target = from;

        if (args.Length > 0)
        {
            var playerName = string.Join(" ", args);
            target = FindPlayer(playerName);

            if (target == null)
            {
                from.SendMessage($"Player '{playerName}' not found.");
                return;
            }
        }

        target.Hits = target.HitsMax;
        target.Stam = target.StamMax;
        target.Mana = target.ManaMax;

        from.SendMessage($"You have healed {target.Name}.");

        if (target != from)
            target.SendMessage($"You have been healed by {from.Name}.");
    }

    private static void Freeze(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: freeze <player name>");
            return;
        }

        var playerName = string.Join(" ", args);
        var target = FindPlayer(playerName);

        if (target == null)
        {
            from.SendMessage($"Player '{playerName}' not found.");
            return;
        }

        // TODO: Implement actual freeze flag
        target.SendMessage("You have been frozen by a staff member.");
        from.SendMessage($"You have frozen {target.Name}.");
    }

    #endregion

    #region Server Management Commands

    private static void Shutdown(Mobile from, string[] args)
    {
        var delay = 30;
        if (args.Length > 0 && int.TryParse(args[0], out var parsedDelay))
            delay = parsedDelay;

        from.SendMessage($"Server will shutdown in {delay} seconds...");
        BroadcastToAll($"[Server] Server shutting down in {delay} seconds!");

        DelayTimer.Create(TimeSpan.FromSeconds(delay), () =>
        {
            BroadcastToAll("[Server] Server is now shutting down!");
            Console.WriteLine($"[Admin] Server shutdown initiated by {from.Name}");
            Core.Shutdown();
        });
    }

    private static void Restart(Mobile from, string[] args)
    {
        var delay = 30;
        if (args.Length > 0 && int.TryParse(args[0], out var parsedDelay))
            delay = parsedDelay;

        from.SendMessage($"Server will restart in {delay} seconds...");
        BroadcastToAll($"[Server] Server restarting in {delay} seconds!");

        DelayTimer.Create(TimeSpan.FromSeconds(delay), () =>
        {
            BroadcastToAll("[Server] Server is now restarting!");
            Console.WriteLine($"[Admin] Server restart initiated by {from.Name}");
            // TODO: Implement actual restart logic
            Core.Shutdown();
        });
    }

    private static void Save(Mobile from, string[] args)
    {
        from.SendMessage("Saving world...");
        BroadcastToAll("[Server] World save in progress...");

        Task.Run(() =>
        {
            World.Save();
            from.SendMessage("World save complete.");
            BroadcastToAll("[Server] World save complete!");
        });
    }

    private static void Backup(Mobile from, string[] args)
    {
        from.SendMessage("Creating backup...");

        Task.Run(() =>
        {
            try
            {
                var backupPath = Path.Combine(Configuration.BackupsPath,
                    $"manual_{DateTime.UtcNow:yyyyMMdd_HHmmss}");
                Directory.CreateDirectory(backupPath);

                // Copy saves to backup
                var savesPath = Configuration.SavesPath;
                foreach (var file in Directory.GetFiles(savesPath))
                {
                    var fileName = Path.GetFileName(file);
                    File.Copy(file, Path.Combine(backupPath, fileName), true);
                }

                from.SendMessage($"Backup created successfully at {backupPath}");
                Console.WriteLine($"[Admin] Backup created by {from.Name} at {backupPath}");
            }
            catch (Exception ex)
            {
                from.SendMessage($"Backup failed: {ex.Message}");
                Console.WriteLine($"[Admin] Backup failed: {ex}");
            }
        });
    }

    private static void Broadcast(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: broadcast <message>");
            return;
        }

        var message = string.Join(" ", args);
        BroadcastToAll($"[Staff] {message}");
        Console.WriteLine($"[Admin] {from.Name} broadcast: {message}");
    }

    #endregion

    #region Event Management Commands

    private static void StartEvent(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: startevent <event name>");
            from.SendMessage("Available events: BattleRoyale, KingOfTheHill, TechRace, TeamDeathmatch, BossRush, etc.");
            return;
        }

        var eventName = string.Join(" ", args);
        from.SendMessage($"Starting event: {eventName}");
        // TODO: Integrate with EventSystem
        Console.WriteLine($"[Admin] {from.Name} started event: {eventName}");
    }

    private static void StopEvent(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: stopevent <event name>");
            return;
        }

        var eventName = string.Join(" ", args);
        from.SendMessage($"Stopping event: {eventName}");
        // TODO: Integrate with EventSystem
        Console.WriteLine($"[Admin] {from.Name} stopped event: {eventName}");
    }

    private static void ListEvents(Mobile from, string[] args)
    {
        from.SendMessage("Active Events:");
        from.SendMessage("- None currently running");
        // TODO: Integrate with EventSystem to list active events
    }

    #endregion

    #region Monitoring Commands

    private static void ServerInfo(Mobile from, string[] args)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== Server Information ===");
        sb.AppendLine($"Uptime: {Core.UpTime:dd\\.hh\\:mm\\:ss}");
        sb.AppendLine($"Tick Count: {Core.TickCount:N0}");
        sb.AppendLine($"Start Time: {Core.StartTime:yyyy-MM-dd HH:mm:ss} UTC");
        sb.AppendLine($"Entities: {World.EntityCount:N0}");
        sb.AppendLine($"Mobiles: {World.MobileCount:N0}");
        sb.AppendLine($"Items: {World.ItemCount:N0}");
        sb.AppendLine($"Online Players: {GetOnlinePlayerCount()}");
        sb.AppendLine($".NET Version: {Environment.Version}");

        var lines = sb.ToString().Split('\n');
        foreach (var line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
                from.SendMessage(line.TrimEnd());
        }
    }

    private static void PlayerList(Mobile from, string[] args)
    {
        var players = World.Mobiles.Where(m => m.NetState != null).ToList();

        from.SendMessage($"=== Online Players ({players.Count}) ===");

        foreach (var player in players.OrderBy(p => p.Name))
        {
            var accessLevel = AdminSystem.GetAccessLevel(player);
            var accessStr = accessLevel > AccessLevel.Player ? $" [{accessLevel}]" : "";
            from.SendMessage($"- {player.Name}{accessStr} at {player.Location}");
        }
    }

    private static void Performance(Mobile from, string[] args)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== Performance Metrics ===");
        sb.AppendLine($"Tick Count: {Core.TickCount:N0}");
        sb.AppendLine($"TPS (Target): 50");
        sb.AppendLine($"Uptime: {Core.UpTime:dd\\.hh\\:mm\\:ss}");

        var avgTicksPerSecond = Core.UpTime.TotalSeconds > 0
            ? Core.TickCount / Core.UpTime.TotalSeconds
            : 0;
        sb.AppendLine($"Avg TPS: {avgTicksPerSecond:F2}");

        var lines = sb.ToString().Split('\n');
        foreach (var line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
                from.SendMessage(line.TrimEnd());
        }
    }

    private static void MemoryInfo(Mobile from, string[] args)
    {
        var totalMemory = GC.GetTotalMemory(false);
        var gen0 = GC.CollectionCount(0);
        var gen1 = GC.CollectionCount(1);
        var gen2 = GC.CollectionCount(2);

        from.SendMessage("=== Memory Information ===");
        from.SendMessage($"Total Memory: {totalMemory / 1024 / 1024:N2} MB");
        from.SendMessage($"GC Gen0 Collections: {gen0:N0}");
        from.SendMessage($"GC Gen1 Collections: {gen1:N0}");
        from.SendMessage($"GC Gen2 Collections: {gen2:N0}");
        from.SendMessage($"Process Memory: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024:N2} MB");
    }

    #endregion

    #region World Management Commands

    private static void CreateItem(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            from.SendMessage("Usage: add <item id>");
            return;
        }

        if (int.TryParse(args[0], out var itemId))
        {
            var item = new Item(itemId)
            {
                Location = from.Location,
                Map = from.Map
            };

            from.SendMessage($"Created item with ID {itemId} at your location.");
        }
        else
        {
            from.SendMessage("Invalid item ID.");
        }
    }

    private static void CreateMobile(Mobile from, string[] args)
    {
        var mobile = new Mobile
        {
            Name = args.Length > 0 ? string.Join(" ", args) : "NPC",
            Location = from.Location,
            Map = from.Map
        };

        from.SendMessage($"Created NPC '{mobile.Name}' at your location.");
    }

    private static void Delete(Mobile from, string[] args)
    {
        // TODO: Implement targeting system for entity selection
        from.SendMessage("Target the entity you wish to delete.");
        from.SendMessage("(Targeting system not yet implemented)");
    }

    private static void Go(Mobile from, string[] args)
    {
        if (args.Length < 2)
        {
            from.SendMessage("Usage: go <x> <y> [z]");
            return;
        }

        if (int.TryParse(args[0], out var x) && int.TryParse(args[1], out var y))
        {
            var z = args.Length >= 3 && int.TryParse(args[2], out var zCoord) ? zCoord : 0;
            from.Location = new Point3D(x, y, z);
            from.SendMessage($"You have teleported to {x}, {y}, {z}.");
        }
        else
        {
            from.SendMessage("Invalid coordinates.");
        }
    }

    private static void Properties(Mobile from, string[] args)
    {
        // TODO: Implement property editor
        from.SendMessage("Property editor not yet implemented.");
        from.SendMessage("This will allow viewing and editing entity properties.");
    }

    #endregion

    #region Access Level Commands

    private static void SetAccess(Mobile from, string[] args)
    {
        if (args.Length < 2)
        {
            from.SendMessage("Usage: setaccess <player> <level>");
            from.SendMessage("Levels: Player, Counselor, GameMaster, Seer, Administrator, Developer, Owner");
            return;
        }

        var target = FindPlayer(args[0]);
        if (target == null)
        {
            from.SendMessage($"Player '{args[0]}' not found.");
            return;
        }

        if (Enum.TryParse<AccessLevel>(args[1], true, out var level))
        {
            if (level > AdminSystem.GetAccessLevel(from))
            {
                from.SendMessage("You cannot set an access level higher than your own.");
                return;
            }

            AdminSystem.SetAccessLevel(target, level);
            from.SendMessage($"Set {target.Name}'s access level to {level}.");
            Console.WriteLine($"[Admin] {from.Name} set {target.Name}'s access to {level}");
        }
        else
        {
            from.SendMessage("Invalid access level.");
        }
    }

    private static void GetAccess(Mobile from, string[] args)
    {
        if (args.Length == 0)
        {
            var myLevel = AdminSystem.GetAccessLevel(from);
            from.SendMessage($"Your access level: {myLevel}");
            return;
        }

        var playerName = string.Join(" ", args);
        var target = FindPlayer(playerName);

        if (target == null)
        {
            from.SendMessage($"Player '{playerName}' not found.");
            return;
        }

        var level = AdminSystem.GetAccessLevel(target);
        from.SendMessage($"{target.Name}'s access level: {level}");
    }

    #endregion

    #region Admin Menu

    private static void OpenAdminMenu(Mobile from, string[] args)
    {
        AdminMenu.Open(from);
    }

    private static void Help(Mobile from, string[] args)
    {
        var commands = AdminSystem.GetAvailableCommands(from).ToList();

        from.SendMessage("=== Available Commands ===");

        if (commands.Count == 0)
        {
            from.SendMessage("No commands available. Use 'menu' for admin menu.");
            return;
        }

        var grouped = commands.GroupBy(cmd => cmd.RequiredLevel);

        foreach (var group in grouped.OrderBy(g => g.Key))
        {
            from.SendMessage($"--- {group.Key} Commands ---");
            foreach (var cmd in group.OrderBy(c => c.Name))
            {
                from.SendMessage($"  {cmd.Name} - {cmd.Description}");
            }
        }

        from.SendMessage($"Total: {commands.Count} commands available");
        from.SendMessage("Type 'menu' to open the admin menu interface.");
    }

    #endregion

    #region Helper Methods

    private static Mobile? FindPlayer(string name)
    {
        return World.Mobiles.FirstOrDefault(m =>
            m.NetState != null &&
            m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    private static void BroadcastToAll(string message)
    {
        foreach (var mobile in World.Mobiles.Where(m => m.NetState != null))
        {
            mobile.SendMessage(message);
        }
    }

    private static int GetOnlinePlayerCount()
    {
        return World.Mobiles.Count(m => m.NetState != null);
    }

    #endregion
}

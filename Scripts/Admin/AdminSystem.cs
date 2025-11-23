using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Server.Admin;

/// <summary>
/// Modern admin system with permission-based command framework
/// </summary>
public static class AdminSystem
{
    private static readonly ConcurrentDictionary<string, AdminCommand> _commands = new();
    private static readonly ConcurrentDictionary<Serial, AccessLevel> _accessLevels = new();

    /// <summary>
    /// Called by ScriptCompiler during server initialization
    /// </summary>
    public static void Configure()
    {
        Initialize();
    }

    public static void Initialize()
    {
        Console.WriteLine("Initializing Admin System...");

        // Register all admin commands
        AdminCommands.RegisterCommands();

        Console.WriteLine($"Admin System initialized with {_commands.Count} commands.");
    }

    public static void RegisterCommand(string name, string description, AccessLevel requiredLevel,
        Action<Mobile, string[]> handler)
    {
        var command = new AdminCommand
        {
            Name = name,
            Description = description,
            RequiredLevel = requiredLevel,
            Handler = handler
        };

        _commands[name.ToLower()] = command;
    }

    public static bool ExecuteCommand(Mobile from, string commandLine)
    {
        if (string.IsNullOrWhiteSpace(commandLine))
            return false;

        var parts = commandLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return false;

        var commandName = parts[0].ToLower();
        var args = parts.Skip(1).ToArray();

        if (!_commands.TryGetValue(commandName, out var command))
        {
            from.SendMessage($"Unknown command: {commandName}");
            return false;
        }

        var accessLevel = GetAccessLevel(from);
        if (accessLevel < command.RequiredLevel)
        {
            from.SendMessage("You do not have permission to use this command.");
            return false;
        }

        try
        {
            command.Handler(from, args);
            return true;
        }
        catch (Exception ex)
        {
            from.SendMessage($"Command error: {ex.Message}");
            if (Configuration.Debug)
                Console.WriteLine($"Command '{commandName}' error: {ex}");
            return false;
        }
    }

    public static AccessLevel GetAccessLevel(Mobile mobile)
    {
        if (_accessLevels.TryGetValue(mobile.Serial, out var level))
            return level;

        return AccessLevel.Player;
    }

    public static void SetAccessLevel(Mobile mobile, AccessLevel level)
    {
        _accessLevels[mobile.Serial] = level;
        mobile.SendMessage($"Your access level has been set to: {level}");
    }

    public static IEnumerable<AdminCommand> GetAvailableCommands(Mobile mobile)
    {
        var accessLevel = GetAccessLevel(mobile);
        return _commands.Values.Where(cmd => accessLevel >= cmd.RequiredLevel)
            .OrderBy(cmd => cmd.RequiredLevel)
            .ThenBy(cmd => cmd.Name);
    }

    public static IEnumerable<AdminCommand> GetAllCommands()
    {
        return _commands.Values.OrderBy(cmd => cmd.RequiredLevel).ThenBy(cmd => cmd.Name);
    }
}

/// <summary>
/// Access levels for admin system
/// </summary>
public enum AccessLevel
{
    Player = 0,
    Counselor = 1,
    GameMaster = 2,
    Seer = 3,
    Administrator = 4,
    Developer = 5,
    Owner = 6
}

/// <summary>
/// Represents an admin command
/// </summary>
public class AdminCommand
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AccessLevel RequiredLevel { get; set; }
    public Action<Mobile, string[]> Handler { get; set; } = null!;
}

/// <summary>
/// Extension methods for Mobile to support admin commands
/// </summary>
public static class MobileAdminExtensions
{
    public static bool IsStaff(this Mobile mobile)
    {
        return AdminSystem.GetAccessLevel(mobile) >= AccessLevel.Counselor;
    }

    public static bool IsGameMaster(this Mobile mobile)
    {
        return AdminSystem.GetAccessLevel(mobile) >= AccessLevel.GameMaster;
    }

    public static bool IsAdmin(this Mobile mobile)
    {
        return AdminSystem.GetAccessLevel(mobile) >= AccessLevel.Administrator;
    }

    public static void ExecuteAdminCommand(this Mobile mobile, string commandLine)
    {
        AdminSystem.ExecuteCommand(mobile, commandLine);
    }
}

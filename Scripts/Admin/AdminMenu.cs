using System.Text;

namespace Server.Admin;

/// <summary>
/// Modern interactive admin menu system
/// </summary>
public static class AdminMenu
{
    public static void Open(Mobile from)
    {
        var accessLevel = AdminSystem.GetAccessLevel(from);

        if (accessLevel < AccessLevel.Counselor)
        {
            from.SendMessage("You do not have access to the admin menu.");
            return;
        }

        ShowMainMenu(from);
    }

    private static void ShowMainMenu(Mobile from)
    {
        var accessLevel = AdminSystem.GetAccessLevel(from);

        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║     Shadow Labyrinth Admin Menu       ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage($"║ Access Level: {accessLevel,-24} ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ 1. Player Management                   ║");
        from.SendMessage("║ 2. Server Management                   ║");
        from.SendMessage("║ 3. Event Management                    ║");
        from.SendMessage("║ 4. World Management                    ║");
        from.SendMessage("║ 5. Monitoring & Diagnostics            ║");
        from.SendMessage("║ 6. Access Control                      ║");
        from.SendMessage("║ 7. Quick Actions                       ║");
        from.SendMessage("║ 8. All Commands (Help)                 ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type a number to select a menu category");
        from.SendMessage("Or use command directly: <command> [args]");
    }

    public static void ShowPlayerManagementMenu(Mobile from)
    {
        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║      Player Management Commands        ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ kick <player>      - Kick player       ║");
        from.SendMessage("║ ban <player>       - Ban player        ║");
        from.SendMessage("║ jail <player>      - Jail player       ║");
        from.SendMessage("║ mute <player>      - Mute player       ║");
        from.SendMessage("║ freeze <player>    - Freeze player     ║");
        from.SendMessage("║ bring <player>     - Bring to you      ║");
        from.SendMessage("║ tele <player>      - Teleport to       ║");
        from.SendMessage("║ res <player>       - Resurrect         ║");
        from.SendMessage("║ heal <player>      - Heal player       ║");
        from.SendMessage("║ setstats <p> <h> <s> <m> - Set stats  ║");
        from.SendMessage("║ setskill <p> <i> <v> - Set skill      ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type 'menu' to return to main menu");
    }

    public static void ShowServerManagementMenu(Mobile from)
    {
        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║      Server Management Commands        ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ save               - Save world        ║");
        from.SendMessage("║ backup             - Create backup     ║");
        from.SendMessage("║ broadcast <msg>    - Announce to all   ║");
        from.SendMessage("║ shutdown [sec]     - Shutdown server   ║");
        from.SendMessage("║ restart [sec]      - Restart server    ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type 'menu' to return to main menu");
    }

    public static void ShowEventManagementMenu(Mobile from)
    {
        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║       Event Management Commands        ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ startevent <name>  - Start event       ║");
        from.SendMessage("║ stopevent <name>   - Stop event        ║");
        from.SendMessage("║ listevents         - List active       ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ Available Events:                      ║");
        from.SendMessage("║ - BattleRoyale                         ║");
        from.SendMessage("║ - KingOfTheHill                        ║");
        from.SendMessage("║ - TechRace                             ║");
        from.SendMessage("║ - TeamDeathmatch                       ║");
        from.SendMessage("║ - BossRush                             ║");
        from.SendMessage("║ - And 10 more...                       ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type 'menu' to return to main menu");
    }

    public static void ShowWorldManagementMenu(Mobile from)
    {
        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║      World Management Commands         ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ go <x> <y> [z]     - Teleport to loc   ║");
        from.SendMessage("║ add <itemid>       - Create item       ║");
        from.SendMessage("║ spawnnpc [name]    - Spawn NPC         ║");
        from.SendMessage("║ delete             - Delete target     ║");
        from.SendMessage("║ props              - Edit properties   ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type 'menu' to return to main menu");
    }

    public static void ShowMonitoringMenu(Mobile from)
    {
        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║    Monitoring & Diagnostics Menu       ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ serverinfo (info)  - Server stats      ║");
        from.SendMessage("║ playerlist (who)   - Online players    ║");
        from.SendMessage("║ performance (perf) - Performance       ║");
        from.SendMessage("║ memory             - Memory usage      ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type 'menu' to return to main menu");
    }

    public static void ShowAccessControlMenu(Mobile from)
    {
        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║       Access Control Commands          ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ setaccess <p> <lvl> - Set access       ║");
        from.SendMessage("║ getaccess [player]  - Check access     ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ Access Levels:                         ║");
        from.SendMessage("║ - Player (0)                           ║");
        from.SendMessage("║ - Counselor (1)                        ║");
        from.SendMessage("║ - GameMaster (2)                       ║");
        from.SendMessage("║ - Seer (3)                             ║");
        from.SendMessage("║ - Administrator (4)                    ║");
        from.SendMessage("║ - Developer (5)                        ║");
        from.SendMessage("║ - Owner (6)                            ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type 'menu' to return to main menu");
    }

    public static void ShowQuickActionsMenu(Mobile from)
    {
        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║          Quick Actions Menu            ║");
        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage("║                                        ║");
        from.SendMessage("║ Q1. res              - Resurrect self  ║");
        from.SendMessage("║ Q2. heal             - Heal self       ║");
        from.SendMessage("║ Q3. save             - Save world      ║");
        from.SendMessage("║ Q4. who              - Player list     ║");
        from.SendMessage("║ Q5. info             - Server info     ║");
        from.SendMessage("║ Q6. perf             - Performance     ║");
        from.SendMessage("║                                        ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type 'menu' to return to main menu");
    }

    public static void ShowAllCommandsMenu(Mobile from)
    {
        var commands = AdminSystem.GetAvailableCommands(from).ToList();

        from.SendMessage("╔════════════════════════════════════════╗");
        from.SendMessage("║        All Available Commands          ║");
        from.SendMessage("╠════════════════════════════════════════╣");

        var grouped = commands.GroupBy(cmd => cmd.RequiredLevel);
        var count = 0;

        foreach (var group in grouped.OrderBy(g => g.Key))
        {
            from.SendMessage($"║ --- {group.Key} Commands ---");

            foreach (var cmd in group.OrderBy(c => c.Name))
            {
                var line = $"║ {cmd.Name,-15} - {cmd.Description}";
                if (line.Length > 40)
                    line = line.Substring(0, 37) + "...";
                else
                    line = line.PadRight(40) + " ║";

                from.SendMessage(line);
                count++;
            }
        }

        from.SendMessage("╠════════════════════════════════════════╣");
        from.SendMessage($"║ Total Commands: {count,-23} ║");
        from.SendMessage("╚════════════════════════════════════════╝");
        from.SendMessage("");
        from.SendMessage("Type 'menu' to return to main menu");
    }
}

/// <summary>
/// Menu selection handler for numbered menu options
/// </summary>
public class AdminMenuHandler
{
    public static void ProcessMenuInput(Mobile from, string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return;

        input = input.Trim();

        // Check if it's a menu number selection
        if (int.TryParse(input, out var selection))
        {
            HandleMenuSelection(from, selection);
            return;
        }

        // Otherwise, treat as a command
        from.ExecuteAdminCommand(input);
    }

    private static void HandleMenuSelection(Mobile from, int selection)
    {
        switch (selection)
        {
            case 1:
                AdminMenu.ShowPlayerManagementMenu(from);
                break;
            case 2:
                AdminMenu.ShowServerManagementMenu(from);
                break;
            case 3:
                AdminMenu.ShowEventManagementMenu(from);
                break;
            case 4:
                AdminMenu.ShowWorldManagementMenu(from);
                break;
            case 5:
                AdminMenu.ShowMonitoringMenu(from);
                break;
            case 6:
                AdminMenu.ShowAccessControlMenu(from);
                break;
            case 7:
                AdminMenu.ShowQuickActionsMenu(from);
                break;
            case 8:
                AdminMenu.ShowAllCommandsMenu(from);
                break;
            default:
                from.SendMessage("Invalid menu selection. Type 'menu' to see options.");
                break;
        }
    }
}

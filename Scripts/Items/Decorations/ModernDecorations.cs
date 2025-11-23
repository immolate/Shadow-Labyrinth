using Server;

namespace Server.Items;

/// <summary>
/// Modern decorative items
/// </summary>
public class HolographicDisplay : Item
{
    public HolographicDisplay() : base(0x1EA7) // Crystal
    {
        Name = "Holographic Display";
        Hue = 1266; // Cyan
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The holographic display flickers with data streams!");
    }
}

/// <summary>
/// Energy Core - Glowing power source
/// </summary>
public class EnergyCore : Item
{
    public EnergyCore() : base(0x1EA7)
    {
        Name = "Energy Core";
        Hue = 1358; // Purple
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The energy core pulses with raw power!");
    }
}

/// <summary>
/// Data Crystal - Information storage
/// </summary>
public class DataCrystal : Item
{
    private readonly string[] _dataMessages = new[]
    {
        "Ancient knowledge flows through the crystal...",
        "You see fragments of forgotten technology...",
        "The crystal contains schematics for advanced devices...",
        "Mysterious coordinates flash across your vision..."
    };

    public DataCrystal() : base(0x1F1C) // Crystal
    {
        Name = "Data Crystal";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        var message = _dataMessages[Random.Shared.Next(_dataMessages.Length)];
        from.SendMessage(message);
    }
}

/// <summary>
/// Cybernetic Workbench - Crafting station
/// </summary>
public class CyberneticWorkbench : Item
{
    public CyberneticWorkbench() : base(0x13E3) // Anvil-like
    {
        Name = "Cybernetic Workbench";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A sophisticated crafting station for creating advanced items.");
    }
}

/// <summary>
/// Teleportation Beacon - Travel marker
/// </summary>
public class TeleportationBeacon : Item
{
    public TeleportationBeacon() : base(0x176B) // Brazier
    {
        Name = "Teleportation Beacon";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The beacon hums, ready to guide teleportation...");
        // In full implementation, this would mark a recall location
    }
}

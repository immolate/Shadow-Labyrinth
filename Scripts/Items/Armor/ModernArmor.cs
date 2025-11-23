using Server;

namespace Server.Items;

/// <summary>
/// Modern armor base class
/// </summary>
public abstract class BaseArmor : Item
{
    protected BaseArmor(int itemId) : base(itemId)
    {
    }

    public int PhysicalResistance { get; set; }
    public int FireResistance { get; set; }
    public int ColdResistance { get; set; }
    public int PoisonResistance { get; set; }
    public int EnergyResistance { get; set; }
    public Layer ArmorLayer { get; set; }
}

/// <summary>
/// Nanoweave Chest - High-tech body armor
/// </summary>
public class NanoweaveChest : BaseArmor
{
    public NanoweaveChest() : base(0x13BF) // Plate chest
    {
        Name = "Nanoweave Combat Vest";
        Hue = 1150; // Tech blue
        PhysicalResistance = 15;
        FireResistance = 10;
        ColdResistance = 10;
        PoisonResistance = 12;
        EnergyResistance = 20;
        ArmorLayer = Layer.InnerTorso;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The nanoweave fibers shimmer with protective energy!");
    }
}

/// <summary>
/// Quantum Shield - Energy shield device
/// </summary>
public class QuantumShield : BaseArmor
{
    public QuantumShield() : base(0x1B76) // Heater shield
    {
        Name = "Quantum Energy Shield";
        Hue = 1266; // Cyan
        PhysicalResistance = 20;
        FireResistance = 15;
        ColdResistance = 15;
        PoisonResistance = 10;
        EnergyResistance = 25;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A quantum field shimmers into existence!");
    }
}

/// <summary>
/// Exosuit Helm - Powered helmet
/// </summary>
public class ExosuitHelm : BaseArmor
{
    public ExosuitHelm() : base(0x140E) // Close helm
    {
        Name = "Exosuit Helmet";
        Hue = 1152; // Metallic
        PhysicalResistance = 12;
        FireResistance = 8;
        ColdResistance = 8;
        PoisonResistance = 15;
        EnergyResistance = 18;
        ArmorLayer = Layer.Helm;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The HUD displays tactical information!");
    }
}

/// <summary>
/// Cybernetic Gloves - Enhanced gauntlets
/// </summary>
public class CyberneticGloves : BaseArmor
{
    public CyberneticGloves() : base(0x13C6) // Plate gloves
    {
        Name = "Cybernetic Gloves";
        Hue = 1161; // Silver
        PhysicalResistance = 10;
        FireResistance = 5;
        ColdResistance = 5;
        PoisonResistance = 8;
        EnergyResistance = 15;
        ArmorLayer = Layer.Gloves;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Your grip strength increases!");
    }
}

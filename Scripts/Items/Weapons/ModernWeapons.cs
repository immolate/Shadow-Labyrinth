using Server;

namespace Server.Items;

/// <summary>
/// Modern weapon base class with enhanced properties
/// </summary>
public abstract class BaseWeapon : Item
{
    protected BaseWeapon(int itemId) : base(itemId)
    {
        Layer = Layer.OneHanded;
    }

    public int MinDamage { get; set; } = 1;
    public int MaxDamage { get; set; } = 5;
    public int Speed { get; set; } = 40;
    public Layer Layer { get; set; }

    public virtual int GetDamage()
    {
        return Random.Shared.Next(MinDamage, MaxDamage + 1);
    }
}

/// <summary>
/// Plasma Sword - Modern sci-fi weapon
/// </summary>
public class PlasmaSword : BaseWeapon
{
    public PlasmaSword() : base(0x13B9) // Longsword graphic
    {
        Name = "Plasma Sword";
        Hue = 1266; // Bright cyan
        MinDamage = 25;
        MaxDamage = 45;
        Speed = 35;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The plasma blade hums with energy!");
    }
}

/// <summary>
/// Quantum Katana - High-tech blade
/// </summary>
public class QuantumKatana : BaseWeapon
{
    public QuantumKatana() : base(0x13FF) // Katana graphic
    {
        Name = "Quantum Katana";
        Hue = 1358; // Purple energy
        MinDamage = 30;
        MaxDamage = 50;
        Speed = 30;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Reality warps around the quantum edge!");
    }
}

/// <summary>
/// Cybernetic Axe - Tech-enhanced weapon
/// </summary>
public class CyberneticAxe : BaseWeapon
{
    public CyberneticAxe() : base(0x0F49) // Executioner's Axe
    {
        Name = "Cybernetic Axe";
        Hue = 1152; // Metallic blue
        MinDamage = 35;
        MaxDamage = 55;
        Speed = 45;
        Layer = Layer.TwoHanded;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The cybernetic servos whir to life!");
    }
}

/// <summary>
/// Nano Dagger - Advanced stealth weapon
/// </summary>
public class NanoDagger : BaseWeapon
{
    public NanoDagger() : base(0x0F52) // Dagger
    {
        Name = "Nano Dagger";
        Hue = 1161; // Silver
        MinDamage = 15;
        MaxDamage = 30;
        Speed = 25;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Nanobots swarm across the blade!");
    }
}

public enum Layer : byte
{
    Invalid = 0x00,
    OneHanded = 0x01,
    TwoHanded = 0x02,
    Shoes = 0x03,
    Pants = 0x04,
    Shirt = 0x05,
    Helm = 0x06,
    Gloves = 0x07,
    Ring = 0x08,
    Talisman = 0x09,
    Neck = 0x0A,
    Hair = 0x0B,
    Waist = 0x0C,
    InnerTorso = 0x0D,
    Bracelet = 0x0E,
    FacialHair = 0x10,
    MiddleTorso = 0x11,
    Earrings = 0x12,
    Arms = 0x13,
    Cloak = 0x14,
    Backpack = 0x15,
    OuterTorso = 0x16,
    OuterLegs = 0x17,
    InnerLegs = 0x18,
    Mount = 0x19,
    ShopBuy = 0x1A,
    ShopResale = 0x1B,
    ShopSell = 0x1C,
    Bank = 0x1D
}

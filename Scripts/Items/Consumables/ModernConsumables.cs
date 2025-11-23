using Server;

namespace Server.Items;

/// <summary>
/// Modern consumable items with advanced effects
/// </summary>
public abstract class BaseConsumable : Item
{
    protected BaseConsumable(int itemId) : base(itemId)
    {
    }

    public abstract void Consume(Mobile from);

    public override void OnDoubleClick(Mobile from)
    {
        if (!from.Map.IsValidLocation(from.Location))
        {
            from.SendMessage("You cannot use that here.");
            return;
        }

        Consume(from);

        if (--Amount <= 0)
            Delete();
    }
}

/// <summary>
/// Nano Healing Injector - Advanced healing
/// </summary>
public class NanoHealingInjector : BaseConsumable
{
    public NanoHealingInjector() : base(0x0F0C) // Potion bottle
    {
        Name = "Nano Healing Injector";
        Hue = 53; // Green
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var healAmount = Random.Shared.Next(30, 50);
        from.Hits = Math.Min(from.Hits + healAmount, from.HitsMax);
        from.SendMessage($"The nanobots heal you for {healAmount} hit points!");
    }
}

/// <summary>
/// Quantum Energy Cell - Mana restoration
/// </summary>
public class QuantumEnergyCell : BaseConsumable
{
    public QuantumEnergyCell() : base(0x0F0C)
    {
        Name = "Quantum Energy Cell";
        Hue = 1266; // Cyan
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var manaAmount = Random.Shared.Next(25, 40);
        from.Mana = Math.Min(from.Mana + manaAmount, from.ManaMax);
        from.SendMessage($"Quantum energy surges through you, restoring {manaAmount} mana!");
    }
}

/// <summary>
/// Stamina Booster - Enhanced stamina restoration
/// </summary>
public class StaminaBooster : BaseConsumable
{
    public StaminaBooster() : base(0x0F0C)
    {
        Name = "Stamina Booster";
        Hue = 38; // Orange
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.Stam = from.StamMax;
        from.SendMessage("You feel completely refreshed!");
    }
}

/// <summary>
/// Enhancement Chip - Temporary stat boost
/// </summary>
public class EnhancementChip : BaseConsumable
{
    public EnhancementChip() : base(0x1BFB) // Gem
    {
        Name = "Enhancement Chip";
        Hue = 1152; // Tech blue
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("Neural pathways enhanced! Your stats temporarily increase!");
        // In a full implementation, this would apply timed buffs
    }
}

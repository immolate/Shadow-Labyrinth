using Server;

namespace Server.Items;

// ============================================================================
// ADVANCED CONSUMABLES & ENHANCERS - 30 Items
// ============================================================================

// HEALING ITEMS - 8 Items
public class RegenerationSerum : BaseConsumable
{
    public RegenerationSerum() : base(0x0F0C)
    {
        Name = "Regeneration Serum";
        Hue = 53;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var healAmount = Random.Shared.Next(40, 70);
        from.Hits = Math.Min(from.Hits + healAmount, from.HitsMax);
        from.SendMessage($"The serum heals you for {healAmount} hit points!");
    }
}

public class BiofoamInjector : BaseConsumable
{
    public BiofoamInjector() : base(0x0F0C)
    {
        Name = "Biofoam Injector";
        Hue = 67;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var healAmount = Random.Shared.Next(50, 80);
        from.Hits = Math.Min(from.Hits + healAmount, from.HitsMax);
        from.SendMessage($"Biofoam seals your wounds, healing {healAmount} HP!");
    }
}

public class NaniteSolution : BaseConsumable
{
    public NaniteSolution() : base(0x0F0C)
    {
        Name = "Nanite Solution";
        Hue = 1161;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var healAmount = Random.Shared.Next(35, 60);
        from.Hits = Math.Min(from.Hits + healAmount, from.HitsMax);
        from.SendMessage($"Nanites repair your body for {healAmount} HP!");
    }
}

public class QuickHealPatch : BaseConsumable
{
    public QuickHealPatch() : base(0x0E21) // Bandage
    {
        Name = "Quick Heal Patch";
        Hue = 53;
        Amount = 5;
    }

    public override void Consume(Mobile from)
    {
        var healAmount = Random.Shared.Next(15, 30);
        from.Hits = Math.Min(from.Hits + healAmount, from.HitsMax);
        from.SendMessage($"The patch heals you for {healAmount} HP!");
    }
}

public class CellRegenerator : BaseConsumable
{
    public CellRegenerator() : base(0x0F0C)
    {
        Name = "Cell Regenerator";
        Hue = 1266;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.Hits = from.HitsMax;
        from.SendMessage("Your cells regenerate to full health!");
    }
}

public class SynapticHealer : BaseConsumable
{
    public SynapticHealer() : base(0x0F0C)
    {
        Name = "Synaptic Healer";
        Hue = 1358;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var healAmount = Random.Shared.Next(45, 75);
        from.Hits = Math.Min(from.Hits + healAmount, from.HitsMax);
        from.SendMessage($"Neural pathways heal your body for {healAmount} HP!");
    }
}

public class MedicalStimpack : BaseConsumable
{
    public MedicalStimpack() : base(0x0F0C)
    {
        Name = "Medical Stimpack";
        Hue = 38;
        Amount = 3;
    }

    public override void Consume(Mobile from)
    {
        var healAmount = Random.Shared.Next(25, 45);
        from.Hits = Math.Min(from.Hits + healAmount, from.HitsMax);
        from.SendMessage($"The stimpack heals {healAmount} HP!");
    }
}

public class PhotonicHealingGel : BaseConsumable
{
    public PhotonicHealingGel() : base(0x0F0C)
    {
        Name = "Photonic Healing Gel";
        Hue = 1266;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var healAmount = Random.Shared.Next(55, 85);
        from.Hits = Math.Min(from.Hits + healAmount, from.HitsMax);
        from.SendMessage($"Photonic energy heals you for {healAmount} HP!");
    }
}

// ENERGY/MANA ITEMS - 8 Items
public class PsionicBooster : BaseConsumable
{
    public PsionicBooster() : base(0x0F0C)
    {
        Name = "Psionic Booster";
        Hue = 1358;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var manaAmount = Random.Shared.Next(30, 50);
        from.Mana = Math.Min(from.Mana + manaAmount, from.ManaMax);
        from.SendMessage($"Psionic energy restores {manaAmount} mana!");
    }
}

public class NeuralStimulant : BaseConsumable
{
    public NeuralStimulant() : base(0x0F0C)
    {
        Name = "Neural Stimulant";
        Hue = 1152;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var manaAmount = Random.Shared.Next(35, 55);
        from.Mana = Math.Min(from.Mana + manaAmount, from.ManaMax);
        from.SendMessage($"Neural pathways surge with {manaAmount} mana!");
    }
}

public class EtherealEssence : BaseConsumable
{
    public EtherealEssence() : base(0x0F0C)
    {
        Name = "Ethereal Essence";
        Hue = 1266;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.Mana = from.ManaMax;
        from.SendMessage("Ethereal energy fills your mind completely!");
    }
}

public class FocusCrystal : BaseConsumable
{
    public FocusCrystal() : base(0x1F1C)
    {
        Name = "Focus Crystal";
        Hue = 1152;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var manaAmount = Random.Shared.Next(40, 60);
        from.Mana = Math.Min(from.Mana + manaAmount, from.ManaMax);
        from.SendMessage($"The crystal restores {manaAmount} mana!");
    }
}

public class PowerCore : BaseConsumable
{
    public PowerCore() : base(0x1EA7)
    {
        Name = "Power Core";
        Hue = 1358;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var manaAmount = Random.Shared.Next(50, 70);
        from.Mana = Math.Min(from.Mana + manaAmount, from.ManaMax);
        from.SendMessage($"Raw power surges through you, restoring {manaAmount} mana!");
    }
}

public class MindFlux : BaseConsumable
{
    public MindFlux() : base(0x0F0C)
    {
        Name = "Mind Flux";
        Hue = 1161;
        Amount = 3;
    }

    public override void Consume(Mobile from)
    {
        var manaAmount = Random.Shared.Next(20, 35);
        from.Mana = Math.Min(from.Mana + manaAmount, from.ManaMax);
        from.SendMessage($"Mental flux restores {manaAmount} mana!");
    }
}

public class PlasmaBattery : BaseConsumable
{
    public PlasmaBattery() : base(0x1EA7)
    {
        Name = "Plasma Battery";
        Hue = 1266;
        Amount = 5;
    }

    public override void Consume(Mobile from)
    {
        var manaAmount = Random.Shared.Next(25, 40);
        from.Mana = Math.Min(from.Mana + manaAmount, from.ManaMax);
        from.SendMessage($"Plasma energy restores {manaAmount} mana!");
    }
}

public class CognitiveEnhancer : BaseConsumable
{
    public CognitiveEnhancer() : base(0x0F0C)
    {
        Name = "Cognitive Enhancer";
        Hue = 1152;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var manaAmount = Random.Shared.Next(45, 65);
        from.Mana = Math.Min(from.Mana + manaAmount, from.ManaMax);
        from.SendMessage($"Cognitive enhancement restores {manaAmount} mana!");
    }
}

// STAMINA ITEMS - 6 Items
public class AdrenalineShot : BaseConsumable
{
    public AdrenalineShot() : base(0x0F0C)
    {
        Name = "Adrenaline Shot";
        Hue = 38;
        Amount = 3;
    }

    public override void Consume(Mobile from)
    {
        from.Stam = from.StamMax;
        from.SendMessage("Adrenaline surges through your veins!");
    }
}

public class EndurancePill : BaseConsumable
{
    public EndurancePill() : base(0x0F7A) // Pill
    {
        Name = "Endurance Pill";
        Hue = 53;
        Amount = 5;
    }

    public override void Consume(Mobile from)
    {
        var stamAmount = Random.Shared.Next(40, 60);
        from.Stam = Math.Min(from.Stam + stamAmount, from.StamMax);
        from.SendMessage($"Endurance restored: {stamAmount} stamina!");
    }
}

public class EnergyDrink : BaseConsumable
{
    public EnergyDrink() : base(0x099B) // Bottle
    {
        Name = "Quantum Energy Drink";
        Hue = 1266;
        Amount = 3;
    }

    public override void Consume(Mobile from)
    {
        var stamAmount = Random.Shared.Next(50, 70);
        from.Stam = Math.Min(from.Stam + stamAmount, from.StamMax);
        from.SendMessage($"Energy floods your system: {stamAmount} stamina!");
    }
}

public class StaminaGel : BaseConsumable
{
    public StaminaGel() : base(0x0F0C)
    {
        Name = "Stamina Gel";
        Hue = 1152;
        Amount = 5;
    }

    public override void Consume(Mobile from)
    {
        var stamAmount = Random.Shared.Next(30, 50);
        from.Stam = Math.Min(from.Stam + stamAmount, from.StamMax);
        from.SendMessage($"The gel restores {stamAmount} stamina!");
    }
}

public class VitalitySerum : BaseConsumable
{
    public VitalitySerum() : base(0x0F0C)
    {
        Name = "Vitality Serum";
        Hue = 53;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.Stam = from.StamMax;
        from.Hits = Math.Min(from.Hits + 20, from.HitsMax);
        from.SendMessage("Vitality restored!");
    }
}

public class PerformanceEnhancer : BaseConsumable
{
    public PerformanceEnhancer() : base(0x0F0C)
    {
        Name = "Performance Enhancer";
        Hue = 1358;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        var stamAmount = Random.Shared.Next(60, 80);
        from.Stam = Math.Min(from.Stam + stamAmount, from.StamMax);
        from.SendMessage($"Peak performance achieved: {stamAmount} stamina!");
    }
}

// ENHANCEMENT ITEMS - 8 Items
public class StrengthAmplifier : BaseConsumable
{
    public StrengthAmplifier() : base(0x1BFB)
    {
        Name = "Strength Amplifier";
        Hue = 38;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("Your strength increases dramatically!");
        // In full implementation: add temporary stat buff
    }
}

public class ReflexBooster : BaseConsumable
{
    public ReflexBooster() : base(0x1BFB)
    {
        Name = "Reflex Booster";
        Hue = 1152;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("Your reflexes sharpen to superhuman levels!");
    }
}

public class IntelligenceMatrix : BaseConsumable
{
    public IntelligenceMatrix() : base(0x1BFB)
    {
        Name = "Intelligence Matrix";
        Hue = 1266;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("Your mind expands with newfound clarity!");
    }
}

public class ResistanceInjector : BaseConsumable
{
    public ResistanceInjector() : base(0x0F0C)
    {
        Name = "Resistance Injector";
        Hue = 1161;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("Your body becomes resistant to damage!");
    }
}

public class BerserkSerum : BaseConsumable
{
    public BerserkSerum() : base(0x0F0C)
    {
        Name = "Berserk Serum";
        Hue = 38;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("Rage fills your being! Damage output increased!");
    }
}

public class ShieldingElixir : BaseConsumable
{
    public ShieldingElixir() : base(0x0F0C)
    {
        Name = "Shielding Elixir";
        Hue = 1152;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("A protective aura surrounds you!");
    }
}

public class HastePill : BaseConsumable
{
    public HastePill() : base(0x0F7A)
    {
        Name = "Haste Pill";
        Hue = 1266;
        Amount = 3;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("Time seems to slow around you!");
    }
}

public class RegenerationCapsule : BaseConsumable
{
    public RegenerationCapsule() : base(0x0F7A)
    {
        Name = "Regeneration Capsule";
        Hue = 53;
        Amount = 1;
    }

    public override void Consume(Mobile from)
    {
        from.SendMessage("Your body begins regenerating health over time!");
    }
}

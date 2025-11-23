using Server;

namespace Server.Items;

// ============================================================================
// MODERN CLOTHING & ACCESSORIES - 20 Items
// ============================================================================

public class TechJacket : Item
{
    public TechJacket() : base(0x1DB9) // Robe
    {
        Name = "Tech Jacket";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The jacket's integrated circuits activate!");
    }
}

public class CombatSuit : Item
{
    public CombatSuit() : base(0x1DB9)
    {
        Name = "Combat Suit";
        Hue = 1;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A tactical combat suit!");
    }
}

public class NanoweaveRobe : Item
{
    public NanoweaveRobe() : base(0x1DB9)
    {
        Name = "Nanoweave Robe";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Nanofibers shimmer in the light!");
    }
}

public class StealthCloak : Item
{
    public StealthCloak() : base(0x1515) // Cloak
    {
        Name = "Stealth Cloak";
        Hue = 1;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The cloak bends light around you!");
    }
}

public class ScientistCoat : Item
{
    public ScientistCoat() : base(0x1DB9)
    {
        Name = "Scientist's Lab Coat";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A pristine laboratory coat!");
    }
}

public class EngineerCoveralls : Item
{
    public EngineerCoveralls() : base(0x1EFD) // Shirt
    {
        Name = "Engineer's Coveralls";
        Hue = 38;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Practical work clothing!");
    }
}

public class TacticalVest : Item
{
    public TacticalVest() : base(0x1EFD)
    {
        Name = "Tactical Vest";
        Hue = 1;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A tactical vest with many pockets!");
    }
}

public class FlightSuit : Item
{
    public FlightSuit() : base(0x1DB9)
    {
        Name = "Flight Suit";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A pilot's flight suit!");
    }
}

public class MedicUniform : Item
{
    public MedicUniform() : base(0x1DB9)
    {
        Name = "Medic Uniform";
        Hue = 53;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A medical professional's uniform!");
    }
}

public class SecurityUniform : Item
{
    public SecurityUniform() : base(0x1DB9)
    {
        Name = "Security Uniform";
        Hue = 1;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("An official security uniform!");
    }
}

public class ResearcherOutfit : Item
{
    public ResearcherOutfit() : base(0x1DB9)
    {
        Name = "Researcher's Outfit";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The outfit of a distinguished researcher!");
    }
}

public class MercenaryGear : Item
{
    public MercenaryGear() : base(0x1DB9)
    {
        Name = "Mercenary Gear";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Rugged mercenary equipment!");
    }
}

public class SpaceHelmet : Item
{
    public SpaceHelmet() : base(0x140E)
    {
        Name = "Space Helmet";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A sealed space helmet!");
    }
}

public class TechBoots : Item
{
    public TechBoots() : base(0x170B) // Boots
    {
        Name = "Tech Boots";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Boots with integrated technology!");
    }
}

public class MagneticBoots : Item
{
    public MagneticBoots() : base(0x170B)
    {
        Name = "Magnetic Boots";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("These boots can lock to metal surfaces!");
    }
}

public class PowerGloves : Item
{
    public PowerGloves() : base(0x13C6)
    {
        Name = "Power Gloves";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Gloves that amplify strength!");
    }
}

public class InsulatedSuit : Item
{
    public InsulatedSuit() : base(0x1DB9)
    {
        Name = "Insulated Suit";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Protection from extreme temperatures!");
    }
}

public class RadiationSuit : Item
{
    public RadiationSuit() : base(0x1DB9)
    {
        Name = "Radiation Suit";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Protection from radiation!");
    }
}

public class ExoSkeleton : Item
{
    public ExoSkeleton() : base(0x1DB9)
    {
        Name = "Exo-Skeleton Frame";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A powered exoskeleton frame!");
    }
}

public class SmartGlasses : Item
{
    public SmartGlasses() : base(0x0F49) // Generic item
    {
        Name = "Smart Glasses";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("AR display activates!");
    }
}

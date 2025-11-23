using Server;

namespace Server.Items;

// ============================================================================
// ADVANCED ARMOR SETS - 40 Items
// ============================================================================

// FULL BODY ARMOR - 8 Items
public class ExosuitChestplate : BaseArmor
{
    public ExosuitChestplate() : base(0x13BF) // Plate chest
    {
        Name = "Exosuit Chestplate";
        Hue = 1150;
        PhysicalResistance = 18;
        FireResistance = 12;
        ColdResistance = 12;
        PoisonResistance = 15;
        EnergyResistance = 22;
        ArmorLayer = Layer.InnerTorso;
    }
}

public class PlasmaArmorVest : BaseArmor
{
    public PlasmaArmorVest() : base(0x13BF)
    {
        Name = "Plasma Armor Vest";
        Hue = 1266;
        PhysicalResistance = 16;
        FireResistance = 20;
        ColdResistance = 10;
        PoisonResistance = 12;
        EnergyResistance = 25;
        ArmorLayer = Layer.InnerTorso;
    }
}

public class TitaniumBattlesuit : BaseArmor
{
    public TitaniumBattlesuit() : base(0x13C4) // Plate arms
    {
        Name = "Titanium Battlesuit";
        Hue = 1161;
        PhysicalResistance = 20;
        FireResistance = 15;
        ColdResistance = 15;
        PoisonResistance = 13;
        EnergyResistance = 18;
        ArmorLayer = Layer.InnerTorso;
    }
}

public class NanoweaveBodysuit : BaseArmor
{
    public NanoweaveBodysuit() : base(0x1C08) // Leather tunic
    {
        Name = "Nanoweave Bodysuit";
        Hue = 1152;
        PhysicalResistance = 14;
        FireResistance = 11;
        ColdResistance = 11;
        PoisonResistance = 14;
        EnergyResistance = 24;
        ArmorLayer = Layer.InnerTorso;
    }
}

public class AdaptiveArmorChest : BaseArmor
{
    public AdaptiveArmorChest() : base(0x13BF)
    {
        Name = "Adaptive Armor Chest";
        Hue = 1358;
        PhysicalResistance = 17;
        FireResistance = 17;
        ColdResistance = 17;
        PoisonResistance = 17;
        EnergyResistance = 20;
        ArmorLayer = Layer.InnerTorso;
    }
}

public class StealthSuitTorso : BaseArmor
{
    public StealthSuitTorso() : base(0x1C08)
    {
        Name = "Stealth Suit Torso";
        Hue = 1;
        PhysicalResistance = 12;
        FireResistance = 8;
        ColdResistance = 8;
        PoisonResistance = 16;
        EnergyResistance = 28;
        ArmorLayer = Layer.InnerTorso;
    }
}

public class ReinforcedCombatArmor : BaseArmor
{
    public ReinforcedCombatArmor() : base(0x13BF)
    {
        Name = "Reinforced Combat Armor";
        Hue = 1150;
        PhysicalResistance = 22;
        FireResistance = 14;
        ColdResistance = 14;
        PoisonResistance = 12;
        EnergyResistance = 16;
        ArmorLayer = Layer.InnerTorso;
    }
}

public class QuantumPhaseArmor : BaseArmor
{
    public QuantumPhaseArmor() : base(0x13BF)
    {
        Name = "Quantum Phase Armor";
        Hue = 1266;
        PhysicalResistance = 15;
        FireResistance = 18;
        ColdResistance = 18;
        PoisonResistance = 18;
        EnergyResistance = 30;
        ArmorLayer = Layer.InnerTorso;
    }
}

// HELMETS - 10 Items
public class TacticalHUD : BaseArmor
{
    public TacticalHUD() : base(0x140E) // Close helm
    {
        Name = "Tactical HUD Helmet";
        Hue = 1152;
        PhysicalResistance = 14;
        FireResistance = 10;
        ColdResistance = 10;
        PoisonResistance = 18;
        EnergyResistance = 20;
        ArmorLayer = Layer.Helm;
    }
}

public class NeuralInterfaceHelm : BaseArmor
{
    public NeuralInterfaceHelm() : base(0x140E)
    {
        Name = "Neural Interface Helm";
        Hue = 1358;
        PhysicalResistance = 12;
        FireResistance = 8;
        ColdResistance = 8;
        PoisonResistance = 20;
        EnergyResistance = 25;
        ArmorLayer = Layer.Helm;
    }
}

public class PlasmaHelmet : BaseArmor
{
    public PlasmaHelmet() : base(0x140E)
    {
        Name = "Plasma Helmet";
        Hue = 1266;
        PhysicalResistance = 15;
        FireResistance = 12;
        ColdResistance = 8;
        PoisonResistance = 15;
        EnergyResistance = 22;
        ArmorLayer = Layer.Helm;
    }
}

public class ReconVisor : BaseArmor
{
    public ReconVisor() : base(0x1451) // Bone helm
    {
        Name = "Recon Visor";
        Hue = 1152;
        PhysicalResistance = 10;
        FireResistance = 6;
        ColdResistance = 6;
        PoisonResistance = 22;
        EnergyResistance = 28;
        ArmorLayer = Layer.Helm;
    }
}

public class CombatExoHelm : BaseArmor
{
    public CombatExoHelm() : base(0x140E)
    {
        Name = "Combat Exo Helm";
        Hue = 1150;
        PhysicalResistance = 16;
        FireResistance = 12;
        ColdResistance = 12;
        PoisonResistance = 14;
        EnergyResistance = 18;
        ArmorLayer = Layer.Helm;
    }
}

public class ShieldedHeadgear : BaseArmor
{
    public ShieldedHeadgear() : base(0x140E)
    {
        Name = "Shielded Headgear";
        Hue = 1161;
        PhysicalResistance = 13;
        FireResistance = 13;
        ColdResistance = 13;
        PoisonResistance = 16;
        EnergyResistance = 24;
        ArmorLayer = Layer.Helm;
    }
}

public class CyberneticCrown : BaseArmor
{
    public CyberneticCrown() : base(0x2B6F) // Crown
    {
        Name = "Cybernetic Crown";
        Hue = 1266;
        PhysicalResistance = 8;
        FireResistance = 6;
        ColdResistance = 6;
        PoisonResistance = 25;
        EnergyResistance = 32;
        ArmorLayer = Layer.Helm;
    }
}

public class PhotonMask : BaseArmor
{
    public PhotonMask() : base(0x1451)
    {
        Name = "Photon Mask";
        Hue = 1358;
        PhysicalResistance = 9;
        FireResistance = 7;
        ColdResistance = 7;
        PoisonResistance = 20;
        EnergyResistance = 30;
        ArmorLayer = Layer.Helm;
    }
}

public class AdaptiveHelm : BaseArmor
{
    public AdaptiveHelm() : base(0x140E)
    {
        Name = "Adaptive Helm";
        Hue = 1152;
        PhysicalResistance = 14;
        FireResistance = 14;
        ColdResistance = 14;
        PoisonResistance = 18;
        EnergyResistance = 20;
        ArmorLayer = Layer.Helm;
    }
}

public class StealthHood : BaseArmor
{
    public StealthHood() : base(0x1451)
    {
        Name = "Stealth Hood";
        Hue = 1;
        PhysicalResistance = 8;
        FireResistance = 5;
        ColdResistance = 5;
        PoisonResistance = 24;
        EnergyResistance = 35;
        ArmorLayer = Layer.Helm;
    }
}

// GLOVES - 8 Items
public class PowerGauntlets : BaseArmor
{
    public PowerGauntlets() : base(0x13C6) // Plate gloves
    {
        Name = "Power Gauntlets";
        Hue = 1150;
        PhysicalResistance = 12;
        FireResistance = 8;
        ColdResistance = 8;
        PoisonResistance = 10;
        EnergyResistance = 16;
        ArmorLayer = Layer.Gloves;
    }
}

public class TacticalGloves : BaseArmor
{
    public TacticalGloves() : base(0x13C6)
    {
        Name = "Tactical Gloves";
        Hue = 1152;
        PhysicalResistance = 10;
        FireResistance = 6;
        ColdResistance = 6;
        PoisonResistance = 12;
        EnergyResistance = 18;
        ArmorLayer = Layer.Gloves;
    }
}

public class NanoGloves : BaseArmor
{
    public NanoGloves() : base(0x13C6)
    {
        Name = "Nano Gloves";
        Hue = 1161;
        PhysicalResistance = 8;
        FireResistance = 5;
        ColdResistance = 5;
        PoisonResistance = 14;
        EnergyResistance = 22;
        ArmorLayer = Layer.Gloves;
    }
}

public class EnergyChannelingGloves : BaseArmor
{
    public EnergyChannelingGloves() : base(0x13C6)
    {
        Name = "Energy Channeling Gloves";
        Hue = 1266;
        PhysicalResistance = 9;
        FireResistance = 7;
        ColdResistance = 7;
        PoisonResistance = 11;
        EnergyResistance = 24;
        ArmorLayer = Layer.Gloves;
    }
}

public class ReinforcedGauntlets : BaseArmor
{
    public ReinforcedGauntlets() : base(0x13C6)
    {
        Name = "Reinforced Gauntlets";
        Hue = 1150;
        PhysicalResistance = 14;
        FireResistance = 10;
        ColdResistance = 10;
        PoisonResistance = 8;
        EnergyResistance = 14;
        ArmorLayer = Layer.Gloves;
    }
}

public class ShockGloves : BaseArmor
{
    public ShockGloves() : base(0x13C6)
    {
        Name = "Shock Gloves";
        Hue = 1152;
        PhysicalResistance = 11;
        FireResistance = 9;
        ColdResistance = 7;
        PoisonResistance = 10;
        EnergyResistance = 20;
        ArmorLayer = Layer.Gloves;
    }
}

public class PhaseGrips : BaseArmor
{
    public PhaseGrips() : base(0x13C6)
    {
        Name = "Phase Grips";
        Hue = 1358;
        PhysicalResistance = 7;
        FireResistance = 6;
        ColdResistance = 6;
        PoisonResistance = 15;
        EnergyResistance = 28;
        ArmorLayer = Layer.Gloves;
    }
}

public class AdaptiveGloves : BaseArmor
{
    public AdaptiveGloves() : base(0x13C6)
    {
        Name = "Adaptive Gloves";
        Hue = 1152;
        PhysicalResistance = 10;
        FireResistance = 10;
        ColdResistance = 10;
        PoisonResistance = 12;
        EnergyResistance = 18;
        ArmorLayer = Layer.Gloves;
    }
}

// SHIELDS - 8 Items
public class PlasmaShield : BaseArmor
{
    public PlasmaShield() : base(0x1B76) // Heater shield
    {
        Name = "Plasma Shield";
        Hue = 1266;
        PhysicalResistance = 18;
        FireResistance = 22;
        ColdResistance = 12;
        PoisonResistance = 10;
        EnergyResistance = 28;
    }
}

public class EnergyBarrier : BaseArmor
{
    public EnergyBarrier() : base(0x1B76)
    {
        Name = "Energy Barrier";
        Hue = 1152;
        PhysicalResistance = 15;
        FireResistance = 18;
        ColdResistance = 18;
        PoisonResistance = 15;
        EnergyResistance = 32;
    }
}

public class ForceField : BaseArmor
{
    public ForceField() : base(0x1B76)
    {
        Name = "Force Field";
        Hue = 1358;
        PhysicalResistance = 20;
        FireResistance = 20;
        ColdResistance = 20;
        PoisonResistance = 12;
        EnergyResistance = 30;
    }
}

public class KineticShield : BaseArmor
{
    public KineticShield() : base(0x1B76)
    {
        Name = "Kinetic Shield";
        Hue = 1150;
        PhysicalResistance = 25;
        FireResistance = 15;
        ColdResistance = 15;
        PoisonResistance = 10;
        EnergyResistance = 20;
    }
}

public class PhotonBuckler : BaseArmor
{
    public PhotonBuckler() : base(0x1B73) // Buckler
    {
        Name = "Photon Buckler";
        Hue = 1266;
        PhysicalResistance = 14;
        FireResistance = 16;
        ColdResistance = 16;
        PoisonResistance = 14;
        EnergyResistance = 26;
    }
}

public class AdaptiveShield : BaseArmor
{
    public AdaptiveShield() : base(0x1B76)
    {
        Name = "Adaptive Shield";
        Hue = 1152;
        PhysicalResistance = 18;
        FireResistance = 18;
        ColdResistance = 18;
        PoisonResistance = 16;
        EnergyResistance = 24;
    }
}

public class HardlightShield : BaseArmor
{
    public HardlightShield() : base(0x1B76)
    {
        Name = "Hardlight Shield";
        Hue = 1358;
        PhysicalResistance = 16;
        FireResistance = 20;
        ColdResistance = 20;
        PoisonResistance = 14;
        EnergyResistance = 28;
    }
}

public class TeslaDeflector : BaseArmor
{
    public TeslaDeflector() : base(0x1B76)
    {
        Name = "Tesla Deflector";
        Hue = 1152;
        PhysicalResistance = 12;
        FireResistance = 16;
        ColdResistance = 10;
        PoisonResistance = 12;
        EnergyResistance = 35;
    }
}

// LEGS - 6 Items
public class ExosuitLeggings : BaseArmor
{
    public ExosuitLeggings() : base(0x13BE) // Plate legs
    {
        Name = "Exosuit Leggings";
        Hue = 1150;
        PhysicalResistance = 16;
        FireResistance = 10;
        ColdResistance = 10;
        PoisonResistance = 12;
        EnergyResistance = 18;
        ArmorLayer = Layer.Pants;
    }
}

public class PlasmaLegs : BaseArmor
{
    public PlasmaLegs() : base(0x13BE)
    {
        Name = "Plasma Leggings";
        Hue = 1266;
        PhysicalResistance = 14;
        FireResistance = 18;
        ColdResistance = 8;
        PoisonResistance = 10;
        EnergyResistance = 22;
        ArmorLayer = Layer.Pants;
    }
}

public class NanoweaveLeggings : BaseArmor
{
    public NanoweaveLeggings() : base(0x13BE)
    {
        Name = "Nanoweave Leggings";
        Hue = 1161;
        PhysicalResistance = 12;
        FireResistance = 9;
        ColdResistance = 9;
        PoisonResistance = 14;
        EnergyResistance = 24;
        ArmorLayer = Layer.Pants;
    }
}

public class ReinforcedLegs : BaseArmor
{
    public ReinforcedLegs() : base(0x13BE)
    {
        Name = "Reinforced Legs";
        Hue = 1150;
        PhysicalResistance = 18;
        FireResistance = 12;
        ColdResistance = 12;
        PoisonResistance = 10;
        EnergyResistance = 16;
        ArmorLayer = Layer.Pants;
    }
}

public class AdaptiveLeggings : BaseArmor
{
    public AdaptiveLeggings() : base(0x13BE)
    {
        Name = "Adaptive Leggings";
        Hue = 1152;
        PhysicalResistance = 15;
        FireResistance = 15;
        ColdResistance = 15;
        PoisonResistance = 12;
        EnergyResistance = 20;
        ArmorLayer = Layer.Pants;
    }
}

public class StealthLegs : BaseArmor
{
    public StealthLegs() : base(0x13BE)
    {
        Name = "Stealth Leggings";
        Hue = 1;
        PhysicalResistance = 10;
        FireResistance = 6;
        ColdResistance = 6;
        PoisonResistance = 16;
        EnergyResistance = 28;
        ArmorLayer = Layer.Pants;
    }
}

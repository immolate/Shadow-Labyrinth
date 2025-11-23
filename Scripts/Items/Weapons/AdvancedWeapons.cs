using Server;

namespace Server.Items;

// ============================================================================
// ADVANCED ENERGY WEAPONS - 15 Items
// ============================================================================

public class PhotonRifle : BaseWeapon
{
    public PhotonRifle() : base(0x13FD) // Bow graphic
    {
        Name = "Photon Rifle";
        Hue = 1266; // Cyan
        MinDamage = 40;
        MaxDamage = 60;
        Speed = 35;
        Layer = Layer.TwoHanded;
    }
}

public class LaserPistol : BaseWeapon
{
    public LaserPistol() : base(0x0F62) // Crossbow graphic
    {
        Name = "Laser Pistol";
        Hue = 1358; // Purple
        MinDamage = 20;
        MaxDamage = 35;
        Speed = 25;
    }
}

public class PulseHammer : BaseWeapon
{
    public PulseHammer() : base(0x143D) // Hammer graphic
    {
        Name = "Pulse Hammer";
        Hue = 1152; // Blue
        MinDamage = 45;
        MaxDamage = 70;
        Speed = 50;
        Layer = Layer.TwoHanded;
    }
}

public class IonBlade : BaseWeapon
{
    public IonBlade() : base(0x13B6) // Scimitar
    {
        Name = "Ion Blade";
        Hue = 1161; // Silver
        MinDamage = 28;
        MaxDamage = 48;
        Speed = 30;
    }
}

public class GravitonMace : BaseWeapon
{
    public GravitonMace() : base(0x0F5C) // Mace
    {
        Name = "Graviton Mace";
        Hue = 1266;
        MinDamage = 32;
        MaxDamage = 52;
        Speed = 38;
    }
}

public class NeuralWhip : BaseWeapon
{
    public NeuralWhip() : base(0x166F) // Whip graphic
    {
        Name = "Neural Whip";
        Hue = 1358;
        MinDamage = 18;
        MaxDamage = 38;
        Speed = 22;
    }
}

public class ElectroSpear : BaseWeapon
{
    public ElectroSpear() : base(0x0F62) // Spear
    {
        Name = "Electro Spear";
        Hue = 1152;
        MinDamage = 35;
        MaxDamage = 55;
        Speed = 40;
        Layer = Layer.TwoHanded;
    }
}

public class PhotonStaff : BaseWeapon
{
    public PhotonStaff() : base(0x0DF0) // Staff
    {
        Name = "Photon Staff";
        Hue = 1266;
        MinDamage = 30;
        MaxDamage = 50;
        Speed = 35;
        Layer = Layer.TwoHanded;
    }
}

public class DisruptorBlade : BaseWeapon
{
    public DisruptorBlade() : base(0x13FF) // Katana
    {
        Name = "Disruptor Blade";
        Hue = 1161;
        MinDamage = 33;
        MaxDamage = 53;
        Speed = 32;
    }
}

public class VortexCannon : BaseWeapon
{
    public VortexCannon() : base(0x13FD) // Bow
    {
        Name = "Vortex Cannon";
        Hue = 1358;
        MinDamage = 50;
        MaxDamage = 75;
        Speed = 55;
        Layer = Layer.TwoHanded;
    }
}

public class CryoBlade : BaseWeapon
{
    public CryoBlade() : base(0x13B9) // Longsword
    {
        Name = "Cryo Blade";
        Hue = 1153; // Ice blue
        MinDamage = 27;
        MaxDamage = 47;
        Speed = 33;
    }
}

public class PyroclasticSword : BaseWeapon
{
    public PyroclasticSword() : base(0x13B9) // Longsword
    {
        Name = "Pyroclastic Sword";
        Hue = 1161; // Fire red
        MinDamage = 29;
        MaxDamage = 49;
        Speed = 34;
    }
}

public class TeslaRod : BaseWeapon
{
    public TeslaRod() : base(0x0E87) // Pitchfork
    {
        Name = "Tesla Rod";
        Hue = 1152;
        MinDamage = 25;
        MaxDamage = 45;
        Speed = 28;
    }
}

public class AntiMatterBow : BaseWeapon
{
    public AntiMatterBow() : base(0x13B2) // Bow
    {
        Name = "Anti-Matter Bow";
        Hue = 1; // Black
        MinDamage = 42;
        MaxDamage = 65;
        Speed = 38;
        Layer = Layer.TwoHanded;
    }
}

public class SonicResonator : BaseWeapon
{
    public SonicResonator() : base(0x0DF0) // Staff
    {
        Name = "Sonic Resonator";
        Hue = 1266;
        MinDamage = 28;
        MaxDamage = 48;
        Speed = 30;
        Layer = Layer.TwoHanded;
    }
}

// ============================================================================
// ADVANCED MELEE WEAPONS - 15 Items
// ============================================================================

public class MonofilamentBlade : BaseWeapon
{
    public MonofilamentBlade() : base(0x0F52) // Dagger
    {
        Name = "Monofilament Blade";
        Hue = 1161;
        MinDamage = 20;
        MaxDamage = 40;
        Speed = 20;
    }
}

public class VibrationKatana : BaseWeapon
{
    public VibrationKatana() : base(0x13FF) // Katana
    {
        Name = "Vibration Katana";
        Hue = 1152;
        MinDamage = 35;
        MaxDamage = 58;
        Speed = 32;
    }
}

public class PowerFist : BaseWeapon
{
    public PowerFist() : base(0x13F8) // Kryss
    {
        Name = "Power Fist";
        Hue = 1150;
        MinDamage = 38;
        MaxDamage = 60;
        Speed = 40;
    }
}

public class ChainBlade : BaseWeapon
{
    public ChainBlade() : base(0x13FF) // Katana
    {
        Name = "Chain Blade";
        Hue = 1161;
        MinDamage = 40;
        MaxDamage = 62;
        Speed = 45;
    }
}

public class PlasmaScythe : BaseWeapon
{
    public PlasmaScythe() : base(0x26BA) // Bardiche
    {
        Name = "Plasma Scythe";
        Hue = 1266;
        MinDamage = 42;
        MaxDamage = 68;
        Speed = 48;
        Layer = Layer.TwoHanded;
    }
}

public class HardlightBlade : BaseWeapon
{
    public HardlightBlade() : base(0x13B9) // Longsword
    {
        Name = "Hardlight Blade";
        Hue = 1358;
        MinDamage = 30;
        MaxDamage = 52;
        Speed = 30;
    }
}

public class NanobladeKnife : BaseWeapon
{
    public NanobladeKnife() : base(0x13F6) // Butcher knife
    {
        Name = "Nanoblade Knife";
        Hue = 1161;
        MinDamage = 22;
        MaxDamage = 38;
        Speed = 24;
    }
}

public class GaussHammer : BaseWeapon
{
    public GaussHammer() : base(0x143D) // Hammer
    {
        Name = "Gauss Hammer";
        Hue = 1152;
        MinDamage = 48;
        MaxDamage = 72;
        Speed = 52;
        Layer = Layer.TwoHanded;
    }
}

public class PhotonRapier : BaseWeapon
{
    public PhotonRapier() : base(0x13F4) // Rapier
    {
        Name = "Photon Rapier";
        Hue = 1266;
        MinDamage = 24;
        MaxDamage = 44;
        Speed = 26;
    }
}

public class NeuralLance : BaseWeapon
{
    public NeuralLance() : base(0x26C0) // Lance
    {
        Name = "Neural Lance";
        Hue = 1358;
        MinDamage = 38;
        MaxDamage = 62;
        Speed = 42;
        Layer = Layer.TwoHanded;
    }
}

public class MagneticAxe : BaseWeapon
{
    public MagneticAxe() : base(0x0F49) // Axe
    {
        Name = "Magnetic Axe";
        Hue = 1150;
        MinDamage = 36;
        MaxDamage = 58;
        Speed = 44;
        Layer = Layer.TwoHanded;
    }
}

public class PhaseBlade : BaseWeapon
{
    public PhaseBlade() : base(0x13B6) // Scimitar
    {
        Name = "Phase Blade";
        Hue = 1161;
        MinDamage = 26;
        MaxDamage = 46;
        Speed = 28;
    }
}

public class QuantumClaws : BaseWeapon
{
    public QuantumClaws() : base(0x13F8) // Kryss (dual)
    {
        Name = "Quantum Claws";
        Hue = 1266;
        MinDamage = 32;
        MaxDamage = 50;
        Speed = 25;
    }
}

public class StasisMaul : BaseWeapon
{
    public StasisMaul() : base(0x143B) // Maul
    {
        Name = "Stasis Maul";
        Hue = 1152;
        MinDamage = 50;
        MaxDamage = 78;
        Speed = 58;
        Layer = Layer.TwoHanded;
    }
}

public class EtherealSaber : BaseWeapon
{
    public EtherealSaber() : base(0x13FF) // Katana
    {
        Name = "Ethereal Saber";
        Hue = 1358;
        MinDamage = 31;
        MaxDamage = 51;
        Speed = 31;
    }
}

// ============================================================================
// RANGED TECH WEAPONS - 10 Items
// ============================================================================

public class RailgunCrossbow : BaseWeapon
{
    public RailgunCrossbow() : base(0x0F50) // Crossbow
    {
        Name = "Railgun Crossbow";
        Hue = 1150;
        MinDamage = 45;
        MaxDamage = 68;
        Speed = 42;
        Layer = Layer.TwoHanded;
    }
}

public class PlasmaRepeater : BaseWeapon
{
    public PlasmaRepeater() : base(0x13FD) // Bow
    {
        Name = "Plasma Repeater";
        Hue = 1266;
        MinDamage = 38;
        MaxDamage = 58;
        Speed = 35;
        Layer = Layer.TwoHanded;
    }
}

public class NeedlerGun : BaseWeapon
{
    public NeedlerGun() : base(0x0F50) // Crossbow
    {
        Name = "Needler Gun";
        Hue = 1161;
        MinDamage = 28;
        MaxDamage = 42;
        Speed = 22;
    }
}

public class IonCannon : BaseWeapon
{
    public IonCannon() : base(0x13FD) // Bow
    {
        Name = "Ion Cannon";
        Hue = 1152;
        MinDamage = 55;
        MaxDamage = 82;
        Speed = 60;
        Layer = Layer.TwoHanded;
    }
}

public class PhotonicBlaster : BaseWeapon
{
    public PhotonicBlaster() : base(0x0F50) // Crossbow
    {
        Name = "Photonic Blaster";
        Hue = 1266;
        MinDamage = 32;
        MaxDamage = 52;
        Speed = 30;
    }
}

public class GaussCarbine : BaseWeapon
{
    public GaussCarbine() : base(0x13FD) // Bow
    {
        Name = "Gauss Carbine";
        Hue = 1150;
        MinDamage = 42;
        MaxDamage = 64;
        Speed = 38;
        Layer = Layer.TwoHanded;
    }
}

public class NeuralDartGun : BaseWeapon
{
    public NeuralDartGun() : base(0x0F50) // Crossbow
    {
        Name = "Neural Dart Gun";
        Hue = 1358;
        MinDamage = 18;
        MaxDamage = 32;
        Speed = 18;
    }
}

public class DisintegrationBeam : BaseWeapon
{
    public DisintegrationBeam() : base(0x13FD) // Bow
    {
        Name = "Disintegration Beam";
        Hue = 1;
        MinDamage = 60;
        MaxDamage = 90;
        Speed = 65;
        Layer = Layer.TwoHanded;
    }
}

public class ShockRifle : BaseWeapon
{
    public ShockRifle() : base(0x13FD) // Bow
    {
        Name = "Shock Rifle";
        Hue = 1152;
        MinDamage = 40;
        MaxDamage = 62;
        Speed = 40;
        Layer = Layer.TwoHanded;
    }
}

public class CryoCannon : BaseWeapon
{
    public CryoCannon() : base(0x0F50) // Crossbow
    {
        Name = "Cryo Cannon";
        Hue = 1153;
        MinDamage = 35;
        MaxDamage = 55;
        Speed = 36;
        Layer = Layer.TwoHanded;
    }
}

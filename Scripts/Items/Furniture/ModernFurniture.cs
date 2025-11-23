using Server;

namespace Server.Items;

// ============================================================================
// MODERN FURNITURE & DECORATIONS - 30 Items
// ============================================================================

// TECH WORKSTATIONS - 10 Items
public class QuantumForge : Item
{
    public QuantumForge() : base(0x0FB1) // Forge
    {
        Name = "Quantum Forge";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The quantum forge manipulates matter at the atomic level!");
    }
}

public class EngineeringStation : Item
{
    public EngineeringStation() : base(0x13E3) // Anvil
    {
        Name = "Engineering Station";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Advanced engineering tools await your use!");
    }
}

public class ResearchDesk : Item
{
    public ResearchDesk() : base(0x0A97) // Desk
    {
        Name = "Research Desk";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The research desk contains cutting-edge equipment!");
    }
}

public class ChemistryLab : Item
{
    public ChemistryLab() : base(0x09A9) // Mortar
    {
        Name = "Chemistry Lab";
        Hue = 53;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A sophisticated chemical synthesis station!");
    }
}

public class ElectronicsStation : Item
{
    public ElectronicsStation() : base(0x13E3)
    {
        Name = "Electronics Station";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Circuits and components cover the workbench!");
    }
}

public class NanotechAssembler : Item
{
    public NanotechAssembler() : base(0x13E3)
    {
        Name = "Nanotech Assembler";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Nanobots swarm the assembly surface!");
    }
}

public class BiotechLab : Item
{
    public BiotechLab() : base(0x09A9)
    {
        Name = "Biotech Lab";
        Hue = 67;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Biological samples await analysis!");
    }
}

public class RoboticsFabricator : Item
{
    public RoboticsFabricator() : base(0x13E3)
    {
        Name = "Robotics Fabricator";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Robotic parts can be assembled here!");
    }
}

public class WeaponModStation : Item
{
    public WeaponModStation() : base(0x13E3)
    {
        Name = "Weapon Modification Station";
        Hue = 38;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Weapon upgrades and modifications available!");
    }
}

public class ArmorForge : Item
{
    public ArmorForge() : base(0x0FB1)
    {
        Name = "Advanced Armor Forge";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Advanced armor can be crafted here!");
    }
}

// DISPLAY SYSTEMS - 8 Items
public class HolographicProjector : Item
{
    public HolographicProjector() : base(0x1EA7)
    {
        Name = "Holographic Projector";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Holographic images dance in the air!");
    }
}

public class QuantumDisplay : Item
{
    public QuantumDisplay() : base(0x1EA7)
    {
        Name = "Quantum Display";
        Hue = 1358;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Quantum particles form impossible images!");
    }
}

public class InfoScreen : Item
{
    public InfoScreen() : base(0x1EA8)
    {
        Name = "Information Screen";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Data streams across the display!");
    }
}

public class TacticalMap : Item
{
    public TacticalMap() : base(0x1EA8)
    {
        Name = "Tactical Map Display";
        Hue = 38;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A 3D tactical map appears!");
    }
}

public class SecurityMonitor : Item
{
    public SecurityMonitor() : base(0x1EA8)
    {
        Name = "Security Monitor";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Security feeds show various locations!");
    }
}

public class StatusBoard : Item
{
    public StatusBoard() : base(0x1EA8)
    {
        Name = "Status Board";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("All systems nominal!");
    }
}

public class HoloGlobe : Item
{
    public HoloGlobe() : base(0x1EA7)
    {
        Name = "Holographic Globe";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A rotating holographic planet appears!");
    }
}

public class DataVisualizerItem : Item
{
    public DataVisualizerItem() : base(0x1EA7)
    {
        Name = "Data Visualizer";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Complex data rendered in beautiful patterns!");
    }
}

// FURNITURE - 12 Items
public class CyberneticChair : Item
{
    public CyberneticChair() : base(0x0B2E) // Chair
    {
        Name = "Cybernetic Chair";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The chair adjusts to perfect ergonomic form!");
    }
}

public class StasisPod : Item
{
    public StasisPod() : base(0x0E7F) // Coffin-like
    {
        Name = "Stasis Pod";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The stasis pod could preserve someone indefinitely!");
    }
}

public class CommandThrone : Item
{
    public CommandThrone() : base(0x0B2E)
    {
        Name = "Command Throne";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A throne fit for a tech ruler!");
    }
}

public class MeditationChamber : Item
{
    public MeditationChamber() : base(0x0E7F)
    {
        Name = "Meditation Chamber";
        Hue = 1358;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Neural enhancement through meditation!");
    }
}

public class StorageLocker : Item
{
    public StorageLocker() : base(0x09AB) // Cabinet
    {
        Name = "Secure Storage Locker";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Biometric lock engaged!");
    }
}

public class TechDesk : Item
{
    public TechDesk() : base(0x0A97)
    {
        Name = "Tech Desk";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A modern workstation!");
    }
}

public class EnergyBed : Item
{
    public EnergyBed() : base(0x0A63) // Bed
    {
        Name = "Energy Recovery Bed";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The bed pulses with rejuvenating energy!");
    }
}

public class ModularShelf : Item
{
    public ModularShelf() : base(0x0A98) // Bookshelf
    {
        Name = "Modular Storage Shelf";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Efficient modular storage!");
    }
}

public class TechTable : Item
{
    public TechTable() : base(0x0A30) // Table
    {
        Name = "Tech Table";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A sleek modern table!");
    }
}

public class LightingPanel : Item
{
    public LightingPanel() : base(0x0A15) // Lamp
    {
        Name = "Lighting Panel";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Soft ambient light fills the area!");
    }
}

public class SecurityDoor : Item
{
    public SecurityDoor() : base(0x0675) // Door
    {
        Name = "Security Door";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Biometric scan required!");
    }
}

public class PlantGrowth : Item
{
    public PlantGrowth() : base(0x0C35) // Plant
    {
        Name = "Hydroponic Plant";
        Hue = 67;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A genetically enhanced plant!");
    }
}

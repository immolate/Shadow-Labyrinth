using Server;

namespace Server.Items;

// ============================================================================
// ELECTRONIC DEVICES & GADGETS - 30 Items
// ============================================================================

// COMPUTERS & TERMINALS - 8 Items
public class QuantumComputer : Item
{
    public QuantumComputer() : base(0x1EA8) // Crystal/computer graphic
    {
        Name = "Quantum Computer";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The quantum processor displays incomprehensible calculations!");
    }
}

public class HolographicTerminal : Item
{
    public HolographicTerminal() : base(0x1EA7)
    {
        Name = "Holographic Terminal";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("A holographic interface materializes before you!");
    }
}

public class DataPad : Item
{
    public DataPad() : base(0x0FF1) // Scroll
    {
        Name = "Data Pad";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The data pad displays various files and documents.");
    }
}

public class PersonalComputer : Item
{
    public PersonalComputer() : base(0x1EA8)
    {
        Name = "Personal Computer";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Accessing personal files...");
    }
}

public class CyberneticInterface : Item
{
    public CyberneticInterface() : base(0x1EA7)
    {
        Name = "Cybernetic Interface";
        Hue = 1358;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Neural interface connection established!");
    }
}

public class QuantumTablet : Item
{
    public QuantumTablet() : base(0x0FF1)
    {
        Name = "Quantum Tablet";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The quantum tablet displays real-time data!");
    }
}

public class ServerCore : Item
{
    public ServerCore() : base(0x1EA8)
    {
        Name = "Server Core";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The server core hums with processing power!");
    }
}

public class ArtificialIntelligence : Item
{
    public ArtificialIntelligence() : base(0x1EA7)
    {
        Name = "AI Core";
        Hue = 1358;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The AI speaks: 'How may I assist you?'");
    }
}

// SCANNERS & SENSORS - 8 Items
public class TricorderScanner : Item
{
    public TricorderScanner() : base(0x1BFB)
    {
        Name = "Tricorder Scanner";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Scanning... life forms detected nearby!");
    }
}

public class BioScanner : Item
{
    public BioScanner() : base(0x1BFB)
    {
        Name = "Bio Scanner";
        Hue = 53;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Bio-signature analysis in progress...");
    }
}

public class EnergyDetector : Item
{
    public EnergyDetector() : base(0x1BFB)
    {
        Name = "Energy Detector";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Energy readings fluctuating wildly!");
    }
}

public class MineralAnalyzer : Item
{
    public MineralAnalyzer() : base(0x1BFB)
    {
        Name = "Mineral Analyzer";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Analyzing mineral composition...");
    }
}

public class TacticalScanner : Item
{
    public TacticalScanner() : base(0x1BFB)
    {
        Name = "Tactical Scanner";
        Hue = 38;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Tactical overlay activated!");
    }
}

public class QuantumSensor : Item
{
    public QuantumSensor() : base(0x1BFB)
    {
        Name = "Quantum Sensor";
        Hue = 1358;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Quantum fluctuations detected!");
    }
}

public class WeatherAnalyzer : Item
{
    public WeatherAnalyzer() : base(0x1BFB)
    {
        Name = "Weather Analyzer";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Current conditions: Clear skies, mild temperature.");
    }
}

public class RadiationCounter : Item
{
    public RadiationCounter() : base(0x1BFB)
    {
        Name = "Radiation Counter";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("*click* *click* Radiation levels normal.");
    }
}

// COMMUNICATION DEVICES - 7 Items
public class QuantumCommunicator : Item
{
    public QuantumCommunicator() : base(0x1BFB)
    {
        Name = "Quantum Communicator";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Quantum entangled communication established!");
    }
}

public class SubspaceCommunicator : Item
{
    public SubspaceCommunicator() : base(0x1BFB)
    {
        Name = "Subspace Communicator";
        Hue = 1358;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Subspace channel open!");
    }
}

public class EmergencyBeacon : Item
{
    public EmergencyBeacon() : base(0x1BFB)
    {
        Name = "Emergency Beacon";
        Hue = 38;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Emergency beacon activated! Signal broadcasting...");
    }
}

public class EncryptedComlink : Item
{
    public EncryptedComlink() : base(0x1BFB)
    {
        Name = "Encrypted Comlink";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Secure channel established!");
    }
}

public class UniversalTranslator : Item
{
    public UniversalTranslator() : base(0x1BFB)
    {
        Name = "Universal Translator";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Translation matrix active!");
    }
}

public class NeuralLink : Item
{
    public NeuralLink() : base(0x1BFB)
    {
        Name = "Neural Link";
        Hue = 1358;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Neural link established!");
    }
}

public class TacticalCommSet : Item
{
    public TacticalCommSet() : base(0x1BFB)
    {
        Name = "Tactical Comm Set";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Team channel ready!");
    }
}

// TOOLS & GADGETS - 7 Items
public class MultiTool : Item
{
    public MultiTool() : base(0x1EB8) // Tool graphic
    {
        Name = "Multi-Tool";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The multi-tool contains every function imaginable!");
    }
}

public class GravityGloves : Item
{
    public GravityGloves() : base(0x13C6)
    {
        Name = "Gravity Manipulation Gloves";
        Hue = 1266;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Gravity fields shimmer around your hands!");
    }
}

public class PersonalShieldGenerator : Item
{
    public PersonalShieldGenerator() : base(0x1EA7)
    {
        Name = "Personal Shield Generator";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Personal shield activated!");
    }
}

public class CloakingDevice : Item
{
    public CloakingDevice() : base(0x1EA7)
    {
        Name = "Cloaking Device";
        Hue = 1;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("You shimmer and begin to fade from view!");
    }
}

public class JetpackModule : Item
{
    public JetpackModule() : base(0x1EA8)
    {
        Name = "Jetpack Module";
        Hue = 1150;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("Thrust engaged! You feel lighter!");
    }
}

public class GrapplingHook : Item
{
    public GrapplingHook() : base(0x14FC) // Hook
    {
        Name = "Advanced Grappling Hook";
        Hue = 1152;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The grappling hook fires with magnetic precision!");
    }
}

public class MiningDrone : Item
{
    public MiningDrone() : base(0x1EA8)
    {
        Name = "Mining Drone";
        Hue = 1161;
    }

    public override void OnDoubleClick(Mobile from)
    {
        from.SendMessage("The drone hovers nearby, ready to mine!");
    }
}

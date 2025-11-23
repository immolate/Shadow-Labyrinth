using Server;

namespace Server.Items;

// ============================================================================
// MODERN RESOURCES & MATERIALS - 20 Items
// ============================================================================

public class QuantumCrystal : Item
{
    public QuantumCrystal() : base(0x1F1C)
    {
        Name = "Quantum Crystal";
        Hue = 1266;
        Amount = 10;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Quantum Crystal [{Amount}]");
    }
}

public class PlasmaCore : Item
{
    public PlasmaCore() : base(0x1EA7)
    {
        Name = "Plasma Core";
        Hue = 1358;
        Amount = 5;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Plasma Core [{Amount}]");
    }
}

public class NanotubeBundle : Item
{
    public NanotubeBundle() : base(0x1BE0) // Ingot-like
    {
        Name = "Carbon Nanotube Bundle";
        Hue = 1161;
        Amount = 20;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Carbon Nanotube Bundle [{Amount}]");
    }
}

public class TitaniumAlloy : Item
{
    public TitaniumAlloy() : base(0x1BF2) // Ingot
    {
        Name = "Titanium Alloy";
        Hue = 1150;
        Amount = 15;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Titanium Alloy [{Amount}]");
    }
}

public class NaniteCanister : Item
{
    public NaniteCanister() : base(0x0F0E) // Bottle
    {
        Name = "Nanite Canister";
        Hue = 1161;
        Amount = 10;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Nanite Canister [{Amount}]");
    }
}

public class EnergyCell : Item
{
    public EnergyCell() : base(0x1EA7)
    {
        Name = "Energy Cell";
        Hue = 1152;
        Amount = 25;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Energy Cell [{Amount}]");
    }
}

public class SyntheticFiber : Item
{
    public SyntheticFiber() : base(0x0DF8) // Cloth
    {
        Name = "Synthetic Fiber";
        Hue = 1266;
        Amount = 30;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Synthetic Fiber [{Amount}]");
    }
}

public class CircuitBoard : Item
{
    public CircuitBoard() : base(0x1EA8)
    {
        Name = "Advanced Circuit Board";
        Hue = 1152;
        Amount = 12;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Circuit Board [{Amount}]");
    }
}

public class Microprocessor : Item
{
    public Microprocessor() : base(0x1BFB)
    {
        Name = "Microprocessor";
        Hue = 1150;
        Amount = 15;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Microprocessor [{Amount}]");
    }
}

public class PhotonicCrystal : Item
{
    public PhotonicCrystal() : base(0x1F1C)
    {
        Name = "Photonic Crystal";
        Hue = 1358;
        Amount = 8;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Photonic Crystal [{Amount}]");
    }
}

public class GrapheneSheet : Item
{
    public GrapheneSheet() : base(0x14EF) // Sheet
    {
        Name = "Graphene Sheet";
        Hue = 1;
        Amount = 20;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Graphene Sheet [{Amount}]");
    }
}

public class SuperconductorWire : Item
{
    public SuperconductorWire() : base(0x1BEF) // Wire
    {
        Name = "Superconductor Wire";
        Hue = 1152;
        Amount = 18;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Superconductor Wire [{Amount}]");
    }
}

public class FusionCell : Item
{
    public FusionCell() : base(0x1EA7)
    {
        Name = "Fusion Cell";
        Hue = 1266;
        Amount = 6;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Fusion Cell [{Amount}]");
    }
}

public class BiomaterialSample : Item
{
    public BiomaterialSample() : base(0x0F7E) // Organic
    {
        Name = "Biomaterial Sample";
        Hue = 67;
        Amount = 15;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Biomaterial Sample [{Amount}]");
    }
}

public class CeramicPlating : Item
{
    public CeramicPlating() : base(0x1BF2)
    {
        Name = "Ceramic Plating";
        Hue = 1150;
        Amount = 12;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Ceramic Plating [{Amount}]");
    }
}

public class PolymerResin : Item
{
    public PolymerResin() : base(0x0F0E)
    {
        Name = "Polymer Resin";
        Hue = 1161;
        Amount = 20;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Polymer Resin [{Amount}]");
    }
}

public class CrystallineMatrix : Item
{
    public CrystallineMatrix() : base(0x1F1C)
    {
        Name = "Crystalline Matrix";
        Hue = 1152;
        Amount = 10;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Crystalline Matrix [{Amount}]");
    }
}

public class NeuralGel : Item
{
    public NeuralGel() : base(0x0F0E)
    {
        Name = "Neural Gel";
        Hue = 1358;
        Amount = 8;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Neural Gel [{Amount}]");
    }
}

public class ReactiveAgent : Item
{
    public ReactiveAgent() : base(0x0F0E)
    {
        Name = "Reactive Agent";
        Hue = 38;
        Amount = 12;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Reactive Agent [{Amount}]");
    }
}

public class ExoticMatter : Item
{
    public ExoticMatter() : base(0x1EA7)
    {
        Name = "Exotic Matter";
        Hue = 1266;
        Amount = 3;
    }

    public override void OnSingleClick(Mobile from)
    {
        from.SendMessage($"Exotic Matter [{Amount}] - Extremely rare!");
    }
}

using Server;

namespace Server.Mobiles;

/// <summary>
/// Modern NPC base class with AI hooks
/// </summary>
public abstract class BaseNPC : Mobile
{
    protected BaseNPC()
    {
        Fame = 0;
        Karma = 0;
    }

    public int Fame { get; set; }
    public int Karma { get; set; }
    public virtual bool IsInvulnerable => false;

    public virtual void OnThink()
    {
        // AI processing hook
    }

    public virtual void OnDamaged(Mobile from, int amount)
    {
        Hits = Math.Max(0, Hits - amount);

        if (Hits <= 0)
        {
            OnDeath(from);
        }
    }

    public virtual void OnDeath(Mobile killer)
    {
        SendMessage("*dies*");
        Delete();
    }
}

/// <summary>
/// Cybernetic Guard - Advanced security NPC
/// </summary>
public class CyberneticGuard : BaseNPC
{
    public CyberneticGuard()
    {
        Name = "Cybernetic Guard";
        Body = 0x190; // Human male
        Hue = 1150; // Metallic blue

        HitsMax = Hits = 200;
        StamMax = Stam = 150;
        ManaMax = Mana = 100;

        Fame = 5000;
        Karma = 5000;
    }

    public override void OnDoubleClick(Mobile from)
    {
        Say("Perimeter secure. State your business.");
    }

    private void Say(string text)
    {
        // In full implementation, this would broadcast speech
        if (Configuration.Debug)
            Console.WriteLine($"[{Name}] {text}");
    }
}

/// <summary>
/// Quantum Merchant - High-tech trader
/// </summary>
public class QuantumMerchant : BaseNPC
{
    public QuantumMerchant()
    {
        Name = "Quantum Merchant";
        Body = 0x191; // Human female
        Hue = 0; // Normal

        HitsMax = Hits = 100;
        StamMax = Stam = 100;
        ManaMax = Mana = 100;

        Fame = 1000;
        Karma = 1000;
    }

    public override bool IsInvulnerable => true;

    public override void OnDoubleClick(Mobile from)
    {
        Say("Welcome! I offer the finest quantum-enhanced goods!");
        // In full implementation, this would open vendor menu
    }

    private void Say(string text)
    {
        if (Configuration.Debug)
            Console.WriteLine($"[{Name}] {text}");
    }
}

/// <summary>
/// Rogue AI - Hostile synthetic entity
/// </summary>
public class RogueAI : BaseNPC
{
    public RogueAI()
    {
        Name = "Rogue AI Entity";
        Body = 0x2F4; // Wisp body
        Hue = 1358; // Purple

        HitsMax = Hits = 150;
        StamMax = Stam = 200;
        ManaMax = Mana = 150;

        Fame = 3000;
        Karma = -3000;
    }

    public override void OnThink()
    {
        // Hostile AI would scan for targets here
        base.OnThink();
    }

    public override void OnDeath(Mobile killer)
    {
        Say("SYSTEM... FAILURE... REINITIALIZING...");
        base.OnDeath(killer);
    }

    private void Say(string text)
    {
        if (Configuration.Debug)
            Console.WriteLine($"[{Name}] {text}");
    }
}

/// <summary>
/// Tech Sage - Knowledge NPC
/// </summary>
public class TechSage : BaseNPC
{
    private readonly string[] _wisdom = new[]
    {
        "The ancients knew secrets we're only beginning to rediscover...",
        "Technology and magic are merely different expressions of power.",
        "In the quantum realm, all possibilities exist simultaneously.",
        "The Shadow Labyrinth holds mysteries beyond comprehension."
    };

    public TechSage()
    {
        Name = "Tech Sage Aria";
        Body = 0x191; // Human female
        Hue = 0;

        HitsMax = Hits = 100;
        StamMax = Stam = 100;
        ManaMax = Mana = 200;

        Fame = 8000;
        Karma = 8000;
    }

    public override bool IsInvulnerable => true;

    public override void OnDoubleClick(Mobile from)
    {
        var wisdom = _wisdom[Random.Shared.Next(_wisdom.Length)];
        Say(wisdom);
    }

    private void Say(string text)
    {
        if (Configuration.Debug)
            Console.WriteLine($"[{Name}] {text}");
    }
}

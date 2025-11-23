namespace Server;

/// <summary>
/// Modern serial implementation with value semantics
/// </summary>
public readonly record struct Serial(int Value) : IComparable<Serial>
{
    public static readonly Serial MinusOne = new(-1);
    public static readonly Serial Zero = new(0);

    public bool IsValid => Value > 0;
    public bool IsItem => Value >= 0x40000000;
    public bool IsMobile => Value > 0 && Value < 0x40000000;

    public int CompareTo(Serial other) => Value.CompareTo(other.Value);

    public static implicit operator int(Serial serial) => serial.Value;
    public static implicit operator Serial(int value) => new(value);

    public override string ToString() => $"0x{Value:X8}";
}

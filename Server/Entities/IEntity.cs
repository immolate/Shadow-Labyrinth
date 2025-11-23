namespace Server;

/// <summary>
/// Base interface for all world entities
/// </summary>
public interface IEntity
{
    Serial Serial { get; }
    Point3D Location { get; set; }
    Map Map { get; set; }
    string Name { get; set; }
    bool Deleted { get; }

    void Delete();
}

/// <summary>
/// 3D Point with modern record syntax
/// </summary>
public readonly record struct Point3D(int X, int Y, int Z)
{
    public static readonly Point3D Zero = new(0, 0, 0);

    public int GetDistance(Point3D other)
    {
        int xDelta = Math.Abs(X - other.X);
        int yDelta = Math.Abs(Y - other.Y);
        int zDelta = Math.Abs(Z - other.Z);

        return (int)Math.Sqrt(xDelta * xDelta + yDelta * yDelta + zDelta * zDelta);
    }

    public override string ToString() => $"({X}, {Y}, {Z})";
}

/// <summary>
/// 2D Point with modern record syntax
/// </summary>
public readonly record struct Point2D(int X, int Y)
{
    public static readonly Point2D Zero = new(0, 0);

    public override string ToString() => $"({X}, {Y})";
}

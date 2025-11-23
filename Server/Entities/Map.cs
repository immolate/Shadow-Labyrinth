namespace Server;

/// <summary>
/// Map/Facet system with modern collection management
/// </summary>
public class Map
{
    private static readonly List<Map> _allMaps = new();

    public static Map Felucca { get; private set; }
    public static Map Trammel { get; private set; }
    public static Map Ilshenar { get; private set; }
    public static Map Malas { get; private set; }
    public static Map Tokuno { get; private set; }
    public static Map TerMur { get; private set; }

    static Map()
    {
        Felucca = new Map(0, "Felucca", 7168, 4096);
        Trammel = new Map(1, "Trammel", 7168, 4096);
        Ilshenar = new Map(2, "Ilshenar", 2304, 1600);
        Malas = new Map(3, "Malas", 2560, 2048);
        Tokuno = new Map(4, "Tokuno", 1448, 1448);
        TerMur = new Map(5, "Ter Mur", 1280, 4096);
    }

    private Map(int mapId, string name, int width, int height)
    {
        MapId = mapId;
        Name = name;
        Width = width;
        Height = height;

        _allMaps.Add(this);
    }

    public int MapId { get; }
    public string Name { get; }
    public int Width { get; }
    public int Height { get; }

    public static Map? GetMap(int mapId)
    {
        return mapId >= 0 && mapId < _allMaps.Count ? _allMaps[mapId] : null;
    }

    public bool IsValidLocation(Point3D location)
    {
        return location.X >= 0 && location.X < Width &&
               location.Y >= 0 && location.Y < Height;
    }

    public override string ToString() => Name;
}

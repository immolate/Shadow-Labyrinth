using System.Buffers;
using System.Text;

namespace Server.Network;

/// <summary>
/// Modern packet system using ArrayPool for performance
/// </summary>
public abstract class Packet
{
    private byte[]? _compiled;
    protected readonly MemoryStream _stream;
    protected readonly BinaryWriter _writer;

    protected Packet(int packetId, int length = 0)
    {
        PacketId = packetId;

        if (length > 0)
        {
            _stream = new MemoryStream(length);
        }
        else
        {
            _stream = new MemoryStream();
        }

        _writer = new BinaryWriter(_stream);

        // Write packet ID
        _writer.Write((byte)packetId);

        // If dynamic length, reserve space for length
        if (length == 0)
        {
            _writer.Write((short)0);
        }
    }

    public int PacketId { get; }

    public byte[] Compile()
    {
        if (_compiled != null)
            return _compiled;

        // Update length if dynamic
        if (_stream.Length > 3 && _stream.GetBuffer()[1] == 0 && _stream.GetBuffer()[2] == 0)
        {
            var length = (short)_stream.Length;
            _stream.Position = 1;
            _writer.Write((byte)(length >> 8));
            _writer.Write((byte)length);
        }

        _compiled = _stream.ToArray();
        return _compiled;
    }

    // Modern helper methods
    protected void WriteAsciiFixed(string value, int length)
    {
        var bytes = new byte[length];
        var encoded = Encoding.ASCII.GetBytes(value);
        Array.Copy(encoded, bytes, Math.Min(encoded.Length, length));
        _writer.Write(bytes);
    }

    protected void WriteUTF8Null(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        _writer.Write(bytes);
        _writer.Write((byte)0);
    }

    protected void WriteBigEndian(short value)
    {
        _writer.Write((byte)(value >> 8));
        _writer.Write((byte)value);
    }

    protected void WriteBigEndian(int value)
    {
        _writer.Write((byte)(value >> 24));
        _writer.Write((byte)(value >> 16));
        _writer.Write((byte)(value >> 8));
        _writer.Write((byte)value);
    }
}

/// <summary>
/// Server list packet (example)
/// </summary>
public sealed class ServerListPacket : Packet
{
    public ServerListPacket() : base(0xA8)
    {
        _writer.Write((byte)0x5D); // System info flag
        _writer.Write((short)1); // Server count

        _writer.Write((short)0); // Server index
        WriteAsciiFixed(Configuration.ServerName, 32);
        _writer.Write((byte)0); // Full %
        _writer.Write((byte)0); // Timezone
        _writer.Write((int)0x7F000001); // IP (127.0.0.1)
    }
}

/// <summary>
/// ASCII message packet
/// </summary>
public sealed class AsciiMessage : Packet
{
    public AsciiMessage(Serial serial, int bodyType, MessageType type, int hue, int font, string name, string text)
        : base(0x1C)
    {
        WriteBigEndian(serial.Value);
        WriteBigEndian((short)bodyType);
        _writer.Write((byte)type);
        WriteBigEndian((short)hue);
        WriteBigEndian((short)font);
        WriteAsciiFixed(name, 30);
        WriteAsciiFixed(text, text.Length + 1);
    }
}

public enum MessageType : byte
{
    Regular = 0x00,
    System = 0x01,
    Emote = 0x02,
    Label = 0x06,
    Spell = 0x08
}

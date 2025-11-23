using System.Net.Sockets;
using System.Collections.Concurrent;

namespace Server.Network;

/// <summary>
/// Modern network state with async I/O
/// </summary>
public class NetState : IDisposable
{
    private readonly Socket _socket;
    private readonly ConcurrentQueue<Packet> _sendQueue = new();
    private readonly byte[] _recvBuffer = new byte[4096];
    private bool _disposing;

    public NetState(Socket socket)
    {
        _socket = socket;
        Mobile = new Mobile();
        Mobile.NetState = this;

        // Start receiving
        BeginReceive();
    }

    public Mobile Mobile { get; }
    public bool Running { get; private set; } = true;

    private async void BeginReceive()
    {
        try
        {
            while (Running && !_disposing)
            {
                var received = await _socket.ReceiveAsync(_recvBuffer, SocketFlags.None);

                if (received == 0)
                {
                    Dispose();
                    break;
                }

                // Process received data
                ProcessReceived(_recvBuffer.AsSpan(0, received));
            }
        }
        catch (Exception ex)
        {
            if (Configuration.Debug)
                Console.WriteLine($"NetState receive error: {ex.Message}");

            Dispose();
        }
    }

    private void ProcessReceived(ReadOnlySpan<byte> data)
    {
        // TODO: Packet processing
        if (Configuration.Debug)
            Console.WriteLine($"Received {data.Length} bytes from {Mobile.Name}");
    }

    public void Send(Packet packet)
    {
        if (_disposing || !Running)
            return;

        _sendQueue.Enqueue(packet);
        _ = FlushSendQueue();
    }

    private async Task FlushSendQueue()
    {
        try
        {
            while (_sendQueue.TryDequeue(out var packet))
            {
                var data = packet.Compile();
                await _socket.SendAsync(data, SocketFlags.None);
            }
        }
        catch (Exception ex)
        {
            if (Configuration.Debug)
                Console.WriteLine($"NetState send error: {ex.Message}");

            Dispose();
        }
    }

    public void Dispose()
    {
        if (_disposing)
            return;

        _disposing = true;
        Running = false;

        try
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
        catch
        {
            // Ignore
        }

        MessagePump.RemoveClient(this);

        Console.WriteLine($"Client disconnected: {Mobile.Name}");
    }
}

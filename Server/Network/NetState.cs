using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Server.Network;

/// <summary>
/// Highly optimized network state with async I/O, buffer pooling, and batched sends
/// </summary>
public class NetState : IDisposable
{
    private readonly Socket _socket;
    private readonly ConcurrentQueue<Packet> _sendQueue = new();
    private readonly byte[] _recvBuffer;
    private bool _disposing;
    private int _sending;

    // Use ArrayPool for buffer pooling
    private static readonly ArrayPool<byte> _bufferPool = ArrayPool<byte>.Shared;

    public NetState(Socket socket)
    {
        _socket = socket;
        Mobile = new Mobile();
        Mobile.NetState = this;

        // Rent buffer from pool - reduces GC pressure
        _recvBuffer = _bufferPool.Rent(8192);

        // Start receiving
        BeginReceive();
    }

    public Mobile Mobile { get; }
    public bool Running { get; private set; } = true;

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private async void BeginReceive()
    {
        try
        {
            while (Running && !_disposing)
            {
                var received = await _socket.ReceiveAsync(
                    new Memory<byte>(_recvBuffer, 0, _recvBuffer.Length),
                    SocketFlags.None);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ProcessReceived(ReadOnlySpan<byte> data)
    {
        // TODO: Packet processing
        if (Configuration.Debug)
            Console.WriteLine($"Received {data.Length} bytes from {Mobile.Name}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Send(Packet packet)
    {
        if (_disposing || !Running)
            return;

        _sendQueue.Enqueue(packet);

        // Only one task should be flushing at a time
        if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
        {
            _ = FlushSendQueue();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private async Task FlushSendQueue()
    {
        try
        {
            // Batch multiple packets together for better throughput
            const int maxBatch = 32;
            var batch = new List<Packet>(maxBatch);

            while (Running && !_disposing)
            {
                batch.Clear();

                // Collect up to maxBatch packets
                while (batch.Count < maxBatch && _sendQueue.TryDequeue(out var packet))
                {
                    batch.Add(packet);
                }

                if (batch.Count == 0)
                    break;

                // Send all packets in batch
                foreach (var packet in batch)
                {
                    var data = packet.Compile();
                    await _socket.SendAsync(data, SocketFlags.None);
                }

                // Small delay to allow more packets to queue
                if (_sendQueue.Count == 0)
                    await Task.Delay(1);
            }
        }
        catch (Exception ex)
        {
            if (Configuration.Debug)
                Console.WriteLine($"NetState send error: {ex.Message}");

            Dispose();
        }
        finally
        {
            Interlocked.Exchange(ref _sending, 0);

            // If more packets arrived, start flushing again
            if (!_sendQueue.IsEmpty && Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
            {
                _ = FlushSendQueue();
            }
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

        // Return buffer to pool
        if (_recvBuffer != null)
        {
            _bufferPool.Return(_recvBuffer);
        }

        MessagePump.RemoveClient(this);

        Console.WriteLine($"Client disconnected: {Mobile.Name}");
    }
}

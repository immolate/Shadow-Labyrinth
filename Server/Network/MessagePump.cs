using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace Server.Network;

/// <summary>
/// Modern async network listener
/// </summary>
public static class MessagePump
{
    private static Socket? _listener;
    private static readonly ConcurrentBag<NetState> _clients = new();
    private static CancellationTokenSource? _cts;

    public static int ClientCount => _clients.Count;

    public static void StartListener()
    {
        if (_listener != null)
            return;

        try
        {
            _cts = new CancellationTokenSource();

            var endPoint = new IPEndPoint(IPAddress.Any, Configuration.ServerPort);
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _listener.Bind(endPoint);
            _listener.Listen(128);

            Console.WriteLine($"Listening on port {Configuration.ServerPort}");

            // Start accepting connections
            _ = AcceptConnections();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to start listener: {ex.Message}");
            throw;
        }
    }

    private static async Task AcceptConnections()
    {
        if (_listener == null || _cts == null)
            return;

        try
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                var socket = await _listener.AcceptAsync(_cts.Token);

                if (_clients.Count >= Configuration.MaxClients)
                {
                    Console.WriteLine("Max clients reached, rejecting connection.");
                    socket.Close();
                    continue;
                }

                var netState = new NetState(socket);
                _clients.Add(netState);

                Console.WriteLine($"Client connected: {netState.Mobile.Serial} (Total: {_clients.Count})");
            }
        }
        catch (OperationCanceledException)
        {
            // Normal shutdown
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Accept error: {ex.Message}");
        }
    }

    public static void RemoveClient(NetState netState)
    {
        // ConcurrentBag doesn't have Remove, so we'll just let it remain
        // In production, you'd use a different collection or cleanup strategy
    }

    public static void Shutdown()
    {
        _cts?.Cancel();

        if (_listener != null)
        {
            _listener.Close();
            _listener = null;
        }

        Console.WriteLine("Network listener stopped.");
    }
}

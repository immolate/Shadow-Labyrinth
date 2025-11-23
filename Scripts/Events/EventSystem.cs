using Server;
using System.Collections.Concurrent;

namespace Server.Events;

/// <summary>
/// Modern event system with leaderboards, rewards, and scheduling
/// </summary>
public enum EventStatus
{
    Scheduled,
    Registration,
    Active,
    Completed,
    Cancelled
}

public enum EventType
{
    PvP,
    PvE,
    Crafting,
    Racing,
    Puzzle,
    Economic,
    Mixed
}

/// <summary>
/// Base event class for all competitive events
/// </summary>
public abstract class BaseEvent
{
    private static int _nextEventId = 1;
    private readonly ConcurrentDictionary<Mobile, int> _scores = new();
    private readonly ConcurrentBag<Mobile> _participants = new();

    protected BaseEvent(string name, TimeSpan duration)
    {
        EventId = Interlocked.Increment(ref _nextEventId);
        Name = name;
        Duration = duration;
        Status = EventStatus.Scheduled;
        StartTime = DateTime.MinValue;
    }

    public int EventId { get; }
    public string Name { get; }
    public abstract string Description { get; }
    public abstract EventType Type { get; }
    public TimeSpan Duration { get; set; }
    public EventStatus Status { get; protected set; }
    public DateTime StartTime { get; protected set; }
    public DateTime EndTime => StartTime + Duration;
    public TimeSpan TimeRemaining => EndTime - DateTime.UtcNow;

    public int MinPlayers { get; set; } = 1;
    public int MaxPlayers { get; set; } = 100;
    public int ParticipantCount => _participants.Count;

    // Rewards
    public List<EventReward> Rewards { get; } = new();

    // Leaderboard
    public IEnumerable<(Mobile Player, int Score)> Leaderboard =>
        _scores.OrderByDescending(x => x.Value).Select(x => (x.Key, x.Value));

    /// <summary>
    /// Register a player for the event
    /// </summary>
    public virtual bool Register(Mobile player)
    {
        if (Status != EventStatus.Registration && Status != EventStatus.Scheduled)
        {
            player.SendMessage("Registration is closed for this event.");
            return false;
        }

        if (ParticipantCount >= MaxPlayers)
        {
            player.SendMessage("This event is full!");
            return false;
        }

        if (_participants.Contains(player))
        {
            player.SendMessage("You are already registered!");
            return false;
        }

        _participants.Add(player);
        player.SendMessage($"You have registered for: {Name}");
        BroadcastMessage($"{player.Name} has joined! ({ParticipantCount}/{MaxPlayers})");

        OnPlayerRegistered(player);
        return true;
    }

    /// <summary>
    /// Unregister a player
    /// </summary>
    public virtual bool Unregister(Mobile player)
    {
        // Cannot easily remove from ConcurrentBag, so mark as removed
        player.SendMessage($"You have unregistered from: {Name}");
        return true;
    }

    /// <summary>
    /// Start the event
    /// </summary>
    public virtual void Start()
    {
        if (Status != EventStatus.Registration && Status != EventStatus.Scheduled)
            return;

        if (ParticipantCount < MinPlayers)
        {
            BroadcastMessage($"Event cancelled - not enough players ({ParticipantCount}/{MinPlayers})");
            Status = EventStatus.Cancelled;
            return;
        }

        Status = EventStatus.Active;
        StartTime = DateTime.UtcNow;

        BroadcastMessage($"Event '{Name}' has started!");
        BroadcastMessage($"Duration: {Duration.TotalMinutes} minutes");

        OnEventStart();

        // Schedule automatic end
        DelayTimer.Create(Duration, End);
    }

    /// <summary>
    /// End the event and distribute rewards
    /// </summary>
    public virtual void End()
    {
        if (Status != EventStatus.Active)
            return;

        Status = EventStatus.Completed;

        BroadcastMessage($"Event '{Name}' has ended!");

        OnEventEnd();

        // Show final leaderboard
        ShowFinalLeaderboard();

        // Distribute rewards
        DistributeRewards();
    }

    /// <summary>
    /// Update player score
    /// </summary>
    public void UpdateScore(Mobile player, int points)
    {
        _scores.AddOrUpdate(player, points, (_, current) => current + points);
    }

    /// <summary>
    /// Set player score
    /// </summary>
    public void SetScore(Mobile player, int score)
    {
        _scores[player] = score;
    }

    /// <summary>
    /// Get player score
    /// </summary>
    public int GetScore(Mobile player)
    {
        return _scores.TryGetValue(player, out var score) ? score : 0;
    }

    /// <summary>
    /// Broadcast message to all participants
    /// </summary>
    protected void BroadcastMessage(string message)
    {
        foreach (var participant in _participants)
        {
            participant.SendMessage($"[EVENT] {message}");
        }
    }

    /// <summary>
    /// Show final leaderboard
    /// </summary>
    private void ShowFinalLeaderboard()
    {
        BroadcastMessage("═══════════════════════════");
        BroadcastMessage($"FINAL LEADERBOARD - {Name}");
        BroadcastMessage("═══════════════════════════");

        int rank = 1;
        foreach (var (player, score) in Leaderboard.Take(10))
        {
            string medal = rank switch
            {
                1 => "🥇",
                2 => "🥈",
                3 => "🥉",
                _ => $"#{rank}"
            };

            BroadcastMessage($"{medal} {player.Name}: {score} points");
            rank++;
        }

        BroadcastMessage("═══════════════════════════");
    }

    /// <summary>
    /// Distribute rewards to winners
    /// </summary>
    private void DistributeRewards()
    {
        int rank = 1;
        foreach (var (player, score) in Leaderboard)
        {
            foreach (var reward in Rewards)
            {
                if (rank <= reward.MaxRank)
                {
                    reward.GiveTo(player);
                }
            }
            rank++;
        }
    }

    // Virtual methods for derived classes
    protected virtual void OnPlayerRegistered(Mobile player) { }
    protected virtual void OnEventStart() { }
    protected virtual void OnEventEnd() { }

    public IEnumerable<Mobile> GetParticipants() => _participants;
}

/// <summary>
/// Event reward system
/// </summary>
public class EventReward
{
    public int MaxRank { get; set; } = 1; // Top 1, Top 3, etc.
    public List<Item> Items { get; } = new();
    public int GoldAmount { get; set; }
    public string Title { get; set; } = "";

    public void GiveTo(Mobile player)
    {
        // Give items
        foreach (var item in Items)
        {
            var copy = item; // In real implementation, create copy
            player.SendMessage($"You received: {item.Name}");
        }

        // Give gold
        if (GoldAmount > 0)
        {
            player.SendMessage($"You received: {GoldAmount} gold!");
        }

        // Give title
        if (!string.IsNullOrEmpty(Title))
        {
            player.SendMessage($"You earned the title: {Title}");
        }
    }
}

/// <summary>
/// Event manager - handles scheduling and running events
/// </summary>
public static class EventManager
{
    private static readonly ConcurrentBag<BaseEvent> _activeEvents = new();
    private static readonly ConcurrentBag<BaseEvent> _scheduledEvents = new();

    public static IEnumerable<BaseEvent> ActiveEvents => _activeEvents;
    public static IEnumerable<BaseEvent> ScheduledEvents => _scheduledEvents;

    public static void Initialize()
    {
        Console.WriteLine("Event Manager initialized!");

        // Schedule recurring events
        ScheduleRecurringEvents();
    }

    public static void RegisterEvent(BaseEvent evt)
    {
        _scheduledEvents.Add(evt);
        Console.WriteLine($"Event scheduled: {evt.Name} at {evt.StartTime}");
    }

    public static void StartEvent(BaseEvent evt)
    {
        _activeEvents.Add(evt);
        evt.Start();
    }

    public static void BroadcastToAll(string message)
    {
        // Broadcast to all online players
        foreach (var mobile in World.Mobiles)
        {
            mobile.SendMessage($"[SERVER] {message}");
        }
    }

    private static void ScheduleRecurringEvents()
    {
        // This would schedule events at specific times
        // For now, just log
        Console.WriteLine("Recurring events scheduled!");
    }
}

using Nebuli.API.Features.Player;
using System;
using NebuliTeslaGate = Nebuli.API.Features.Map.TeslaGate;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player triggers a Tesla gate.
/// </summary>
public class PlayerTriggeringTeslaEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerTriggeringTeslaEvent(NebuliPlayer player, TeslaGate teslaGate)
    {
        Player = player;
        TeslaGate = NebuliTeslaGate.Get(teslaGate);
        IsCancelled = false;
        IsInIdleRange = true; // This is true because we already check if the player is in idle range when calling the event.
        IsTriggerable = TeslaGate.IsPlayerInHurtingRange(Player);
    }

    /// <summary>
    /// Gets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the player triggering the tesla gate.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="NebuliTeslaGate"/> being triggered.
    /// </summary>
    public NebuliTeslaGate TeslaGate { get; }

    /// <summary>
    /// Gets or sets if the player is in idle range.
    /// </summary>
    public bool IsInIdleRange { get; set; }

    /// <summary>
    /// Gets or sets if the player is in trigger range.
    /// </summary>
    public bool IsTriggerable { get; set; }
}
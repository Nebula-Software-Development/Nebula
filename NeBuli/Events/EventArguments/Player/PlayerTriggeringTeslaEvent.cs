using Nebuli.API.Features.Player;
using System;
using Nebuli.API.Features.Map;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerTriggeringTeslaEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerTriggeringTeslaEvent(ReferenceHub player, TeslaGate teslaGate, bool inIdleRange, bool isTriggerable)
    {
        Player = NebuliPlayer.Get(player);
        TeslaGate = API.Features.Map.TeslaGate.Get(teslaGate);
        IsCancelled = false;
        IsInIdleRange = TeslaGate.IsPlayerInIdleRange(Player);
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
    /// Gets the <see cref="API.Features.Map.TeslaGate"/> being triggered.
    /// </summary>
    public API.Features.Map.TeslaGate TeslaGate { get; }

    /// <summary>
    /// Gets or sets if the player is in idle range.
    /// </summary>
    public bool IsInIdleRange { get; set; }

    /// <summary>
    /// Gets or sets if the player is in trigger range.
    /// </summary>
    public bool IsTriggerable { get; set; }
}

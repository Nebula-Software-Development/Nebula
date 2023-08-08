using Nebuli.API.Features.Player;
using System;
using Nebuli.API.Features.Map;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerTriggeringTesla : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerTriggeringTesla(ReferenceHub player, TeslaGate teslaGate, bool inIdleRange, bool isTriggerable)
    {
        Player = NebuliPlayer.Get(player);
        TeslaGate = NebuliTeslaGate.Get(teslaGate);
        IsCancelled = false;
        IsInIdleRange = teslaGate.IsInIdleRange(player);
        IsTriggerable = NebuliTeslaGate.Get(teslaGate).InHurtRange(NebuliPlayer.Get(player));
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

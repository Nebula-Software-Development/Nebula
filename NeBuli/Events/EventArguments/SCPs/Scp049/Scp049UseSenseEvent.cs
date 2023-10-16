using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

/// <summary>
/// Triggered when SCP-049 uses its sense ability.
/// </summary>
public class Scp049UseSenseEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp049UseSenseEvent(ReferenceHub player, ReferenceHub target, float distance)
    {
        Player = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(target);
        Distance = distance;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the events target.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// Gets or sets the distance from SCP-049 to the target.
    /// </summary>
    public float Distance { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
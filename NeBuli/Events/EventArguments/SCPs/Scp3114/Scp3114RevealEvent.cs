using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114;

/// <summary>
/// Triggered before SCP-3114 reveals.
/// </summary>
public class Scp3114RevealEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp3114RevealEvent(ReferenceHub hub)
    {
        Player = NebuliPlayer.Get(hub);
        IsCancelled = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsCancelled { get; set; }
}

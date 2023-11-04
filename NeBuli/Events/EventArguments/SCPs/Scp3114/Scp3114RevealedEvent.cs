using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114;

/// <summary>
/// Triggered after SCP-3114 reveals.
/// </summary>
public class Scp3114RevealedEvent : EventArgs, IPlayerEvent
{
    public Scp3114RevealedEvent(ReferenceHub hub)
    {
        Player = NebuliPlayer.Get(hub);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public NebuliPlayer Player { get; }
}

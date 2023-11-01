using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114;

/// <summary>
/// Triggered when SCP-3114 completes its disguise.
/// </summary>
public class Scp3114DisguisedEvent : EventArgs, IPlayerEvent
{
    public Scp3114DisguisedEvent(ReferenceHub player, DynamicRagdoll ragdoll)
    {
        Player = NebuliPlayer.Get(player);
        Ragdoll = Ragdoll.Get(ragdoll);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The ragdoll that was disgused into.
    /// </summary>
    public Ragdoll Ragdoll { get; }
}


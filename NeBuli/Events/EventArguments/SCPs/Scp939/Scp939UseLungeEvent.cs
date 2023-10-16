using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 uses the lunge ability.
/// </summary>
public class Scp939UseLungeEvent : EventArgs, IPlayerEvent
{
    public Scp939UseLungeEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
    }

    /// <summary>
    /// Gets the player lunging.
    /// </summary>
    public NebuliPlayer Player { get; }
}
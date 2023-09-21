using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

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
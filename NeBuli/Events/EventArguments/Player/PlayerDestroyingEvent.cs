using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerDestroyingEvent : EventArgs, IPlayerEvent
{
    public PlayerDestroyingEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
    }

    /// <summary>
    /// Gets the player being destroyed.
    /// </summary>
    public NebuliPlayer Player { get; }
}
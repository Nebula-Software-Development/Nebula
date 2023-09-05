using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerDestroyingEvent : EventArgs, IPlayerEvent
{
    public PlayerDestroyingEvent(ReferenceHub player)
    {
        Player = API.Features.Player.Get(player);
    }

    /// <summary>
    /// Gets the player being destroyed.
    /// </summary>
    public API.Features.Player Player { get; }
}

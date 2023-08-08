using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerEscapingPocket : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerEscapingPocket(ReferenceHub player, bool successful)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
        Successful = successful;
    }
    /// <summary>
    /// The player calling the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// If the escape was succesful or not.
    /// </summary>
    public bool Successful { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}

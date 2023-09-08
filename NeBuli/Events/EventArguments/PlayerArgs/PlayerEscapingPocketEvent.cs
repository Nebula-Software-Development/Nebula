using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerEscapingPocketEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerEscapingPocketEvent(ReferenceHub player, bool successful)
    {
        Player = API.Features.Player.Get(player);
        IsCancelled = false;
        Successful = successful;
    }
    /// <summary>
    /// The player calling the event.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// If the escape was succesful or not.
    /// </summary>
    public bool Successful { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}

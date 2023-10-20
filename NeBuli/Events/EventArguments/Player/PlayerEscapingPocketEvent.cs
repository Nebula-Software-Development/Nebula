using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player escapes from a pocket dimension.
/// </summary>
public class PlayerEscapingPocketEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerEscapingPocketEvent(ReferenceHub player, bool successful)
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
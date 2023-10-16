using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player changes their nickname.
/// </summary>
public class PlayerChangingNicknameEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerChangingNicknameEvent(ReferenceHub ply, string newName)
    {
        Player = NebuliPlayer.Get(ply);
        NewName = newName;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets the new nickname.
    /// </summary>
    public string NewName { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}

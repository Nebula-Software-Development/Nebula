using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerTogglingNoClipEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerTogglingNoClipEvent(ReferenceHub player, bool newState, bool isPermitted)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
        NewState = newState;
        IsPermitted = isPermitted;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the new state of noclip. True for enabled, false for disabled.
    /// </summary>
    public bool NewState { get; }

    /// <summary>
    /// Gets if the player is permitted to enabled noclip.
    /// </summary>
    public bool IsPermitted { get; }
}

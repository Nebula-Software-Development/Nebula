using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 toggles focus.
/// </summary>
public class Scp939ToggleFocusEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939ToggleFocusEvent(ReferenceHub player, bool state)
    {
        Player = NebuliPlayer.Get(player);
        State = state;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player toggeling focus mode.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public bool State { get; set; }
}
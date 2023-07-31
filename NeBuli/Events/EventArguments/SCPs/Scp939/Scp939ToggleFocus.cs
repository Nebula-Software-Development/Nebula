using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939ToggleFocus : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939ToggleFocus(ReferenceHub player, bool state)
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
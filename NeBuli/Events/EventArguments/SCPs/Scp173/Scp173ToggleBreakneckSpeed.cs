using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

public class Scp173ToggleBreakneckSpeed : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp173ToggleBreakneckSpeed(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets the player toggeling breakneck speed.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
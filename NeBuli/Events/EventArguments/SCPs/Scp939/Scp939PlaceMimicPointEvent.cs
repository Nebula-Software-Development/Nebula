using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939PlaceMimicPointEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlaceMimicPointEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets the player placing the mimic point.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
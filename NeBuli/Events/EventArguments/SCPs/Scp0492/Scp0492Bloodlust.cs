using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp0492;

public class Scp0492Bloodlust : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp0492Bloodlust(ReferenceHub player, ReferenceHub observer)
    {
        Player = NebuliPlayer.Get(player);
        Observer = NebuliPlayer.Get(observer);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets the player triggering blood lust.
    /// </summary>
    public NebuliPlayer Player { get; }
    
    /// <summary>
    /// Gets the observing player.
    /// </summary>
    public NebuliPlayer Observer { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

public class Scp049LoseSenseTarget : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp049LoseSenseTarget(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets the player losing the target.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
using System;
using Nebuli.API.Features;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

public class Scp049StartResurrect : EventArgs, IPlayerEvent, IRadgollEvent, ICancellableEvent
{
    public Scp049StartResurrect(ReferenceHub player, BasicRagdoll baseRagdoll)
    {
        Player = NebuliPlayer.Get(player);
        Ragdoll = Ragdoll.Get(baseRagdoll);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Ragdoll"/> being resurrected.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
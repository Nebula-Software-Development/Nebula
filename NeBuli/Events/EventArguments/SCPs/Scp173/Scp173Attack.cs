using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

public class Scp173Attack : EventArgs, IDamageEvent, ICancellableEvent
{
    public Scp173Attack(ReferenceHub player, ReferenceHub target)
    {
        Attacker = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(target);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets SCP-173.
    /// </summary>
    public NebuliPlayer Attacker { get; }

    /// <summary>
    /// Gets the target being attacked.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
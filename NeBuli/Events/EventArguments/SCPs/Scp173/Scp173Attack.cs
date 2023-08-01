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
    
    public NebuliPlayer Attacker { get; }

    public NebuliPlayer Target { get; }

    public bool IsCancelled { get; set; }
}
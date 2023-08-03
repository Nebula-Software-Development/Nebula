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
    
    public NebuliPlayer Player { get; }

    public Ragdoll Ragdoll { get; }

    public bool IsCancelled { get; set; }
}
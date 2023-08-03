using System;
using Nebuli.API.Features;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp0492;

public class Scp0492ConsumeCorpse : EventArgs, IPlayerEvent, IRadgollEvent, ICancellableEvent
{
    public Scp0492ConsumeCorpse(ReferenceHub player, BasicRagdoll baseRagdoll)
    {
        Player = NebuliPlayer.Get(player);
        Ragdoll = Ragdoll.Get(baseRagdoll);
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }

    public Ragdoll Ragdoll { get; }

    public bool IsCancelled { get; set; }
}
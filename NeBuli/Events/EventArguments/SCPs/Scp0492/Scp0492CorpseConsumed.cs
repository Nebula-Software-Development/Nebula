using System;
using Nebuli.API.Features;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp0492;

public class Scp0492CorpseConsumed : EventArgs, IPlayerEvent, IRadgollEvent
{
    public Scp0492CorpseConsumed(ReferenceHub player, BasicRagdoll baseRagdoll)
    {
        Player = NebuliPlayer.Get(player);
        Ragdoll = Ragdoll.Get(baseRagdoll);
        HealthToReceive = 100;
    }
    
    public Ragdoll Ragdoll { get; }

    public NebuliPlayer Player { get; }
    
    public float HealthToReceive { get; set; }
}
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
    
    /// <summary>
    /// Gets the <see cref="API.Features.Ragdoll"/> that was consumed.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// Gets the player that consumed the corpse.
    /// </summary>
    public NebuliPlayer Player { get; }
    
    /// <summary>
    /// The health amount the player will recieve.
    /// </summary>
    public float HealthToReceive { get; set; }
}
using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerSpawningRagdollEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerSpawningRagdollEvent(ReferenceHub owner, DamageHandlerBase damageHandlerBase, BasicRagdoll basicRagdoll)
    {
        Player = NebuliPlayer.Get(owner);
        Ragdoll = Ragdoll.Get(basicRagdoll);
        DamageHandler = damageHandlerBase;
        IsCancelled = false;
    }

    public NebuliPlayer Player { get; }

    public bool IsCancelled { get; set; }

    public DamageHandlerBase DamageHandler { get; }

    public Ragdoll Ragdoll { get; }
}

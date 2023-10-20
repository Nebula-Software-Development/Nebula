using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player spawns a ragdoll.
/// </summary>
public class PlayerSpawningRagdollEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerSpawningRagdollEvent(ReferenceHub owner, DamageHandlerBase damageHandlerBase, BasicRagdoll basicRagdoll)
    {
        Player = NebuliPlayer.Get(owner);
        Ragdoll = Ragdoll.Get(basicRagdoll);
        DamageHandler = damageHandlerBase;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the owner of the ragdoll.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the <see cref="DamageHandlerBase"/> of the ragdoll.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Ragdoll"/> being spawned.
    /// </summary>
    public Ragdoll Ragdoll { get; }
}
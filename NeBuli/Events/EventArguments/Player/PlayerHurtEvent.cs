using Nebuli.API.Features.Player;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerHurtEvent : EventArgs, IDamageEvent, ICancellableEvent
{
    public PlayerHurtEvent(AttackerDamageHandler attacker, ReferenceHub target, DamageHandlerBase dmgB)
    {
        Attacker = NebuliPlayer.Get(attacker.Attacker.Hub);
        Target = NebuliPlayer.Get(target);
        DamageHandlerBase = dmgB;
        IsCancelled = false;
    }

    public PlayerHurtEvent(ReferenceHub attacker, ReferenceHub target, DamageHandlerBase dmgB)
    {
        Attacker = NebuliPlayer.Get(attacker);
        Target = NebuliPlayer.Get(target);
        DamageHandlerBase = dmgB;
        IsCancelled = false;
    }

    /// <summary>
    /// The attacker of the target.
    /// </summary>
    public NebuliPlayer Attacker { get; }

    /// <summary>
    /// The player being attacked.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// The <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the player being attacked.
    /// </summary>
    public DamageHandlerBase DamageHandlerBase { get; }

    /// <summary>
    /// If the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}
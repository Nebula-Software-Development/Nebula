using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerHurtEvent : EventArgs, IDamageEvent, ICancellableEvent
{
    public PlayerHurtEvent(AttackerDamageHandler attacker, ReferenceHub target, DamageHandlerBase dmgB)
    {
        Attacker = API.Features.Player.Get(attacker.Attacker.Hub);
        Target = API.Features.Player.Get(target);
        DamageHandlerBase = dmgB;
        IsCancelled = false;
    }

    /// <summary>
    /// The attacker of the target.
    /// </summary>
    public API.Features.Player Attacker { get; }

    /// <summary>
    /// The player being attacked.
    /// </summary>
    public API.Features.Player Target { get; }

    /// <summary>
    /// The <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the player being attacked.
    /// </summary>
    public DamageHandlerBase DamageHandlerBase { get; }

    /// <summary>
    /// If the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}
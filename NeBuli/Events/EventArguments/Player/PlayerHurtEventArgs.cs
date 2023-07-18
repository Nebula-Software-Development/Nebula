using Nebuli.API.Features.Player;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerHurtEventArgs : EventArgs, IDamageEvent, ICancellableEvent
{
    public PlayerHurtEventArgs(AttackerDamageHandler attacker, ReferenceHub target, DamageHandlerBase dmgB)
    {
        Attacker = NebuliPlayer.Get(attacker.Attacker.Hub);
        Target = NebuliPlayer.Get(target);
        DamageHandlerBase = dmgB;
        IsCancelled = false;
    }

    public NebuliPlayer Attacker { get; }

    public NebuliPlayer Target { get; }

    public DamageHandlerBase DamageHandlerBase { get; }

    public bool IsCancelled { get; set; }
}
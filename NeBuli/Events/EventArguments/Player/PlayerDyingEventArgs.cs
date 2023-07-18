using Nebuli.API.Features.Player;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerDyingEventArgs : EventArgs, IDamageEvent, ICancellableEvent
{
    public PlayerDyingEventArgs(AttackerDamageHandler attacker, ReferenceHub target, DamageHandlerBase dmgB)
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
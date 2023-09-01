using Nebuli.API.Features.Player;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerDyingEvent : EventArgs, ICancellableEvent, IPlayerEvent
{
    public PlayerDyingEvent(ReferenceHub target, DamageHandlerBase dmgB)
    {
        Player = NebuliPlayer.Get(target);
        if (DamageHandlerBase is AttackerDamageHandler attackerDamageHandler)
            Killer = NebuliPlayer.Get(attackerDamageHandler.Attacker.Hub);
        DamageHandlerBase = dmgB;
        IsCancelled = false;
    }
    /// <summary>
    /// The player thats dying.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the killer of the event, if any.
    /// </summary>
    public NebuliPlayer Killer { get; }

    /// <summary>
    /// The <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the event.
    /// </summary>
    public DamageHandlerBase DamageHandlerBase { get; set; }

    /// <summary>
    /// If the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}
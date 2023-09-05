using Nebuli.API.Features;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerDyingEvent : EventArgs, ICancellableEvent, IPlayerEvent
{
    public PlayerDyingEvent(ReferenceHub target, DamageHandlerBase dmgB)
    {
        Player = Player.Get(target);        
        DamageHandlerBase = dmgB;
        if (DamageHandlerBase is AttackerDamageHandler attackerDamageHandler)
            Killer = Player.Get(attackerDamageHandler.Attacker.Hub);
        IsCancelled = false;
    }
    /// <summary>
    /// The player thats dying.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the killer of the event, if any.
    /// </summary>
    public API.Features.Player Killer { get; }

    /// <summary>
    /// The <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the event.
    /// </summary>
    public DamageHandlerBase DamageHandlerBase { get; set; }

    /// <summary>
    /// If the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}
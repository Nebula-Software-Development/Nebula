using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered after the player has died.
/// </summary>
public class PlayerDiedEvent : EventArgs, IPlayerEvent
{
    public PlayerDiedEvent(ReferenceHub target, DamageHandlerBase dmgB)
    {
        Player = NebuliPlayer.Get(target);
        DamageHandlerBase = dmgB;
        if (DamageHandlerBase is AttackerDamageHandler attackerDamageHandler)
            Killer = NebuliPlayer.Get(attackerDamageHandler.Attacker.Hub);
    }

    /// <summary>
    /// Gets the player that died.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the event.
    /// </summary>
    public DamageHandlerBase DamageHandlerBase { get; }

    /// <summary>
    /// Gets the killer of the event, if any.
    /// </summary>
    public NebuliPlayer Killer { get; }
}
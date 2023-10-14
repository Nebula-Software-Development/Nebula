using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp0492;

/// <summary>
/// Triggered when SCP-049-2 attacks a player.
/// </summary>
public class Scp0492AttackEvent : EventArgs, IDamageEvent, ICancellableEvent
{
    public Scp0492AttackEvent(ReferenceHub player, ReferenceHub target)
    {
        Attacker = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(target);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the attacker, or SCP-0492.
    /// </summary>
    public NebuliPlayer Attacker { get; }

    /// <summary>
    /// Gets the player being attacked.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
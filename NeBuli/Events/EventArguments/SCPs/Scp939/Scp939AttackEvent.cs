using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 attacks.
/// </summary>
public class Scp939AttackEvent : EventArgs, IDamageEvent, ICancellableEvent
{
    public Scp939AttackEvent(ReferenceHub player, uint netId)
    {
        Attacker = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(netId);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the attacking player, SCP-939
    /// </summary>
    public NebuliPlayer Attacker { get; }

    /// <summary>
    /// Gets the player being attacked.
    /// NOTE: The Player can be null if the attacker attacks a window or something else that is not a player.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
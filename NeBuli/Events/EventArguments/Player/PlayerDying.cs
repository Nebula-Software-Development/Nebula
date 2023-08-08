using Nebuli.API.Features.Player;
using PlayerStatsSystem;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerDying : EventArgs, ICancellableEvent, IPlayerEvent
{
    public PlayerDying(ReferenceHub target, DamageHandlerBase dmgB)
    {
        Player = NebuliPlayer.Get(target);
        DamageHandlerBase = dmgB;
        IsCancelled = false;
    }
    /// <summary>
    /// The player thats dying.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the event.
    /// </summary>
    public DamageHandlerBase DamageHandlerBase { get; set; }

    /// <summary>
    /// If the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}
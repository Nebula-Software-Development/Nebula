using Nebuli.API.Features.Player;
using PlayerRoles.PlayableScps.Scp079;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp079;

/// <summary>
/// Triggered when SCP-079 is gaining experience points.
/// </summary>
public class Scp079GainingExperienceEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp079GainingExperienceEvent(ReferenceHub player, int amount, Scp079HudTranslation reason)
    {
        Player = NebuliPlayer.Get(player);
        Amount = amount;
        Reason = reason;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the amount of XP that will be granted.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Scp079HudTranslation"/> reason.
    /// </summary>
    public Scp079HudTranslation Reason { get; set; }
}
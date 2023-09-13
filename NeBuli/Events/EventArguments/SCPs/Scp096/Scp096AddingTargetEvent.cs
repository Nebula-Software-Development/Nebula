using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp096;

public class Scp096AddingTargetEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp096AddingTargetEvent(ReferenceHub player, ReferenceHub looker, bool isForLooking)
    {
        Player = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(looker);
        LookedAt096 = isForLooking;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player playing as SCP-096.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the target being added.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// Gets if the player looked at SCP-096, will be false if the target triggered 096 by other means.
    /// </summary>
    public bool LookedAt096 { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
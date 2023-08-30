using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp096;

public class Scp096EnragingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp096EnragingEvent(ReferenceHub player, float duration)
    {
        Player = NebuliPlayer.Get(player);
        Duration = duration;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player enraging.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets the initial duration of the rage.
    /// </summary>
    public float Duration { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}

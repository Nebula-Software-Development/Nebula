using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp0492;

/// <summary>
/// Triggered when SCP-049-2 goes into a bloodlust state.
/// </summary>
public class Scp0492BloodlustEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp0492BloodlustEvent(ReferenceHub player, ReferenceHub observer)
    {
        Player = NebuliPlayer.Get(player);
        Observer = NebuliPlayer.Get(observer);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering blood lust.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the observing player.
    /// </summary>
    public NebuliPlayer Observer { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
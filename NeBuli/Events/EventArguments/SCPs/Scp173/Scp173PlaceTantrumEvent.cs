using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

/// <summary>
/// Triggered when SCP-173 attempts to place a tantrum.
/// </summary>
public class Scp173PlaceTantrumEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp173PlaceTantrumEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player placing the tantrum.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
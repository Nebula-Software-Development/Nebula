using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

/// <summary>
/// Triggered when SCP-173 toggles its breakneck speed mode.
/// </summary>
public class Scp173ToggleBreakneckSpeedEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp173ToggleBreakneckSpeedEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player toggeling breakneck speed.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
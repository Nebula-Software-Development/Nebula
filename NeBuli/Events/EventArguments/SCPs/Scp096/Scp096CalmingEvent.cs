﻿using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp096;

/// <summary>
/// Triggered when SCP-096 is calming down from an enraged state.
/// </summary>
public class Scp096CalmingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp096CalmingEvent(ReferenceHub player, bool clearingTime)
    {
        Player = NebuliPlayer.Get(player);
        ClearingTime = clearingTime;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the remaining time will be cleared or not.
    /// </summary>
    public bool ClearingTime { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
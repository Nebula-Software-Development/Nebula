// -----------------------------------------------------------------------
// <copyright file=Scp106AttackingEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp106;

/// <summary>
/// Triggered when SCP-106 is attacking a player.
/// </summary>
public class Scp106AttackingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp106AttackingEvent(ReferenceHub scp, ReferenceHub target)
    {
        Player = NebuliPlayer.Get(scp);
        Target = NebuliPlayer.Get(target);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the player being attacked.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
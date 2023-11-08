// -----------------------------------------------------------------------
// <copyright file=Scp3114StranglingEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;
using static PlayerRoles.PlayableScps.Scp3114.Scp3114Strangle;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114;

/// <summary>
/// Triggered when SCP-3114 starts to strangle.
/// </summary>
public class Scp3114StranglingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp3114StranglingEvent(ReferenceHub player, StrangleTarget target)
    {
        Player = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(target.Target);
        StrangleTarget = target;
        IsCancelled = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The player being strangled.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// The <see cref="PlayerRoles.PlayableScps.Scp3114.Scp3114Strangle.StrangleTarget"/> of the event.
    /// </summary>
    public StrangleTarget StrangleTarget { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsCancelled { get; set; } = false;
}

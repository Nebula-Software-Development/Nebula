// -----------------------------------------------------------------------
// <copyright file=Scp3114RevealEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114;

/// <summary>
/// Triggered before SCP-3114 reveals.
/// </summary>
public class Scp3114RevealEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp3114RevealEvent(ReferenceHub hub)
    {
        Player = NebuliPlayer.Get(hub);
        IsCancelled = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsCancelled { get; set; }
}

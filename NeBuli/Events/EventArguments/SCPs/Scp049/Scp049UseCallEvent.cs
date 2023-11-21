// -----------------------------------------------------------------------
// <copyright file=Scp049UseCallEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

/// <summary>
/// Triggered when SCP-049 uses its call ability.
/// </summary>
public class Scp049UseCallEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp049UseCallEvent(ReferenceHub player)
    {
        Player = API.Features.Player.Get(player);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
// -----------------------------------------------------------------------
// <copyright file=Scp049UseSenseEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

/// <summary>
/// Triggered when SCP-049 uses its sense ability.
/// </summary>
public class Scp049UseSenseEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp049UseSenseEvent(ReferenceHub player, ReferenceHub target, float distance)
    {
        Player = API.Features.Player.Get(player);
        Target = API.Features.Player.Get(target);
        Distance = distance;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets the events target.
    /// </summary>
    public API.Features.Player Target { get; }

    /// <summary>
    /// Gets or sets the distance from SCP-049 to the target.
    /// </summary>
    public float Distance { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
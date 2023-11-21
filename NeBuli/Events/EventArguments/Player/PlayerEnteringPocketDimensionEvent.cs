// -----------------------------------------------------------------------
// <copyright file=PlayerEnteringPocketDimensionEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player enters the pocket dimension.
/// </summary>
public class PlayerEnteringPocketDimensionEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerEnteringPocketDimensionEvent(API.Features.Player player, API.Features.Player target)
    {
        Player = player;
        Target = target;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player teleporting the target, or SCP-106.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets the player being teleported.
    /// </summary>
    public API.Features.Player Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
// -----------------------------------------------------------------------
// <copyright file=Scp173ToggleBreakneckSpeedEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

/// <summary>
/// Triggered when SCP-173 toggles its breakneck speed mode.
/// </summary>
public class Scp173ToggleBreakneckSpeedEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp173ToggleBreakneckSpeedEvent(ReferenceHub player)
    {
        Player = API.Features.Player.Get(player);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player toggeling breakneck speed.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
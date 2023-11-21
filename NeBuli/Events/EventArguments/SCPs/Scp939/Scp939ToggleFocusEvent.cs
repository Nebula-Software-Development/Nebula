// -----------------------------------------------------------------------
// <copyright file=Scp939ToggleFocusEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 toggles focus.
/// </summary>
public class Scp939ToggleFocusEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939ToggleFocusEvent(ReferenceHub player, bool state)
    {
        Player = API.Features.Player.Get(player);
        State = state;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player toggeling focus mode.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public bool State { get; set; }
}
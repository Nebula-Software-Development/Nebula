// -----------------------------------------------------------------------
// <copyright file=Scp096AddingTargetEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.SCPs.Scp096;

/// <summary>
/// Triggered when SCP-096 is adding a target to its list.
/// </summary>
public class Scp096AddingTargetEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp096AddingTargetEvent(ReferenceHub player, ReferenceHub looker, bool isForLooking)
    {
        Player = API.Features.Player.Get(player);
        Target = API.Features.Player.Get(looker);
        LookedAt096 = isForLooking;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player playing as SCP-096.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets the target being added.
    /// </summary>
    public API.Features.Player Target { get; }

    /// <summary>
    /// Gets if the player looked at SCP-096, will be false if the target triggered 096 by other means.
    /// </summary>
    public bool LookedAt096 { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
// -----------------------------------------------------------------------
// <copyright file=WarheadStartingEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Server;

/// <summary>
/// Triggered before the warhead starts its detonation sequence.
/// </summary>
public class WarheadStartingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public WarheadStartingEvent(bool automatic, bool suppressSubtitles, ReferenceHub trigger)
    {
        Player = trigger == null ? API.Features.Server.NebuliHost : NebuliPlayer.Get(trigger);
        SuppressSubtitles = suppressSubtitles;
        IsAutomatic = automatic;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the Alpha Warhead.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if C.A.S.S.I.E should suppress subtitles or not.
    /// </summary>
    public bool SuppressSubtitles { get; set; }

    /// <summary>
    /// Gets or sets if the detonation is a automatic detonation.
    /// </summary>
    public bool IsAutomatic { get; set; }

    /// <summary>
    /// Gets or sets if the event is canceled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
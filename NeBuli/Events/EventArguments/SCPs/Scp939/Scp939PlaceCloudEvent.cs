// -----------------------------------------------------------------------
// <copyright file=Scp939PlaceCloudEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 places a cloud of gas.
/// </summary>
public class Scp939PlaceCloudEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlaceCloudEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }

    /// <summary>
    /// The player placing the cloud.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
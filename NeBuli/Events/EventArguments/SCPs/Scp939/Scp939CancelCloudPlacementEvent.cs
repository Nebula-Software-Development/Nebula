// -----------------------------------------------------------------------
// <copyright file=Scp939CancelCloudPlacementEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 cancels cloud placement.
/// </summary>
public class Scp939CancelCloudPlacementEvent : EventArgs, IPlayerEvent
{
    public Scp939CancelCloudPlacementEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
    }

    /// <summary>
    /// The player canceling the cloud placement.
    /// </summary>
    public NebuliPlayer Player { get; }
}
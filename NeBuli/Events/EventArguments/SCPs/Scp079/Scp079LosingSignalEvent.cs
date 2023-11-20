// -----------------------------------------------------------------------
// <copyright file=Scp079LosingSignalEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp079;

/// <summary>
/// Triggered when SCP-079 is losing its signal.
/// </summary>
public class Scp079LosingSignalEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp079LosingSignalEvent(PlayerRoleBase player, float timeToLoseSignal)
    {
        if (player.TryGetOwner(out ReferenceHub hub))
        {
            Player = NebuliPlayer.Get(hub);
        }
        DurationOfSignalLoss = timeToLoseSignal;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player losing signal.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the duration of the signal loss.
    /// </summary>
    public float DurationOfSignalLoss { get; set; }
}
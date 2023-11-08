// -----------------------------------------------------------------------
// <copyright file=Scp049FinishResurrectEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

/// <summary>
/// Triggered when SCP-049 successfully finishes resurrecting a player.
/// </summary>
public class Scp049FinishResurrectEvent : EventArgs, IPlayerEvent, IRadgollEvent, ICancellableEvent
{
    public Scp049FinishResurrectEvent(ReferenceHub player, BasicRagdoll baseRagdoll)
    {
        Player = NebuliPlayer.Get(player);
        Ragdoll = Ragdoll.Get(baseRagdoll);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Ragdoll"/> that was resurrected.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
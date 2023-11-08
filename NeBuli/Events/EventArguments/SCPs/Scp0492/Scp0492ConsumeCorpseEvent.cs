// -----------------------------------------------------------------------
// <copyright file=Scp0492ConsumeCorpseEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp0492;

/// <summary>
/// Triggered when SCP-049-2 starts to consumes a corpse.
/// </summary>
public class Scp0492ConsumeCorpseEvent : EventArgs, IPlayerEvent, IRadgollEvent, ICancellableEvent
{
    public Scp0492ConsumeCorpseEvent(ReferenceHub player, BasicRagdoll baseRagdoll)
    {
        Player = NebuliPlayer.Get(player);
        Ragdoll = Ragdoll.Get(baseRagdoll);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player consuming the corpse.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The <see cref="API.Features.Ragdoll"/> being consumed.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
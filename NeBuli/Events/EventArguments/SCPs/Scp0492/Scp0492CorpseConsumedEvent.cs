// -----------------------------------------------------------------------
// <copyright file=Scp0492CorpseConsumedEvent.cs company="NebuliTeam">
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
/// Triggered when SCP-049-2 finishes consuming a corpse.
/// </summary>
public class Scp0492CorpseConsumedEvent : EventArgs, IPlayerEvent, IRadgollEvent
{
    public Scp0492CorpseConsumedEvent(ReferenceHub player, BasicRagdoll baseRagdoll)
    {
        Player = NebuliPlayer.Get(player);
        Ragdoll = Ragdoll.Get(baseRagdoll);
        HealthToReceive = 100;
    }

    /// <summary>
    /// Gets the <see cref="API.Features.Ragdoll"/> that was consumed.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// Gets the player that consumed the corpse.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The health amount the player will recieve.
    /// </summary>
    public float HealthToReceive { get; set; }
}
// -----------------------------------------------------------------------
// <copyright file=Scp3114DisguisingEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114;

/// <summary>
/// Triggered when SCP-3114 starts to complete its disguise.
/// </summary>
public class Scp3114DisguisingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp3114DisguisingEvent(ReferenceHub player, BasicRagdoll ragdoll)
    {
        Player = NebuliPlayer.Get(player);
        Ragdoll = Ragdoll.Get(ragdoll as DynamicRagdoll);
        IsCancelled = false;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The ragdoll being disguised into. CAN BE NULL.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsCancelled { get; set; }
}

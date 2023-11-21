// -----------------------------------------------------------------------
// <copyright file=Scp3114DisguisedEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114;

/// <summary>
/// Triggered when SCP-3114 completes its disguise.
/// </summary>
public class Scp3114DisguisedEvent : EventArgs, IPlayerEvent
{
    public Scp3114DisguisedEvent(ReferenceHub player, DynamicRagdoll ragdoll)
    {
        Player = API.Features.Player.Get(player);
        Ragdoll = Ragdoll.Get(ragdoll);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// The ragdoll that was disgused into.
    /// </summary>
    public Ragdoll Ragdoll { get; }
}


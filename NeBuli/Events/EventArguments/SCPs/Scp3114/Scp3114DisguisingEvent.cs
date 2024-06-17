// -----------------------------------------------------------------------
// <copyright file=Scp3114DisguisingEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.API.Features;
using Nebula.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;

namespace Nebula.Events.EventArguments.SCPs.Scp3114
{
    /// <summary>
    ///     Triggered when SCP-3114 starts to complete its disguise.
    /// </summary>
    public class Scp3114DisguisingEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp3114DisguisingEvent(ReferenceHub player, BasicRagdoll ragdoll)
        {
            Player = API.Features.Player.Get(player);
            Ragdoll = Ragdoll.Get(ragdoll as DynamicRagdoll);
            IsCancelled = false;
        }

        /// <summary>
        ///     The ragdoll being disguised into. CAN BE NULL.
        /// </summary>
        public Ragdoll Ragdoll { get; }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
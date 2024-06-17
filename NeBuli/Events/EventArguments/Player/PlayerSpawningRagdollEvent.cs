// -----------------------------------------------------------------------
// <copyright file=PlayerSpawningRagdollEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.API.Features;
using Nebula.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;
using PlayerStatsSystem;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player spawns a ragdoll.
    /// </summary>
    public class PlayerSpawningRagdollEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerSpawningRagdollEvent(ReferenceHub owner, DamageHandlerBase damageHandlerBase,
            BasicRagdoll basicRagdoll)
        {
            Player = API.Features.Player.Get(owner);
            Ragdoll = Ragdoll.Get(basicRagdoll);
            DamageHandler = damageHandlerBase;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the <see cref="DamageHandlerBase" /> of the ragdoll.
        /// </summary>
        public DamageHandlerBase DamageHandler { get; }

        /// <summary>
        ///     Gets the <see cref="API.Features.Ragdoll" /> being spawned.
        /// </summary>
        public Ragdoll Ragdoll { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the owner of the ragdoll.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
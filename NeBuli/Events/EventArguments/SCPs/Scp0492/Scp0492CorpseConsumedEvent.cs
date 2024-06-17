// -----------------------------------------------------------------------
// <copyright file=Scp0492CorpseConsumedEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.API.Features;
using Nebula.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;

namespace Nebula.Events.EventArguments.SCPs.Scp0492
{
    /// <summary>
    ///     Triggered when SCP-049-2 finishes consuming a corpse.
    /// </summary>
    public class Scp0492CorpseConsumedEvent : EventArgs, IPlayerEvent, IRadgollEvent
    {
        public Scp0492CorpseConsumedEvent(ReferenceHub player, BasicRagdoll baseRagdoll)
        {
            Player = API.Features.Player.Get(player);
            Ragdoll = Ragdoll.Get(baseRagdoll);
            HealthToReceive = 100;
        }

        /// <summary>
        ///     The health amount the player will recieve.
        /// </summary>
        public float HealthToReceive { get; set; }

        /// <summary>
        ///     Gets the player that consumed the corpse.
        /// </summary>
        public API.Features.Player Player { get; }

        /// <summary>
        ///     Gets the <see cref="API.Features.Ragdoll" /> that was consumed.
        /// </summary>
        public Ragdoll Ragdoll { get; }
    }
}
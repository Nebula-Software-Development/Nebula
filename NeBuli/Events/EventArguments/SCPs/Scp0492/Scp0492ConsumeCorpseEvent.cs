// -----------------------------------------------------------------------
// <copyright file=Scp0492ConsumeCorpseEvent.cs company="NebulaTeam">
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
    ///     Triggered when SCP-049-2 starts to consumes a corpse.
    /// </summary>
    public class Scp0492ConsumeCorpseEvent : EventArgs, IPlayerEvent, IRadgollEvent, ICancellableEvent
    {
        public Scp0492ConsumeCorpseEvent(ReferenceHub player, BasicRagdoll baseRagdoll)
        {
            Player = API.Features.Player.Get(player);
            Ragdoll = Ragdoll.Get(baseRagdoll);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player consuming the corpse.
        /// </summary>
        public API.Features.Player Player { get; }

        /// <summary>
        ///     The <see cref="API.Features.Ragdoll" /> being consumed.
        /// </summary>
        public Ragdoll Ragdoll { get; }
    }
}
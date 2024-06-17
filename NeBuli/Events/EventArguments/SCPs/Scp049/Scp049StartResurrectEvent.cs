// -----------------------------------------------------------------------
// <copyright file=Scp049StartResurrectEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.API.Features;
using Nebula.Events.EventArguments.Interfaces;
using PlayerRoles.Ragdolls;

namespace Nebula.Events.EventArguments.SCPs.Scp049
{
    /// <summary>
    ///     Triggered when SCP-049 starts the resurrection process.
    /// </summary>
    public class Scp049StartResurrectEvent : EventArgs, IPlayerEvent, IRadgollEvent, ICancellableEvent
    {
        public Scp049StartResurrectEvent(ReferenceHub player, BasicRagdoll baseRagdoll)
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
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }

        /// <summary>
        ///     Gets the <see cref="API.Features.Ragdoll" /> being resurrected.
        /// </summary>
        public Ragdoll Ragdoll { get; }
    }
}
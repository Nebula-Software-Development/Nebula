// -----------------------------------------------------------------------
// <copyright file=Scp3114DisguisedEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
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
    ///     Triggered when SCP-3114 completes its disguise.
    /// </summary>
    public class Scp3114DisguisedEvent : EventArgs, IPlayerEvent
    {
        public Scp3114DisguisedEvent(ReferenceHub player, DynamicRagdoll ragdoll)
        {
            Player = API.Features.Player.Get(player);
            Ragdoll = Ragdoll.Get(ragdoll);
        }

        /// <summary>
        ///     The ragdoll that was disgused into.
        /// </summary>
        public Ragdoll Ragdoll { get; }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
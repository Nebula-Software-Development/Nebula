// -----------------------------------------------------------------------
// <copyright file=Scp939AttackEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp939
{
    /// <summary>
    ///     Triggered when SCP-939 attacks.
    /// </summary>
    public class Scp939AttackEvent : EventArgs, IDamageEvent, ICancellableEvent
    {
        public Scp939AttackEvent(ReferenceHub player, uint netId)
        {
            Attacker = API.Features.Player.Get(player);
            Target = API.Features.Player.Get(netId);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the attacking player, SCP-939
        /// </summary>
        public API.Features.Player Attacker { get; }

        /// <summary>
        ///     Gets the player being attacked.
        ///     NOTE: The Player can be null if the attacker attacks a window or something else that is not a player.
        /// </summary>
        public API.Features.Player Target { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file=Scp0492AttackEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp0492
{
    /// <summary>
    ///     Triggered when SCP-049-2 attacks a player.
    /// </summary>
    public class Scp0492AttackEvent : EventArgs, IDamageEvent, ICancellableEvent
    {
        public Scp0492AttackEvent(ReferenceHub player, ReferenceHub target)
        {
            Attacker = API.Features.Player.Get(player);
            Target = API.Features.Player.Get(target);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the attacker, or SCP-0492.
        /// </summary>
        public API.Features.Player Attacker { get; }

        /// <summary>
        ///     Gets the player being attacked.
        /// </summary>
        public API.Features.Player Target { get; }
    }
}
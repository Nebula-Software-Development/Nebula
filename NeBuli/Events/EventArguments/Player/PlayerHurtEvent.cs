// -----------------------------------------------------------------------
// <copyright file=PlayerHurtEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerStatsSystem;

namespace Nebuli.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player is hurt.
    /// </summary>
    public class PlayerHurtEvent : EventArgs, IDamageEvent, ICancellableEvent
    {
        public PlayerHurtEvent(AttackerDamageHandler attacker, ReferenceHub target, DamageHandlerBase dmgB)
        {
            Attacker = API.Features.Player.Get(attacker.Attacker.Hub);
            Target = API.Features.Player.Get(target);
            DamageHandlerBase = dmgB;
            IsCancelled = false;
        }

        public PlayerHurtEvent(ReferenceHub attacker, ReferenceHub target, DamageHandlerBase dmgB)
        {
            Attacker = API.Features.Player.Get(attacker);
            Target = API.Features.Player.Get(target);
            DamageHandlerBase = dmgB;
            IsCancelled = false;
        }

        /// <summary>
        ///     The <see cref="PlayerStatsSystem.DamageHandlerBase" /> of the player being attacked.
        /// </summary>
        public DamageHandlerBase DamageHandlerBase { get; }

        /// <summary>
        ///     If the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The attacker of the target.
        /// </summary>
        public API.Features.Player Attacker { get; }

        /// <summary>
        ///     The player being attacked.
        /// </summary>
        public API.Features.Player Target { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file=PlayerDyingEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;
using PlayerStatsSystem;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player is dying.
    /// </summary>
    public class PlayerDyingEvent : EventArgs, ICancellableEvent, IPlayerEvent
    {
        public PlayerDyingEvent(ReferenceHub target, DamageHandlerBase dmgB)
        {
            Player = API.Features.Player.Get(target);
            DamageHandlerBase = dmgB;
            if (DamageHandlerBase is AttackerDamageHandler attackerDamageHandler)
            {
                Killer = API.Features.Player.Get(attackerDamageHandler.Attacker.Hub);
            }

            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the killer of the event, if any.
        /// </summary>
        public API.Features.Player Killer { get; }

        /// <summary>
        ///     The <see cref="PlayerStatsSystem.DamageHandlerBase" /> of the event.
        /// </summary>
        public DamageHandlerBase DamageHandlerBase { get; set; }

        /// <summary>
        ///     If the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player thats dying.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file=PlayerDiedEvent.cs company="Nebula-Software-Development">
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
    ///     Triggered after the player has died.
    /// </summary>
    public class PlayerDiedEvent : EventArgs, IPlayerEvent
    {
        public PlayerDiedEvent(ReferenceHub target, DamageHandlerBase dmgB)
        {
            Player = API.Features.Player.Get(target);
            DamageHandlerBase = dmgB;
            if (DamageHandlerBase is AttackerDamageHandler attackerDamageHandler)
            {
                Killer = API.Features.Player.Get(attackerDamageHandler.Attacker.Hub);
            }
        }

        /// <summary>
        ///     The <see cref="PlayerStatsSystem.DamageHandlerBase" /> of the event.
        /// </summary>
        public DamageHandlerBase DamageHandlerBase { get; }

        /// <summary>
        ///     Gets the killer of the event, if any.
        /// </summary>
        public API.Features.Player Killer { get; }

        /// <summary>
        ///     Gets the player that died.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
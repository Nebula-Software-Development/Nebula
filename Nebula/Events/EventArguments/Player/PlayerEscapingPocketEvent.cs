﻿// -----------------------------------------------------------------------
// <copyright file=PlayerEscapingPocketEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player escapes from a pocket dimension.
    /// </summary>
    public class PlayerEscapingPocketEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerEscapingPocketEvent(ReferenceHub player, bool successful)
        {
            Player = API.Features.Player.Get(player);
            IsCancelled = false;
            Successful = successful;
        }

        /// <summary>
        ///     If the escape was succesful or not.
        /// </summary>
        public bool Successful { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player calling the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
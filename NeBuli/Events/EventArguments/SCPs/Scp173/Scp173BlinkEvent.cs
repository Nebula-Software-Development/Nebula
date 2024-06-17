// -----------------------------------------------------------------------
// <copyright file=Scp173BlinkEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Nebula.Events.EventArguments.Interfaces;
using UnityEngine;

namespace Nebula.Events.EventArguments.SCPs.Scp173
{
    /// <summary>
    ///     Triggered when SCP-173 performs a blink.
    /// </summary>
    public class Scp173BlinkEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp173BlinkEvent(ReferenceHub player, Vector3 position, List<API.Features.Player> blinkers)
        {
            Player = API.Features.Player.Get(player);
            Position = position;
            IsCancelled = false;
            Blinkers = blinkers;
        }

        /// <summary>
        ///     The position 173 will be teleported to.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        ///     Gets the players blinking.
        /// </summary>
        public List<API.Features.Player> Blinkers { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
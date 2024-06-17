// -----------------------------------------------------------------------
// <copyright file=Scp0492BloodlustEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp0492
{
    /// <summary>
    ///     Triggered when SCP-049-2 goes into a bloodlust state.
    /// </summary>
    public class Scp0492BloodlustEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp0492BloodlustEvent(ReferenceHub player, ReferenceHub observer)
        {
            Player = API.Features.Player.Get(player);
            Observer = API.Features.Player.Get(observer);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the observing player.
        /// </summary>
        public API.Features.Player Observer { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player triggering blood lust.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
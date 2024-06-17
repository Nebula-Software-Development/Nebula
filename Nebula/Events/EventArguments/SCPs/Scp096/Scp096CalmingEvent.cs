// -----------------------------------------------------------------------
// <copyright file=Scp096CalmingEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp096
{
    /// <summary>
    ///     Triggered when SCP-096 is calming down from an enraged state.
    /// </summary>
    public class Scp096CalmingEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp096CalmingEvent(ReferenceHub player, bool clearingTime)
        {
            Player = API.Features.Player.Get(player);
            ClearingTime = clearingTime;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the remaining time will be cleared or not.
        /// </summary>
        public bool ClearingTime { get; set; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
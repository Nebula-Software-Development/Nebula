// -----------------------------------------------------------------------
// <copyright file=Scp096EnragingEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;

namespace Nebuli.Events.EventArguments.SCPs.Scp096
{
    /// <summary>
    ///     Triggered when SCP-096 is entering an enraged state.
    /// </summary>
    public class Scp096EnragingEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp096EnragingEvent(ReferenceHub player, float duration)
        {
            Player = API.Features.Player.Get(player);
            Duration = duration;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets the initial duration of the rage.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player enraging.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
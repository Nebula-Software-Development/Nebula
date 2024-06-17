// -----------------------------------------------------------------------
// <copyright file=Scp173ToggleBreakneckSpeedEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp173
{
    /// <summary>
    ///     Triggered when SCP-173 toggles its breakneck speed mode.
    /// </summary>
    public class Scp173ToggleBreakneckSpeedEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp173ToggleBreakneckSpeedEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player toggeling breakneck speed.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
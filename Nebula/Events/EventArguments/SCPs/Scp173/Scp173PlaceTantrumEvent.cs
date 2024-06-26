// -----------------------------------------------------------------------
// <copyright file=Scp173PlaceTantrumEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp173
{
    /// <summary>
    ///     Triggered when SCP-173 attempts to place a tantrum.
    /// </summary>
    public class Scp173PlaceTantrumEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp173PlaceTantrumEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player placing the tantrum.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
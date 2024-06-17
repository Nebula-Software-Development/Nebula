// -----------------------------------------------------------------------
// <copyright file=Scp330InteractEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using InventorySystem.Items.Usables.Scp330;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp330
{
    /// <summary>
    ///     Triggered when a player is interacting with SCP-330.
    /// </summary>
    public class Scp330InteractEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp330InteractEvent(ReferenceHub hub, int uses, CandyKindID candy)
        {
            Player = API.Features.Player.Get(hub);
            Candy = candy;
            IsCancelled = false;
            Uses = uses;
        }

        /// <summary>
        ///     Gets or sets the <see cref="CandyKindID" /> being given.
        /// </summary>
        public CandyKindID Candy { get; set; }

        /// <summary>
        ///     Gets or sets the total amount of times the player has used SCP-330.
        /// </summary>
        public int Uses { get; set; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player interacting with SCP-330.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
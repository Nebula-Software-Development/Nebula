// -----------------------------------------------------------------------
// <copyright file=SpawningItemEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using InventorySystem.Items.Pickups;
using Nebula.API.Features.Items.Pickups;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Server
{
    /// <summary>
    ///     Triggered before a item spawns.
    /// </summary>
    public class SpawningItemEvent : EventArgs, ICancellableEvent
    {
        public SpawningItemEvent(ItemPickupBase pickup)
        {
            Pickup = Pickup.Get(pickup);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the <see cref="API.Features.Items.Pickups.Pickup" /> being spawned.
        /// </summary>
        public Pickup Pickup { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}
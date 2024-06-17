// -----------------------------------------------------------------------
// <copyright file=PlayerPickingUpItemEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using InventorySystem.Items.Pickups;
using Nebula.API.Features.Items.Pickups;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player picks up an item.
    /// </summary>
    public class PlayerPickingUpItemEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerPickingUpItemEvent(ReferenceHub player, ItemPickupBase targetItem)
        {
            Player = API.Features.Player.Get(player);
            Item = Pickup.Get(targetItem);
            IsCancelled = false;
        }

        /// <summary>
        ///     The <see cref="Pickup" /> being picked up.
        /// </summary>
        public Pickup Item { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player picking up the item.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file=PlayerPickingUpAmmoEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.Events.EventArguments.Interfaces;

namespace Nebuli.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player picks up ammo.
    /// </summary>
    public class PlayerPickingUpAmmoEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerPickingUpAmmoEvent(ReferenceHub player, ItemPickupBase ammo)
        {
            Player = API.Features.Player.Get(player);
            Ammo = (AmmoPickup)Pickup.Get(ammo);
            IsCancelled = false;
        }

        /// <summary>
        ///     The <see cref="AmmoPickup" />.
        /// </summary>
        public AmmoPickup Ammo { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player picking up ammo.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
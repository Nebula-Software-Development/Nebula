// -----------------------------------------------------------------------
// <copyright file=PlayerShotEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.API.Features.Items;
using Nebuli.Events.EventArguments.Interfaces;
using Firearm = InventorySystem.Items.Firearms.Firearm;

namespace Nebuli.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player shoots a firearm.
    /// </summary>
    public class PlayerShotEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerShotEventArgs(ReferenceHub player, Firearm firearm)
        {
            Player = API.Features.Player.Get(player);
            Firearm = Item.Get(firearm) as API.Features.Items.Firearm;
            IsCancelled = false;
            Item = Item.Get(firearm);
        }

        /// <summary>
        ///     Gets the <see cref="API.Features.Items.Firearm" /> base.
        /// </summary>
        public API.Features.Items.Firearm Firearm { get; }

        /// <summary>
        ///     Gets the <see cref="API.Features.Items.Item" /> wrapper class for this item.
        /// </summary>
        public Item Item { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the <see cref="API.Features.Player" /> triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
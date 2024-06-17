// -----------------------------------------------------------------------
// <copyright file=PlayerDroppingItemEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using InventorySystem.Items;
using Nebula.API.Features.Items;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player drops an item.
    /// </summary>
    public class PlayerDroppingItemEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerDroppingItemEvent(ReferenceHub player, ItemBase item)
        {
            Player = API.Features.Player.Get(player);
            Item = Item.Get(item);
            IsCancelled = false;
        }

        /// <summary>
        ///     The <see cref="API.Features.Items.Item" /> being dropped.
        /// </summary>
        public Item Item { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player dropping the item.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
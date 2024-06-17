// -----------------------------------------------------------------------
// <copyright file=PlayerChangedItemEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
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
    ///     Triggered when a player changes their current item.
    /// </summary>
    public class PlayerChangedItemEvent : EventArgs, IPlayerEvent
    {
        public PlayerChangedItemEvent(ReferenceHub ply, ItemBase item)
        {
            Player = API.Features.Player.Get(ply);
            NewItem = Player.CurrentItem;
            PreviousItem = Item.Get(item);
        }

        /// <summary>
        ///     Gets the <see cref="Item" /> the player now has, or <c>null</c> if none.
        /// </summary>
        public Item NewItem { get; }

        /// <summary>
        ///     Gets the <see cref="Item" /> being switched from, or <c>null</c> if none.
        /// </summary>
        public Item PreviousItem { get; }

        /// <summary>
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
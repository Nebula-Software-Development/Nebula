// -----------------------------------------------------------------------
// <copyright file=PlayerFlippingCoinEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.API.Features.Enum;
using Nebula.API.Features.Items;
using Nebula.Events.EventArguments.Interfaces;
using Coin = InventorySystem.Items.Coin.Coin;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player flips a coin.
    /// </summary>
    public class PlayerFlippingCoinEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerFlippingCoinEvent(ReferenceHub ply, Coin item, bool tails)
        {
            Player = API.Features.Player.Get(ply);
            Coin = (API.Features.Items.Coin)Item.Get(item);
            Side = tails ? CoinSide.Heads : CoinSide.Tails;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the <see cref="API.Features.Items.Coin" /> being flipped.
        /// </summary>
        public API.Features.Items.Coin Coin { get; }

        /// <summary>
        ///     Gets the <see cref="CoinSide" /> the coin will land on.
        /// </summary>
        public CoinSide Side { get; set; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player flipping the coin.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
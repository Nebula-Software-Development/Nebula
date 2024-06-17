// -----------------------------------------------------------------------
// <copyright file=Coin.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebula.API.Features.Items
{
    public class Coin : Item
    {
        internal Coin(InventorySystem.Items.Coin.Coin itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the coins base.
        /// </summary>
        public new InventorySystem.Items.Coin.Coin Base { get; }
    }
}
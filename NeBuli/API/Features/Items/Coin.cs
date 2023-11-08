// -----------------------------------------------------------------------
// <copyright file=Coin.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebuli.API.Features.Items;

public class Coin : Item
{
    /// <summary>
    /// Gets the coins base.
    /// </summary>
    public new InventorySystem.Items.Coin.Coin Base { get; }

    internal Coin(InventorySystem.Items.Coin.Coin itemBase) : base(itemBase)
    {
        Base = itemBase;
    }
}
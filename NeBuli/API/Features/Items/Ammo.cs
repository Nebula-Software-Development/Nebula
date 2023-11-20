// -----------------------------------------------------------------------
// <copyright file=Ammo.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebuli.API.Features.Items;

public class Ammo : Item
{
    /// <summary>
    /// Gets the <see cref="InventorySystem.Items.Firearms.Ammo.AmmoItem"/> base.
    /// </summary>
    public new InventorySystem.Items.Firearms.Ammo.AmmoItem Base { get; }

    internal Ammo(InventorySystem.Items.Firearms.Ammo.AmmoItem itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets or sets the ammo's unit price.
    /// </summary>
    public int UnitPrice
    {
        get => Base.UnitPrice;
        set => Base.UnitPrice = value;
    }
}
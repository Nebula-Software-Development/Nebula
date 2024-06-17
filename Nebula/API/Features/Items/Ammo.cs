// -----------------------------------------------------------------------
// <copyright file=Ammo.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.Firearms.Ammo;

namespace Nebula.API.Features.Items
{
    public class Ammo : Item
    {
        internal Ammo(AmmoItem itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="InventorySystem.Items.Firearms.Ammo.AmmoItem" /> base.
        /// </summary>
        public new AmmoItem Base { get; }

        /// <summary>
        ///     Gets or sets the ammo's unit price.
        /// </summary>
        public int UnitPrice
        {
            get => Base.UnitPrice;
            set => Base.UnitPrice = value;
        }
    }
}
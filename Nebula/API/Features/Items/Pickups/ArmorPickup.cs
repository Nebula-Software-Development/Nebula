// -----------------------------------------------------------------------
// <copyright file=ArmorPickup.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using BodyArmorPickupBase = InventorySystem.Items.Armor.BodyArmorPickup;

namespace Nebula.API.Features.Items.Pickups
{
    public class ArmorPickup : Pickup
    {
        internal ArmorPickup(BodyArmorPickupBase pickupBase) : base(pickupBase)
        {
            Base = pickupBase;
        }

        /// <summary>
        ///     Gets the <see cref="BodyArmorPickupBase" /> base.
        /// </summary>
        public new BodyArmorPickupBase Base { get; }
    }
}
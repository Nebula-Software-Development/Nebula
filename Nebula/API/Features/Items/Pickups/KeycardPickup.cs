// -----------------------------------------------------------------------
// <copyright file=KeycardPickup.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Keycards;
using Nebula.API.Extensions;

namespace Nebula.API.Features.Items.Pickups
{
    public class KeycardPickup : Pickup
    {
        internal KeycardPickup(InventorySystem.Items.Keycards.KeycardPickup pickupBase) : base(pickupBase)
        {
            Base = pickupBase;
            if (ItemType.GetItemBase() is KeycardItem item)
            {
                KeycardPermissions = item.Permissions;
            }
        }

        /// <summary>
        ///     Gets the base of the pickup.
        /// </summary>
        public new InventorySystem.Items.Keycards.KeycardPickup Base { get; }

        /// <summary>
        ///     Gets the <see cref="KeycardPickup" /> <see cref="Interactables.Interobjects.DoorUtils.KeycardPermissions" />.
        /// </summary>
        public KeycardPermissions KeycardPermissions { get; } = KeycardPermissions.None;
    }
}
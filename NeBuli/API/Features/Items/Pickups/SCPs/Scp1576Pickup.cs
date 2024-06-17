// -----------------------------------------------------------------------
// <copyright file=Scp1576Pickup.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using ItemBase = InventorySystem.Items.Usables.Scp1576.Scp1576Pickup;

namespace Nebula.API.Features.Items.Pickups.SCPs
{
    public class Scp1576Pickup : Pickup
    {
        internal Scp1576Pickup(ItemBase itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="ItemBase" /> base.
        /// </summary>
        public new ItemBase Base { get; }
    }
}
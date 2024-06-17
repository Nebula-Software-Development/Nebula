// -----------------------------------------------------------------------
// <copyright file=Scp244Pickup.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Scp244Base = InventorySystem.Items.Usables.Scp244.Scp244DeployablePickup;

namespace Nebula.API.Features.Items.Pickups.SCPs
{
    public class Scp244Pickup : Pickup
    {
        internal Scp244Pickup(Scp244Base pickupBase) : base(pickupBase)
        {
            Base = pickupBase;
        }

        /// <summary>
        ///     Gets the <see cref="Scp244Base" /> base.
        /// </summary>
        public new Scp244Base Base { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file=JailbirdPickup.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.Jailbird;
using JailbirdPickupBase = InventorySystem.Items.Jailbird.JailbirdPickup;

namespace Nebula.API.Features.Items.Pickups.SCPs
{
    public class JailbirdPickup : Pickup
    {
        internal JailbirdPickup(JailbirdPickupBase pickupBase) : base(pickupBase)
        {
            Base = pickupBase;
        }

        /// <summary>
        ///     Gets the <see cref="JailbirdPickupBase" /> base.
        /// </summary>
        public new JailbirdPickupBase Base { get; }

        /// <summary>
        ///     Get or sets the JailBirds <see cref="JailbirdWearState" />.
        /// </summary>
        public JailbirdWearState WearState
        {
            get => Base.Wear;
            set => Base.Wear = value;
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file=FirearmPickup.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.Firearms;
using InventorySystem.Items.Pickups;
using FirearmPickupBase = InventorySystem.Items.Firearms.FirearmPickup;

namespace Nebuli.API.Features.Items.Pickups
{
    /// <summary>
    ///     Represents a wrapper class for FirearmPickupBase.
    /// </summary>
    public class FirearmPickup : Pickup
    {
        /// <summary>
        ///     Gets the FirearmPickup base.
        /// </summary>
        public new FirearmPickupBase Base;

        internal FirearmPickup(FirearmPickupBase pickupBase) : base(pickupBase)
        {
            Base = pickupBase;
        }

        /// <summary>
        ///     Gets or sets the PickupSyncInfo of the FirearmPickup.
        /// </summary>
        public new PickupSyncInfo Info
        {
            get => Base.Info;
            set => Base.Info = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the FirearmPickup is distributed or not.
        /// </summary>
        public bool Distributed
        {
            get => Base.Distributed;
            set => Base.Distributed = value;
        }

        /// <summary>
        ///     Gets or sets the FirearmStatus of the FirearmPickup.
        /// </summary>
        public FirearmStatus FirearmStatus
        {
            get => Base.Status;
            set => Base.Status = value;
        }

        /// <summary>
        ///     Gets or sets the firearms status flags.
        /// </summary>
        public FirearmStatusFlags Flags
        {
            get => Base.NetworkStatus.Flags;
            set => Base.NetworkStatus =
                new FirearmStatus(Base.NetworkStatus.Ammo, value, Base.NetworkStatus.Attachments);
        }

        /// <summary>
        ///     Gets or sets the amount of ammo for the FirearmPickup.
        /// </summary>
        public byte Ammo
        {
            get => Base.NetworkStatus.Ammo;
            set => Base.NetworkStatus =
                new FirearmStatus(value, Base.NetworkStatus.Flags, Base.NetworkStatus.Attachments);
        }

        /// <summary>
        ///     Gets or sets the firearms attachments.
        /// </summary>
        public uint Attachments
        {
            get => Base.NetworkStatus.Attachments;
            set => Base.NetworkStatus = new FirearmStatus(Base.NetworkStatus.Ammo, Flags, value);
        }
    }
}
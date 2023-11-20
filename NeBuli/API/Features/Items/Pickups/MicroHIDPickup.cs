// -----------------------------------------------------------------------
// <copyright file=MicroHIDPickup.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using MicroHIDPickupBase = InventorySystem.Items.MicroHID.MicroHIDPickup;

namespace Nebuli.API.Features.Items.Pickups;

public class MicroHIDPickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="MicroHIDPickupBase"/> base.
    /// </summary>
    public new MicroHIDPickupBase Base { get; }

    internal MicroHIDPickup(MicroHIDPickupBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets or sets the Mirco-HID's current energy level.
    /// </summary>
    public float EnergyLevel
    {
        get => Base.NetworkEnergy;
        set => Base.NetworkEnergy = value;
    }
}
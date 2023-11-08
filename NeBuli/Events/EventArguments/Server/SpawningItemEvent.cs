// -----------------------------------------------------------------------
// <copyright file=SpawningItemEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Server;

/// <summary>
/// Triggered before a item spawns.
/// </summary>
public class SpawningItemEvent : EventArgs, ICancellableEvent
{
    public SpawningItemEvent(ItemPickupBase pickup)
    {
        Pickup = Pickup.Get(pickup);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the <see cref="API.Features.Items.Pickups.Pickup"/> being spawned.
    /// </summary>
    public Pickup Pickup { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
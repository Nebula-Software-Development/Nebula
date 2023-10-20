﻿using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player picks up an item.
/// </summary>
public class PlayerPickingUpItemEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpItemEvent(ReferenceHub player, ItemPickupBase targetItem)
    {
        Player = NebuliPlayer.Get(player);
        Item = Pickup.Get(targetItem);
        IsCancelled = false;
    }

    /// <summary>
    /// The player picking up the item.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The <see cref="Pickup"/> being picked up.
    /// </summary>
    public Pickup Item { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
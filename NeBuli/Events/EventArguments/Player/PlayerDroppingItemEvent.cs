using InventorySystem.Items;
using Nebuli.API.Features.Items;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player drops an item.
/// </summary>
public class PlayerDroppingItemEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerDroppingItemEvent(ReferenceHub player, ItemBase item)
    {
        Player = NebuliPlayer.Get(player);
        Item = Item.Get(item);
        IsCancelled = false;
    }

    /// <summary>
    /// The player dropping the item.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The <see cref="API.Features.Items.Item"/> being dropped.
    /// </summary>
    public Item Item { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
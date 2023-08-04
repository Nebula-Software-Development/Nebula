using InventorySystem.Items;
using Nebuli.API.Features.Items;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerDroppingItem : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerDroppingItem(ReferenceHub player, ItemBase item)
    {
        Player = NebuliPlayer.Get(player);
        Item = Item.ItemGet(item);
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

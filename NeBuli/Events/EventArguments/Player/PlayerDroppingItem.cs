using InventorySystem.Items;
using Nebuli.API.Features.Items;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerDroppingItem : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerDroppingItem(ItemBase item, ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        Item = Item.ItemGet(item);
        IsCancelled = false;
    }

    public NebuliPlayer Player { get; }

    public Item Item { get; }

    public bool IsCancelled { get; set; }
}

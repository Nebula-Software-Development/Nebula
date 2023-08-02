using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerPickingUpItem : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpItem(ReferenceHub player, ItemPickupBase targetItem)
    {
        Player = NebuliPlayer.Get(player);
        Item = Pickup.GetPickup(targetItem);
        IsCancelled = false;
    }

    public NebuliPlayer Player { get; }

    public Pickup Item { get; }

    public bool IsCancelled { get; set; }
}

using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerPickingUpItemEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpItemEvent(ReferenceHub player, ItemPickupBase targetItem)
    {
        Player = API.Features.Player.Player.Get(player);
        Item = Pickup.GetPickup(targetItem);
        IsCancelled = false;
    }

    /// <summary>
    /// The player picking up the item.
    /// </summary>
    public API.Features.Player.Player Player { get; }

    /// <summary>
    /// The <see cref="Pickup"/> being picked up.
    /// </summary>
    public Pickup Item { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}

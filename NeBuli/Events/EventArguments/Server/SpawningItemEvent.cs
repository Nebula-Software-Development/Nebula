using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using System;

namespace Nebuli.Events.EventArguments.Server;

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

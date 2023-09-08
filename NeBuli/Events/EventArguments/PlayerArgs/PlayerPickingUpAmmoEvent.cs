using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerPickingUpAmmoEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpAmmoEvent(ReferenceHub player, ItemPickupBase ammo)
    {
        Player = API.Features.Player.Player.Get(player);
        Ammo = (AmmoPickup)Pickup.Get(ammo);
        IsCancelled = false;
    }

    /// <summary>
    /// The player picking up ammo.
    /// </summary>
    public API.Features.Player.Player Player { get; }

    /// <summary>
    /// The <see cref="AmmoPickup"/>.
    /// </summary>
    public AmmoPickup Ammo { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}

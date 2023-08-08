using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerPickingUpAmmo : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpAmmo(ReferenceHub player, InventorySystem.Items.Firearms.Ammo.AmmoPickup ammo)
    {
        Player = NebuliPlayer.Get(player);
        Ammo = (AmmoPickup)Pickup.Get(ammo);
        IsCancelled = false;
    }

    /// <summary>
    /// The player picking up ammo.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The <see cref="AmmoPickup"/>.
    /// </summary>
    public AmmoPickup Ammo { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}

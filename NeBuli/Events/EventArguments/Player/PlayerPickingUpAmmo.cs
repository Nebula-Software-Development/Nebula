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

    public NebuliPlayer Player { get; }

    public AmmoPickup Ammo { get; }

    public bool IsCancelled { get; set; }
}

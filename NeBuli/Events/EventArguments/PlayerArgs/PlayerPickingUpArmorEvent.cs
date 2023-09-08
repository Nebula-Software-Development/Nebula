using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerPickingUpArmorEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpArmorEvent(ReferenceHub player, ItemPickupBase armor)
    {
        Player = API.Features.Player.Player.Get(player);
        Armor = (ArmorPickup)Pickup.Get(armor);
        IsCancelled = false;
    }

    public API.Features.Player.Player Player { get; }

    public ArmorPickup Armor { get; }

    public bool IsCancelled { get; set; }
}

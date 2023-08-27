using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerPickingUpArmorEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpArmorEvent(ReferenceHub player, ItemPickupBase armor)
    {
        Player = NebuliPlayer.Get(player);
        Armor = (ArmorPickup)Pickup.Get(armor);
        IsCancelled = false;
    }

    public NebuliPlayer Player { get; }

    public ArmorPickup Armor { get; }

    public bool IsCancelled { get; set; }
}

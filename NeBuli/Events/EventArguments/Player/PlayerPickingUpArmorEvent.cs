using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player picks up armor.
/// </summary>
public class PlayerPickingUpArmorEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerPickingUpArmorEvent(ReferenceHub player, ItemPickupBase armor)
    {
        Player = NebuliPlayer.Get(player);
        Armor = (ArmorPickup)Pickup.Get(armor);
        IsCancelled = false;
    }

    public NebuliPlayer Player { get; }

    /// <summary>
    /// The <see cref="ArmorPickup"/> being picked up.
    /// </summary>
    public ArmorPickup Armor { get; }

    public bool IsCancelled { get; set; }
}
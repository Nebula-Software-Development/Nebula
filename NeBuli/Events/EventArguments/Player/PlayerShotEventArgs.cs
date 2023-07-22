using InventorySystem.Items.Firearms;
using Nebuli.API.Features.Item;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerShotEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerShotEventArgs(ReferenceHub player, Firearm firearm)
    {
        Player = NebuliPlayer.Get(player);
        Firearm = firearm;
        IsCancelled = false;
        Item = Item.ItemGet(firearm);
    }

    /// <summary>
    /// Gets the <see cref="InventorySystem.Items.Firearms.Firearm"/> base.
    /// </summary>
    public Firearm Firearm { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the <see cref="NebuliPlayer"/> triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Item.Item"/> wrapper class for this item.
    /// </summary>
    public Item Item { get; }
}
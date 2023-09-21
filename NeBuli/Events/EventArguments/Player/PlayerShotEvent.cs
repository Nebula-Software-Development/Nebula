using Nebuli.API.Features.Items;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerShotEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerShotEventArgs(ReferenceHub player, InventorySystem.Items.Firearms.Firearm firearm)
    {
        Player = NebuliPlayer.Get(player);
        Firearm = Item.Get(firearm) as Firearm;
        IsCancelled = false;
        Item = Item.Get(firearm);
    }

    /// <summary>
    /// Gets the <see cref="API.Features.Items.Firearm"/> base.
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
    /// Gets the <see cref="API.Features.Items.Item"/> wrapper class for this item.
    /// </summary>
    public Item Item { get; }
}
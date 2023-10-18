using InventorySystem.Items;
using Nebuli.API.Features.Items;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player changes their current item.
/// </summary>
public class PlayerChangedItemEvent : EventArgs, IPlayerEvent
{
    public PlayerChangedItemEvent(ReferenceHub ply, ItemBase item)
    {
        Player = NebuliPlayer.Get(ply);
        NewItem = Player.CurrentItem;
        PreviousItem = Item.Get(item);
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="Item"/> the player now has, or <c>null</c> if none.
    /// </summary>
    public Item NewItem { get; }

    /// <summary>
    /// Gets the <see cref="Item"/> being switched from, or <c>null</c> if none.
    /// </summary>
    public Item PreviousItem { get; }
}
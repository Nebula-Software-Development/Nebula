using Nebuli.API.Features.Enum;
using Nebuli.API.Features.Items;
using Nebuli.API.Features.Player;
using System;
using Coin = InventorySystem.Items.Coin.Coin;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerFlippingCoinEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerFlippingCoinEvent(ReferenceHub ply, Coin item, bool tails)
    {
        Player = NebuliPlayer.Get(ply);
        Coin = (API.Features.Items.Coin)Item.Get(item);
        Side = tails ? CoinSide.Heads : CoinSide.Tails;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player flipping the coin.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Items.Coin"/> being flipped.
    /// </summary>
    public API.Features.Items.Coin Coin { get; }

    /// <summary>
    /// Gets the <see cref="CoinSide"/> the coin will land on.
    /// </summary>
    public CoinSide Side { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}

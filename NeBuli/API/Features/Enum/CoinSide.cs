using InventorySystem.Items.Coin;
using System;

namespace Nebuli.API.Features.Enum;

/// <summary>
/// Represents different sides of the <see cref="Coin"/>.
/// </summary>
[Flags]
public enum CoinSide
{
    /// <summary>
    /// The head side.
    /// </summary>
    Heads,

    /// <summary>
    /// The tails side.
    /// </summary>
    Tails,
}

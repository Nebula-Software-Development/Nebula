// -----------------------------------------------------------------------
// <copyright file=CoinSide.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using InventorySystem.Items.Coin;

namespace Nebuli.API.Features.Enum
{
    /// <summary>
    ///     Represents different sides of the <see cref="Coin" />.
    /// </summary>
    [Flags]
    public enum CoinSide
    {
        /// <summary>
        ///     The head side.
        /// </summary>
        Heads,

        /// <summary>
        ///     The tails side.
        /// </summary>
        Tails
    }
}
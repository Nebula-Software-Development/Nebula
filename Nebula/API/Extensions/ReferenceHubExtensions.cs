﻿// -----------------------------------------------------------------------
// <copyright file=ReferenceHubExtensions.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebula.API.Features;
using Nebula.API.Features.Enum;

namespace Nebula.API.Extensions
{
    /// <summary>
    ///     Extension methods for <see cref="ReferenceHub" />.
    /// </summary>
    public static class ReferenceHubExtensions
    {
        /// <summary>
        ///     Converts a <see cref="ReferenceHub" /> into a <see cref="Player" />.
        /// </summary>
        /// <param name="referenceHub"></param>
        /// <returns></returns>
        public static Player ToNebulaPlayer(this ReferenceHub referenceHub)
        {
            return referenceHub == null ? null : Player.Get(referenceHub);
        }

        /// <summary>
        ///     Converts a <see cref="RankColorType" /> enum value to its string representation.
        /// </summary>
        /// <param name="rankColorType">The RankColorType enum value to convert.</param>
        /// <returns>The string representation of the enum value.</returns>
        public static string ToColorString(this RankColorType rankColorType)
        {
            return rankColorType switch
            {
                RankColorType.Default => "default",
                RankColorType.Pink => "pink",
                RankColorType.Red => "red",
                RankColorType.Brown => "brown",
                RankColorType.Silver => "silver",
                RankColorType.LightGreen => "light_green",
                RankColorType.Crimson => "crimson",
                RankColorType.Cyan => "cyan",
                RankColorType.Aqua => "aqua",
                RankColorType.DeepPink => "deep_pink",
                RankColorType.Tomato => "tomato",
                RankColorType.Yellow => "yellow",
                RankColorType.Magenta => "magenta",
                RankColorType.BlueGreen => "blue_green",
                RankColorType.Orange => "orange",
                RankColorType.Lime => "lime",
                RankColorType.Green => "green",
                RankColorType.Emerald => "emerald",
                RankColorType.Carmine => "carmine",
                RankColorType.Nickel => "nickel",
                RankColorType.Mint => "mint",
                RankColorType.ArmyGreen => "army_green",
                RankColorType.Pumpkin => "pumpkin",
                _ => "default"
            };
        }

        /// <summary>
        ///     Converts a string color representation to its <see cref="RankColorType" /> enum value.
        /// </summary>
        /// <param name="colorString">The string representation of the color.</param>
        /// <returns>The corresponding RankColorType enum value, or RankColorType.Default if not found.</returns>
        public static RankColorType ToRankColorType(this string colorString)
        {
            return colorString switch
            {
                "default" => RankColorType.Default,
                "pink" => RankColorType.Pink,
                "red" => RankColorType.Red,
                "brown" => RankColorType.Brown,
                "silver" => RankColorType.Silver,
                "light_green" => RankColorType.LightGreen,
                "crimson" => RankColorType.Crimson,
                "cyan" => RankColorType.Cyan,
                "aqua" => RankColorType.Aqua,
                "deep_pink" => RankColorType.DeepPink,
                "tomato" => RankColorType.Tomato,
                "yellow" => RankColorType.Yellow,
                "magenta" => RankColorType.Magenta,
                "blue_green" => RankColorType.BlueGreen,
                "orange" => RankColorType.Orange,
                "lime" => RankColorType.Lime,
                "green" => RankColorType.Green,
                "emerald" => RankColorType.Emerald,
                "carmine" => RankColorType.Carmine,
                "nickel" => RankColorType.Nickel,
                "mint" => RankColorType.Mint,
                "army_green" => RankColorType.ArmyGreen,
                "pumpkin" => RankColorType.Pumpkin,
                _ => RankColorType.Default
            };
        }
    }
}
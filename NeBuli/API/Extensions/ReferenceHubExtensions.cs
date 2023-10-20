﻿using Nebuli.API.Features.Enum;
using Nebuli.API.Features.Player;
using System;
using UnityEngine;

namespace Nebuli.API.Extensions;

/// <summary>
/// Extension methods for <see cref="ReferenceHub"/>.
/// </summary>
public static class ReferenceHubExtensions
{
    /// <summary>
    /// Converts a <see cref="ReferenceHub"/> into a <see cref="NebuliPlayer"/>.
    /// </summary>
    /// <param name="referenceHub"></param>
    /// <returns></returns>
    public static NebuliPlayer ToNebuliPlayer(this ReferenceHub referenceHub) => referenceHub == null ? null : NebuliPlayer.Get(referenceHub);

    /// <summary>
    /// Converts a <see cref="RankColorType"/> enum value to its string representation.
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
            _ => "default",
        };
    }

    /// <summary>
    /// Converts a string color representation to its <see cref="RankColorType"/> enum value.
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
            _ => RankColorType.Default,
        };
    }

    //Credit to o5zereth for code below.
    /// <summary>
    /// Converts a quaternion rotation into a pair of unsigned shorts (horizontal, vertical) for client representation.
    /// </summary>
    /// <param name="rotation">The quaternion rotation to convert.</param>
    /// <returns>A tuple containing the horizontal and vertical angles as unsigned shorts.</returns>
    public static (ushort horizontal, ushort vertical) ToClientUShorts(this Quaternion rotation)
    {
        const float ToHorizontal = ushort.MaxValue / 360f;
        const float ToVertical = ushort.MaxValue / 176f;

        float fixVertical = -rotation.eulerAngles.x;

        if (fixVertical < -90f)
        {
            fixVertical += 360f;
        }
        else if (fixVertical > 270f)
        {
            fixVertical -= 360f;
        }

        float horizontal = Mathf.Clamp(rotation.eulerAngles.y, 0f, 360f);
        float vertical = Mathf.Clamp(fixVertical, -88f, 88f) + 88f;

        return ((ushort)Math.Round(horizontal * ToHorizontal), (ushort)Math.Round(vertical * ToVertical));
    }
}
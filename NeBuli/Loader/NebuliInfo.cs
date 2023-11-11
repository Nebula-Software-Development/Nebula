// -----------------------------------------------------------------------
// <copyright file=NebuliInfo.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;

namespace Nebuli.Loader;

/// <summary>
/// Class for Nebuli information, like version.
/// </summary>
public static class NebuliInfo
{
    internal const string NebuliVersionConst = "1.3.4";

    /// <summary>
    /// Gets a <see cref="Version"/> representing Nebuli's current build version.
    /// </summary>
    public static Version NebuliVersion { get; } = new(NebuliVersionConst);
}
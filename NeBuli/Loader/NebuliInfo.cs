// -----------------------------------------------------------------------
// <copyright file=NebulaInfo.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;

namespace Nebula.Loader
{
    /// <summary>
    ///     Class for Nebula information, like version.
    /// </summary>
    public static class NebulaInfo
    {
        internal const string NebulaVersionConst = "1.3.6";

        /// <summary>
        ///     Gets a <see cref="Version" /> representing Nebula's current build version.
        /// </summary>
        public static Version NebulaVersion { get; } = new(NebulaVersionConst);
    }
}
// -----------------------------------------------------------------------
// <copyright file=AmmoType.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebula.API.Features.Enum
{
    /// <summary>
    ///     Represents different types of ammunition.
    /// </summary>
    public enum AmmoType
    {
        /// <summary>
        ///     No specific ammo type.
        /// </summary>
        None = ItemType.None,

        /// <summary>
        ///     9x19mm NATO ammunition.
        /// </summary>
        NATO9 = ItemType.Ammo9x19,

        /// <summary>
        ///     5.56x45mm NATO ammunition.
        /// </summary>
        NATO556 = ItemType.Ammo556x45,

        /// <summary>
        ///     7.62x39mm NATO ammunition.
        /// </summary>
        NATO762 = ItemType.Ammo762x39,

        /// <summary>
        ///     12-gauge shotgun ammunition.
        /// </summary>
        Ammo12Gauge = ItemType.Ammo12gauge,

        /// <summary>
        ///     .44 caliber ammunition.
        /// </summary>
        Ammo44Caliber = ItemType.Ammo44cal
    }
}
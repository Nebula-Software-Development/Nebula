// -----------------------------------------------------------------------
// <copyright file=Scp079PingType.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebula.API.Features.Enum
{
    /// <summary>
    ///     Enumerates the types of pings that SCP-079 can generate.
    /// </summary>
    public enum Scp079PingType : byte
    {
        /// <summary>
        ///     Represents a ping indicating a generator.
        /// </summary>
        Generator,

        /// <summary>
        ///     Represents a ping indicating a projectile.
        /// </summary>
        Projectile,

        /// <summary>
        ///     Represents a ping indicating a Micro-HID.
        /// </summary>
        MicroHid,

        /// <summary>
        ///     Represents a ping indicating a human.
        /// </summary>
        Human,

        /// <summary>
        ///     Represents a ping indicating an elevator.
        /// </summary>
        Elevator,

        /// <summary>
        ///     Represents a ping indicating a door.
        /// </summary>
        Door,

        /// <summary>
        ///     Represents a general ping.
        /// </summary>
        Default
    }
}
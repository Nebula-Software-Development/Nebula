// -----------------------------------------------------------------------
// <copyright file=DoorLockingType.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;

namespace Nebuli.API.Features.Enum
{
    /// <summary>
    ///     Enumerates the types of locks that can be applied to doors.
    /// </summary>
    [Flags]
    public enum DoorLockingType
    {
        /// <summary>
        ///     No lock applied.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Regular lock controlled by SCP-079.
        /// </summary>
        RegularSCP079 = 1,

        /// <summary>
        ///     Lockdown controlled by SCP-079.
        /// </summary>
        LockdownSCP079 = 2,

        /// <summary>
        ///     Lock applied during alpha warhead detonation.
        /// </summary>
        WarheadDetonation = 4,

        /// <summary>
        ///     Lock applied via administrative command.
        /// </summary>
        AdminCommand = 8,

        /// <summary>
        ///     Lock applied during decontamination lockdown.
        /// </summary>
        DecontaminationLockdown = 16,

        /// <summary>
        ///     Lock applied during evacuation for decontamination.
        /// </summary>
        DecontaminationEvacuation = 32,

        /// <summary>
        ///     Lock for special door features.
        /// </summary>
        SpecialFeature = 64,

        /// <summary>
        ///     Lock applied when the door has no power.
        /// </summary>
        NoPower = 128,

        /// <summary>
        ///     Isolation lock for containment.
        /// </summary>
        Isolation = 256,

        /// <summary>
        ///     Lock applied by SCP-2176.
        /// </summary>
        LockdownSCP2176 = 512
    }
}
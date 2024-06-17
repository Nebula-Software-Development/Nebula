// -----------------------------------------------------------------------
// <copyright file=EscapeType.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebula.API.Features.Enum
{
    /// <summary>
    ///     Represents different types of possible escapes in-game.
    /// </summary>
    public enum EscapeType
    {
        /// <summary>
        ///     None.
        /// </summary>
        None,

        /// <summary>
        ///     Class-D escape type.
        /// </summary>
        ClassD,

        /// <summary>
        ///     Cuffed Class-D escape type.
        /// </summary>
        CuffedClassD,

        /// <summary>
        ///     Scientist escape type.
        /// </summary>
        Scientist,

        /// <summary>
        ///     Cuffed scientist escape type.
        /// </summary>
        CuffedScientist,

        /// <summary>
        ///     A custom/plugin escape type.
        /// </summary>
        PluginEscape
    }
}
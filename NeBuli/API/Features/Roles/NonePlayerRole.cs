// -----------------------------------------------------------------------
// <copyright file=NonePlayerRole.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles;

namespace Nebula.API.Features.Roles
{
    /// <summary>
    ///     Represents the <see cref="RoleTypeId.None" /> role in-game.
    /// </summary>
    public class NonePlayerRole : Role
    {
        internal NonePlayerRole(NoneRole roleBase) : base(roleBase)
        {
            Base = roleBase;
        }

        /// <summary>
        ///     Gets the <see cref="NoneRole" /> base.
        /// </summary>
        public new NoneRole Base { get; }
    }
}
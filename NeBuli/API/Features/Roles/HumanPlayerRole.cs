// -----------------------------------------------------------------------
// <copyright file=HumanPlayerRole.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles;
using Respawning;

namespace Nebuli.API.Features.Roles
{
    /// <summary>
    ///     Represents any human role in-game.
    /// </summary>
    public class HumanPlayerRole : FpcRoleBase
    {
        internal HumanPlayerRole(HumanRole role) : base(role)
        {
            Base = role;
        }

        /// <summary>
        ///     Gets the <see cref="HumanRole" /> base.
        /// </summary>
        public new HumanRole Base { get; }

        /// <summary>
        ///     Gets the roles assigned <see cref="SpawnableTeamType" />.
        /// </summary>
        public SpawnableTeamType AssingedSpawnAbleTeam => Base.AssignedSpawnableTeam;

        /// <summary>
        ///     Gets if the role uses unit names.
        /// </summary>
        public bool UsesUnitName => Base.UsesUnitNames;
    }
}
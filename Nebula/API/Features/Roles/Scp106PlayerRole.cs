﻿// -----------------------------------------------------------------------
// <copyright file=Scp106PlayerRole.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles;
using PlayerRoles.PlayableScps.Scp106;

namespace Nebula.API.Features.Roles
{
    /// <summary>
    ///     Represents the <see cref="RoleTypeId.Scp106" /> role in-game.
    /// </summary>
    public class Scp106PlayerRole : Role
    {
        internal Scp106PlayerRole(Scp106Role scpRole) : base(scpRole)
        {
            Base = scpRole;
        }

        /// <summary>
        ///     Gets the <see cref="Scp106Role" /> base.
        /// </summary>
        public new Scp106Role Base { get; }

        /// <summary>
        ///     Gets if SCP-106 is submerged.
        /// </summary>
        public bool IsSubmerged => Base.IsSubmerged;

        /// <summary>
        ///     Gets if SCP-106 can trigger a tesla gate shock.
        /// </summary>
        public bool CanActivateTeslaShock => Base.CanActivateShock;
    }
}
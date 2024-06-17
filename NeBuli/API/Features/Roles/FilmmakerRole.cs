// -----------------------------------------------------------------------
// <copyright file=FilmmakerRole.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles;
using UnityEngine;
using FilmmakerRoleBase = PlayerRoles.Filmmaker.FilmmakerRole;

namespace Nebula.API.Features.Roles
{
    /// <summary>
    ///     Represents the <see cref="RoleTypeId.Filmmaker" /> role in-game.
    /// </summary>
    public class FilmmakerRole : Role
    {
        internal FilmmakerRole(FilmmakerRoleBase role) : base(role)
        {
            Base = role;
        }

        /// <summary>
        ///     Gets the <see cref="FilmmakerRoleBase" /> base.
        /// </summary>
        public new FilmmakerRoleBase Base { get; }

        /// <summary>
        ///     Gets the <see cref="FilmmakerRoleBase" /> custom role name.
        /// </summary>
        public string CustomRoleName => Base.CustomRoleName;

        /// <summary>
        ///     Gets the <see cref="FilmmakerRoleBase" /> camera position.
        /// </summary>
        public Vector3 CameraPosition
        {
            get => Base.CameraPosition;
            set => Base.CameraPosition = value;
        }

        /// <summary>
        ///     Gets the <see cref="FilmmakerRoleBase" /> camera rotation.
        /// </summary>
        public Quaternion CameraRotation
        {
            get => Base.CameraRotation;
            set => Base.CameraRotation = value;
        }
    }
}
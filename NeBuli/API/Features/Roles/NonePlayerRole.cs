// -----------------------------------------------------------------------
// <copyright file=NonePlayerRole.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles;

namespace Nebuli.API.Features.Roles;

/// <summary>
/// Represents the <see cref="RoleTypeId.None"/> role in-game.
/// </summary>
public class NonePlayerRole : Role
{
    /// <summary>
    /// Gets the <see cref="NoneRole"/> base.
    /// </summary>
    public new NoneRole Base { get; }
    internal NonePlayerRole(NoneRole roleBase) : base(roleBase) => Base = roleBase;
}

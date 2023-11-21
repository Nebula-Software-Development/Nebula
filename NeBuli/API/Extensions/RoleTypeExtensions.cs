// -----------------------------------------------------------------------
// <copyright file=RoleTypeExtensions.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem;
using InventorySystem.Configs;
using Nebuli.API.Features.Roles;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using UnityEngine;

namespace Nebuli.API.Extensions
{
    /// <summary>
    ///     Extension for managing <see cref="Role" /> and <see cref="RoleTypeId" /> easier.
    /// </summary>
    public static class RoleTypeExtensions
    {
        /// <summary>
        ///     Unsafely casts a role to a specific type.
        /// </summary>
        /// <typeparam name="T">The type to cast the role to.</typeparam>
        /// <param name="role">The role to cast.</param>
        /// <returns>The role cast to the specified type, or null if the cast is not possible.</returns>
        public static T ConvertTo<T>(this Role role) where T : Role
        {
            return role as T;
        }

        /// <summary>
        ///     Safely attempts to cast a role to a specific type.
        /// </summary>
        /// <typeparam name="T">The type to cast the role to.</typeparam>
        /// <param name="role">The role to cast.</param>
        /// <param name="castedRole">The role cast to the specified type, if the cast is successful.</param>
        /// <returns>True if the cast is successful, false otherwise.</returns>
        public static bool TryCastTo<T>(this Role role, out T castedRole) where T : Role
        {
            castedRole = role as T;
            return castedRole != null;
        }

        /// <summary>
        ///     Gets if the <see cref="RoleTypeId" /> is any SCP role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool IsSCP(this RoleTypeId role)
        {
            return role.GetFaction() == Faction.SCP;
        }

        /// <summary>
        ///     Gets if the <see cref="RoleTypeId" /> is apart of <see cref="Faction.FoundationStaff" />.
        /// </summary>
        public static bool IsFoundationStaff(this RoleTypeId role)
        {
            return role.GetFaction() == Faction.FoundationStaff;
        }

        /// <summary>
        ///     Checks if the specified <see cref="RoleTypeId" /> is a member of the Mobile Task Force (MTF).
        /// </summary>
        /// <param name="role">The <see cref="RoleTypeId" />  to check.</param>
        /// <param name="includeGuards">Set to true to include guards as MTF members, false to exclude them.</param>
        /// <returns>True if the <see cref="RoleTypeId" />  is part of the MTF, false otherwise.</returns>
        public static bool IsMTF(this RoleTypeId role, bool includeGuards = true)
        {
            if (includeGuards)
            {
                return role.GetTeam() == Team.FoundationForces;
            }

            return role != RoleTypeId.FacilityGuard && role.GetTeam() == Team.FoundationForces;
        }

        /// <summary>
        ///     Checks if the <see cref="RoleTypeId" />  is a member of the Chaos Insurgency (CI).
        /// </summary>
        /// <param name="role">The <see cref="RoleTypeId" />  to check.</param>
        /// <returns>True if the <see cref="RoleTypeId" />  is part of the Chaos Insurgency, false otherwise.</returns>
        public static bool IsCI(this RoleTypeId role)
        {
            return role.GetTeam() == Team.ChaosInsurgency;
        }

        /// <summary>
        ///     Gets if the <see cref="RoleTypeId" /> is apart of <see cref="Faction.FoundationEnemy" />.
        /// </summary>
        public static bool IsFoundationEnemy(this RoleTypeId role)
        {
            return role.GetFaction() == Faction.FoundationEnemy;
        }

        /// <summary>
        ///     Gets if the <see cref="RoleTypeId" /> is apart of <see cref="Faction.Unclassified" />.
        /// </summary>
        public static bool IsUnclassified(this RoleTypeId role)
        {
            return role.GetFaction() == Faction.Unclassified;
        }

        /// <summary>
        ///     Gets the <see cref="RoleTypeId" /> full name.
        /// </summary>
        public static string FullRoleName(this RoleTypeId role)
        {
            return role.GetBaseRole().RoleName;
        }

        /// <summary>
        ///     Gets the roles <see cref="PlayerRoleBase" />.
        /// </summary>
        public static PlayerRoleBase GetBaseRole(this RoleTypeId role)
        {
            return TryGetBaseRole(role, out PlayerRoleBase playerRole) ? playerRole : null;
        }

        /// <summary>
        ///     Tries to get a <see cref="PlayerRoleBase" /> from the specified <see cref="RoleTypeId" />.
        /// </summary>
        public static bool TryGetBaseRole(this RoleTypeId role, out PlayerRoleBase playerRoleBase)
        {
            return PlayerRoleLoader.TryGetRoleTemplate(role, out playerRoleBase);
        }

        /// <summary>
        ///     Tries to get a <see cref="PlayerRoleBase" /> from the specified <see cref="Role" />.
        /// </summary>
        public static bool TryGetBaseRole(this Role role, out PlayerRoleBase playerRoleBase)
        {
            return TryGetBaseRole(role.RoleTypeId, out playerRoleBase);
        }

        /// <summary>
        ///     Gets a random spawn position for the specified role type.
        /// </summary>
        /// <param name="roleType">The <see cref="RoleTypeId" /> to use.</param>
        /// <returns>
        ///     A random spawn position as a <see cref="Vector3" /> or <see cref="Vector3.zero" /> if no valid position is
        ///     found.
        /// </returns>
        public static Vector3 GetRandomSpawnPosition(this RoleTypeId roleType)
        {
            if (!roleType.TryGetBaseRole(out PlayerRoleBase roleBase) || roleBase is not IFpcRole fpc)
            {
                return Vector3.zero;
            }

            ISpawnpointHandler spawn = fpc.SpawnpointHandler;
            if (spawn == null || !spawn.TryGetSpawnpoint(out Vector3 pos, out float _))
            {
                return Vector3.zero;
            }

            return pos;
        }

        /// <summary>
        ///     Gets the default inventory for the specified role.
        /// </summary>
        /// <param name="role">The <see cref="RoleTypeId" /> to use.</param>
        /// <returns>The default inventory if found, null otherwise.</returns>
        public static InventoryRoleInfo? GetDefaultInventory(this RoleTypeId role)
        {
            return TryGetDefaultInventory(role, out InventoryRoleInfo roleInfo) ? roleInfo : null;
        }

        /// <summary>
        ///     Tries to get the default inventory for the specified role.
        /// </summary>
        /// <param name="role">The <see cref="RoleTypeId" /> to use.</param>
        /// <param name="inventoryRoleInfo">The inventory role information if found, otherwise null.</param>
        /// <returns>True if the default inventory is found, false otherwise.</returns>
        public static bool TryGetDefaultInventory(this RoleTypeId role, out InventoryRoleInfo inventoryRoleInfo)
        {
            return StartingInventories.DefinedInventories.TryGetValue(role, out inventoryRoleInfo);
        }

        /// <summary>
        ///     Sets the default inventory for the specified role.
        /// </summary>
        /// <param name="role">The <see cref="RoleTypeId" /> to use.</param>
        /// <param name="inventoryRoleInfo">The inventory role information to set.</param>
        public static void SetDefaultInventory(this RoleTypeId role, InventoryRoleInfo inventoryRoleInfo)
        {
            StartingInventories.DefinedInventories.UpdateOrAdd(role, inventoryRoleInfo);
        }

        /// <summary>
        ///     Gets the color associated with a role based on its <see cref="RoleTypeId" />.
        /// </summary>
        /// <param name="role">The <see cref="RoleTypeId" />.</param>
        /// <returns>The color associated with the role, or <see cref="Color.white" /> if the role is not found.</returns>
        public static Color GetRoleColor(this RoleTypeId role)
        {
            if (!role.TryGetBaseRole(out PlayerRoleBase roleBase))
            {
                return Color.white;
            }

            return roleBase.RoleColor;
        }

        /// <summary>
        ///     Gets the roles full name colored with the <see cref="RoleTypeId" /> color.
        /// </summary>
        /// <param name="role">The <see cref="RoleTypeId" />.</param>
        /// <returns>
        ///     The string with the roles name and already color formatted, or a empty string if the
        ///     <see cref="PlayerRoleBase" /> cannot be found.
        /// </returns>
        public static string GetColoredName(this RoleTypeId role)
        {
            if (!role.TryGetBaseRole(out PlayerRoleBase roleBase))
            {
                return string.Empty;
            }

            return roleBase.GetColoredName();
        }
    }
}
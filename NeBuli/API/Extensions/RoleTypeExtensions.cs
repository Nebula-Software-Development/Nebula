using Nebuli.API.Features.Roles;
using PlayerRoles;
using UnityEngine;

namespace Nebuli.API.Extensions;

public static class RoleTypeExtensions
{
    /// <summary>
    /// Casts a role to a specific type.
    /// </summary>
    /// <typeparam name="T">The type to cast the role to.</typeparam>
    /// <param name="role">The role to cast.</param>
    /// <returns>The role cast to the specified type, or null if the cast is not possible.</returns>
    public static T ConvertTo<T>(this Role role) where T : Role
    {
        return role as T;
    }

    public static bool IsHuman(this Role role) => role.RoleTypeId.IsHuman();

    public static bool IsSCP(this Role role) => role.RoleTypeId.GetFaction() == Faction.SCP;
}

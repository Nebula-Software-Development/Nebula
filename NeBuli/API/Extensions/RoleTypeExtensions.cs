using Nebuli.API.Features.Roles;
using PlayerRoles;

namespace Nebuli.API.Extensions;

/// <summary>
/// Extension for managing <see cref="Role"/> and <see cref="RoleTypeId"/> easier.
/// </summary>
public static class RoleTypeExtensions
{
    /// <summary>
    /// Unsafley casts a role to a specific type.
    /// </summary>
    /// <typeparam name="T">The type to cast the role to.</typeparam>
    /// <param name="role">The role to cast.</param>
    /// <returns>The role cast to the specified type, or null if the cast is not possible.</returns>
    public static T ConvertTo<T>(this Role role) where T : Role => role as T;

    /// <summary>
    /// Safely attempts to cast a role to a specific type.
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
    /// Gets if the <see cref="Role"/> is a human role.
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public static bool IsHuman(this Role role) => role.RoleTypeId.IsHuman();

    /// <summary>
    /// Gets if the <see cref="Role"/> is a alive role.
    /// </summary>
    public static bool IsAlive(this Role role) => role.RoleTypeId.IsAlive();

    /// <summary>
    /// Gets if the <see cref="Role"/> is any SCP role.
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public static bool IsSCP(this Role role) => role.RoleTypeId.GetFaction() == Faction.SCP;

    /// <summary>
    /// Gets if the <see cref="Role"/> is apart of <see cref="Faction.FoundationStaff"/>.
    /// </summary>
    public static bool IsFoundationStaff(this Role role) => role.RoleTypeId.GetFaction() == Faction.FoundationStaff;

    /// <summary>
    /// Gets if the <see cref="Role"/> is apart of <see cref="Faction.FoundationEnemy"/>.
    /// </summary>
    public static bool IsFoundationEnemy(this Role role) => role.RoleTypeId.GetFaction() == Faction.FoundationEnemy;

    /// <summary>
    /// Gets if the <see cref="Role"/> is apart of <see cref="Faction.Unclassified"/>.
    /// </summary>
    public static bool IsUnclassified(this Role role) => role.RoleTypeId.GetFaction() == Faction.Unclassified;
}

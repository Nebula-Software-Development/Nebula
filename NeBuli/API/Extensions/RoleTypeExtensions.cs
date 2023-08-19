using Nebuli.API.Features.Roles;
using PlayerRoles;

namespace Nebuli.API.Extensions;

public static class RoleTypeExtensions
{
    /// <summary>
    /// Casts a role to a specific type.
    /// </summary>
    /// <typeparam name="T">The type to cast the role to.</typeparam>
    /// <param name="role">The role to cast.</param>
    /// <returns>The role cast to the specified type, or null if the cast is not possible.</returns>
    public static T ConvertTo<T>(this Role role) where T : Role => role as T;

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

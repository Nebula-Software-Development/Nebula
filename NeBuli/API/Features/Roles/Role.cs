using Nebuli.API.Features.Player;
using PlayerRoles;

namespace Nebuli.API.Features.Roles;

public class Role
{
    internal Role(PlayerRoleBase roleBase)
    {
        if(roleBase.TryGetOwner(out ReferenceHub hub))
            Owner = NebuliPlayer.TryGet(hub, out NebuliPlayer ply) ? ply : null;
        Base = roleBase;
    }

    /// <summary>
    /// Gets the owner of the <see cref="PlayerRoleBase"/>.
    /// </summary>
    public NebuliPlayer Owner { get; }

    /// <summary>
    /// Gets the <see cref="PlayerRoleBase"/>.
    /// </summary>
    public PlayerRoleBase Base { get; }

    /// <summary>
    /// Gets the total time this role has been active.
    /// </summary>
    public float TotalTimeAlive => Base.ActiveTime;

    /// <summary>
    /// Gets the <see cref="PlayerRoles.RoleTypeId"/> of this role.
    /// </summary>
    public RoleTypeId RoleTypeId => Base.RoleTypeId;

    /// <summary>
    /// Gets the roles name.
    /// </summary>
    public string RoleName => Base.RoleName;

    /// <summary>
    /// Gets or sets the roles <see cref="PlayerRoles.RoleSpawnFlags"/>.
    /// </summary>
    public RoleSpawnFlags RoleSpawnFlags
    {
        get => Base.ServerSpawnFlags;
        set => Base.ServerSpawnFlags = value;
    }
    
    /// <summary>
    /// Gets or sets the roles <see cref="PlayerRoles.RoleChangeReason"/>.
    /// </summary>
    public RoleChangeReason RoleChangeReason
    {
        get => Base.ServerSpawnReason;
        set => Base.ServerSpawnReason = value;
    }

    /// <summary>
    /// Gets the roles <see cref="PlayerRoles.Team"/>.
    /// </summary>
    public Team Team => Base.Team;

    /// <summary>
    /// Gets if the roles team is dead.
    /// </summary>
    public bool IsDead => Team is Team.Dead;

    /// <summary>
    /// Gets if the roles team is alive.
    /// </summary>
    public bool IsAlive => !IsDead;

    /// <summary>
    /// Sets the owner of this role to a new role.
    /// </summary>
    /// <param name="newRole">The new role to set.</param>
    /// <param name="reason">The reason for the role change.</param>
    /// <param name="roleSpawnFlags">The roles spawn flags.</param>
    public void SetNewRole(RoleTypeId newRole, RoleChangeReason reason, RoleSpawnFlags roleSpawnFlags) => Owner.ReferenceHub.roleManager.ServerSetRole(newRole, reason, roleSpawnFlags);
}

using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp939;
using UnityEngine;
using PlayerRoles.PlayableScps.Scp079;

namespace Nebuli.API.Features.Roles;

public abstract class Role
{
    protected Role(PlayerRoleBase roleBase)
    {
        if (roleBase.TryGetOwner(out ReferenceHub hub))
            Owner = NebuliPlayer.TryGet(hub, out NebuliPlayer ply) ? ply : null;
        Base = roleBase;
        Owner.CurrentRole = this;
    }

    /// <summary>
    /// Gets the owner of this <see cref="PlayerRoleBase"/>.
    /// </summary>
    public NebuliPlayer Owner { get; }

    /// <summary>
    /// Gets the roles position.
    /// </summary>
    public Vector3 Position => Owner.Position;

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
    public abstract RoleTypeId RoleTypeId { get; }

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
    /// Gets the roles <see cref="UnityEngine.Color"/>.
    /// </summary>
    public Color Color => Base.RoleColor;

    /// <summary>
    /// Gets the roles GameObject.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Sets the owner of this role to a new role.
    /// </summary>
    /// <param name="newRole">The new role to set.</param>
    /// <param name="reason">The reason for the role change.</param>
    /// <param name="roleSpawnFlags">The roles spawn flags.</param>
    public void SetNewRole(RoleTypeId newRole, RoleChangeReason reason = RoleChangeReason.RemoteAdmin, RoleSpawnFlags roleSpawnFlags = RoleSpawnFlags.All)
        => Owner.ReferenceHub.roleManager.ServerSetRole(newRole, reason, roleSpawnFlags);

    /// <summary>
    /// Creates a new role based on the given PlayerRoleBase instance.
    /// </summary>
    /// <param name="role">The PlayerRoleBase instance to create the role from.</param>
    /// <returns>The created Role instance.</returns>
    public static Role CreateNew(PlayerRoleBase role)
    {
        return role switch
        {
            HumanRole human => new HumanPlayerRole(human),
            Scp939Role scp939 => new Scp939PlayerRole(scp939),
            Scp079Role scp079 => new Scp079PlayerRole(scp079),
            _ => null,
        };
    }
}
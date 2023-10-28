using Scp3114RoleBase = PlayerRoles.PlayableScps.Scp3114.Scp3114Role;

namespace Nebuli.API.Features.Roles;

public class Scp3114PlayerRole : FpcRoleBase
{
    /// <summary>
    /// Gets the <see cref="Scp3114RoleBase"/> base.
    /// </summary>
    public new Scp3114RoleBase Base { get; }
    internal Scp3114PlayerRole(Scp3114RoleBase fpcRole) : base(fpcRole)
    {
        Base = fpcRole;
    }

    /// <summary>
    /// Gets or sets if the role is currently disguised.
    /// </summary>
    public bool Disguised
    {
        get => Base.Disguised;
        set => Base.Disguised = value;
    }
}

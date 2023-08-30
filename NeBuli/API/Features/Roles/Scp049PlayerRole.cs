

using PlayerRoles.PlayableScps.Scp049;

namespace Nebuli.API.Features.Roles;

public class Scp049PlayerRole : FpcRoleBase
{
    /// <summary>
    /// Gets the <see cref="Scp049Role"/> base.
    /// </summary>
    public new Scp049Role Base { get; }
    internal Scp049PlayerRole(Scp049Role role) : base(role) 
    {
        Base = role;
    }
}

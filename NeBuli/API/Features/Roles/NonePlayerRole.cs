using PlayerRoles;

namespace Nebuli.API.Features.Roles;

public class NonePlayerRole : Role
{
    /// <summary>
    /// Gets the <see cref="NoneRole"/> base.
    /// </summary>
    public new NoneRole Base { get; }
    internal NonePlayerRole(NoneRole roleBase) : base(roleBase) => Base = roleBase;
}

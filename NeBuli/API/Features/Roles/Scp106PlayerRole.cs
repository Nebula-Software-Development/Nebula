using PlayerRoles.PlayableScps.Scp106;


namespace Nebuli.API.Features.Roles
{
    public class Scp106PlayerRole : FpcRoleBase
    {
        /// <summary>
        /// Gets the <see cref="Scp106Role"/> base.
        /// </summary>
        public new Scp106Role Base { get; }
        public Scp106PlayerRole(Scp106Role scpRole) : base(scpRole)
        {
            Base = scpRole;
        }

        /// <summary>
        /// Gets if SCP-106 is submerged.
        /// </summary>
        public bool IsSubmerged => Base.IsSubmerged;

        /// <summary>
        /// Gets if SCP-106 can trigger a tesla gate shock.
        /// </summary>
        public bool CanActivateTeslaShock => Base.CanActivateShock;
    }
}

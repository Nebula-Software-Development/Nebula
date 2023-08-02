using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.Voice;
using System;
using UnityEngine;

namespace Nebuli.API.Features.Roles;

public class Scp173PlayerRole : FpcRoleBase
{
    /// <summary>
    /// Gets the <see cref="Scp173Role"/> base.
    /// </summary>
    public new Scp173Role Base { get; }
    internal Scp173PlayerRole(Scp173Role role) : base(role) 
    {
        Base = role;
        SetupSubroutines();
    }


    /// <summary>
    /// Gets the roles <see cref="PlayerRoles.RoleTypeId"/>.
    /// </summary>
    public override RoleTypeId RoleTypeId => Base.RoleTypeId;

    /// <summary>
    /// Gets the role's <see cref="HumeShieldModuleBase"/>.
    /// </summary>
    public HumeShieldModuleBase HumeShield => Base.HumeShieldModule;

    /// <summary>
    /// Gets the roles <see cref="FirstPersonMovementModule"/>.
    /// </summary>
    public FirstPersonMovementModule FpcModule => Base.FpcModule;

    /// <summary>
    /// Gets the roles camera position.
    /// </summary>
    public Vector3 CameraPosition => Base.CameraPosition;

    /// <summary>
    /// Gets the roles <see cref="SubroutineManagerModule"/>.
    /// </summary>
    public SubroutineManagerModule ManagerModule { get; internal set; }

    /// <summary>
    /// Gets the roles <see cref="HumeShieldModuleBase"/>.
    /// </summary>
    public HumeShieldModuleBase HumeShieldModule { get; internal set; }

    /// <summary>
    /// Gets SCP-173's tantrum ability.
    /// </summary>
    public Scp173TantrumAbility TantrumAbility { get; internal set; }

    /// <summary>
    /// Gets SCP-173's breakneck speed ability.
    /// </summary>
    public Scp173BreakneckSpeedsAbility BreakneckSpeedsAbility { get; internal set; }

    /// <summary>
    /// Gets SCP-173's teleport ability.
    /// </summary>
    public Scp173TeleportAbility TeleportAbility { get; internal set; }

    internal void SetupSubroutines()
    {
        try
        {
            ManagerModule = Base.SubroutineModule;
            HumeShieldModule = Base.HumeShieldModule;

            Scp173TantrumAbility tantrumAbility;
            if (ManagerModule.TryGetSubroutine(out tantrumAbility))
                TantrumAbility = tantrumAbility;
            Scp173BreakneckSpeedsAbility breakNeck;
            if (ManagerModule.TryGetSubroutine(out breakNeck))
                BreakneckSpeedsAbility = breakNeck;
            Scp173TeleportAbility teleportAbility;
            if(ManagerModule.TryGetSubroutine(out teleportAbility))
                TeleportAbility = teleportAbility;

        }
        catch(Exception e)
        {
            Log.Error("An error occurred setting up SCP-173 subroutines! Full error --> \n" + e);
        }
    }
}

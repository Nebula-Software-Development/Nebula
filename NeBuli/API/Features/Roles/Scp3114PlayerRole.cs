using PlayerRoles;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp3114;
using PlayerRoles.PlayableScps.Subroutines;
using System;
using System.Collections.Generic;
using UnityEngine;
using static PlayerRoles.PlayableScps.Scp3114.Scp3114Identity;
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
        SetupSubroutines();
    }

    /// <summary>
    /// Gets the amount of damage dealt by SCP-3114's slap attack.
    /// </summary>
    public float SlapDamage => Slap.DamageAmount;

    /// <summary>
    /// Initiates SCP-3114's slap attack.
    /// </summary>
    public void Attack() => Slap.ServerPerformAttack();

    /// <summary>
    /// Plays a swing sound for SCP-3114's attack.
    /// </summary>
    public void PlaySwingSound() => Slap.PlaySwingSound();

    /// <summary>
    /// Gets a list of logged identities in SCP-3114's history.
    /// </summary>
    public List<Scp3114History.LoggedIdentity> LoggedIdentities => History.History;

    /// <summary>
    /// Gets or sets the duration of SCP-3114's disguise.
    /// </summary>
    public float DisguiseDuration
    {
        get => Identity._disguiseDurationSeconds;
        set => Identity._disguiseDurationSeconds = value;
    }

    /// <summary>
    /// Gets the stolen role ID of SCP-3114.
    /// </summary>
    public RoleTypeId StolenRoleID => Identity.CurIdentity.StolenRole;

    /// <summary>
    /// Gets or sets the dance variant for SCP-3114.
    /// </summary>
    public int DanceVariant
    {
        get => Dance.DanceVariant;
        set => Dance.DanceVariant = value;
    }

    /// <summary>
    /// Gets the progression status of SCP-3114's disguise.
    /// </summary>
    public float ProgressionStatus => Disguise.ProgressStatus;

    /// <summary>
    /// Gets or sets whether SCP-3114 is in progress.
    /// </summary>
    public bool InProgress
    {
        get => Disguise.IsInProgress;
        set => Disguise.IsInProgress = value;
    }

    /// <summary>
    /// Gets or sets the stolen ragdoll for SCP-3114.
    /// </summary>
    public Ragdoll StolenRagdoll
    {
        get => Ragdoll.Get(Identity.CurIdentity?.Ragdoll);
        set
        {
            Identity.CurIdentity.Ragdoll = value?.Base;
            Identity.ServerResendIdentity();
        }
    }

    /// <summary>
    /// Gets or sets the disguise status of SCP-3114.
    /// </summary>
    public DisguiseStatus DisguiseStatus
    {
        get => Identity.CurIdentity.Status;
        set => Identity.CurIdentity.Status = value;
    }

    /// <summary>
    /// Checks if SCP-3114 is close to a specified ragdoll based on a position.
    /// </summary>
    /// <param name="position">The position to check against.</param>
    /// <returns>Returns true if SCP-3114 is close to the specified ragdoll; otherwise, false.</returns>
    public bool IsCloseToRagdoll(Vector3 position) => Disguise.IsCloseEnough(Position, position);

    /// <summary>
    /// Gets SCP-3114's SubroutineManagerModule.
    /// </summary>
    public SubroutineManagerModule ManagerModule { get; internal set; }

    /// <summary>
    /// Gets SCP-3114's HumeShieldModuleBase.
    /// </summary>
    public HumeShieldModuleBase HumeShieldModule { get; internal set; }

    /// <summary>
    /// Gets SCP-3114's Slap module.
    /// </summary>
    /// <remarks>Bitch slap that mf</remarks>
    public Scp3114Slap Slap { get; internal set; }

    /// <summary>
    /// Gets SCP-3114's Dance module.
    /// </summary>
    public Scp3114Dance Dance { get; internal set; }

    /// <summary>
    /// Gets SCP-3114's Reveal module.
    /// </summary>
    public Scp3114Reveal Reveal { get; internal set; }

    /// <summary>
    /// Gets SCP-3114's History module.
    /// </summary>
    public Scp3114History History { get; internal set; }

    /// <summary>
    /// Gets SCP-3114's FakeModelManager module.
    /// </summary>
    public Scp3114FakeModelManager FakeModelManager { get; internal set; }

    /// <summary>
    /// Gets SCP-3114's Identity module.
    /// </summary>
    public Scp3114Identity Identity { get; internal set; }

    /// <summary>
    /// Gets SCP-3114's Disguise module.
    /// </summary>
    public Scp3114Disguise Disguise { get; internal set; }


    /// <summary>
    /// Gets or sets if the role is currently disguised.
    /// </summary>
    public bool Disguised
    {
        get => Base.Disguised;
        set => Base.Disguised = value;
    }

    internal void SetupSubroutines()
    {
        ManagerModule = Base.SubroutineModule;
        HumeShieldModule = Base.HumeShieldModule;

        try
        {
            if (ManagerModule.TryGetSubroutine(out Scp3114Identity identity))
                Identity = identity;

            if (ManagerModule.TryGetSubroutine(out Scp3114Disguise disguise))
                Disguise = disguise;

            if (ManagerModule.TryGetSubroutine(out Scp3114Dance dance))
                Dance = dance;

            if (ManagerModule.TryGetSubroutine(out Scp3114Reveal reveal))
                Reveal = reveal;

            if (ManagerModule.TryGetSubroutine(out Scp3114History history))
                History = history;

            if(ManagerModule.TryGetSubroutine(out Scp3114FakeModelManager modelManager))
                FakeModelManager = modelManager;

            if(ManagerModule.TryGetSubroutine(out Scp3114Slap slap))
                Slap = slap;
        }
        catch(Exception e)
        {
            Log.Error("An error occurred setting up SCP-3114 subroutines! Full error --> \n" + e);
        }      
    }
}

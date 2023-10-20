using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using PlayerRoles.Voice;
using PlayerStatsSystem;

namespace Nebuli.API.Features.Roles;

public class FpcRoleBase : Role
{
    /// <summary>
    /// Gets the <see cref="FpcStandardRoleBase"/>.
    /// </summary>
    public new FpcStandardRoleBase Base { get; }

    internal FpcRoleBase(FpcStandardRoleBase fpcRole) : base(fpcRole)
    {
        Base = fpcRole;
        Ragdoll = Ragdoll.Get(Base.Ragdoll);
    }

    /// <summary>
    /// Gets the ambient light level of the role.
    /// </summary>
    public float AmbientLight => Base.AmbientLight;

    /// <summary>
    /// Gets a value indicating whether the role has a flashlight.
    /// </summary>
    public bool HasFlashlight => Base.HasFlashlight;

    /// <summary>
    /// Gets a value indicating whether the role is in darkness.
    /// </summary>
    public bool InDarkness => Base.InDarkness;

    /// <summary>
    /// Gets the First Person Movement module of the role.
    /// </summary>
    public FirstPersonMovementModule FirstPersonMovementModule => Base.FpcModule;

    /// <summary>
    /// Gets the Voice module of the role.
    /// </summary>
    public VoiceModuleBase VoiceModule => Base.VoiceModule;

    /// <summary>
    /// Gets the ragdoll associated with the role.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// Gets the maximum health of the role.
    /// </summary>
    public float MaxHealth => Base.MaxHealth;

    /// <summary>
    /// Gets a value indicating whether the role is marked as AFK.
    /// </summary>
    public bool IsAFK => Base.IsAFK;

    ///<summary>
    /// Gets the role's SpectatableModuleBase.
    /// </summary>
    public PlayerRoles.Spectating.SpectatableModuleBase SpectatableModuleBase => Base.SpectatorModule;

    /// <summary>
    /// Gets the roles <see cref="ISpawnpointHandler"/>.
    /// </summary>
    public ISpawnpointHandler SpawnpointHandler => Base.SpawnpointHandler;

    /// <summary>
    /// Re-shows the players start screen.
    /// </summary>
    public void ShowStartScreen() => Base.ShowStartScreen();

    /// <summary>
    /// Gets or sets if the <see cref="FpcRoleBase"/> has noclip.
    /// </summary>
    public bool HasNoclip
    {
        get => Owner.ReferenceHub.playerStats.GetModule<AdminFlagsStat>().HasFlag(AdminFlags.Noclip);
        set => Owner.ReferenceHub.playerStats.GetModule<AdminFlagsStat>().SetFlag(AdminFlags.Noclip, value);
    }
}
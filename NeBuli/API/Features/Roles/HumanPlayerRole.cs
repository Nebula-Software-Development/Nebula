using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using PlayerRoles.Voice;

namespace Nebuli.API.Features.Roles;

public class HumanPlayerRole : Role
{
    internal HumanPlayerRole(HumanRole role) : base(role)
    {
        Base = role;
        Ragdoll = Ragdoll.Get(Base.Ragdoll);
    }

    /// <summary>
    /// Gets the <see cref="HumanRole"/> base.
    /// </summary>
    public new HumanRole Base { get; }

    /// <summary>
    /// Gets the roles <see cref="Player.Ragdoll"/>.
    /// </summary>
    public Ragdoll Ragdoll { get; }

    /// <summary>
    /// Gets the roles <see cref="VoiceModuleBase"/>.
    /// </summary>
    public VoiceModuleBase VoiceModule => Base.VoiceModule;

    /// <summary>
    /// Gets if the role is AFK.
    /// </summary>
    public bool IsAFK => Base.IsAFK;

    /// <summary>
    /// Re-shows the players start screen.
    /// </summary>
    public void ShowStartScreen() => Base.ShowStartScreen();

    /// <summary>
    /// Gets the roles <see cref="ISpawnpointHandler"/>.
    /// </summary>
    public ISpawnpointHandler SpawnpointHandler => Base.SpawnpointHandler;

    /// <summary>
    /// Gets the roles Spectator module.
    /// </summary>
    public PlayerRoles.Spectating.SpectatableModuleBase SpectatableModuleBase => Base.SpectatorModule;

    /// <summary>
    /// Gets the roles RoleTypeId.
    /// </summary>
    public override RoleTypeId RoleTypeId => Base.RoleTypeId;
}
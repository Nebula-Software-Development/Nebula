using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.Spectating;
using RelativePositioning;

namespace Nebuli.API.Features.Roles;

public class OverwatchPlayerRole : Role
{
    /// <summary>
    /// Gets the <see cref="OverwatchRole"/> base.
    /// </summary>
    public new OverwatchRole Base { get; }
    public OverwatchPlayerRole(OverwatchRole role) : base(role) 
    {
        Base = role;
    }
    /// <summary>
    /// Gets the last tracked ReferenceHub by the overwatcher.
    /// </summary>
    public ReferenceHub LastTrackedRef => SpectatorTargetTracker.LastTrackedPlayer;

    /// <summary>
    /// Gets the last tracked player by the overwatcher.
    /// </summary>
    public NebuliPlayer LastTrackedPlayer => NebuliPlayer.Get(SpectatorTargetTracker.LastTrackedPlayer);

    /// <summary>
    /// Gets the RoleTypeId of the role.
    /// </summary>
    public override RoleTypeId RoleTypeId => Base.RoleTypeId;

    /// <summary>
    /// Gets the relative position of the player's death location.
    /// </summary>
    public RelativePosition DeathPosition => Base.DeathPosition;

    /// <summary>
    /// Gets a value indicating whether the overwatcher is ready to respawn.
    /// </summary>
    public bool ReadyToRespawn => Base.ReadyToRespawn;

    /// <summary>
    /// Gets the overwatcher's target tracker.
    /// </summary>
    public SpectatorTargetTracker SpectatorTargetTracker => Base.TrackerPrefab;

    /// <summary>
    /// Gets or sets the current target of the overwatcher.
    /// </summary>
    public SpectatableModuleBase CurrentTarget
    {
        get => SpectatorTargetTracker.CurrentTarget;
        set => SpectatorTargetTracker.CurrentTarget = value;
    }

    /// <summary>
    /// Sets the specified overwatcher <see cref="SpectatableModuleBase"/> to the target.
    /// </summary>
    /// <param name="player">The overwatcher to switch.</param>
    /// <param name="target">The target to switch to.</param>
    public static void SetCurrentTarget(NebuliPlayer player, NebuliPlayer target)
    {
        if (player.CurrentRole is not SpectatorPlayerRole)
            return;
        if (target.CurrentRole is not FpcRoleBase)
            return;
        SpectatorPlayerRole spectator = player.CurrentRole as SpectatorPlayerRole;
        FpcRoleBase targetToSwitchTo = target.CurrentRole as FpcRoleBase;
        spectator.CurrentTarget = targetToSwitchTo.SpectatableModuleBase;
    }
}

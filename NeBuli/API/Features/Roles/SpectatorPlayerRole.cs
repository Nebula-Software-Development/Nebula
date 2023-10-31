using Nebuli.API.Features.Player;
using PlayerRoles.Spectating;
using RelativePositioning;

namespace Nebuli.API.Features.Roles;

public class SpectatorPlayerRole : Role
{
    /// <summary>
    /// Represents a player role that is a spectator.
    /// </summary>
    public new SpectatorRole Base { get; }

    internal SpectatorPlayerRole(SpectatorRole role) : base(role)
    {
        Base = role;
    }

    /// <summary>
    /// Gets the last tracked ReferenceHub by the spectator.
    /// </summary>
    public ReferenceHub LastTrackedRef => SpectatorTargetTracker.LastTrackedPlayer;

    /// <summary>
    /// Gets the last tracked player by the spectator.
    /// </summary>
    public NebuliPlayer LastTrackedPlayer => NebuliPlayer.Get(SpectatorTargetTracker.LastTrackedPlayer);

    /// <summary>
    /// Gets or sets the relative position of the player's death location.
    /// </summary>
    public RelativePosition DeathPosition
    {
        get => Base.DeathPosition;
        set => Base.DeathPosition = value;
    }

    /// <summary>
    /// Gets a value indicating whether the spectator is ready to respawn.
    /// </summary>
    public bool ReadyToRespawn => Base.ReadyToRespawn;

    /// <summary>
    /// Gets the spectator's target tracker.
    /// </summary>
    public SpectatorTargetTracker SpectatorTargetTracker => Base.TrackerPrefab;

    /// <summary>
    /// Gets or sets the current target of the spectator.
    /// </summary>
    public SpectatableModuleBase CurrentTarget
    {
        get => SpectatorTargetTracker.CurrentTarget;
        set => SpectatorTargetTracker.CurrentTarget = value;
    }

    /// <summary>
    /// Sets the specified spectator <see cref="SpectatableModuleBase"/> to the targets.
    /// </summary>
    /// <param name="player">The spectator to switch.</param>
    /// <param name="target">The target to switch to.</param>
    public static void SetCurrentTarget(NebuliPlayer player, NebuliPlayer target)
    {
        if (player.Role is not SpectatorPlayerRole)
            return;
        if (target.Role is not FpcRoleBase)
            return;
        SpectatorPlayerRole spectator = player.Role as SpectatorPlayerRole;
        FpcRoleBase targetToSwitchTo = target.Role as FpcRoleBase;
        spectator.CurrentTarget = targetToSwitchTo.SpectatableModuleBase;
    }
}
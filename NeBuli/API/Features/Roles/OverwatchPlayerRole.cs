// -----------------------------------------------------------------------
// <copyright file=OverwatchPlayerRole.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebula.API.Extensions;
using PlayerRoles;
using PlayerRoles.Spectating;
using RelativePositioning;

namespace Nebula.API.Features.Roles
{
    /// <summary>
    ///     Represents the <see cref="RoleTypeId.Overwatch" /> role in-game.
    /// </summary>
    public class OverwatchPlayerRole : Role
    {
        internal OverwatchPlayerRole(OverwatchRole role) : base(role)
        {
            Base = role;
        }

        /// <summary>
        ///     Gets the <see cref="OverwatchRole" /> base.
        /// </summary>
        public new OverwatchRole Base { get; }

        /// <summary>
        ///     Gets the last tracked ReferenceHub by the overwatcher.
        /// </summary>
        public ReferenceHub LastTrackedRef => SpectatorTargetTracker.LastTrackedPlayer;

        /// <summary>
        ///     Gets the last tracked player by the overwatcher.
        /// </summary>
        public Player LastTrackedPlayer => Player.Get(SpectatorTargetTracker.LastTrackedPlayer);

        /// <summary>
        ///     Gets the relative position of the player's death location.
        /// </summary>
        public RelativePosition DeathPosition => Base.DeathPosition;

        /// <summary>
        ///     Gets a value indicating whether the overwatcher is ready to respawn.
        /// </summary>
        public bool ReadyToRespawn => Base.ReadyToRespawn;

        /// <summary>
        ///     Gets the overwatcher's target tracker.
        /// </summary>
        public SpectatorTargetTracker SpectatorTargetTracker => Base.TrackerPrefab;

        /// <summary>
        ///     Gets or sets the current target of the overwatcher.
        /// </summary>
        public SpectatableModuleBase CurrentTarget
        {
            get => SpectatorTargetTracker.CurrentTarget;
            set => SpectatorTargetTracker.CurrentTarget = value;
        }

        /// <summary>
        ///     Sets the specified overwatcher <see cref="SpectatableModuleBase" /> to the target.
        /// </summary>
        /// <param name="player">The overwatcher to switch.</param>
        /// <param name="target">The target to switch to.</param>
        public static void SetCurrentTarget(Player player, Player target)
        {
            if (!player.Role.TryCastTo(out SpectatorPlayerRole srole))
            {
                return;
            }

            if (!target.Role.TryCastTo(out FpcRoleBase frole))
            {
                return;
            }

            srole.CurrentTarget = frole.SpectatableModuleBase;
        }
    }
}
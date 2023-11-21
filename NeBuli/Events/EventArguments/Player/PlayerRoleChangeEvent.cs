// -----------------------------------------------------------------------
// <copyright file=PlayerRoleChangeEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles;

namespace Nebuli.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player's role changes.
    /// </summary>
    public class PlayerRoleChangeEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerRoleChangeEvent(ReferenceHub ply, RoleTypeId newRole, RoleChangeReason roleChangeReason,
            RoleSpawnFlags roleSpawnFlags)
        {
            Player = API.Features.Player.Get(ply);
            NewRole = newRole;
            Reason = roleChangeReason;
            SpawnFlags = roleSpawnFlags;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets the new role.
        /// </summary>
        public RoleTypeId NewRole { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="RoleChangeReason" />.
        /// </summary>
        public RoleChangeReason Reason { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="RoleSpawnFlags" />.
        /// </summary>
        public RoleSpawnFlags SpawnFlags { get; set; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player that triggered the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
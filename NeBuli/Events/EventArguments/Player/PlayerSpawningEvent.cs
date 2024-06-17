// -----------------------------------------------------------------------
// <copyright file=PlayerSpawningEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;
using PlayerRoles;
using UnityEngine;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player spawns in the game.
    /// </summary>
    public class PlayerSpawningEvent : EventArgs, IPlayerEvent
    {
        public PlayerSpawningEvent(ReferenceHub player, PlayerRoleBase oldRole, PlayerRoleBase newRole,
            Vector3 position, float oldRotation)
        {
            Player = API.Features.Player.Get(player);
            NewRole = newRole.RoleTypeId;
            OldRole = oldRole.RoleTypeId;
            HorizontalRotation = oldRotation;
            Position = position;
        }

        /// <summary>
        ///     Gets the position the player will be spawned at.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        ///     Gets the new <see cref="RoleTypeId" />.
        /// </summary>
        public RoleTypeId NewRole { get; }

        /// <summary>
        ///     Gets the old <see cref="RoleTypeId" />.
        /// </summary>
        public RoleTypeId OldRole { get; }

        /// <summary>
        ///     Gets or sets the players horizontal rotation when spawning.
        /// </summary>
        public float HorizontalRotation { get; set; }

        /// <summary>
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
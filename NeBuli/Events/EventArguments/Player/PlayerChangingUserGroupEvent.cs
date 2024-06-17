// -----------------------------------------------------------------------
// <copyright file=PlayerChangingUserGroupEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player changes their user group.
    /// </summary>
    public class PlayerChangingUserGroupEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerChangingUserGroupEvent(ReferenceHub ply, UserGroup group)
        {
            Player = API.Features.Player.Get(ply);
            Group = group;
        }

        /// <summary>
        ///     Gets the <see cref="UserGroup" /> thats being changed to.
        /// </summary>
        public UserGroup Group { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
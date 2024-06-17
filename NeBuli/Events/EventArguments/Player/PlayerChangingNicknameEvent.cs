// -----------------------------------------------------------------------
// <copyright file=PlayerChangingNicknameEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player changes their nickname.
    /// </summary>
    public class PlayerChangingNicknameEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerChangingNicknameEvent(ReferenceHub ply, string newName)
        {
            Player = API.Features.Player.Get(ply);
            OldName = Player.DisplayNickname;
            NewName = newName;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets the new nickname.
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        ///     Gets the players current name.
        /// </summary>
        public string OldName { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
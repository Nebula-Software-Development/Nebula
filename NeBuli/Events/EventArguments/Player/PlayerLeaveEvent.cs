// -----------------------------------------------------------------------
// <copyright file=PlayerLeaveEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Mirror;
using Nebuli.API.Extensions;
using Nebuli.Events.EventArguments.Interfaces;

namespace Nebuli.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player leaves the server.
    /// </summary>
    public class PlayerLeaveEvent : EventArgs, IPlayerEvent
    {
        public PlayerLeaveEvent(NetworkConnection conn)
        {
            Player = API.Features.Player.Get(conn.identity);
            API.Features.Player.Dictionary.RemoveIfContains(Player.ReferenceHub);
        }

        /// <summary>
        ///     The player calling the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
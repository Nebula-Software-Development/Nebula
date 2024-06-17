// -----------------------------------------------------------------------
// <copyright file=WarheadDetonatingEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Server
{
    /// <summary>
    ///     Triggered before the warhead detonates.
    /// </summary>
    public class WarheadDetonatingEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public WarheadDetonatingEvent(ReferenceHub player)
        {
            Player = API.Features.Player.TryGet(player, out API.Features.Player ply) ? ply : API.Features.Server.Host;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player triggering the event, will be the Host player if null.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
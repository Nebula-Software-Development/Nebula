// -----------------------------------------------------------------------
// <copyright file=PlayerBannedEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using CommandSystem;
using Footprinting;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player is banned from the server.
    /// </summary>
    public class PlayerBannedEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerBannedEvent(Footprint target, ICommandSender issuer, string reason, long duration)
        {
            Player = API.Features.Player.Get(target.Hub);
            Issuer = API.Features.Player.Get(issuer);
            Reason = reason;
            Duration = duration;
            IsCancelled = false;
        }

        /// <summary>
        ///     The player issuing the ban.
        /// </summary>
        public API.Features.Player Issuer { get; }

        /// <summary>
        ///     The reason for the ban.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        ///     The duration of the ban.
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        ///     If the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player being banned.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
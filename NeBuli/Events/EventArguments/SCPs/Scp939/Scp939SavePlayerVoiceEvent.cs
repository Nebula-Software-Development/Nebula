// -----------------------------------------------------------------------
// <copyright file=Scp939SavePlayerVoiceEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;

namespace Nebuli.Events.EventArguments.SCPs.Scp939
{
    /// <summary>
    ///     Triggered when SCP-939 saves a player's voice.
    /// </summary>
    public class Scp939SavePlayerVoiceEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp939SavePlayerVoiceEvent(ReferenceHub player, ReferenceHub target)
        {
            Player = API.Features.Player.Get(player);
            Target = API.Features.Player.Get(target);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the owner of the voice being saved.
        /// </summary>
        public API.Features.Player Target { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player saving the voice.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
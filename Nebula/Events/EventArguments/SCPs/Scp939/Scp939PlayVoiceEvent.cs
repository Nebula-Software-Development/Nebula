// -----------------------------------------------------------------------
// <copyright file=Scp939PlayVoiceEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp939
{
    /// <summary>
    ///     Triggered when SCP-939 plays a voice.
    /// </summary>
    public class Scp939PlayVoiceEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp939PlayVoiceEvent(ReferenceHub player, ReferenceHub target)
        {
            Player = API.Features.Player.Get(player);
            VoicePlayer = API.Features.Player.Get(target);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the voices player owner.
        /// </summary>
        public API.Features.Player VoicePlayer { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player playing the voice.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
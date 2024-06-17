// -----------------------------------------------------------------------
// <copyright file=Scp939PlaySoundEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp939
{
    /// <summary>
    ///     Triggered when SCP-939 plays a sound.
    /// </summary>
    public class Scp939PlaySound : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp939PlaySound(ReferenceHub player, byte option)
        {
            Player = API.Features.Player.Get(player);
            SoundOption = option;
            IsCancelled = false;
        }

        /// <summary>
        ///     The sound to be played.
        /// </summary>
        public byte SoundOption { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player playing the sound.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
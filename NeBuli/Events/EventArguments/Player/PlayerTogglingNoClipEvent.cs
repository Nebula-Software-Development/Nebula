// -----------------------------------------------------------------------
// <copyright file=PlayerTogglingNoClipEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player toggles NoClip mode.
    /// </summary>
    public class PlayerTogglingNoClipEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerTogglingNoClipEvent(bool isPermitted, API.Features.Player player, bool oldState)
        {
            Player = player;
            IsCancelled = false;
            NewState = !oldState;
            IsPermitted = isPermitted;
        }

        /// <summary>
        ///     Gets the new state of noclip. True for enabled, false for disabled.
        /// </summary>
        public bool NewState { get; }

        /// <summary>
        ///     Gets if the player is permitted to enabled noclip.
        /// </summary>
        public bool IsPermitted { get; }

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
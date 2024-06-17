// -----------------------------------------------------------------------
// <copyright file=PlayerDestroyingEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player is destroyed.
    /// </summary>
    public class PlayerDestroyingEvent : EventArgs, IPlayerEvent
    {
        public PlayerDestroyingEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
        }

        /// <summary>
        ///     Gets the player being destroyed.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
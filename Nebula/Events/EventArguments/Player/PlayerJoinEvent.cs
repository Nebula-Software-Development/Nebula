﻿// -----------------------------------------------------------------------
// <copyright file=PlayerJoinEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using CentralAuth;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player joins the server.
    /// </summary>
    public class PlayerJoinEvent : EventArgs, IPlayerEvent
    {
        public PlayerJoinEvent(PlayerAuthenticationManager authManager)
        {
            Player = new API.Features.Player(authManager._hub);
        }

        /// <summary>
        ///     The player calling the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
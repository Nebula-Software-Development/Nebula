// -----------------------------------------------------------------------
// <copyright file=WarheadStoppingEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.Server
{
    /// <summary>
    ///     Triggered before the warhead stops its detonation sequence.
    /// </summary>
    public class WarheadStoppingEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public WarheadStoppingEvent(ReferenceHub player)
        {
            Player = player == null ? API.Features.Server.Host : API.Features.Player.Get(player);
            IsCancelled = false;
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
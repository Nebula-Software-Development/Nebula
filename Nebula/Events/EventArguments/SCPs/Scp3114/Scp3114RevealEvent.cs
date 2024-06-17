// -----------------------------------------------------------------------
// <copyright file=Scp3114RevealEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp3114
{
    /// <summary>
    ///     Triggered before SCP-3114 reveals.
    /// </summary>
    public class Scp3114RevealEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp3114RevealEvent(ReferenceHub hub)
        {
            Player = API.Features.Player.Get(hub);
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
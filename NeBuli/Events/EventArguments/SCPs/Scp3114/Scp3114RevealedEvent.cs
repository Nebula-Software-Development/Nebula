// -----------------------------------------------------------------------
// <copyright file=Scp3114RevealedEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp3114
{
    /// <summary>
    ///     Triggered after SCP-3114 reveals.
    /// </summary>
    public class Scp3114RevealedEvent : EventArgs, IPlayerEvent
    {
        public Scp3114RevealedEvent(ReferenceHub hub)
        {
            Player = API.Features.Player.Get(hub);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
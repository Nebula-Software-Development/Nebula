// -----------------------------------------------------------------------
// <copyright file=Scp3114RevealedEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114
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
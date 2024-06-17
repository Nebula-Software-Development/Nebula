// -----------------------------------------------------------------------
// <copyright file=Scp3114StranglingEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;
using static PlayerRoles.PlayableScps.Scp3114.Scp3114Strangle;

namespace Nebula.Events.EventArguments.SCPs.Scp3114
{
    /// <summary>
    ///     Triggered when SCP-3114 starts to strangle.
    /// </summary>
    public class Scp3114StranglingEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp3114StranglingEvent(ReferenceHub player, StrangleTarget target)
        {
            Player = API.Features.Player.Get(player);
            Target = API.Features.Player.Get(target.Target);
            StrangleTarget = target;
            IsCancelled = false;
        }

        /// <summary>
        ///     The player being strangled.
        /// </summary>
        public API.Features.Player Target { get; }

        /// <summary>
        ///     The <see cref="PlayerRoles.PlayableScps.Scp3114.Scp3114Strangle.StrangleTarget" /> of the event.
        /// </summary>
        public StrangleTarget StrangleTarget { get; set; }

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
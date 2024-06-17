// -----------------------------------------------------------------------
// <copyright file=Scp079LosingSignalEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;
using PlayerRoles;

namespace Nebula.Events.EventArguments.SCPs.Scp079
{
    /// <summary>
    ///     Triggered when SCP-079 is losing its signal.
    /// </summary>
    public class Scp079LosingSignalEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp079LosingSignalEvent(PlayerRoleBase player, float timeToLoseSignal)
        {
            if (player.TryGetOwner(out ReferenceHub hub))
            {
                Player = API.Features.Player.Get(hub);
            }

            DurationOfSignalLoss = timeToLoseSignal;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets the duration of the signal loss.
        /// </summary>
        public float DurationOfSignalLoss { get; set; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player losing signal.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
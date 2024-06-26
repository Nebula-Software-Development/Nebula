// -----------------------------------------------------------------------
// <copyright file=Scp049LoseSenseTargetEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp049
{
    /// <summary>
    ///     Triggered when SCP-049 loses its sense target.
    /// </summary>
    public class Scp049LoseSenseTargetEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp049LoseSenseTargetEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player losing the target.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file=Scp939PlaceCloudEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp939
{
    /// <summary>
    ///     Triggered when SCP-939 places a cloud of gas.
    /// </summary>
    public class Scp939PlaceCloudEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp939PlaceCloudEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player placing the cloud.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
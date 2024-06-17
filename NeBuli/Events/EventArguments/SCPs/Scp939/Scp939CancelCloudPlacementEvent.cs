// -----------------------------------------------------------------------
// <copyright file=Scp939CancelCloudPlacementEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp939
{
    /// <summary>
    ///     Triggered when SCP-939 cancels cloud placement.
    /// </summary>
    public class Scp939CancelCloudPlacementEvent : EventArgs, IPlayerEvent
    {
        public Scp939CancelCloudPlacementEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
        }

        /// <summary>
        ///     The player canceling the cloud placement.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
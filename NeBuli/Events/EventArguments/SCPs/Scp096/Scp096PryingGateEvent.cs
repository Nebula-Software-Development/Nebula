// -----------------------------------------------------------------------
// <copyright file=Scp096PryingGateEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.API.Features.Doors;
using Nebula.Events.EventArguments.Interfaces;
using PryableDoor = Interactables.Interobjects.PryableDoor;

namespace Nebula.Events.EventArguments.SCPs.Scp096
{
    /// <summary>
    ///     Triggered when SCP-096 is trying to pry open a gate.
    /// </summary>
    public class Scp096PryingGateEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp096PryingGateEvent(ReferenceHub player, PryableDoor door)
        {
            Player = API.Features.Player.Get(player);
            PryableDoor = (API.Features.Doors.PryableDoor)Door.Get(door);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the <see cref="API.Features.Doors.PryableDoor" /> being pried.
        /// </summary>
        public API.Features.Doors.PryableDoor PryableDoor { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player prying the door.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
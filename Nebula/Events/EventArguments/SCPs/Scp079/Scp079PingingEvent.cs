// -----------------------------------------------------------------------
// <copyright file=Scp079PingingEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.API.Features.Enum;
using Nebula.API.Features.Map;
using Nebula.Events.EventArguments.Interfaces;
using RelativePositioning;
using UnityEngine;

namespace Nebula.Events.EventArguments.SCPs.Scp079
{
    /// <summary>
    ///     Triggered when SCP-079 is pings.
    /// </summary>
    public class Scp079PingingEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp079PingingEvent(ReferenceHub player, RelativePosition pos, int powerCost, byte index, Vector3 syncPos)
        {
            Player = API.Features.Player.Get(player);
            Room = Room.Get(pos.Position);
            PingType = (Scp079PingType)index;
            PowerCost = powerCost;
            Position = pos.Position;
            SyncPosition = syncPos;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the <see cref="API.Features.Map.Room" /> the ping is in.
        /// </summary>
        public Room Room { get; }

        /// <summary>
        ///     Gets the <see cref="Vector3" /> position the ping is located at.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        ///     Gets or sets the power drain that the ping will take from the player.
        /// </summary>
        public int PowerCost { get; set; }

        /// <summary>
        ///     Gets the <see cref="Scp079PingType" /> of the ping.
        /// </summary>
        public Scp079PingType PingType { get; set; }

        /// <summary>
        ///     Gets the SyncedPosition of the ping.
        /// </summary>
        public Vector3 SyncPosition { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
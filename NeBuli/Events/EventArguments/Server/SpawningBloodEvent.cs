// -----------------------------------------------------------------------
// <copyright file=SpawningBloodEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;
using UnityEngine;

namespace Nebula.Events.EventArguments.Server
{
    /// <summary>
    ///     Triggered before blood spawns.
    /// </summary>
    public class SpawningBloodEvent : EventArgs, ICancellableEvent, IPlayerEvent
    {
        public SpawningBloodEvent(ReferenceHub hub, Ray ray, RaycastHit hit, IDestructible target)
        {
            Player = API.Features.Player.TryGet(hub, out API.Features.Player ply) ? ply : API.Features.Server.Host;
            Target = API.Features.Player.TryGet(target.NetworkId, out API.Features.Player dt) ? dt : null;
            Ray = ray;
            RaycastHit = hit;
            Position = hit.point;
            IsCancelled = false;
        }

        /// <summary>
        ///     The <see cref="UnityEngine.Ray" /> for placing the blood.
        /// </summary>
        public Ray Ray { get; set; }

        /// <summary>
        ///     The position the blood will be placed at.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        ///     The <see cref="UnityEngine.RaycastHit" /> for placing the blood.
        /// </summary>
        public RaycastHit RaycastHit { get; set; }

        /// <summary>
        ///     The <see cref="API.Features.Player" /> thats being attacked, or null if none.
        /// </summary>
        public API.Features.Player Target { get; }

        /// <summary>
        /// Gets or sets if the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The <see cref="API.Features.Player" /> placing the blood.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
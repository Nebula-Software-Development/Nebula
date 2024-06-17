// -----------------------------------------------------------------------
// <copyright file=Scp079ChangingCameraEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.API.Features.Map;
using Nebula.Events.EventArguments.Interfaces;
using PlayerRoles.PlayableScps.Scp079.Cameras;

namespace Nebula.Events.EventArguments.SCPs.Scp079
{
    /// <summary>
    ///     Triggered when SCP-079 is changing its camera.
    /// </summary>
    public class Scp079ChangingCameraEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp079ChangingCameraEvent(ReferenceHub player, float auxdrain, Scp079Camera camera)
        {
            Player = API.Features.Player.Get(player);
            AuxDrain = auxdrain;
            Camera = Camera.Get(camera);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the amount of power that will be drained.
        /// </summary>
        public float AuxDrain { get; set; }

        /// <summary>
        ///     Gets the <see cref="API.Features.Map.Camera" /> being switched to.
        /// </summary>
        public Camera Camera { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player changing the camera.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
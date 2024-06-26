﻿// -----------------------------------------------------------------------
// <copyright file=Scp079InteractingTeslaEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp079
{
    /// <summary>
    ///     Triggered when SCP-079 is interacting with a Tesla gate.
    /// </summary>
    public class Scp079InteractingTeslaEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp079InteractingTeslaEvent(ReferenceHub player, TeslaGate teslaGate)
        {
            Player = API.Features.Player.Get(player);
            TeslaGate = API.Features.Map.TeslaGate.Get(teslaGate);
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets the Tesla Gate being triggered.
        /// </summary>
        public API.Features.Map.TeslaGate TeslaGate { get; }

        /// <summary>
        ///     Gets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
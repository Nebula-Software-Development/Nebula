// -----------------------------------------------------------------------
// <copyright file=Scp939UseLungeEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebula.Events.EventArguments.Interfaces;

namespace Nebula.Events.EventArguments.SCPs.Scp939
{
    /// <summary>
    ///     Triggered when SCP-939 uses the lunge ability.
    /// </summary>
    public class Scp939UseLungeEvent : EventArgs, IPlayerEvent
    {
        public Scp939UseLungeEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
        }

        /// <summary>
        ///     Gets the player lunging.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}
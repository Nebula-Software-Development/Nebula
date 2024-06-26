﻿// -----------------------------------------------------------------------
// <copyright file=Scp330Handlers.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebula.Events.EventArguments.SCPs.Scp330;

namespace Nebula.Events.Handlers
{
    public static class Scp330Handlers
    {
        /// <summary>
        ///     Triggered when a player is interacting with SCP-330.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp330InteractEvent> Interacting;

        internal static void OnInteracting(Scp330InteractEvent ev)
        {
            Interacting.CallEvent(ev);
        }
    }
}
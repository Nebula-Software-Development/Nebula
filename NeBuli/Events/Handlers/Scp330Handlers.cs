// -----------------------------------------------------------------------
// <copyright file=Scp330Handlers.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.SCPs.Scp330;

namespace Nebuli.Events.Handlers;

public static class Scp330Handlers
{
    /// <summary>
    /// Triggered when a player is interacting with SCP-330.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp330InteractEvent> Interacting;

    internal static void OnInteracting(Scp330InteractEvent ev) => Interacting.CallEvent(ev);
}
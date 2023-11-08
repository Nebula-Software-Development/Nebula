// -----------------------------------------------------------------------
// <copyright file=Scp3114RevealedEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp3114;

/// <summary>
/// Triggered after SCP-3114 reveals.
/// </summary>
public class Scp3114RevealedEvent : EventArgs, IPlayerEvent
{
    public Scp3114RevealedEvent(ReferenceHub hub)
    {
        Player = NebuliPlayer.Get(hub);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public NebuliPlayer Player { get; }
}

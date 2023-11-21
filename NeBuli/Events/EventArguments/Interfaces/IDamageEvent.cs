// -----------------------------------------------------------------------
// <copyright file=IDamageEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.Interfaces;

public interface IDamageEvent
{
    /// <summary>
    /// The attacker of the damage handler.
    /// </summary>
    public API.Features.Player Attacker { get; }

    /// <summary>
    /// The Target of the damage handler.
    /// </summary>
    public API.Features.Player Target { get; }
}
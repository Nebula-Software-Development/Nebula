// -----------------------------------------------------------------------
// <copyright file=PlayerLeaveEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Mirror;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player leaves the server.
/// </summary>
public class PlayerLeaveEvent : EventArgs, IPlayerEvent
{
    public PlayerLeaveEvent(NetworkConnection conn)
    {       
        Player = NebuliPlayer.Get(conn.identity);
        if (Player != null && NebuliPlayer.Dictionary.ContainsValue(Player)) NebuliPlayer.Dictionary.Remove(Player.ReferenceHub);
    }

    /// <summary>
    /// The player calling the event.
    /// </summary>
    public NebuliPlayer Player { get; }
}
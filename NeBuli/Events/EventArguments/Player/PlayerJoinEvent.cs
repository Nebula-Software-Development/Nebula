using CentralAuth;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player joins the server.
/// </summary>
public class PlayerJoinEvent : EventArgs, IPlayerEvent
{
    public PlayerJoinEvent(PlayerAuthenticationManager authManager)
    {
        Player = new NebuliPlayer(authManager._hub);
    }

    /// <summary>
    /// The player calling the event.
    /// </summary>
    public NebuliPlayer Player { get; }
}
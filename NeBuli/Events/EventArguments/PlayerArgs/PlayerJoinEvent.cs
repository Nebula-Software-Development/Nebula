using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerJoinEvent : EventArgs, IPlayerEvent
{
    public PlayerJoinEvent(ServerRoles serverRoles)
    {
        Player = new API.Features.Player.Player(serverRoles._hub);
    }

    /// <summary>
    /// The player calling the event.
    /// </summary>
    public API.Features.Player.Player Player { get; }
}
using Mirror;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerLeaveEvent : EventArgs, IPlayerEvent
{
    public PlayerLeaveEvent(NetworkConnection conn)
    {
        Player = NebuliPlayer.Get(conn.identity);
    }

    /// <summary>
    /// The player calling the event.
    /// </summary>
    public NebuliPlayer Player { get; }
}
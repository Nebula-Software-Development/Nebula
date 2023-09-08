using Mirror;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerLeaveEvent : EventArgs, IPlayerEvent
{
    public PlayerLeaveEvent(NetworkConnection conn)
    {
        Player = API.Features.Player.Get(conn.identity);
    }

    /// <summary>
    /// The player calling the event.
    /// </summary>
    public API.Features.Player Player { get; }
}
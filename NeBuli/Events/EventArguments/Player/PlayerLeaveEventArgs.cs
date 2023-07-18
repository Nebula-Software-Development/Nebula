using System;
using Mirror;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerLeaveEventArgs : EventArgs, IPlayerEvent
{
    public PlayerLeaveEventArgs(NetworkConnection conn)
    {
        Player = NebuliPlayer.Get(conn.identity);
    }
    
    /// <summary>
    /// The player calling the event.
    /// </summary>
    public NebuliPlayer Player { get; }
}
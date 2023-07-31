using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939UseLunge : EventArgs, IPlayerEvent
{
    public Scp939UseLunge(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
    }
    
    /// <summary>
    /// Gets the player lunging.
    /// </summary>
    public NebuliPlayer Player { get; }
}
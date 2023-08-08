using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

public class Scp049CancelResurrect : EventArgs, IPlayerEvent
{
    public Scp049CancelResurrect(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
    }
    
    /// <summary>
    /// Gets the player cancelling the ressurect.
    /// </summary>
    public NebuliPlayer Player { get; }
}
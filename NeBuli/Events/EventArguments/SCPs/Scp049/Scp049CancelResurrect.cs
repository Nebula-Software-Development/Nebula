using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

public class Scp049CancelResurrect : EventArgs, IPlayerEvent
{
    public Scp049CancelResurrect(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
    }
    
    public NebuliPlayer Player { get; }
}
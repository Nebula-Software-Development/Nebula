using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939CancelCloudPlacement : EventArgs, IPlayerEvent
{
    public Scp939CancelCloudPlacement(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
    }
    
    public NebuliPlayer Player { get; }
}
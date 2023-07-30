using System;
using Nebuli.API.Features.Player;
using UnityEngine;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

public class Scp173Blink : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp173Blink(ReferenceHub player, Vector3 position)
    {
        Player = NebuliPlayer.Get(player);
        Position = position;
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }
    
    public Vector3 Position { get; }

    public bool IsCancelled { get; set; }
}
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

    /// <summary>
    /// Gets SCP-173
    /// </summary>
    public NebuliPlayer Player { get; }
    
    /// <summary>
    /// Gets the position SCP-173 will teleport to.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
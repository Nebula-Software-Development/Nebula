using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939CancelCloudPlacementEvent : EventArgs, IPlayerEvent
{
    public Scp939CancelCloudPlacementEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
    }

    /// <summary>
    /// The player canceling the cloud placement.
    /// </summary>
    public NebuliPlayer Player { get; }
}
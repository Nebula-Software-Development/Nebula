using CommandSystem;
using Footprinting;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerBannedEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerBannedEventArgs(Footprint target, ICommandSender issuer, string reason, long duration)
    {
        Player = NebuliPlayer.Get(target.Hub);
        Issuer = NebuliPlayer.Get(issuer);
        Reason = reason;
        Duration = duration;
        IsCancelled = false;
    }

    public bool IsCancelled { get; set; }

    public NebuliPlayer Player { get; }

    public NebuliPlayer Issuer { get; }

    public string Reason { get; set; }

    public long Duration { get; set; }
}
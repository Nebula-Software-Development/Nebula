using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerEscapingPocket : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerEscapingPocket(ReferenceHub player, bool successful)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
        Successful = successful;
    }
    public NebuliPlayer Player { get; }

    public bool Successful { get; }

    public bool IsCancelled { get; set; }
}

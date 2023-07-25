using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Server
{
    public class WarheadDetonating : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public WarheadDetonating(ReferenceHub player)
        {
            Player = NebuliPlayer.Get(player);
            IsCancelled = false;
        }

        public NebuliPlayer Player { get; }
        public bool IsCancelled { get; set; }
    }
}
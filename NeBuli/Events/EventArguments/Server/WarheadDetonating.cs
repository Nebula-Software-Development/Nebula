using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Server
{
    public class WarheadDetonating : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public WarheadDetonating(ReferenceHub player)
        {
            Player = NebuliPlayer.TryGet(player, out NebuliPlayer ply) ? ply : API.Features.Server.NebuliHost;
            IsCancelled = false;
        }

        /// <summary>
        /// The player triggering the event, will be the Host player if null.
        /// </summary>
        public NebuliPlayer Player { get; }

        /// <summary>
        /// Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}
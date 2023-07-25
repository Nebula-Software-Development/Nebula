using Nebuli.API.Features.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebuli.Events.EventArguments.Player
{
    public class PlayerTriggeringTesla : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerTriggeringTesla(ReferenceHub player, TeslaGate teslaGate, bool inIdleRange, bool isTriggerable)
        {
            Player = NebuliPlayer.Get(player);
            IsCancelled = false;
            IsInIdleRange = inIdleRange;
            IsTriggerable = isTriggerable;
        }
        public bool IsCancelled { get; set; }

        public NebuliPlayer Player { get; }

        public bool IsInIdleRange { get; set; }

        public bool IsTriggerable { get; set; }
    }
}

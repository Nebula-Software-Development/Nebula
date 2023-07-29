using Nebuli.API.Features.Player;
using System;
using Nebuli.API.Features.Map;

namespace Nebuli.Events.EventArguments.Player
{
    public class PlayerTriggeringTesla : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerTriggeringTesla(ReferenceHub player, TeslaGate teslaGate, bool inIdleRange, bool isTriggerable)
        {
            Player = NebuliPlayer.Get(player);
            TeslaGate = NebuliTeslaGate.Get(teslaGate);
            IsCancelled = false;
            IsInIdleRange = teslaGate.IsInIdleRange(player);
            IsTriggerable = NebuliTeslaGate.Get(teslaGate).InHurtRange(NebuliPlayer.Get(player));
        }
        public bool IsCancelled { get; set; }

        public NebuliPlayer Player { get; }
        
        public NebuliTeslaGate TeslaGate { get; }

        public bool IsInIdleRange { get; set; }

        public bool IsTriggerable { get; set; }
    }
}

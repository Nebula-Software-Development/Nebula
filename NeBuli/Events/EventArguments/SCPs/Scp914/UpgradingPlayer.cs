using Nebuli.API.Features.Player;
using Scp914;
using System;


namespace Nebuli.Events.EventArguments.SCPs.Scp914
{
    public class UpgradingPlayer : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public UpgradingPlayer(ReferenceHub ply, bool upgradeInventory, bool heldOnly, Scp914KnobSetting knobSetting)
        {
            Player = NebuliPlayer.Get(ply);
            IsCancelled = false;
            UpgradeInventory = upgradeInventory;
            HeldOnly = heldOnly;
            KnobSetting = knobSetting;
        }

        public NebuliPlayer Player { get; }

        public bool IsCancelled { get; set; }

        public bool UpgradeInventory { get; set; }

        public bool HeldOnly { get; set; }

        public Scp914KnobSetting KnobSetting { get; set; }
    }
}

using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using Scp914;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp914;

public class UpgradingItem : EventArgs, ICancellableEvent
{
    public UpgradingItem(ItemPickupBase pickup, bool upgradeDropped, Scp914KnobSetting knobSetting)
    {
        Pickup = Pickup.Get(pickup);
        IsCancelled = false;
        KnobSetting = knobSetting;
        UpgradeDropped = upgradeDropped;
    }

    public Pickup Pickup { get; }

    public bool IsCancelled { get; set; }

    public bool UpgradeDropped { get; set; }

    public Scp914KnobSetting KnobSetting { get; set; }
}


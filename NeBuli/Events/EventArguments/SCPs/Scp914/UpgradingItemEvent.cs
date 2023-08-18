using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Player;
using Scp914;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp914;

public class UpgradingItemEvent : EventArgs, ICancellableEvent
{
    public UpgradingItemEvent(ItemPickupBase pickup, bool upgradeDropped, Scp914KnobSetting knobSetting)
    {
        Pickup = Pickup.Get(pickup);
        IsCancelled = false;
        KnobSetting = knobSetting;
        UpgradeDropped = upgradeDropped;
    }

    /// <summary>
    /// Gets the <see cref="API.Features.Items.Pickups.Pickup"/> being upgraded.
    /// </summary>
    public Pickup Pickup { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets if 914 should upgrade dropped.
    /// </summary>
    public bool UpgradeDropped { get; set; }

    /// <summary>
    /// Gets or sets SCP-914s <see cref="Scp914KnobSetting"/>.
    /// </summary>
    public Scp914KnobSetting KnobSetting { get; set; }
}


using Nebuli.API.Features.Player;
using Scp914;
using System;


namespace Nebuli.Events.EventArguments.SCPs.Scp914;

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

    /// <summary>
    /// Gets the player being upgraded.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets if 914 should upgrade the players inventory.
    /// </summary>
    public bool UpgradeInventory { get; set; }

    /// <summary>
    /// Gets or sets if 914 is in HeldOnly mode.
    /// </summary>
    public bool HeldOnly { get; set; }

    /// <summary>
    /// Gets or sets SCP-914s <see cref="Scp914KnobSetting"/>.
    /// </summary>
    public Scp914KnobSetting KnobSetting { get; set; }
}

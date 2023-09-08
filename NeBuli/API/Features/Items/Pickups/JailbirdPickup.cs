using InventorySystem.Items.Jailbird;
using JailbirdPickupBase = InventorySystem.Items.Jailbird.JailbirdPickup;

namespace Nebuli.API.Features.Items.Pickups;

public class JailbirdPickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="JailbirdPickupBase"/> base.
    /// </summary>
    public new JailbirdPickupBase Base { get; }
    internal JailbirdPickup(JailbirdPickupBase pickupBase) : base(pickupBase)
    {
        Base = pickupBase;
    }

    /// <summary>
    /// Get or sets the JailBirds <see cref="JailbirdWearState"/>.
    /// </summary>
    public JailbirdWearState WearState
    {
        get => Base.Wear;
        set => Base.Wear = value;
    }
}

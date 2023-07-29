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
}

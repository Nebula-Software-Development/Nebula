using BodyArmorPickupBase = InventorySystem.Items.Armor.BodyArmorPickup;

namespace Nebuli.API.Features.Items.Pickups;

public class ArmorPickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="BodyArmorPickupBase"/> base.
    /// </summary>
    public new BodyArmorPickupBase Base { get; }

    internal ArmorPickup(BodyArmorPickupBase pickupBase) : base(pickupBase)
    {
        Base = pickupBase;
    }
}
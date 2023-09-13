namespace Nebuli.API.Features.Items.Pickups;

public class AmmoPickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="InventorySystem.Items.Firearms.Ammo.AmmoPickup"/> base.
    /// </summary>
    public new InventorySystem.Items.Firearms.Ammo.AmmoPickup Base { get; }

    internal AmmoPickup(InventorySystem.Items.Firearms.Ammo.AmmoPickup pickupBase) : base(pickupBase)
    {
        Base = pickupBase;
    }
}
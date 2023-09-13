namespace Nebuli.API.Features.Items.Pickups;

public class KeycardPickup : Pickup
{
    /// <summary>
    /// Gets the base of the pickup.
    /// </summary>
    public new InventorySystem.Items.Keycards.KeycardPickup Base { get; }

    internal KeycardPickup(InventorySystem.Items.Keycards.KeycardPickup pickupBase) : base(pickupBase)
    {
        Base = pickupBase;
    }
}
using MicroHIDPickupBase = InventorySystem.Items.MicroHID.MicroHIDPickup;

namespace Nebuli.API.Features.Items.Pickups;

public class MicroHIDPickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="MicroHIDPickupBase"/> base.
    /// </summary>
    public new MicroHIDPickupBase Base { get; }
    internal MicroHIDPickup(MicroHIDPickupBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets or sets the Mirco-HID's current energy level.
    /// </summary>
    public float EnergyLevel
    {
        get => Base.NetworkEnergy; 
        set => Base.NetworkEnergy = value;
    }
}

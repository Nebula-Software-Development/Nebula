using MicroHIDBase = InventorySystem.Items.MicroHID.MicroHIDItem;

namespace Nebuli.API.Features.Items;

/// <summary>
/// MicroHID wrapper class.
/// </summary>
public class MicroHID : Item
{
    /// <summary>
    /// Gets the base object representing this MicroHID item.
    /// </summary>
    public new MicroHIDBase Base { get; }

    internal MicroHID(MicroHIDBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets the energy value represented as a byte.
    /// </summary>
    public byte EnergyToByte => Base.EnergyToByte;

    /// <summary>
    /// Gets the remaining energy of the MicroHID.
    /// </summary>
    public float RemainingEnergy => Base.RemainingEnergy;

    /// <summary>
    /// Gets the readiness level of the MicroHID.
    /// </summary>
    public float Readiness => Base.Readiness;

    /// <summary>
    /// Recharges the MicroHID.
    /// </summary>
    public void Recharge() => Base.Recharge();

    /// <summary>
    /// Fires the MicroHID.
    /// </summary>
    public void Fire() => Base.Fire();
}
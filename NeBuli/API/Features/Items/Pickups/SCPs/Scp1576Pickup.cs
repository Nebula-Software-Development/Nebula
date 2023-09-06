using ItemBase = InventorySystem.Items.Usables.Scp1576.Scp1576Pickup;

namespace Nebuli.API.Features.Items.Pickups.SCPs;

public class Scp1576Pickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="ItemBase"/> base.
    /// </summary>
    public new ItemBase Base { get; }
    internal Scp1576Pickup(ItemBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }
}

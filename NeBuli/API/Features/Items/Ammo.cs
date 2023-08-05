using InventorySystem.Items;


namespace Nebuli.API.Features.Items;

public class Ammo : Item
{
    /// <summary>
    /// Gets the <see cref="InventorySystem.Items.Firearms.Ammo.AmmoItem"/> base.
    /// </summary>
    public new InventorySystem.Items.Firearms.Ammo.AmmoItem Base { get; }
    internal Ammo(InventorySystem.Items.Firearms.Ammo.AmmoItem itemBase) : base(itemBase)
    {
        Base = itemBase;
    }
}

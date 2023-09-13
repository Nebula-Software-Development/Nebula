using Scp1576Base = InventorySystem.Items.Usables.Scp1576.Scp1576Item;

namespace Nebuli.API.Features.Items.SCPs;

public class Scp1576 : Item
{
    /// <summary>
    /// Gets the <see cref="Scp1576Base"/> base.
    /// </summary>
    public new Scp1576Base Base { get; }

    internal Scp1576(Scp1576Base itemBase) : base(itemBase)
    {
        Base = itemBase;
    }
}
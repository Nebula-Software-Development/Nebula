using Scp244Base = InventorySystem.Items.Usables.Scp244.Scp244Item;

namespace Nebuli.API.Features.Items.SCPs;

public class Scp244 : Usable
{
    /// <summary>
    /// Gets the <see cref="Scp244Base"/> base.
    /// </summary>
    public new Scp244Base Base { get; }

    internal Scp244(Scp244Base itemBase) : base(itemBase)
    {
        Base = itemBase;
    }
}
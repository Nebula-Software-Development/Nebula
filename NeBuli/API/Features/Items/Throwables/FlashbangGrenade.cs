using InventorySystem.Items.ThrowableProjectiles;

namespace Nebuli.API.Features.Items.Throwables;

public class FlashbangGrenade : Throwable
{
    /// <summary>
    /// Gets the <see cref="ThrowableItem"/> base.
    /// </summary>
    public new ThrowableItem Base { get; }
    internal FlashbangGrenade(ThrowableItem grenadeBase) : base(grenadeBase)
    {
        Base = grenadeBase;
    }
}

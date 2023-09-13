namespace Nebuli.API.Features.Items;

public class Coin : Item
{
    /// <summary>
    /// Gets the coins base.
    /// </summary>
    public new InventorySystem.Items.Coin.Coin Base { get; }

    internal Coin(InventorySystem.Items.Coin.Coin itemBase) : base(itemBase)
    {
        Base = itemBase;
    }
}
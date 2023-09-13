using JailbirdBase = InventorySystem.Items.Jailbird.JailbirdItem;

namespace Nebuli.API.Features.Items.SCPs;

/// <summary>
/// Jailbird wrapper class.
/// </summary>
public class Jailbird : Item
{
    /// <summary>
    /// Gets the <see cref="JailbirdBase"/> base.
    /// </summary>
    public new JailbirdBase Base { get; }

    internal Jailbird(JailbirdBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets or sets the total charges performed.
    /// </summary>
    public int TotalChargesPerformed
    {
        get => Base.TotalChargesPerformed;
        set => Base.TotalChargesPerformed = value;
    }
}
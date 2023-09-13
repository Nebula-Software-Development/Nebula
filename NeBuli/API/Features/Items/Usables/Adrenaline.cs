using AdrenalineBase = InventorySystem.Items.Usables.Adrenaline;

namespace Nebuli.API.Features.Items.Usables;

public class Adrenaline : Item
{
    /// <summary>
    /// Gets the <see cref="AdrenalineBase"/> base.
    /// </summary>
    public new AdrenalineBase Base { get; }

    internal Adrenaline(AdrenalineBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Activates the usable's effects.
    /// </summary>
    public void ActivateEffect() => Base.ActivateEffects();

    /// <summary>
    /// Gets if the usable is ready to be activated.
    /// </summary>
    public bool ActivationReady => Base.ActivationReady;

    /// <summary>
    /// Gets or sets if the usable is ready to be used.
    /// </summary>
    public bool CanStartUsing
    {
        get => Base.CanStartUsing;
        set => Base.CanStartUsing = value;
    }
}
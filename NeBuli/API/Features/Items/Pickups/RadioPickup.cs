using RadioPickupBase = InventorySystem.Items.Radio.RadioPickup;


namespace Nebuli.API.Features.Items.Pickups;

public class RadioPickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="RadioPickupBase"/> base.
    /// </summary>
    public new RadioPickupBase Base { get; }
    internal RadioPickup(RadioPickupBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets or sets the radios current saved battery.
    /// </summary>
    public float SavedBattery
    {
        get => Base.SavedBattery; 
        set => Base.SavedBattery = value;
    }

    /// <summary>
    /// Gets or sets if the radio is on.
    /// </summary>
    public bool IsOn
    {
        get => Base.SavedEnabled; 
        set => Base.NetworkSavedEnabled = value;
    }

    /// <summary>
    /// Gets or sets the saved range.
    /// </summary>
    public byte SavedRange
    {
        get => Base.SavedRange; 
        set => Base.NetworkSavedRange = value;
    }
}

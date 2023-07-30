using static InventorySystem.Items.Radio.RadioMessages;
using RadioItemBase = InventorySystem.Items.Radio.RadioItem;

namespace Nebuli.API.Features.Items;

/// <summary>
/// Wrapper class for Radios.
/// </summary>
public class Radio : Item
{
    /// <summary>
    /// Gets the <see cref="RadioItemBase"/> base.
    /// </summary>
    public new RadioItemBase Base { get; }
    internal Radio(RadioItemBase itemBase) : base(itemBase)
    { 
        Base = itemBase;
    }

    /// <summary>
    /// Gets the radios current battery percentage.
    /// </summary>
    public byte BatteryPercent => Base.BatteryPercent;

    /// <summary>
    /// Gets if the radio is usable.
    /// </summary>
    public bool IsUsable => Base.IsUsable;

    /// <summary>
    /// Gets the radios current <see cref="RadioRangeLevel"/>.
    /// </summary>
    public RadioRangeLevel CurrentRange => Base.RangeLevel;

}

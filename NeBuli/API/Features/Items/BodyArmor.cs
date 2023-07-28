using InventorySystem;
using InventorySystem.Items.Armor;
using BodyArmorBase = InventorySystem.Items.Armor.BodyArmor;

namespace Nebuli.API.Features.Items;

/// <summary>
/// Wrapper class for body armors
/// </summary>
public class BodyArmor : Item
{
    /// <summary>
    /// Gets the base data for this body armor.
    /// </summary>
    public new BodyArmorBase Base { get; }

    internal BodyArmor(BodyArmorBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets an array of armor ammo limits for this body armor.
    /// </summary>
    public BodyArmorBase.ArmorAmmoLimit[] AmmoLimit => Base.AmmoLimits;

    /// <summary>
    /// Gets an array of armor category limit modifiers for this body armor.
    /// </summary>
    public BodyArmorBase.ArmorCategoryLimitModifier[] ArmorCategoryLimitModifiers => Base.CategoryLimits;

    /// <summary>
    /// Gets or sets a value indicating whether to remove excess guns/ammo on drop.
    /// </summary>
    public bool DontRemoveExcessOnDrop
    {
        get => Base.DontRemoveExcessOnDrop;
        set => Base.DontRemoveExcessOnDrop = value;
    }

    /// <summary>
    /// Gets a value indicating whether this body armor is currently being worn.
    /// </summary>
    public bool IsBeingWorn => Base.IsWorn;

    /// <summary>
    /// Gets the efficacy of this body armor.
    /// </summary>
    public float Efficacy => Base.VestEfficacy;

    /// <summary>
    /// Attempts to retrieve a <see cref="BodyArmorBase"/> object from the specified <paramref name="inventory"/>.
    /// </summary>
    /// <param name="inventory">The inventory from which the armor is to be retrieved.</param>
    /// <param name="armor">An output parameter that will store the retrieved <see cref="BodyArmorBase"/> object if successful.</param>
    public static void TryGetArmor(Inventory inventory, out BodyArmorBase armor)
    {
        BodyArmorUtils.TryGetBodyArmor(inventory, out armor);
    }

    /// <summary>
    /// Processes damage considering the efficacy, bullet penetration, and original damage.
    /// </summary>
    /// <param name="efficacy">The efficacy of the body armor as an integer value (0 to 100).</param>
    /// <param name="damage">The original damage value before armor reduction.</param>
    /// <param name="bulletPenetrationPercent">The percentage of bullet penetration (0 to 100).</param>
    /// <returns>The processed damage value after considering the body armor's efficacy and bullet penetration.</returns>
    public static float ProcessDamage(int efficacy, float damage, int bulletPenetrationPercent) => BodyArmorUtils.ProcessDamage(efficacy, damage, bulletPenetrationPercent);

}


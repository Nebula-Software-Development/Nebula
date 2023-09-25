using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Keycards;
using Nebuli.API.Extensions;

namespace Nebuli.API.Features.Items.Pickups;

public class KeycardPickup : Pickup
{
    /// <summary>
    /// Gets the base of the pickup.
    /// </summary>
    public new InventorySystem.Items.Keycards.KeycardPickup Base { get; }

    internal KeycardPickup(InventorySystem.Items.Keycards.KeycardPickup pickupBase) : base(pickupBase)
    {
        Base = pickupBase;
        if (ItemType.GetItemBase() is KeycardItem item)
            KeycardPermissions = item.Permissions;
    }

    /// <summary>
    /// Gets the <see cref="KeycardPickup"/> <see cref="Interactables.Interobjects.DoorUtils.KeycardPermissions"/>.
    /// </summary>
    public KeycardPermissions KeycardPermissions { get; } = KeycardPermissions.None;
}
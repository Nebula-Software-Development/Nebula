using HarmonyLib;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;
using InventorySystem.Items.Usables;
using Nebuli.API.Features.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nebuli.API.Features.Items;

public class Item
{
    /// <summary>
    /// Gets a Dictionary of <see cref="ItemBase"/>, and their wrapper class <see cref="Item"/>.
    /// </summary>
    public static Dictionary<ItemBase, Item> Dictionary = new();

    /// <summary>
    /// Gets the Item's base.
    /// </summary>
    public ItemBase Base { get; }

    /// <summary>
    /// Gets the owner's <see cref="ReferenceHub"/>.
    /// </summary>
    public ReferenceHub OwnerRefHub => Base.Owner;

    /// <summary>
    /// Tries to get the <see cref="NebuliPlayer"/> (owner) of the Item.
    /// </summary>
    public NebuliPlayer Owner => NebuliPlayer.Get(OwnerRefHub);

    /// <summary>
    /// Creates a new item by wrapping a <see cref="ItemBase"/>.
    /// </summary>
    /// <param name="itemBase">The <see cref="ItemBase"/> to wrap.</param>
    public Item(ItemBase itemBase)
    {
        Base = itemBase;
        if (!Dictionary.ContainsKey(itemBase))
        {
            Dictionary.Add(itemBase, this);
            HandleDerivedItemCreation();
        }
    }

    private void HandleDerivedItemCreation()
    {
        switch (Base)
        {
            case InventorySystem.Items.Keycards.KeycardItem keycard:
                ConvertToDerivedItem(new Keycard(keycard));
                break;

            case InventorySystem.Items.Firearms.Firearm firearm:
                ConvertToDerivedItem(new Firearm(firearm));
                break;

            case InventorySystem.Items.Coin.Coin coin:
                ConvertToDerivedItem(new Coin(coin));
                break;

            case InventorySystem.Items.Armor.BodyArmor armor:
                ConvertToDerivedItem(new BodyArmor(armor));
                break;

            case InventorySystem.Items.Flashlight.FlashlightItem flashlight:
                ConvertToDerivedItem(new Flashlight(flashlight));
                break;

            case InventorySystem.Items.MicroHID.MicroHIDItem microHID:
                ConvertToDerivedItem(new MicroHID(microHID));
                break;

            case InventorySystem.Items.Radio.RadioItem radio:
                ConvertToDerivedItem(new Radio(radio));
                break;

            case InventorySystem.Items.Jailbird.JailbirdItem jailbird:
                ConvertToDerivedItem(new Jailbird(jailbird));
                break;

            case Adrenaline adrenaline:
                ConvertToDerivedItem(new Usables.Adrenaline(adrenaline));
                break;

            case Medkit medkit:
                ConvertToDerivedItem(new Usables.Medkit(medkit));
                break;

            case Painkillers painkillers:
                ConvertToDerivedItem(new Usables.Painkillers(painkillers));
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Item"/> class with the specified item type and owner.
    /// </summary>
    /// <param name="newItemType">The type of the new item.</param>
    public Item(ItemType newItemType) => Server.NebuliHost.ReferenceHub.inventory.CreateItemInstance(new(newItemType, 0), false);

    /// <summary>
    /// Gets a collection of all the current Items.
    /// </summary>
    public static IEnumerable<Item> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the current Items.
    /// </summary>
    public static List<Item> List => Collection.ToList();

    /// <summary>
    /// Gets the items <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets or sets the Items serial.
    /// </summary>
    public ushort Serial
    {
        get => Base.ItemSerial;
        set => Base.ItemSerial = value;
    }

    /// <summary>
    /// Gets the Item name.
    /// </summary>
    public string Name => Base.name;

    /// <summary>
    /// Gets or sets the Item's category.
    /// </summary>
    public ItemCategory ItemCategory
    {
        get => Base.Category;
        set => Base.Category = value;
    }

    /// <summary>
    /// Gets or sets the Item's description type.
    /// </summary>
    public ItemDescriptionType ItemDescriptionType
    {
        get => Base.DescriptionType;
        set => Base.DescriptionType = value;
    }

    /// <summary>
    /// Gets or sets the ItemType.
    /// </summary>
    public ItemType ItemType
    {
        get => Base.ItemTypeId;
        set => Base.ItemTypeId = value;
    }

    /// <summary>
    /// Gets or sets the ItemTierFlags.
    /// </summary>
    public ItemTierFlags ItemTierFlags
    {
        get => Base.TierFlags;
        set => Base.TierFlags = value;
    }

    /// <summary>
    /// Returns a value determening if this item can be equipped.
    /// </summary>
    public bool CanEquip => Base.CanEquip();

    /// <summary>
    /// Returns a value determening if this item can be holstered.
    /// </summary>
    public bool CanHostler => Base.CanHolster();

    /// <summary>
    /// Drops the item.
    /// </summary>
    public void DropItem() => Base.ServerDropItem();

    /// <summary>
    /// Creates a new instance of the <see cref="Item"/> class with the specified item type and owner.
    /// </summary>
    /// <param name="itemType">The type of the new item.</param>
    public static void Create(ItemType itemType)
    {
        Server.NebuliHost.ReferenceHub.inventory.CreateItemInstance(new(itemType, 0), false);
    }

    /// <summary>
    /// Creates a item and gives it to the specified player.
    /// </summary>
    /// <param name="itemType">The ItemType to give. </param>>
    /// <param name="owner">The owner of the </param>
    /// <param name="attachments">The attachments on the weapon.</param>
    public static void CreateAndGive(ItemType itemType, NebuliPlayer owner, Attachment[] attachments = null)
    {
        ItemBase item = owner.Inventory.ServerAddItem(itemType);
        if (item is InventorySystem.Items.Firearms.Firearm firearm)
        {
            FirearmStatusFlags flags = FirearmStatusFlags.MagazineInserted;
            if (attachments is not null)
            {
                firearm.Attachments.AddRangeToArray(attachments);
            }
            firearm.Status = new FirearmStatus(firearm.AmmoManagerModule.MaxAmmo, flags, firearm.GetCurrentAttachmentsCode());
        }
    }

    /// <summary>
    /// Tries to get a <see cref="Item"/> with a <see cref="ItemBase"/>. If one cannot be found, it is created.
    /// </summary>
    /// <param name="itemBase">The <see cref="ItemBase"/> to find the <see cref="Item"/> with.</param>
    /// <returns></returns>
    public static Item Get(ItemBase itemBase) => Dictionary.TryGetValue(itemBase, out var item) ? item : new Item(itemBase);

    /// <summary>
    /// Gets an <see cref="Item"/> with the specified serial number.
    /// </summary>
    /// <param name="serialNumber">The serial number of the item to find.</param>
    /// <returns>The <see cref="Item"/> with the specified serial number if found; otherwise, null.</returns>
    public static Item Get(ushort serialNumber) => Dictionary.Values.FirstOrDefault(item => item.Serial == serialNumber);

    private void ConvertToDerivedItem(Item derivedItem)
    {
        Dictionary[Base] = derivedItem;
    }

}
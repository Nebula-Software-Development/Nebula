using HarmonyLib;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using InventorySystem.Items.Usables;
using InventorySystem.Items.Usables.Scp1576;
using InventorySystem.Items.Usables.Scp244;
using InventorySystem.Items.Usables.Scp330;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Items.SCPs;
using Nebuli.API.Features.Items.Throwables;
using Nebuli.API.Features.Map;
using Nebuli.API.Features.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExplosionGrenade = InventorySystem.Items.ThrowableProjectiles.ExplosionGrenade;

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
    internal Item(ItemBase itemBase)
    {
        Base = itemBase;
        if (!Dictionary.ContainsKey(itemBase)) Dictionary.Add(itemBase, this);
    }

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
    /// Gets the room the <see cref="Item"/> is currently in.
    /// </summary>
    public Room Room => Room.Get(Position);

    /// <summary>
    /// Gets the Items weight.
    /// </summary>
    public float Weight => Base.Weight;

    /// <summary>
    /// Gets the <see cref="Item"/> position.
    /// </summary>
    public Vector3 Position => Base.transform.position;

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
    /// Creates a new instance of the <see cref="Item"/> class with the specified item type.
    /// </summary>
    /// <param name="itemType">The type of the new item.</param>
    public static Item Create(ItemType itemType)
    {
        return Get(Server.NebuliHost.ReferenceHub.inventory.CreateItemInstance(new(itemType, 0), false));
    }

    /// <summary>
    /// Creates a pickup.
    /// </summary>
    /// <param name="position">The position of the pickup.</param>
    /// <param name="rotation">The rotation of the pickup.</param>
    /// <param name="SpawnItem">If the pickup should be spawned in-game.</param>
    public Pickup CreatePickup(Vector3 position, Quaternion rotation = default, bool SpawnItem = true)
    {
        ItemPickupBase item = Object.Instantiate(Base.PickupDropModel, position, rotation);
        item.Info = new PickupSyncInfo(ItemType, Weight, ItemSerialGenerator.GenerateNext());
        Pickup pickup = Pickup.Get(item);
        if (SpawnItem) pickup.Spawn();
        return pickup;
    }

    /// <summary>
    /// Gives the item to a player.
    /// </summary>
    public void Give(NebuliPlayer ply) => ply.AddItem(this);

    /// <summary>
    /// Removes and destroys the item from the owners inventory.
    /// </summary>
    public void Destroy() => Owner.RemoveItem(this);

    /// <summary>
    /// Creates a item and gives it to the specified player.
    /// </summary>
    /// <param name="itemType">The ItemType to give. </param>>
    /// <param name="owner">The owner of the item.</param>
    /// <param name="attachments">The attachments on the weapon.</param>
    public static Item CreateAndGive(ItemType itemType, NebuliPlayer owner, Attachment[] attachments = null)
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
            return Get(item);
        }
        return Get(item);
    }

    /// <summary>
    /// Tries to get a <see cref="Item"/> with a <see cref="ItemBase"/>. If one cannot be found, it is created.
    /// </summary>
    /// <param name="itemBase">The <see cref="ItemBase"/> to find the <see cref="Item"/> with.</param>
    /// <returns></returns>
    public static Item Get(ItemBase itemBase) => Dictionary.TryGetValue(itemBase, out Item item) ? item : GetItem(itemBase);

    /// <summary>
    /// Gets an <see cref="Item"/> with the specified serial number.
    /// </summary>
    /// <param name="serialNumber">The serial number of the item to find.</param>
    /// <returns>The <see cref="Item"/> with the specified serial number if found; otherwise, null.</returns>
    public static Item Get(ushort serialNumber) => Dictionary.Values.FirstOrDefault(item => item.Serial == serialNumber);

    internal static Item GetItem(ItemBase itemBase)
    {
        if(Dictionary.ContainsKey(itemBase)) return Dictionary[itemBase];

        return itemBase switch
        {
            InventorySystem.Items.Firearms.Firearm firearm => new Firearm(firearm),
            InventorySystem.Items.Keycards.KeycardItem keycard => new Keycard(keycard),
            InventorySystem.Items.Coin.Coin coin => new Coin(coin),
            InventorySystem.Items.Armor.BodyArmor armor => new BodyArmor(armor),
            InventorySystem.Items.Flashlight.FlashlightItem flashlight => new Flashlight(flashlight),
            InventorySystem.Items.MicroHID.MicroHIDItem microHID => new MicroHID(microHID),
            InventorySystem.Items.Firearms.Ammo.AmmoItem ammo => new Ammo(ammo),
            InventorySystem.Items.Radio.RadioItem radio => new Radio(radio),
            InventorySystem.Items.Jailbird.JailbirdItem jailbird => new Jailbird(jailbird),
            UsableItem usableItem => usableItem switch
            {
                Scp330Bag scp330 => new Scp330(scp330),
                Adrenaline adreniline => new Usables.Adrenaline(adreniline),
                Medkit medkit => new Usables.Medkit(medkit),
                Painkillers painkillers => new Usables.Painkillers(painkillers),
                Scp244Item scp244Item => new Scp244(scp244Item),
                Scp1576Item scp1576 => new Scp1576(scp1576),
                _ => new Item(usableItem),
            },

            ThrowableItem throwable => throwable.Projectile switch
            {
                ExplosionGrenade => new ExplosiveGrenade(throwable),
                InventorySystem.Items.ThrowableProjectiles.FlashbangGrenade => new Throwables.FlashbangGrenade(throwable),
                _ => new Throwable(throwable),
            },
            _ => new Item(itemBase),
        };
    }
}
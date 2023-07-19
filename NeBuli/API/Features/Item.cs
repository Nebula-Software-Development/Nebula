using InventorySystem.Items;
using Nebuli.API.Features.Player;
using System.Collections.Generic;
using System.Linq;

namespace Nebuli.API.Features
{
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

        internal Item(ItemBase itemBase)
        {
            Base = itemBase;
            Dictionary.Add(itemBase, this);
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
        /// Gets or sets the Items serial.
        /// </summary>
        public ushort Serial
        {
            get => Base.ItemSerial;
            set => Base.ItemSerial = value;
        }

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
        /// Tries to get a <see cref="Item"/> with a <see cref="ItemBase"/>. If one cannot be found, it is created.
        /// </summary>
        /// <param name="itemBase">The <see cref="ItemBase"/> to find the <see cref="Item"/> with.</param>
        /// <returns></returns>
        public static Item ItemGet(ItemBase itemBase) => Dictionary.TryGetValue(itemBase, out var item) ? item : new Item(itemBase);
    }
}

using Footprinting;
using InventorySystem.Items.Pickups;
using Mirror;
using Nebuli.API.Features.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nebuli.API.Features.Items.Pickups;

/// <summary>
/// Wrapper class for handling <see cref="ItemPickupBase"/>.
/// </summary>
public class Pickup
{
    /// <summary>
    /// A Dictionary of <see cref="ItemPickupBase"/>, and their wrapper class <see cref="Pickup"/>.
    /// </summary>
    public static Dictionary<ItemPickupBase, Pickup> Dictionary = new();

    private bool Spawned = false;

    /// <summary>
    /// Gets the <see cref="ItemPickupBase"/>.
    /// </summary>
    public ItemPickupBase Base { get; }

    internal Pickup(ItemPickupBase pickupBase)
    {
        Base = pickupBase;
        Dictionary.Add(pickupBase, this);
    }

    /// <summary>
    /// Gets a collection of all the current Pickups.
    /// </summary>
    public static IEnumerable<Pickup> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the current Pickups.
    /// </summary>
    public static List<Pickup> List => Collection.ToList();

    /// <summary>
    /// Gets or sets the Pickup's <see cref="PickupSyncInfo"/>.
    /// </summary>
    public PickupSyncInfo Info
    {
        get => Base.NetworkInfo;
        set => Base.Info = value;
    }

    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the pickups serial.
    /// </summary>
    public ushort Serial => Base.Info.Serial;

    /// <summary>
    /// Gets or sets if the pickup is locked.
    /// </summary>
    public bool Locked
    {
        get => Base.Info.Locked;
        set => Base.Info.Locked = value;
    }

    /// <summary>
    /// Sets the search time needed to pick this item up by the specified player.
    /// </summary>
    /// <param name="player">The player to set it to.</param>
    public void SearchTime(NebuliPlayer player) => Base.SearchTimeForPlayer(player.ReferenceHub);

    /// <summary>
    /// Sets the search time needed to pick this item up by the specified ReferenceHub.
    /// </summary>
    /// <param name="hub">The ReferenceHub to set it to.</param>
    public void SearchTime(ReferenceHub hub) => Base.SearchTimeForPlayer(hub);

    /// <summary>
    /// Gets the pickups ItemType.
    /// </summary>
    public ItemType ItemType => Base.Info.ItemId;

    /// <summary>
    /// Gets or sets the Items WeightKg.
    /// </summary>
    public float ItemWeightKg
    {
        get => Base.Info.WeightKg;
        set => Base.Info.WeightKg = value;
    }

    /// <summary>
    /// Gets or sets if the pickup is in use.
    /// </summary>
    public bool InUse
    {
        get => Base.Info.InUse;
        set => Base.Info.InUse = value;
    }

    /// <summary>
    /// Gets the Pickup's NetId.
    /// </summary>
    public uint NetID => Base.netId;

    /// <summary>
    /// Gets or sets the Pickup's <see cref="PickupPhysicsModule"/>.
    /// </summary>
    public PickupPhysicsModule PhysicsModule
    {
        get => Base.PhysicsModule;
        set => Base.PhysicsModule = value;
    }

    /// <summary>
    /// Gets the previous owner's <see cref="Footprint"/>
    /// </summary>
    public Footprint PreviousOwnerFootPrint
    {
        get => Base.PreviousOwner;
        set => Base.PreviousOwner = value;
    }

    /// <summary>
    /// Gets the previous owner's ReferenceHub.
    /// </summary>
    public ReferenceHub PreviousOwnerRefHub
    {
        get => ReferenceHub.GetHub(Base.PreviousOwner.PlayerId);
        set => Base.PreviousOwner = new Footprint(value);
    }

    /// <summary>
    /// Gets the previous owner as a <see cref="NebuliPlayer"/>.
    /// </summary>
    public NebuliPlayer PreviousOwner
    {
        get => NebuliPlayer.Get(Base.PreviousOwner);
        set => Base.PreviousOwner = value.Footprint;
    }

    /// <summary>
    /// Gets or sets the position of the pickup.
    /// </summary>
    public Vector3 Position
    {
        get => Base.Position;
        set => Base.Position = value;
    }

    public Quaternion Rotation
    {
        get => Base.Rotation;
        set => Base.Rotation = value;
    }

    /// <summary>
    /// Spawns a pickup at the specified position and rotation with an optional previous owner.
    /// </summary>
    /// <param name="pickup">The <see cref="Pickup"/> to spawn.</param>
    /// <param name="position">The position where the pickup should spawn.</param>
    /// <param name="rotation">The rotation of the pickup when spawned.</param>
    /// <param name="oldOwner">The previous owner of the pickup. If null, the server's host player will be the previous owner.</param>
    public static void SpawnPickup(Pickup pickup, Vector3 position, Quaternion rotation, NebuliPlayer oldOwner = null)
    {
        oldOwner ??= Server.NebuliHost;
        pickup.Position = position;
        pickup.Rotation = rotation;
        pickup.PreviousOwner = oldOwner;
        pickup.Spawn();
    }

    /// <summary>
    /// Destroys the pickup.
    /// </summary>
    public void Destroy()
    {
        Dictionary.Remove(Base);
        Base.DestroySelf();
    }

    /// <summary>
    /// Tries to get a <see cref="Pickup"/> with a <see cref="ItemPickupBase"/>. If one cannot be found, it is created.
    /// </summary>
    /// <param name="itemPickupBase">The <see cref="ItemPickupBase"/> to find the <see cref="Pickup"/> with.</param>
    /// <returns></returns>
    public static Pickup PickupGet(ItemPickupBase itemPickupBase) => Dictionary.TryGetValue(itemPickupBase, out Pickup pickup) ? pickup : new Pickup(itemPickupBase);

    /// <summary>
    /// Spawns the pickup.
    /// </summary>
    public void Spawn()
    {
        if (Spawned)
            return;

        NetworkServer.Spawn(GameObject);
        Spawned = true;
        return;
    }

    /// <summary>
    /// Despawns the pickup.
    /// </summary>
    public void Despawn()
    {
        if (!Spawned)
            return;
        NetworkServer.UnSpawn(GameObject);
        Spawned = false;
        return;
    }

    internal static Pickup GetPickup(ItemPickupBase ItemBase)
    {
        return ItemBase switch
        {
            InventorySystem.Items.Firearms.FirearmPickup firearm => new FirearmPickup(firearm),
            InventorySystem.Items.Keycards.KeycardPickup keycard => new KeycardPickup(keycard),
            InventorySystem.Items.Armor.BodyArmorPickup armorPickup => new ArmorPickup(armorPickup),
            InventorySystem.Items.Jailbird.JailbirdPickup jailbirdPickup => new JailbirdPickup(jailbirdPickup),
            _ => null,
        };
    }
}
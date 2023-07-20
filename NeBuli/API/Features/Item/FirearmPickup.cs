using Footprinting;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Pickups;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FirearmPickupBase = InventorySystem.Items.Firearms.FirearmPickup;

namespace Nebuli.API.Features.Item;

/// <summary>
/// Represents a wrapper class for FirearmPickupBase.
/// </summary>
public class FirearmPickup : Pickup
{
    /// <summary>
    /// Gets the collection of <see cref="FirearmPickupBase"/> and their wrapper class <see cref="FirearmPickup"/>.
    /// </summary>
    public static new Dictionary<FirearmPickupBase, FirearmPickup> Dictionary = new();

    /// <summary>
    /// Gets the FirearmPickup base.
    /// </summary>
    public new FirearmPickupBase Base;

    /// <summary>
    /// Gets the PickupSyncInfo of the FirearmPickup.
    /// </summary>
    public new PickupSyncInfo Info => Base.Info;

    public FirearmPickup(FirearmPickupBase pickupBase) : base(pickupBase)
    {
        Base = pickupBase;
        Dictionary.Add(Base, this);
    }

    /// <summary>
    /// Gets a collection of all the current FirearmPickups.
    /// </summary>
    public static new IEnumerable<FirearmPickup> Collection = Dictionary.Values;

    /// <summary>
    /// Gets a list of all the current FirearmPickups.
    /// </summary>
    public static new List<FirearmPickup> List = Collection.ToList();

    /// <summary>
    /// Gets the Transform of the FirearmPickup.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the Rigidbody of the FirearmPickup.
    /// </summary>
    public Rigidbody Rigidbody => Base.Rb;

    /// <summary>
    /// Gets the GameObject of the FirearmPickup.
    /// </summary>
    public new GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets or sets the PickupPhysicsModule of the FirearmPickup.
    /// </summary>
    public new PickupPhysicsModule PhysicsModule
    {
        get => Base.PhysicsModule;
        set => Base.PhysicsModule = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the FirearmPickup is distributed or not.
    /// </summary>
    public bool Distributed
    {
        get => Base.Distributed;
        set => Base.Distributed = value;
    }

    /// <summary>
    /// Gets or sets the FirearmStatus of the FirearmPickup.
    /// </summary>
    public FirearmStatus FirearmStatus
    {
        get => Base.Status;
        set => Base.Status = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the FirearmPickup is in use or not.
    /// </summary>
    public new bool InUse
    {
        get => Info.InUse;
        set => Base.Info.InUse = value;
    }

    /// <summary>
    /// Gets or sets the serial number of the FirearmPickup.
    /// </summary>
    public new ushort Serial
    {
        get => Info.Serial;
        set => Base.Info.Serial = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the FirearmPickup is locked or not.
    /// </summary>
    public new bool Locked
    {
        get => Info.Locked;
        set => Base.Info.Locked = value;
    }

    /// <summary>
    /// Gets or sets the amount of ammo for the FirearmPickup.
    /// </summary>
    public byte Ammo
    {
        get => Base.NetworkStatus.Ammo;
        set => Base.NetworkStatus = new(value, Base.NetworkStatus.Flags, Base.NetworkStatus.Attachments);
    }

    /// <summary>
    /// Gets the previous owner's Footprint of the FirearmPickup.
    /// </summary>
    public new Footprint PreviousOwnerFootPrint => Base.PreviousOwner;

    /// <summary>
    /// Gets the ItemType of the FirearmPickup.
    /// </summary>
    public new ItemType ItemType => Base.NetworkInfo.ItemId;

    /// <summary>
    /// Gets or sets the position of the FirearmPickup.
    /// </summary>
    public new Vector3 Position
    {
        get => Base.Position;
        set => Base.Position = value;
    }

    /// <summary>
    /// Gets or sets the rotation of the FirearmPickup.
    /// </summary>
    public new Quaternion Rotation
    {
        get => Base.Rotation;
        set => Base.Rotation = value;
    }
}


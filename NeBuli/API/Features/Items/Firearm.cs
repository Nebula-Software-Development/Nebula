using InventorySystem.Items.Firearms;
using InventorySystem.Items;
using InventorySystem.Items.Firearms.BasicMessages;
using InventorySystem.Items.Firearms.Modules;
using Mirror;
using PlayerRoles;
using RelativePositioning;
using UnityEngine;

namespace Nebuli.API.Features.Items;

public class Firearm : Item
{
    /// <summary>
    /// Gets the Firearms base.
    /// </summary>
    public new InventorySystem.Items.Firearms.Firearm Base { get; }
    internal Firearm(InventorySystem.Items.Firearms.Firearm firearm) : base(firearm)
    {
        Base = firearm;
    }

    /// <summary>
    /// Gets the description type of the firearm.
    /// </summary>
    public ItemDescriptionType DescriptionType => Base.DescriptionType;

    /// <summary>
    /// Gets the base stats of the firearm.
    /// </summary>
    public FirearmBaseStats Stats => Base.BaseStats;

    /// <summary>
    /// Gets the ammo type of the firearm.
    /// </summary>
    public ItemType AmmoType => Base.AmmoType;

    /// <summary>
    /// Gets the faction of the firearm.
    /// </summary>
    public Faction Faction => Base.FirearmAffiliation;

    /// <summary>
    /// Gets a value indicating whether the firearm is emitting light.
    /// </summary>
    public bool IsEmittingLight => Base.IsEmittingLight;

    /// <summary>
    /// Gets the status of the firearm.
    /// </summary>
    public FirearmStatus Status => Base.Status;

    /// <summary>
    /// Gets the action module of the firearm.
    /// </summary>
    public IActionModule ActionModule => Base.ActionModule;

    /// <summary>
    /// Fires a shot from the firearm.
    /// </summary>
    public void Shoot()
    {
        InventorySystem.Items.Firearms.Firearm firearm = Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
        if (firearm == null)
            return;
        if (Owner == null)
            return;

        ShotMessage message = new()
        {
            ShooterCameraRotation = Owner.ReferenceHub.PlayerCameraReference.transform.rotation,
            ShooterPosition = new RelativePosition(Owner.ReferenceHub.PlayerCameraReference.transform.position),
            ShooterWeaponSerial = Owner.CurrentItem.Serial,
            TargetNetId = 0,
            TargetPosition = default,
            TargetRotation = Quaternion.identity,
        };

        Physics.Raycast(Owner.ReferenceHub.PlayerCameraReference.transform.position, Owner.ReferenceHub.PlayerCameraReference.transform.forward, out RaycastHit hit, 100f, StandardHitregBase.HitregMask);

        if (hit.transform && hit.transform.TryGetComponentInParent(out NetworkIdentity networkIdentity) && networkIdentity)
        {
            message.TargetNetId = networkIdentity.netId;
            message.TargetPosition = new RelativePosition(networkIdentity.transform.position);
            message.TargetRotation = networkIdentity.transform.rotation;
        }
        else if (hit.transform)
        {
            message.TargetPosition = new RelativePosition(hit.transform.position);
            message.TargetRotation = hit.transform.rotation;
        }
        FirearmBasicMessagesHandler.ServerShotReceived(Owner.ReferenceHub.connectionToClient, message);
    }

    /// <summary>
    /// Reloads the firearm.
    /// </summary>
    public void Reload()
    {
        InventorySystem.Items.Firearms.Firearm firearm = Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
        if (firearm == null)
            return;

        RequestMessage message = new(Serial, RequestType.Reload);
        FirearmBasicMessagesHandler.ServerRequestReceived(Owner.ReferenceHub.connectionToClient, message);
    }

    /// <summary>
    /// Toggles the flashlight attached to the firearm.
    /// </summary>
    public void ToggleFlashlight()
    {
        InventorySystem.Items.Firearms.Firearm firearm = Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
        if (firearm == null)
            return;
        if (Owner == null)
            return;

        RequestMessage message = new(Serial, RequestType.ToggleFlashlight);
        FirearmBasicMessagesHandler.ServerRequestReceived(Owner.ReferenceHub.connectionToClient, message);
    }

    /// <summary>
    /// Unloads the firearm.
    /// </summary>
    public void Unload()
    {
        InventorySystem.Items.Firearms.Firearm firearm = Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
        if (firearm == null)
            return;

        RequestMessage message = new(Serial, RequestType.Unload);
        FirearmBasicMessagesHandler.ServerRequestReceived(Owner.ReferenceHub.connectionToClient, message);
    }

    /// <summary>
    /// Sets the aiming down sight state of the firearm.
    /// </summary>
    /// <param name="shouldADS">True to aim down sight, false to stop aiming down sight.</param>
    public void SetAimDownSight(bool shouldADS)
    {
        InventorySystem.Items.Firearms.Firearm firearm = Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
        if (firearm == null)
            return;

        RequestMessage message = new(Serial, shouldADS ? RequestType.AdsIn : RequestType.AdsOut);
        FirearmBasicMessagesHandler.ServerRequestReceived(Owner.ReferenceHub.connectionToClient, message);
    }
}


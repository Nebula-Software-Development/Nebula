// -----------------------------------------------------------------------
// <copyright file=Firearm.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CameraShaking;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;
using InventorySystem.Items.Firearms.BasicMessages;
using InventorySystem.Items.Firearms.Modules;
using Mirror;
using Nebula.API.Extensions;
using Nebula.API.Features.Enum;
using Nebula.API.Features.Structs;
using PlayerRoles;
using RelativePositioning;
using UnityEngine;

namespace Nebula.API.Features.Items
{
    /// <summary>
    ///     Represents a firearm item in the game.
    /// </summary>
    public class Firearm : Item
    {
        internal static readonly Dictionary<FirearmType, Firearm> TypeToFirearm = new();

        internal static readonly Dictionary<FirearmType, uint> BaseCodes = new();

        public Firearm(InventorySystem.Items.Firearms.Firearm firearm) : base(firearm)
        {
            Base = firearm;
        }

        /// <summary>
        ///     Gets the base firearm object.
        /// </summary>
        public new InventorySystem.Items.Firearms.Firearm Base { get; }

        internal static Dictionary<FirearmType, AttachmentIdentity[]> AvailableAttachmentsValue { get; } = new();

        /// <summary>
        ///     Gets the description type of the firearm.
        /// </summary>
        public ItemDescriptionType DescriptionType => Base.DescriptionType;

        /// <summary>
        ///     Gets the type of the firearm.
        /// </summary>
        public FirearmType Type => ItemType.ToFirearmType();

        /// <summary>
        ///     Gets the firearms max ammo.
        /// </summary>
        public byte MaxAmmo => Base.AmmoManagerModule.MaxAmmo;

        /// <summary>
        ///     Gets or sets the firearms <see cref="FirearmStatusFlags" />
        /// </summary>
        public FirearmStatusFlags StatusFlags
        {
            get => Base.Status.Flags;
            set => new FirearmStatus(CurrentAmmo, value, Base.Status.Attachments);
        }

        /// <summary>
        ///     Gets or sets the attachments of the firearm.
        /// </summary>
        public Attachment[] Attachments
        {
            get => Base.Attachments;
            set => Base.Attachments = value;
        }

        /// <summary>
        ///     Gets the base stats of the firearm.
        /// </summary>
        public FirearmBaseStats Stats => Base.BaseStats;

        /// <summary>
        ///     Gets the ammo type of the firearm.
        /// </summary>
        public AmmoType AmmoType => Base.AmmoType.ToAmmoType();

        /// <summary>
        ///     Gets or sets the current ammo of the firearm.
        /// </summary>
        public byte CurrentAmmo
        {
            get => Base.Status.Ammo;
            set => new FirearmStatus(value, StatusFlags, Base.Status.Attachments);
        }

        public float FireRate
        {
            get
            {
                if (Base is AutomaticFirearm firearm)
                {
                    return firearm._fireRate;
                }

                return 1f;
            }
            set
            {
                if (Base is AutomaticFirearm firearm)
                {
                    firearm._fireRate = value;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the recoil settings for the firearm.
        /// </summary>
        public RecoilSettings Recoil
        {
            get
            {
                if (Base is AutomaticFirearm firearm)
                {
                    return firearm._recoil;
                }

                return default;
            }
            set
            {
                if (Base is AutomaticFirearm auto)
                {
                    auto.ActionModule = new AutomaticAction(Base, auto._semiAutomatic, auto._boltTravelTime,
                        1f / auto._fireRate, auto._dryfireClipId, auto._triggerClipId, auto._gunshotPitchRandomization,
                        value, auto._recoilPattern, false, Mathf.Max(1, auto._chamberSize));
                }
            }
        }

        /// <summary>
        ///     Gets or sets the firearms <see cref="AutomaticFirearm.ActionModule" />
        /// </summary>
        public AutomaticAction AutomaticAction
        {
            get
            {
                if (Base is AutomaticFirearm firearm)
                {
                    return (AutomaticAction)firearm.ActionModule;
                }

                return default;
            }
            set
            {
                if (Base is AutomaticFirearm firearm)
                {
                    firearm.ActionModule = value;
                }
            }
        }

        /// <summary>
        ///     Gets the faction affiliation of the firearm.
        /// </summary>
        public Faction Faction => Base.FirearmAffiliation;

        /// <summary>
        ///     Gets a value indicating whether the firearm is emitting light.
        /// </summary>
        public bool IsEmittingLight => Base.IsEmittingLight;

        /// <summary>
        ///     Gets or sets the status of the firearm.
        /// </summary>
        public FirearmStatus Status
        {
            get => Base.Status;
            set => Base.Status = value;
        }

        /// <summary>
        ///     Gets the action module of the firearm.
        /// </summary>
        public IActionModule ActionModule => Base.ActionModule;

        /// <summary>
        ///     Gets the available attachments for each firearm type.
        /// </summary>
        public static Dictionary<FirearmType, AttachmentIdentity[]> AvailableAttachments => AvailableAttachmentsValue;

        /// <summary>
        ///     Gets the base code of the firearm.
        /// </summary>
        public uint BaseCode => BaseCodes[Type];

        /// <summary>
        ///     Gets the players firearms preferences.
        /// </summary>
        public static IReadOnlyDictionary<Player, Dictionary<FirearmType, AttachmentIdentity[]>> PlayerPreferences
            => AttachmentsServerHandler.PlayerPreferences.Where(kvp => kvp.Key != null).ToDictionary(
                kvp => Player.Get(kvp.Key), kvp
                    => kvp.Value.ToDictionary(subKvp => subKvp.Key.ToFirearmType(), subKvp
                        => subKvp.Key.ToFirearmType().GetAttachmentIdentifiers(subKvp.Value).ToArray()));

        /// <summary>
        ///     Retrieves the attachment identities for enabled attachments of the firearm.
        /// </summary>
        public IEnumerable<AttachmentIdentity> AttachmentIdentities
        {
            get
            {
                foreach (Attachment attachment in Attachments.Where(attachment => attachment.IsEnabled))
                {
                    yield return AvailableAttachments[Type].FirstOrDefault(aa => aa == attachment);
                }
            }
        }

        /// <summary>
        ///     Adds a <see cref="AttachmentIdentity" /> to the firearm.
        /// </summary>
        /// <param name="identifier">The <see cref="AttachmentIdentity" /> to add.</param>
        public void AddAttachment(AttachmentIdentity identifier)
        {
            uint toRemove = 0;

            foreach (Attachment attachment in Base.Attachments)
            {
                if (attachment.Slot == identifier.Slot && attachment.IsEnabled)
                {
                    toRemove = (uint)(1 << (int)attachment.Slot);
                    break;
                }
            }

            uint newCode;
            if (identifier.Code == 0)
            {
                AttachmentIdentity matchingAttachment =
                    AvailableAttachments[Type].FirstOrDefault(attId => attId.Name == identifier.Name);
                newCode = matchingAttachment != null ? matchingAttachment.Code : 0U;
            }
            else
            {
                newCode = identifier.Code;
            }

            uint currentAttachmentsCode = Base.GetCurrentAttachmentsCode();

            Base.ApplyAttachmentsCode((currentAttachmentsCode & ~toRemove) | newCode, true);
            Base.Status = new FirearmStatus(Math.Min(CurrentAmmo, MaxAmmo), Base.Status.Flags, currentAttachmentsCode);
        }

        /// <summary>
        ///     Adds a attachment to the firearm given a <see cref="AttachmentName" />.
        /// </summary>
        public void AddAttachment(AttachmentName attachmentName)
        {
            AddAttachment(AttachmentIdentity.Get(Type, attachmentName));
        }

        /// <summary>
        ///     Fires a shot from the firearm.
        /// </summary>
        public void Shoot()
        {
            if (Owner == null)
            {
                return;
            }

            InventorySystem.Items.Firearms.Firearm firearm =
                Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
            if (firearm == null)
            {
                return;
            }

            ShotMessage message = new()
            {
                ShooterCameraRotation = Owner.PlayerCamera.rotation,
                ShooterPosition = Owner.RelativePosition,
                ShooterWeaponSerial = Serial,
                TargetNetId = 0,
                TargetPosition = default,
                TargetRotation = Quaternion.identity
            };

            Physics.Raycast(Owner.Position, Owner.PlayerCamera.forward, out RaycastHit hit, 100f,
                StandardHitregBase.HitregMask);

            if (hit.transform && hit.transform.TryGetComponentInParent(out NetworkIdentity networkIdentity) &&
                networkIdentity)
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

            FirearmBasicMessagesHandler.ServerShotReceived(Owner.NetworkConnection, message);
        }

        /// <summary>
        ///     Reloads the firearm.
        /// </summary>
        public void Reload()
        {
            if (Owner == null)
            {
                return;
            }

            InventorySystem.Items.Firearms.Firearm firearm =
                Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
            if (firearm == null)
            {
                return;
            }

            RequestMessage message = new(Serial, RequestType.Reload);
            FirearmBasicMessagesHandler.ServerRequestReceived(Owner.NetworkConnection, message);
        }

        /// <summary>
        ///     Toggles the flashlight attached to the firearm.
        /// </summary>
        public void ToggleFlashlight()
        {
            if (Owner == null)
            {
                return;
            }

            InventorySystem.Items.Firearms.Firearm firearm =
                Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
            if (firearm == null)
            {
                return;
            }

            RequestMessage message = new(Serial, RequestType.ToggleFlashlight);
            FirearmBasicMessagesHandler.ServerRequestReceived(Owner.NetworkConnection, message);
        }

        /// <summary>
        ///     Unloads the firearm.
        /// </summary>
        public void Unload()
        {
            if (Owner == null)
            {
                return;
            }

            InventorySystem.Items.Firearms.Firearm firearm =
                Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
            if (firearm == null)
            {
                return;
            }

            RequestMessage message = new(Serial, RequestType.Unload);
            FirearmBasicMessagesHandler.ServerRequestReceived(Owner.NetworkConnection, message);
        }

        /// <summary>
        ///     Sets the aiming down sight state of the firearm.
        /// </summary>
        /// <param name="shouldADS">True to aim down sight, false to stop aiming down sight.</param>
        public void SetAimDownSight(bool shouldADS)
        {
            if (Owner == null)
            {
                return;
            }

            InventorySystem.Items.Firearms.Firearm firearm =
                Owner.Inventory._curInstance as InventorySystem.Items.Firearms.Firearm;
            if (firearm == null)
            {
                return;
            }

            RequestMessage message = new(Serial, shouldADS ? RequestType.AdsIn : RequestType.AdsOut);
            FirearmBasicMessagesHandler.ServerRequestReceived(Owner.NetworkConnection, message);
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file=PlayerInteractingDoorEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Interactables.Interobjects.DoorUtils;
using Nebula.API.Features.Doors;
using Nebula.Events.EventArguments.Interfaces;
using PlayerRoles;
using PluginAPI.Events;

namespace Nebula.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player interacts with a door.
    /// </summary>
    public class PlayerInteractingDoorEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        internal bool allowedInteracting = true;

        internal bool bypassDenied;

        public PlayerInteractingDoorEvent(ReferenceHub ply, DoorVariant door, byte id)
        {
            Player = API.Features.Player.Get(ply);
            Door = Door.Get(door);
            IsCancelled = CalculateIsCancelled(ply, door, id);
        }

        /// <summary>
        ///     Gets the <see cref="API.Features.Doors.Door" /> being interacted with.
        /// </summary>
        public Door Door { get; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player interacting with the door.
        /// </summary>
        public API.Features.Player Player { get; }

        private bool CalculateIsCancelled(ReferenceHub ply, DoorVariant door, byte colliderId)
        {
            if (door.ActiveLocks > 0 && !ply.serverRoles.BypassMode)
            {
                DoorLockMode mode = DoorLockUtils.GetMode((DoorLockReason)door.ActiveLocks);
                if ((!mode.HasFlagFast(DoorLockMode.CanClose) || !mode.HasFlagFast(DoorLockMode.CanOpen)) &&
                    (!mode.HasFlagFast(DoorLockMode.ScpOverride) || !ply.IsSCP()) &&
                    (mode == DoorLockMode.FullLock || (door.TargetState && !mode.HasFlagFast(DoorLockMode.CanClose)) ||
                     (!door.TargetState && !mode.HasFlagFast(DoorLockMode.CanOpen))))
                {
                    PluginAPI.Events.EventManager.ExecuteEvent(new PlayerInteractDoorEvent(ply, door, false));
                    bypassDenied = true;
                    return true;
                }
            }

            if (!door.AllowInteracting(ply, colliderId))
            {
                allowedInteracting = false;
                return true;
            }

            bool flag = ply.GetRoleId() == RoleTypeId.Scp079 ||
                        door.RequiredPermissions.CheckPermissions(ply.inventory.CurInstance, ply);
            PluginAPI.Events.EventManager.ExecuteEvent(new PlayerInteractDoorEvent(ply, door, flag));
            if (!flag)
            {
                return true;
            }

            return false;
        }
    }
}
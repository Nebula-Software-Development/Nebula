// -----------------------------------------------------------------------
// <copyright file=CheckpointDoor.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Interactables.Interobjects.DoorUtils;
using CheckpointDoorBase = Interactables.Interobjects.CheckpointDoor;

namespace Nebula.API.Features.Doors
{
    public class CheckpointDoor : Door
    {
        internal CheckpointDoor(CheckpointDoorBase door) : base(door)
        {
            Base = door;
        }

        /// <summary>
        ///     Gets the <see cref="CheckpointDoorBase" /> base.
        /// </summary>
        public new CheckpointDoorBase Base { get; }

        /// <summary>
        ///     Sets if the checkpoint door is destroyed or not.
        /// </summary>
        public bool IsDestroyed
        {
            get => Base.IsDestroyed;
            set => Base.IsDestroyed = value;
        }

        /// <summary>
        ///     Gets the checkpoint doors subdoors.
        /// </summary>
        public DoorVariant[] Subdoors => Base.SubDoors;

        /// <summary>
        ///     Toggles all the checkpoint doors.
        /// </summary>
        /// <param name="newState"></param>
        public void ToggleAllDoors(bool newState)
        {
            Base.ToggleAllDoors(newState);
        }
    }
}
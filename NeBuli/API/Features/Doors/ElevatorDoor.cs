// -----------------------------------------------------------------------
// <copyright file=ElevatorDoor.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using UnityEngine;
using static Interactables.Interobjects.ElevatorManager;
using ElevatorDoorBase = Interactables.Interobjects.ElevatorDoor;

namespace Nebuli.API.Features.Doors
{
    public class ElevatorDoor : Door
    {
        internal ElevatorDoor(ElevatorDoorBase door) : base(door)
        {
            Base = door;
        }

        /// <summary>
        ///     Gets the <see cref="ElevatorDoorBase" /> base.
        /// </summary>
        public new ElevatorDoorBase Base { get; }

        /// <summary>
        ///     Gets the last player that interacted with the elevator.
        /// </summary>
        public ReferenceHub LastPlayerInteracted => Base._triggerPlayer;

        /// <summary>
        ///     Gets the elevators bottom position.
        /// </summary>
        public Vector3 BottomPosition => Base.BottomPosition;

        /// <summary>
        ///     Gets the elevators top position.
        /// </summary>
        public Vector3 TopPosition => Base.TopPosition;

        /// <summary>
        ///     Gets the elevators group.
        /// </summary>
        public ElevatorGroup Group => Base.Group;
    }
}
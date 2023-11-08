// -----------------------------------------------------------------------
// <copyright file=Elevator.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nebuli.API.Features.Map;

/// <summary>
/// Wrapper class for <see cref="ElevatorChamber"/>.
/// </summary>
public class Elevator
{
    /// <summary>
    /// Gets the <see cref="ElevatorChamber"/> and <see cref="Elevator"/> dictionary.
    /// </summary>
    public static Dictionary<ElevatorChamber, Elevator> Dictionary = new();

    /// <summary>
    /// Gets the <see cref="ElevatorChamber"/> base.
    /// </summary>
    public ElevatorChamber Base { get; }

    internal Elevator(ElevatorChamber elevator)
    {
        Base = elevator;
        Dictionary.Add(elevator, this);
    }

    /// <summary>
    /// Gets a collection of all the <see cref="Elevator"/>.
    /// </summary>
    public static IEnumerable<Elevator> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the <see cref="Elevator"/>.
    /// </summary>
    public static List<Elevator> List => Collection.ToList();

    /// <summary>
    /// Sets the inner door state.
    /// </summary>
    /// <param name="state"></param>
    public void SetInnerDoorState(bool state) => Base.SetInnerDoor(state);

    /// <summary>
    /// Refreshes the elevators locks.
    /// </summary>
    /// <param name="elevatorGroup"></param>
    /// <param name="door"></param>
    public void RefreshLocks(ElevatorManager.ElevatorGroup elevatorGroup, ElevatorDoor door) => Base.RefreshLocks(elevatorGroup, door);

    /// <summary>
    /// Gets the elevators <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets or sets the elevators active locks.
    /// </summary>
    public DoorLockReason ActiveLocks
    {
        get => Base.ActiveLocks;
        set => Base.ActiveLocks = value;
    }

    /// <summary>
    /// Gets if the elevator is ready or not.
    /// </summary>
    public bool IsReady => Base.IsReady;

    /// <summary>
    /// Gets the elevators current level.
    /// </summary>
    public int CurrentLevel => Base.CurrentLevel;

    /// <summary>
    /// Gets the elevators current destination.
    /// </summary>
    public ElevatorDoor CurrentDestination => Base.CurrentDestination;

    /// <summary>
    /// Gets or creates a new <see cref="Elevator"/> wrapper.
    /// </summary>
    /// <param name="elevatorBase">The <see cref="ElevatorChamber"/> to use to make the wrapper.</param>
    /// <returns></returns>
    public static Elevator Get(ElevatorChamber elevatorBase) => Dictionary.TryGetValue(elevatorBase, out Elevator elevator) ? elevator : new(elevatorBase);
}
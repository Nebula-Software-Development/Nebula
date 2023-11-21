// -----------------------------------------------------------------------
// <copyright file=DoorType.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebuli.API.Features.Enum
{
    /// <summary>
    ///     Represents the types of doors in-game.
    /// </summary>
    public enum DoorType
    {
        /// <summary>
        ///     A unknown door.
        /// </summary>
        UnknownDoor,

        /// <summary>
        ///     A door in the light containment area.
        /// </summary>
        LightContainmentDoor,

        /// <summary>
        ///     A door in the heavy containment area.
        /// </summary>
        HeavyContainmentDoor,

        /// <summary>
        ///     A door in the entrance area.
        /// </summary>
        EntranceDoor,

        /// <summary>
        ///     A door in the prison area.
        /// </summary>
        PrisonDoor,

        /// <summary>
        ///     Door leading to SCP-914.
        /// </summary>
        Scp914Door,

        /// <summary>
        ///     Checkpoint armory door (checkpoint LCZ-A).
        /// </summary>
        CheckpointArmoryA,

        /// <summary>
        ///     Checkpoint armory door (checkpoint LCZ-B).
        /// </summary>
        CheckpointArmoryB,

        /// <summary>
        ///     An airlock door.
        /// </summary>
        Airlock,

        /// <summary>
        ///     A door at a checkpoint gate.
        /// </summary>
        CheckpointGate,

        /// <summary>
        ///     A unknown pryable gate.
        /// </summary>
        UnknownGate,

        /// <summary>
        ///     Gate leading to SCP-049's containment area.
        /// </summary>
        Scp049Gate,

        /// <summary>
        ///     New gate leading to SCP-173's containment area.
        /// </summary>
        Scp173NewGate,

        /// <summary>
        ///     Elevator door for the warhead silo.
        /// </summary>
        ElevatorNuke,

        /// <summary>
        ///     Elevator door for SCP-049's containment area.
        /// </summary>
        ElevatorScp049,

        /// <summary>
        ///     Elevator gate at Gate B.
        /// </summary>
        ElevatorGateB,

        /// <summary>
        ///     Elevator gate at Gate A.
        /// </summary>
        ElevatorGateA,

        /// <summary>
        ///     Elevator in Light Containment Zone, part A.
        /// </summary>
        ElevatorLczA,

        /// <summary>
        ///     Elevator in Light Containment Zone, part B.
        /// </summary>
        ElevatorLczB,

        /// <summary>
        ///     The type of elevator is unknown.
        /// </summary>
        UnknownElevator,

        /// <summary>
        ///     Checkpoint door at checkpoint LCZ-A.
        /// </summary>
        CheckpointLczA,

        /// <summary>
        ///     Checkpoint door at checkpoint LCZ-B.
        /// </summary>
        CheckpointLczB,

        /// <summary>
        ///     Checkpoint door at checkpoint EZ-HCZ-A.
        /// </summary>
        CheckpointEzHczA,

        /// <summary>
        ///     Checkpoint door at checkpoint EZ-HCZ-B.
        /// </summary>
        CheckpointEzHczB,

        /// <summary>
        ///     Primary containment door for SCP-106.
        /// </summary>
        Scp106Primary,

        /// <summary>
        ///     Secondary containment door for SCP-106.
        /// </summary>
        Scp106Secondary,

        /// <summary>
        ///     Primary door for escaping the facility.
        /// </summary>
        EscapePrimary,

        /// <summary>
        ///     Secondary door for escaping the facility.
        /// </summary>
        EscapeSecondary,

        /// <summary>
        ///     Intercom room door.
        /// </summary>
        Intercom,

        /// <summary>
        ///     Door leading to the nuke armory.
        /// </summary>
        NukeArmory,

        /// <summary>
        ///     Door leading to the LCZ armory.
        /// </summary>
        LczArmory,

        /// <summary>
        ///     Door leading to the nuke surface.
        /// </summary>
        NukeSurface,

        /// <summary>
        ///     Door for accessing the HID.
        /// </summary>
        HID,

        /// <summary>
        ///     Door leading to the HCZ armory.
        /// </summary>
        HczArmory,

        /// <summary>
        ///     Door for accessing SCP-096's containment chamber.
        /// </summary>
        Scp096,

        /// <summary>
        ///     Door leading to SCP-049's armory.
        /// </summary>
        Scp049Armory,

        /// <summary>
        ///     Door leading to SCP-079's armory.
        /// </summary>
        Scp079Armory,

        /// <summary>
        ///     Gate door for SCP-914.
        /// </summary>
        Scp914Gate,

        /// <summary>
        ///     Gate A.
        /// </summary>
        GateA,

        /// <summary>
        ///     First door for SCP-079.
        /// </summary>
        Scp079First,

        /// <summary>
        ///     Gate B.
        /// </summary>
        GateB,

        /// <summary>
        ///     Second door for SCP-079.
        /// </summary>
        Scp079Second,

        /// <summary>
        ///     Bottom doors for server rooms.
        /// </summary>
        ServersBottom,

        /// <summary>
        ///     Connector door for SCP-173.
        /// </summary>
        Scp173Connector,

        /// <summary>
        ///     Door for the LCZ toilet.
        /// </summary>
        LczWc,

        /// <summary>
        ///     Door for accessing the HID (right side).
        /// </summary>
        HIDRight,

        /// <summary>
        ///     Door for accessing the HID (left side).
        /// </summary>
        HIDLeft,

        /// <summary>
        ///     Armory door for SCP-173.
        /// </summary>
        Scp173Armory,

        /// <summary>
        ///     Gate door for SCP-173.
        /// </summary>
        Scp173Gate,

        /// <summary>
        ///     GR18 gate.
        /// </summary>
        GR18Gate,

        /// <summary>
        ///     Surface gate.
        /// </summary>
        SurfaceGate,

        /// <summary>
        ///     Door for SCP-330's containment chamber.
        /// </summary>
        Scp330,

        /// <summary>
        ///     Chamber door for SCP-330.
        /// </summary>
        Scp330Chamber,

        /// <summary>
        ///     Inner gate for GR18.
        /// </summary>
        GR18Inner,

        /// <summary>
        ///     Cryo door for SCP-939.
        /// </summary>
        Scp939Cryo,

        /// <summary>
        ///     Cafe door in LCZ.
        /// </summary>
        LczCafe,

        /// <summary>
        ///     Bottom door for SCP-173.
        /// </summary>
        Scp173Bottom,

        /// <summary>
        ///     Breakable door near an intercom.
        /// </summary>
        IntercomBreakable,

        /// <summary>
        ///     Breakable door in the prison area.
        /// </summary>
        PrisonBreakable,

        /// <summary>
        ///     Elevator door.
        /// </summary>
        Elevator
    }
}
namespace Nebuli.API.Features.Enum;

public enum CameraType
{
    /// <summary>
    /// Camera that has a unknown location.
    /// </summary>
    Unknown,

    /// <summary>
    /// Camera in the surface exit passage.
    /// </summary>
    SurfaceExitPassage,

    /// <summary>
    /// Camera near Surface Gate A.
    /// </summary>
    SurfaceGateA,

    /// <summary>
    /// Camera near Surface Gate B.
    /// </summary>
    SurfaceGateB,

    /// <summary>
    /// Camera on the main street of the surface area.
    /// </summary>
    SurfaceMainStreet,

    /// <summary>
    /// Camera near the surface airlock.
    /// </summary>
    SurfaceAirlock,

    /// <summary>
    /// Camera on the surface bridge.
    /// </summary>
    SurfaceBridge,

    /// <summary>
    /// Camera near the entrance to the surface tunnel.
    /// </summary>
    SurfaceTunnelEntrance,

    /// <summary>
    /// Camera in the checkpoint hall of the Entrance Zone.
    /// </summary>
    EntranceZoneCheckpointHall,

    /// <summary>
    /// Camera at the crossing in the Entrance Zone.
    /// </summary>
    EntranceZoneCrossing,

    /// <summary>
    /// Camera at a curve in the Entrance Zone.
    /// </summary>
    EntranceZoneCurve,

    /// <summary>
    /// Camera in a hallway of the Entrance Zone.
    /// </summary>
    EntranceZoneHallway,

    /// <summary>
    /// Camera at a three-way intersection in the Entrance Zone.
    /// </summary>
    EntranceZoneThreeWay,

    /// <summary>
    /// Camera near Entrance Zone Gate A.
    /// </summary>
    EntranceZoneGateA,

    /// <summary>
    /// Camera near Entrance Zone Gate B.
    /// </summary>
    EntranceZoneGateB,

    /// <summary>
    /// Camera at the bottom of the intercom area in the Entrance Zone.
    /// </summary>
    EntranceZoneIntercomBottom,

    /// <summary>
    /// Camera in the hallway of the intercom area in the Entrance Zone.
    /// </summary>
    EntranceZoneIntercomHall,

    /// <summary>
    /// Camera at the intercom panel in the Entrance Zone.
    /// </summary>
    EntranceZoneIntercomPanel,

    /// <summary>
    /// Camera on the stairs near the intercom area in the Entrance Zone.
    /// </summary>
    EntranceZoneIntercomStairs,

    /// <summary>
    /// Camera in the large office of the Entrance Zone.
    /// </summary>
    EntranceZoneLargeOffice,

    /// <summary>
    /// Camera in the loading dock area of the Entrance Zone.
    /// </summary>
    EntranceZoneLoadingDock,

    /// <summary>
    /// Camera in a minor office of the Entrance Zone.
    /// </summary>
    EntranceZoneMinorOffice,

    /// <summary>
    /// Camera in a office of the Entrance Zone.
    /// </summary>
    EntranceZoneTwoStoryOffice,


    /// <summary>
    /// Camera in the armory area of Heavy Containment Zone, near SCP-049.
    /// </summary>
    HeavyContainmentZone049Armory,

    /// <summary>
    /// Camera inside SCP-049's containment chamber in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone049ContainmentChamber,

    /// <summary>
    /// Camera at the top of the elevator shaft in SCP-049's area of Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone049ElevatorTop,

    /// <summary>
    /// Camera in a hallway of SCP-049's area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone049Hallway,

    /// <summary>
    /// Camera on the top floor of SCP-049's area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone049TopFloor,

    /// <summary>
    /// Camera in the tunnel area of SCP-049's section in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone049Tunnel,

    /// <summary>
    /// Camera at the airlock entrance of SCP-079's area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone079Airlock,

    /// <summary>
    /// Camera inside SCP-079's containment chamber in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone079ContainmentChamber,

    /// <summary>
    /// Camera in a hallway of SCP-079's area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone079Hallway,

    /// <summary>
    /// Camera at the kill switch location of SCP-079's area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone079KillSwitch,

    /// <summary>
    /// Camera inside SCP-096's containment chamber in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone096ContainmentChamber,

    /// <summary>
    /// Camera on the bridge overlooking SCP-106's containment chamber.
    /// </summary>
    HeavyContainmentZone106Bridge,

    /// <summary>
    /// Camera on the catwalk above SCP-106's containment chamber.
    /// </summary>
    HeavyContainmentZone106Catwalk,

    /// <summary>
    /// Camera in the recontainment chamber for SCP-106.
    /// </summary>
    HeavyContainmentZone106Recontainment,

    /// <summary>
    /// Camera at the checkpoint for Entrance Zone from Heavy Containment Zone (Checkpoint EZ).
    /// </summary>
    HeavyContainmentZoneCheckpointEz,
    /// <summary>
    /// Camera at the checkpoint for Heavy Containment Zone from Entrance Zone (Checkpoint HCZ).
    /// </summary>
    HeavyContainmentZoneCheckpointHcz,

    /// <summary>
    /// Camera in the containment chamber of SCP-939 in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZone939,

    /// <summary>
    /// Camera in the armory area of Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneArmory,

    /// <summary>
    /// Camera inside the interior of the armory in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneArmoryInterior,

    /// <summary>
    /// Camera at a crossing point within Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneCrossing,

    /// <summary>
    /// Camera in elevator system A of Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneElevatorSystemA,

    /// <summary>
    /// Camera in elevator system B of Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneElevatorSystemB,

    /// <summary>
    /// Camera in a hallway of Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneHallway,

    /// <summary>
    /// Camera at a three-way intersection within Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneThreeWay,

    /// <summary>
    /// Camera at the bottom level of the server room area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneServersBottom,

    /// <summary>
    /// Camera on the stairs leading to the upper level of the server room area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneServersStairs,

    /// <summary>
    /// Camera at the top level of the server room area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneServersTop,

    /// <summary>
    /// Camera near the Tesla gate in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneTeslaGate,

    /// <summary>
    /// Camera on the bridge in the test room area of Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneTestroomBridge,

    /// <summary>
    /// Camera in the main room of the test room area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneTestroomMain,

    /// <summary>
    /// Camera in the office of the test room area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneTestroomOffice,

    /// <summary>
    /// Camera in the armory area of the warhead control room in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneWarheadArmory,

    /// <summary>
    /// Camera inside the control room of the warhead in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneWarheadControl,

    /// <summary>
    /// Camera in a hallway of the warhead control area in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneWarheadHallway,

    /// <summary>
    /// Camera on the top level of the warhead control room in Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneWarheadTop,
    /// <summary>
    /// Camera at the bottom of SCP-173's containment chamber in Light Containment Zone.
    /// </summary>
    LightContainmentZone173Bottom,

    /// <summary>
    /// Camera inside SCP-173's containment chamber in Light Containment Zone.
    /// </summary>
    LightContainmentZone173ContainmentChamber,

    /// <summary>
    /// Camera in a hallway of Light Containment Zone.
    /// </summary>
    LightContainmentZone173Hall,

    /// <summary>
    /// Camera at the stairs in Light Containment Zone.
    /// </summary>
    LightContainmentZone173Stairs,

    /// <summary>
    /// Camera at the airlock entrance of SCP-914's area in Light Containment Zone.
    /// </summary>
    LightContainmentZone914Airlock,

    /// <summary>
    /// Camera inside SCP-914's containment chamber in Light Containment Zone.
    /// </summary>
    LightContainmentZone914ContainmentChamber,

    /// <summary>
    /// Camera at an airlock entrance in Light Containment Zone.
    /// </summary>
    LightContainmentZoneAirlock,

    /// <summary>
    /// Camera in the armory area of Light Containment Zone.
    /// </summary>
    LightContainmentZoneArmory,

    /// <summary>
    /// Camera at the back area of the cellblock in Light Containment Zone.
    /// </summary>
    LightContainmentZoneCellblockBack,

    /// <summary>
    /// Camera at the entry area of the cellblock in Light Containment Zone.
    /// </summary>
    LightContainmentZoneCellblockEntry,

    /// <summary>
    /// Camera at the entry of Checkpoint A in Light Containment Zone.
    /// </summary>
    LightContainmentZoneCheckpointAEntry,

    /// <summary>
    /// Camera at the inner area of Checkpoint A in Light Containment Zone.
    /// </summary>
    LightContainmentZoneCheckpointAInner,

    /// <summary>
    /// Camera at the entry of Checkpoint B in Light Containment Zone.
    /// </summary>
    LightContainmentZoneCheckpointBEntry,

    /// <summary>
    /// Camera at the inner area of Checkpoint B in Light Containment Zone.
    /// </summary>
    LightContainmentZoneCheckpointBInner,

    /// <summary>
    /// Camera in the glass room area of Light Containment Zone.
    /// </summary>
    LightContainmentZoneGlassroom,

    /// <summary>
    /// Camera at the entry of the glass room in Light Containment Zone.
    /// </summary>
    LightContainmentZoneGlassroomEntry,

    /// <summary>
    /// Camera in the greenhouse area of Light Containment Zone.
    /// </summary>
    LightContainmentZoneGreenhouse,

    /// <summary>
    /// Camera at a crossing point within Light Containment Zone.
    /// </summary>
    LightContainmentZoneCrossing,

    /// <summary>
    /// Camera at a curve in Light Containment Zone.
    /// </summary>
    LightContainmentZoneCurve,

    /// <summary>
    /// Camera in elevator system A of Light Containment Zone.
    /// </summary>
    LightContainmentZoneElevatorSystemA,

    /// <summary>
    /// Camera in elevator system B of Light Containment Zone.
    /// </summary>
    LightContainmentZoneElevatorSystemB,

    /// <summary>
    /// Camera in a hallway of Light Containment Zone.
    /// </summary>
    LightContainmentZoneHallway,

    /// <summary>
    /// Camera at a three-way intersection within Light Containment Zone.
    /// </summary>
    LightContainmentZoneThreeWay,

    /// <summary>
    /// Camera in the PC office area of Light Containment Zone.
    /// </summary>
    LightContainmentZonePcOffice,

    /// <summary>
    /// Camera in the restrooms area of Light Containment Zone.
    /// </summary>
    LightContainmentZoneRestrooms,

    /// <summary>
    /// Camera in the hallway near the toilet corridor in Light Containment Zone.
    /// </summary>
    LightContainmentZoneToiletCorridorHallway,

    /// <summary>
    /// Camera in the test chamber area of Light Containment Zone.
    /// </summary>
    LightContainmentZoneTestChamber,

    /// <summary>
    /// Camera in the HID Chamber of Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneHIDChamber,

    /// <summary>
    /// Camera in the HID Hallway of Heavy Containment Zone.
    /// </summary>
    HeavyContainmentZoneHIDHallway,
}

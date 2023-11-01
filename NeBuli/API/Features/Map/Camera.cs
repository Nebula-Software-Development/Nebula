using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CameraBase = PlayerRoles.PlayableScps.Scp079.Cameras.Scp079Camera;
using CameraType = Nebuli.API.Features.Enum.CameraType;

namespace Nebuli.API.Features.Map;

/// <summary>
/// Provides a easy API interface to  <see cref="CameraBase"/>.
/// </summary>
public class Camera
{
    /// <summary>
    /// Gets the <see cref="CameraBase"/> base.
    /// </summary>
    public CameraBase Base { get; }

    /// <summary>
    /// Gets a dictionary of all the <see cref="CameraBase"/>, and their wrapper, <see cref="Camera"/>.
    /// </summary>
    public static Dictionary<CameraBase, Camera> Dictionary = new();

    internal Camera(CameraBase cameraBase)
    {
        Base = cameraBase;
        Dictionary.Add(Base, this);
        Type = GetCameraTypeFromName(Name);
    }

    /// <summary>
    /// Gets a collection of all cameras.
    /// </summary>
    public static IEnumerable<Camera> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all cameras.
    /// </summary>
    public static List<Camera> List => Collection.ToList();

    /// <summary>
    /// Gets the position of the camera.
    /// </summary>
    public Vector3 CameraPosition => Base.CameraPosition;

    /// <summary>
    /// Gets the GameObject associated with the camera.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the current zoom level of the camera.
    /// </summary>
    public float CurrentZoomLevel => Base.ZoomAxis.CurrentZoom;

    /// <summary>
    /// Gets or sets whether the camera is currently being used.
    /// </summary>
    public bool IsBeingUsed
    {
        get => Base.IsActive;
        set => Base.IsActive = value;
    }

    /// <summary>
    /// Gets the name of the camera.
    /// </summary>
    public string Name => Base.Label;

    /// <summary>
    /// Gets the unique identifier of the camera.
    /// </summary>
    public int Id => Base.SyncId;

    /// <summary>
    /// Gets or sets the position of the camera.
    /// </summary>
    public Vector3 Position
    {
        get => Base.Position;
        set => Base.Position = value;
    }

    /// <summary>
    /// Gets the room that the camera is in.
    /// </summary>
    public Room Room => Room.Get(Base.Room);

    /// <summary>
    /// Gets or sets the horizontal rotation of the camera.
    /// </summary>
    public float HorizontalRotation
    {
        get => Base.HorizontalRotation;
        set => Base.HorizontalRotation = value;
    }

    /// <summary>
    /// Gets or sets the vertical rotation of the camera.
    /// </summary>
    public float VerticalRotation
    {
        get => Base.VerticalRotation;
        set => Base.VerticalRotation = value;
    }

    /// <summary>
    /// Gets or sets the roll rotation of the camera.
    /// </summary>
    public float RollRotation
    {
        get => Base.RollRotation;
        set => Base.RollRotation = value;
    }

    /// <summary>
    /// Gets the <see cref="CameraType"/> of the camera.
    /// </summary>
    public CameraType Type { get; }

    /// <summary>
    /// Tries to get a <see cref="Camera"/> with the specified <see cref="CameraBase"/>. If none are found, it creates one.
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static Camera Get(CameraBase camera) => Dictionary.TryGetValue(camera, out Camera baseCamera) ? baseCamera : new Camera(camera);

    /// <summary>
    /// Gets a <see cref="Camera"/> with the specified <see cref="CameraType"/>.
    /// </summary>
    /// <param name="cameraType">The <see cref="CameraType"/>.</param>
    /// <returns>The first camera found in the list with the specified <see cref="CameraType"/>.</returns>
    public static Camera GetType(CameraType cameraType) => List.FirstOrDefault(x => x.Type == cameraType);

    internal static CameraType GetCameraTypeFromName(string cameraName)
    {
        return cameraName switch
        {
            "EXIT PASSAGE" => CameraType.SurfaceExitPassage,
            "GATE A SURFACE" => CameraType.SurfaceGateA,
            "GATE B SURFACE" => CameraType.SurfaceGateB,
            "MAIN STREET" => CameraType.SurfaceMainStreet,
            "SURFACE AIRLOCK" => CameraType.SurfaceAirlock,
            "SURFACE BRIDGE" => CameraType.SurfaceBridge,
            "TUNNEL ENTRANCE" => CameraType.SurfaceTunnelEntrance,
            "CHKPT (EZ HALL)" => CameraType.EntranceZoneCheckpointHall,
            "EZ CROSSING" => CameraType.EntranceZoneCrossing,
            "EZ CURVE" => CameraType.EntranceZoneCurve,
            "EZ HALLWAY" => CameraType.EntranceZoneHallway,
            "EZ THREE-WAY" => CameraType.EntranceZoneThreeWay,
            "GATE A" => CameraType.EntranceZoneGateA,
            "GATE B" => CameraType.EntranceZoneGateB,
            "INTERCOM BOTTOM" => CameraType.EntranceZoneIntercomBottom,
            "INTERCOM HALL" => CameraType.EntranceZoneIntercomHall,
            "INTERCOM PANEL" => CameraType.EntranceZoneIntercomPanel,
            "INTERCOM STAIRS" => CameraType.EntranceZoneIntercomStairs,
            "LARGE OFFICE" => CameraType.EntranceZoneLargeOffice,
            "LOADING DOCK" => CameraType.EntranceZoneLoadingDock,
            "MINOR OFFICE" => CameraType.EntranceZoneMinorOffice,
            "TWO-STORY OFFICE" => CameraType.EntranceZoneTwoStoryOffice,
            "049 ARMORY" => CameraType.HeavyContainmentZone049Armory,
            "049 CONT CHAMBER" => CameraType.HeavyContainmentZone049ContainmentChamber,
            "049 ELEV TOP" => CameraType.HeavyContainmentZone049ElevatorTop,
            "049 HALLWAY" => CameraType.HeavyContainmentZone049Hallway,
            "049 TOP FLOOR" => CameraType.HeavyContainmentZone049TopFloor,
            "049 TUNNEL" => CameraType.HeavyContainmentZone049Tunnel,
            "079 AIRLOCK" => CameraType.HeavyContainmentZone079Airlock,
            "079 CONT CHAMBER" => CameraType.HeavyContainmentZone079ContainmentChamber,
            "079 HALLWAY" => CameraType.HeavyContainmentZone079Hallway,
            "079 KILL SWITCH" => CameraType.HeavyContainmentZone079KillSwitch,
            "096 CONT CHAMBER" => CameraType.HeavyContainmentZone096ContainmentChamber,
            "106 BRIDGE" => CameraType.HeavyContainmentZone106Bridge,
            "106 CATWALK" => CameraType.HeavyContainmentZone106Catwalk,
            "106 RECONTAINMENT" => CameraType.HeavyContainmentZone106Recontainment,
            "CHKPT (EZ)" => CameraType.HeavyContainmentZoneCheckpointEz,
            "CHKPT (HCZ)" => CameraType.HeavyContainmentZoneCheckpointHcz,
            "HCZ 939" => CameraType.HeavyContainmentZone939,
            "HCZ ARMORY" => CameraType.HeavyContainmentZoneArmory,
            "HCZ ARMORY INTERIOR" => CameraType.HeavyContainmentZoneArmoryInterior,
            "HCZ CROSSING" => CameraType.HeavyContainmentZoneCrossing,
            "HCZ ELEV SYS A" => CameraType.HeavyContainmentZoneElevatorSystemA,
            "HCZ ELEV SYS B" => CameraType.HeavyContainmentZoneElevatorSystemB,
            "HCZ HALLWAY" => CameraType.HeavyContainmentZoneHallway,
            "HCZ THREE-WAY" => CameraType.HeavyContainmentZoneThreeWay,
            "SERVERS BOTTOM" => CameraType.HeavyContainmentZoneServersBottom,
            "SERVERS STAIRS" => CameraType.HeavyContainmentZoneServersStairs,
            "SERVERS TOP" => CameraType.HeavyContainmentZoneServersTop,
            "TESLA GATE" => CameraType.HeavyContainmentZoneTeslaGate,
            "TESTROOM BRIDGE" => CameraType.HeavyContainmentZoneTestroomBridge,
            "TESTROOM MAIN" => CameraType.HeavyContainmentZoneTestroomMain,
            "TESTROOM OFFICE" => CameraType.HeavyContainmentZoneTestroomOffice,
            "WARHEAD ARMORY" => CameraType.HeavyContainmentZoneWarheadArmory,
            "WARHEAD CONTROL" => CameraType.HeavyContainmentZoneWarheadControl,
            "WARHEAD HALLWAY" => CameraType.HeavyContainmentZoneWarheadHallway,
            "WARHEAD TOP" => CameraType.HeavyContainmentZoneWarheadTop,
            "173 BOTTOM" => CameraType.LightContainmentZone173Bottom,
            "173 CONT CHAMBER" => CameraType.LightContainmentZone173ContainmentChamber,
            "173 HALL" => CameraType.LightContainmentZone173Hall,
            "173 STAIRS" => CameraType.LightContainmentZone173Stairs,
            "914 AIRLOCK" => CameraType.LightContainmentZone914Airlock,
            "914 CONT CHAMBER" => CameraType.LightContainmentZone914ContainmentChamber,
            "AIRLOCK" => CameraType.LightContainmentZoneAirlock,
            "ARMORY" => CameraType.LightContainmentZoneArmory,
            "CELLBLOCK BACK" => CameraType.LightContainmentZoneCellblockBack,
            "CELLBLOCK ENTRY" => CameraType.LightContainmentZoneCellblockEntry,
            "CHKPT A ENTRY" => CameraType.LightContainmentZoneCheckpointAEntry,
            "CHKPT A INNER" => CameraType.LightContainmentZoneCheckpointAInner,
            "CHKPT B ENTRY" => CameraType.LightContainmentZoneCheckpointBEntry,
            "CHKPT B INNER" => CameraType.LightContainmentZoneCheckpointBInner,
            "GLASSROOM" => CameraType.LightContainmentZoneGlassroom,
            "GLASSROOM ENTRY" => CameraType.LightContainmentZoneGlassroomEntry,
            "GREENHOUSE" => CameraType.LightContainmentZoneGreenhouse,
            "CROSSING" => CameraType.LightContainmentZoneCrossing,
            "CURVE" => CameraType.LightContainmentZoneCurve,
            "ELEV SYS A" => CameraType.LightContainmentZoneElevatorSystemA,
            "ELEV SYS B" => CameraType.LightContainmentZoneElevatorSystemB,
            "HALLWAY" => CameraType.LightContainmentZoneHallway,
            "THREE-WAY" => CameraType.LightContainmentZoneThreeWay,
            "PC OFFICE" => CameraType.LightContainmentZonePcOffice,
            "RESTROOMS" => CameraType.LightContainmentZoneRestrooms,
            "TOILET CORRIDOR HALLWAY" => CameraType.LightContainmentZoneToiletCorridorHallway,
            "TEST CHAMBER" => CameraType.LightContainmentZoneTestChamber,
            _ => CameraType.Unknown,
        };
    }
}
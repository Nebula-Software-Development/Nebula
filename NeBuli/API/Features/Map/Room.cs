using MapGeneration;
using Nebuli.API.Extensions;
using Nebuli.API.Features.Doors;
using Nebuli.API.Features.Enum;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nebuli.API.Features.Map;

/// <summary>
/// Wrapper class for handling rooms in-game easier.
/// </summary>
public class Room
{
    /// <summary>
    /// Gets a dictionary of all <see cref="RoomIdentifier"/> to <see cref="Room"/>.
    /// </summary>
    public static readonly Dictionary<RoomIdentifier, Room> Dictionary = new();

    internal Room(RoomIdentifier identifier)
    {
        Base = identifier;
        Dictionary.Add(identifier, this);
        LightController = GameObject.GetComponentInChildren<RoomLightController>();
        Type = FindType(identifier.gameObject);
    }

    /// <summary>
    /// Gets the rooms base.
    /// </summary>
    public RoomIdentifier Base { get; }

    /// <summary>
    /// Gets all the doors that belong this this <see cref="Room"/>.
    /// </summary>
    public List<Door> Doors => Door.List.Where(door => door.CurrentRoom == this).ToList();

    /// <summary>
    /// Gets the rooms <see cref="RoomLightController"/>.
    /// </summary>
    public RoomLightController LightController { get; internal set; }

    /// <summary>
    /// Gets a collection of all the rooms.
    /// </summary>
    public static IEnumerable<Room> Collection => Dictionary.Values;

    /// <summary>
    /// Gets the rooms <see cref="RoomType"/>.
    /// </summary>
    public RoomType Type { get; } = RoomType.Unknown;

    /// <summary>
    /// Gets a list of all the rooms on the server.
    /// </summary>
    public static List<Room> List => Collection.ToList();

    /// <summary>
    /// Gets the rooms current <see cref="Vector3"/> position.
    /// </summary>
    public Vector3 Position => Base.transform.position;

    /// <summary>
    /// Gets the rooms name.
    /// </summary>
    public string Name => Base.name;

    /// <summary>
    /// Gets the rooms current <see cref="Quaternion"/> rotation.
    /// </summary>
    public Quaternion Rotation => Base.transform.rotation;

    /// <summary>
    /// Gets the rooms <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the rooms <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets a value if the given <see cref="Vector3"/> position is in this room.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool IsInRoom(Vector3 position) => RoomIdUtils.IsTheSameRoom(position, Position);

    /// <summary>
    /// Disables the room lights for the specified amount of time.
    /// </summary>
    public void DisableRoomLights(float duration) => LightController.ServerFlickerLights(duration);

    /// <summary>
    /// Tries to get the rooms main coordniates.
    /// </summary>
    /// <param name="coords"></param>
    /// <returns></returns>
    public bool TryGetMainCoords(out Vector3Int coords) => Base.TryGetMainCoords(out coords);

    /// <summary>
    /// Gets a random room. If zone is null, it'll search all rooms from all zones.
    /// </summary>
    /// <param name="zone"></param>
    /// <returns></returns>
    public static Room GetRandomRoom(FacilityZone? zone = null)
    {
        if (zone is null)
            return List[Loader.Loader.Random.Next(List.Count)];
        else
            return List.Where(room => room.Zone == zone).OrderBy(_ => Loader.Loader.Random.Next()).SelectRandom();
    }

    /// <summary>
    /// Gets a list of all the rooms in a specified zone.
    /// </summary>
    /// <param name="zone"></param>
    /// <returns></returns>
    public static List<Room> GetRoomsInZone(FacilityZone zone) => List.Where(room => room.Zone == zone).ToList();

    /// <summary>
    /// Gets or sets if the rooms light are enabled.
    /// </summary>
    public bool LightsEnabled
    {
        get => LightController.LightsEnabled;
        set => LightController.LightsEnabled = value;
    }

    /// <summary>
    /// Gets or sets the rooms color.
    /// </summary>
    public Color RoomColor
    {
        get => LightController.NetworkOverrideColor;
        set => LightController.NetworkOverrideColor = value;
    }

    /// <summary>
    /// Resets the rooms color.
    /// </summary>
    public void ColorReset() => RoomColor = Color.clear;

    /// <summary>
    /// Toggles the room lights on/off.
    /// </summary>
    /// <param name="state"></param>
    public void ToggleLights(bool state) => LightController.SetLights(state);

    /// <summary>
    /// Gets the <see cref="RoomName"/> of the room.
    /// </summary>

    /// <summary>
    /// Gets the <see cref="FacilityZone"/> of the room.
    /// </summary>
    public FacilityZone Zone => Base.Zone;

    /// <summary>
    /// Gets the <see cref="RoomShape"/> of the room.
    /// </summary>
    public RoomShape Shape => Base.Shape;

    /// <summary>
    /// Gets the global point of the room.
    /// </summary>
    /// <param name="localPoint"></param>
    /// <returns></returns>
    public Vector3 GetGlobalPoint(Vector3 localPoint) => Transform.TransformPoint(localPoint);

    /// <summary>
    /// Gets the local point of the room.
    /// </summary>
    /// <param name="globalPoint"></param>
    /// <returns></returns>
    public Vector3 GetLocalPoint(Vector3 globalPoint) => Transform.InverseTransformPoint(globalPoint);

    /// <summary>
    /// Gets a <see cref="Room"/> based off the given <see cref="RoomIdentifier"/>.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static Room Get(RoomIdentifier identifier) => Dictionary.TryGetValue(identifier, out var room) ? room : new Room(identifier);

    /// <summary>
    /// Gets the first room which <see cref="RoomType"/> is equal to the given <see cref="RoomType"/>.
    /// </summary>
    public static Room GetType(RoomType type) => List.FirstOrDefault(room => room.Type == type);

    /// <summary>
    /// Gets a <see cref="Room"/> based off the given <see cref="Vector3"/>.
    /// </summary>
    /// <param name="position">The positon to look for a room at.</param>
    /// <param name="getNearest">Indicates whether to get the nearest room if the exact room is not found.</param>
    /// <returns>A <see cref="Room"/> instance if found; otherwise, <c>null</c>.</returns>
    public static Room Get(Vector3 position, bool getNearest = true)
    {
        Room room = Get(RoomIdUtils.RoomAtPositionRaycasts(position, true));
        if (room is not null)
            return room;
        if (getNearest)
            return GetNearestRoom(position);
        return null;
    }

    /// <summary>
    /// Gets the nearest room to the specified position.
    /// </summary>
    /// <param name="position">The position for which to find the nearest room.</param>
    public static Room GetNearestRoom(Vector3 position)
    {
        Room nearestRoom = List[0];
        float nearestDistance = Vector3.SqrMagnitude(nearestRoom.Position - position);
        for (int i = 1; i < List.Count; i++)
        {
            float distance = Vector3.SqrMagnitude(List[i].Position - position);
            if (distance < nearestDistance)
            {
                nearestRoom = List[i];
                nearestDistance = distance;
            }
        }
        return nearestRoom;
    }

    private static RoomType FindType(GameObject gameObject)
    {
        switch (gameObject.name.GetSubstringBeforeCharacter(' '))
        {
            case "LCZ_Armory": return RoomType.LczArmory;
            case "LCZ_Curve": return RoomType.LczCurve;
            case "LCZ_Straight": return RoomType.LczStraight;
            case "LCZ_330": return RoomType.LczSCP330;
            case "LCZ_914": return RoomType.Lcz914;
            case "LCZ_Crossing": return RoomType.LczCrossing;
            case "LCZ_TCross": return RoomType.LczTCross;
            case "LCZ_Cafe": return RoomType.LczCafe;
            case "LCZ_Plants": return RoomType.LczPlants;
            case "LCZ_Toilets": return RoomType.LczToilets;
            case "LCZ_Airlock": return RoomType.LczAirlock;
            case "LCZ_173": return RoomType.Lcz173;
            case "LCZ_ClassDSpawn": return RoomType.LczClassDSpawn;
            case "LCZ_ChkpB": return RoomType.LczCheckpointB;
            case "LCZ_372": return RoomType.LczGlassBox;
            case "LCZ_ChkpA": return RoomType.LczCheckpointA;
            case "HCZ_079": return RoomType.Hcz079;
            case "HCZ_Room3ar": return RoomType.HczArmory;
            case "HCZ_Testroom": return RoomType.HczTestRoom;
            case "HCZ_Hid": return RoomType.HczHid;
            case "HCZ_049": return RoomType.Hcz049;
            case "HCZ_Crossing": return RoomType.HczCrossing;
            case "HCZ_106": return RoomType.Hcz106;
            case "HCZ_Nuke": return RoomType.HczNuke;
            case "HCZ_Tesla": return RoomType.HczTesla;
            case "HCZ_Servers": return RoomType.HczServers;
            case "HCZ_Room3": return RoomType.HczTCross;
            case "HCZ_457": return RoomType.Hcz096;
            case "HCZ_Curve": return RoomType.HczCurve;
            case "HCZ_Straight": return RoomType.HczStraight;
            case "EZ_Endoof": return RoomType.EntRedVent;
            case "EZ_Intercom": return RoomType.EntIntercom;
            case "EZ_GateA": return RoomType.EntGateA;
            case "EZ_PCs_small": return RoomType.EntDownstairsPcs;
            case "EZ_Curve": return RoomType.EntCurve;
            case "EZ_PCs": return RoomType.EntPcs;
            case "EZ_Crossing": return RoomType.EntCrossing;
            case "EZ_CollapsedTunnel": return RoomType.EntCollapsedTunnel;
            case "EZ_Smallrooms2": return RoomType.EntConference;
            case "EZ_Straight": return RoomType.EntStraight;
            case "EZ_Cafeteria": return RoomType.EntCafeteria;
            case "EZ_upstairs": return RoomType.EntUpstairsPcs;
            case "EZ_GateB": return RoomType.EntGateB;
            case "EZ_Shelter": return RoomType.EntShelter;
            case "EZ_ThreeWay": return RoomType.EntTCross;
            case "PocketWorld": return RoomType.Pocket;
            case "Outside": return RoomType.Surface;
            case "HCZ_939": return RoomType.Hcz939;
            case "EZ Part": return RoomType.EntCheckpointHallway;
            case "HCZ_ChkpA": return RoomType.HczElevatorA;
            case "HCZ_ChkpB": return RoomType.HczElevatorB;
            default:
                if (gameObject.transform.parent != null && gameObject.transform.parent.name == "HCZ_EZ_Checkpoint (A)")
                    return RoomType.HczEzCheckpointA;
                if (gameObject.transform.parent != null && gameObject.transform.parent.name == "HCZ_EZ_Checkpoint (B)")
                    return RoomType.HczEzCheckpointB;
                return RoomType.Unknown;
        }
    }
}
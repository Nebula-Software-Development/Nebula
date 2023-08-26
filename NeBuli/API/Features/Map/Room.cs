using MapGeneration;
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
    }

    /// <summary>
    /// Gets the rooms base.
    /// </summary>
    public RoomIdentifier Base { get; }

    /// <summary>
    /// Gets the rooms <see cref="RoomLightController"/>.
    /// </summary>
    public RoomLightController LightController { get; internal set; }

    /// <summary>
    /// Gets a collection of all the rooms.
    /// </summary>
    public static IEnumerable<Room> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the rooms on the server.
    /// </summary>
    public static List<Room> List => Collection.ToList();

    /// <summary>
    /// Gets the rooms current <see cref="Vector3"/> position.
    /// </summary>
    public Vector3 Position => Base.transform.position;

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
    /// <param name="duration"></param>
    public void DisableRoomLights(int duration) => LightController.ServerFlickerLights(duration);

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
            return List.Where(room => room.Zone == zone).OrderBy(_ => Loader.Loader.Random.Next()).FirstOrDefault();
    }

    /// <summary>
    /// Gets a list of all the rooms in a specified zone.
    /// </summary>
    /// <param name="zone"></param>
    /// <returns></returns>
    public static List<Room> GetRoomsInZone(FacilityZone zone) => List.Where(room => room.Zone == zone).ToList();

    /// <summary>
    /// Gets if the rooms light are enabled.
    /// </summary>
    public bool LightsEnabled => LightController.LightsEnabled;

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
    public RoomName Name => Base.Name;

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
    /// Gets a <see cref="Room"/> based off the given <see cref="Vector3"/>.
    /// </summary>
    /// <param name="position">The positon to look for a room at.</param>
    /// <returns></returns>
    public static Room Get(Vector3 position) => RoomIdUtils.RoomAtPositionRaycasts(position, true) is RoomIdentifier roomIdentifier ? Get(roomIdentifier) : GetNearestRoom(position);

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
}
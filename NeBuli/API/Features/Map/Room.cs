using MapGeneration;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nebuli.API.Features.Map;

public class Room
{
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

    public Vector3 GetGlobalPoint(Vector3 localPoint) => Transform.TransformPoint(localPoint);

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
    public static Room Get(Vector3 position) => RoomIdUtils.RoomAtPositionRaycasts(position, true) is RoomIdentifier roomIdentifier ? Get(roomIdentifier) : null;
}
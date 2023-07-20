using MapGeneration;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Nebuli.API.Features.Map;

public class Room
{
    public static readonly Dictionary<RoomIdentifier, Room> Dictionary = new();

    internal Room(RoomIdentifier identifier)
    {
        Base = identifier;

        Dictionary.Add(identifier, this);
    }

    public RoomIdentifier Base { get; }

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
    public static Room Get(RoomIdentifier identifier)
    {
        return Dictionary.TryGetValue(identifier, out var room) ? room : new Room(identifier);
    }

    /// <summary>
    /// Gets a <see cref="Room"/> based off the given <see cref="Vector3"/>.
    /// </summary>
    /// <param name="position">The positon to look for a room at.</param>
    /// <returns></returns>
    public static Room Get(Vector3 position)
    {
        return RoomIdUtils.RoomAtPositionRaycasts(position, true) is RoomIdentifier roomIdentifier ? Get(roomIdentifier) : null;
    }
}
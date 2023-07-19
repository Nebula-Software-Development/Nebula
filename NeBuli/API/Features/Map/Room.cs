using System.Collections.Generic;
using System.Linq;
using MapGeneration;
using Nebuli.Enum;
using UnityEngine;

namespace Nebuli.API.Features.Map;

public class Room
{
    public static readonly Dictionary<RoomIdentifier, Room> Dictionary = new ();

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
    
    public Vector3 Position => Base.transform.position;
    
    public Quaternion Rotation => Base.transform.rotation;
    
    public GameObject GameObject => Base.gameObject;
    
    public Transform Transform => Base.transform;
    
    public RoomName Name => Base.Name;

    public FacilityZone Zone => Base.Zone;
    
    public Vector3 GetGlobalPoint(Vector3 localPoint) => Transform.TransformPoint(localPoint);

    public Vector3 GetLocalPoint(Vector3 globalPoint) => Transform.InverseTransformPoint(globalPoint);
    
    public static Room Get(RoomIdentifier identifier)
    {
        return Dictionary.TryGetValue(identifier, out var room) ? room : new Room(identifier);
    }

    public static Room Get(Vector3 position)
    {
        return RoomIdUtils.RoomAtPositionRaycasts(position, true) is RoomIdentifier roomIdentifier ? Get(roomIdentifier) : null;
    }
}
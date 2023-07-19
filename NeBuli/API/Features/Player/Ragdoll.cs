using PlayerRoles;
using PlayerStatsSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nebuli.API.Features.Player;

/// <summary>
/// Allows easier use of in-game ragdolls by wrapping the <see cref="BasicRagdoll"/> class.
/// </summary>
public class Ragdoll
{
    /// <summary>
    /// Gets a Dictionary of <see cref="BasicRagdoll"/>, and their wrapper class <see cref="Ragdoll"/>.
    /// </summary>
    public static readonly Dictionary<BasicRagdoll, Ragdoll> Dictionary = new();

    /// <summary>
    /// Gets the <see cref="BasicRagdoll"/> that this class is wrapping.
    /// </summary>
    public BasicRagdoll Base { get; }

    /// <summary>
    /// Gets the RagdollData from the <see cref="BasicRagdoll"/>.
    /// </summary>
    public RagdollData RagdollData => Base.Info;

    /// <summary>
    /// Tries to get the player assosiated with the ragdoll.
    /// </summary>
    public NebuliPlayer OwnerPlayer => NebuliPlayer.Get(ReferenceHub);

    internal Ragdoll(BasicRagdoll basicRagdoll)
    {
        Base = basicRagdoll;
        Dictionary.Add(basicRagdoll, this);
    }

    public static IEnumerable<Ragdoll> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the ragdolls on the server.
    /// </summary>
    public static List<Ragdoll> List => Collection.ToList();

    /// <summary>
    /// Gets the owner's ReferenceHub of the ragdoll.
    /// </summary>
    public ReferenceHub ReferenceHub
    {
        get => Base.NetworkInfo.OwnerHub;
    }

    /// <summary>
    /// Get or set if the Ragdoll is frozen or not.
    /// </summary>
    public bool IsFrozen
    {
        get => Base._frozen;
        set => Base._frozen = value;
    }

    /// <summary>
    /// Gets the ragdolls transform.
    /// </summary>
    public Transform Transform
    {
        get => Base.transform;
    }

    /// <summary>
    /// Gets the ragdolls GameObject.
    /// </summary>
    public GameObject GameObject
    {
        get => Base.gameObject;
    }

    /// <summary>
    /// Get or set the ragdolls origin point.
    /// </summary>
    public Transform OriginPoint
    {
        get => Base._originPoint;
        set => Base._originPoint = value;
    }

    /// <summary>
    /// Get or set the ragdolls starting position.
    /// </summary>
    public Vector3 StartPosition
    {
        get => Base.NetworkInfo.StartPosition;
        set => new RagdollData(ReferenceHub, DamageHandlerBase, RoleTypeId, value, RagdollRotation, RagdollName, CreationTime);
    }

    /// <summary>
    /// Get or set the <see cref="PlayerRoles.RoleTypeId"/> of the ragdoll.
    /// </summary>
    public RoleTypeId RoleTypeId
    {
        get => Base.NetworkInfo.RoleType;
        set => new RagdollData(ReferenceHub, DamageHandlerBase, value, StartPosition, RagdollRotation, RagdollName, CreationTime);
    }

    /// <summary>
    /// Get or sets the <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the ragdoll.
    /// </summary>
    public DamageHandlerBase DamageHandlerBase
    {
        get => Base.NetworkInfo.Handler;
        set => new RagdollData(ReferenceHub, value, RoleTypeId, StartPosition, RagdollRotation, RagdollName, CreationTime);
    }

    /// <summary>
    /// Get or sest the Nickname of the ragdoll.
    /// </summary>
    public string RagdollName
    {
        get => Base.NetworkInfo.Nickname;
        set => new RagdollData(ReferenceHub, DamageHandlerBase, RoleTypeId, StartPosition, RagdollRotation, value, CreationTime);
    }

    /// <summary>
    /// Get or sets the ragdolls start rotation.
    /// </summary>
    public Quaternion RagdollRotation
    {
        get => Base.NetworkInfo.StartRotation;
        set => new RagdollData(ReferenceHub, DamageHandlerBase, RoleTypeId, StartPosition, value, RagdollName, CreationTime);
    }

    /// <summary>
    /// Get or sets the creation time of the ragdoll
    /// </summary>
    public double CreationTime
    {
        get => Base.NetworkInfo.CreationTime;
        set => new RagdollData(ReferenceHub, DamageHandlerBase, RoleTypeId, StartPosition, RagdollRotation, RagdollName, value);
    }

    public static Ragdoll Get(BasicRagdoll ragdollBase)
    {
        if (Dictionary.TryGetValue(ragdollBase, out Ragdoll ragdoll))
            return ragdoll;

        return new Ragdoll(ragdollBase);
    }
}
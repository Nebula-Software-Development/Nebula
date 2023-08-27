using PlayerRoles;
using PlayerStatsSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Nebuli.API.Features.Player;
using System;
using Object = UnityEngine.Object;

namespace Nebuli.API.Features;

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
    /// Gets or sets the RagdollData from the <see cref="BasicRagdoll"/>.
    /// </summary>
    public RagdollData RagdollData
    {
        get => Base.Info;
        set => Base.Info = value;
    }

    /// <summary>
    /// Tries to get the player assosiated with the ragdoll.
    /// </summary>
    public NebuliPlayer OwnerPlayer => NebuliPlayer.Get(ReferenceHub);

    internal Ragdoll(BasicRagdoll basicRagdoll)
    {
        if (basicRagdoll.NetworkInfo.OwnerHub is null) return;
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
    public ReferenceHub ReferenceHub => Base.NetworkInfo.OwnerHub;

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
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the ragdolls GameObject.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

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
        set => RagdollData = new RagdollData(ReferenceHub, DamageHandlerBase, RoleTypeId, value, RagdollRotation, RagdollName, CreationTime);
    }

    /// <summary>
    /// Get or set the <see cref="PlayerRoles.RoleTypeId"/> of the ragdoll.
    /// </summary>
    public RoleTypeId RoleTypeId
    {
        get => Base.NetworkInfo.RoleType;
        set => RagdollData = new RagdollData(ReferenceHub, DamageHandlerBase, value, StartPosition, RagdollRotation, RagdollName, CreationTime);
    }

    /// <summary>
    /// Get or sets the <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the ragdoll.
    /// </summary>
    public DamageHandlerBase DamageHandlerBase
    {
        get => Base.NetworkInfo.Handler;
        set => RagdollData = new RagdollData(ReferenceHub, value, RoleTypeId, StartPosition, RagdollRotation, RagdollName, CreationTime);
    }

    /// <summary>
    /// Get or sets the Nickname of the ragdoll.
    /// </summary>
    public string RagdollName
    {
        get => Base.NetworkInfo.Nickname;
        set => RagdollData = new RagdollData(ReferenceHub, DamageHandlerBase, RoleTypeId, StartPosition, RagdollRotation, value, CreationTime);
    }

    /// <summary>
    /// Get or sets the ragdolls start rotation.
    /// </summary>
    public Quaternion RagdollRotation
    {
        get => Base.NetworkInfo.StartRotation;
        set => RagdollData = new RagdollData(ReferenceHub, DamageHandlerBase, RoleTypeId, StartPosition, value, RagdollName, CreationTime);
    }

    /// <summary>
    /// Get or sets the creation time of the ragdoll
    /// </summary>
    public double CreationTime
    {
        get => Base.NetworkInfo.CreationTime;
        set => RagdollData = new RagdollData(ReferenceHub, DamageHandlerBase, RoleTypeId, StartPosition, RagdollRotation, RagdollName, value);
    }

    /// <summary>
    /// Gets the existence time of the ragdoll
    /// </summary>
    public TimeSpan ExistenceTime => TimeSpan.FromSeconds(Base.NetworkInfo.ExistenceTime);

    /// <summary>
    /// Gets or creates a new ragdoll with the specified <see cref="BasicRagdoll"/>.
    /// </summary>
    /// <param name="ragdollBase">The <see cref="BasicRagdoll"/> to use to look.</param>
    /// <returns></returns>
    public static Ragdoll Get(BasicRagdoll ragdollBase) => Dictionary.TryGetValue(ragdollBase, out Ragdoll rag) ? rag : new Ragdoll(ragdollBase);

    /// <summary>
    /// Destroys the ragdoll.
    /// </summary>
    public void Destroy() => Object.Destroy(GameObject);
}
// -----------------------------------------------------------------------
// <copyright file=Ragdoll.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Mirror;
using Nebuli.API.Extensions;
using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.Ragdolls;
using PlayerStatsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    /// Gets if the ragdoll is a <see cref="DynamicRagdoll"/>.
    /// </summary>
    public bool IsDynamic { get; } = false;

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
        if (basicRagdoll.NetworkInfo.OwnerHub is null) ReferenceHub = Server.NebuliHost.ReferenceHub;
        else ReferenceHub = basicRagdoll.NetworkInfo.OwnerHub;
        Base = basicRagdoll;
        Dictionary.UpdateOrAdd(basicRagdoll, this);
        if (basicRagdoll is DynamicRagdoll) IsDynamic = true;
    }

    public static IEnumerable<Ragdoll> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the ragdolls on the server.
    /// </summary>
    public static List<Ragdoll> List => Collection.ToList();

    /// <summary>
    /// Gets the owner's ReferenceHub of the ragdoll.
    /// </summary>
    public ReferenceHub ReferenceHub { get; }

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
    /// Creates a new ragdoll.
    /// </summary>
    /// <param name="networkInfo">The data associated with the ragdoll.</param>
    /// <param name="ragdoll">The ragdoll created.</param>
    public static bool Create(RagdollData networkInfo, out Ragdoll ragdoll)
    {
        ragdoll = null;

        if (networkInfo.RoleType.GetBaseRole() is not IRagdollRole ragdollRole)
            return false;

        GameObject modelRagdoll = ragdollRole.Ragdoll.gameObject;
        if (modelRagdoll == null || !Object.Instantiate(modelRagdoll).TryGetComponent(out BasicRagdoll basicRagdoll))
            return false;

        basicRagdoll.NetworkInfo = networkInfo;

        ragdoll = new Ragdoll(basicRagdoll)
        {
            StartPosition = networkInfo.StartPosition,
            RagdollRotation = networkInfo.StartRotation
        };

        return true;
    }

    /// <summary>
    /// Creates a new <see cref="Ragdoll"/> with the specified parameters.
    /// </summary>
    /// <param name="Nickname">The nickname associated with the ragdoll.</param>
    /// <param name="role">The <see cref="PlayerRoles.RoleTypeId"/> of the ragdoll's role.</param>
    /// <param name="damageHandlerBase">The <see cref="PlayerStatsSystem.DamageHandlerBase"/> for the ragdoll.</param>
    /// <param name="owner">The optional <see cref="NebuliPlayer"/> owner of the ragdoll.</param>
    /// <param name="position">The optional position of the ragdoll.</param>
    /// <param name="rotation">The optional rotation of the ragdoll.</param>
    /// <param name="creationTime">The optional creation time of the ragdoll.</param>
    /// <returns>The created <see cref="Ragdoll"/> instance, or <c>null</c> if creation failed.</returns>
    public static Ragdoll Create(string Nickname, RoleTypeId role, DamageHandlerBase damageHandlerBase, NebuliPlayer owner = null, Vector3 position = default, Quaternion rotation = default, double creationTime = default)
    {
        owner ??= Server.NebuliHost;
        if (Create(new RagdollData(owner.ReferenceHub, damageHandlerBase, roleType: role, position, rotation, Nickname, creationTime), out Ragdoll ragdoll))
            return ragdoll;
        return null;
    }

    /// <summary>
    /// Creates a new <see cref="Ragdoll"/> with the specified parameters and spawns it.
    /// </summary>
    /// /// <param name="Nickname">The nickname associated with the ragdoll.</param>
    /// <param name="role">The <see cref="PlayerRoles.RoleTypeId"/> of the ragdoll's role.</param>
    /// <param name="damageHandlerBase">The <see cref="PlayerStatsSystem.DamageHandlerBase"/> for the ragdoll.</param>
    /// <param name="owner">The optional <see cref="NebuliPlayer"/> owner of the ragdoll.</param>
    /// <param name="position">The optional position of the ragdoll.</param>
    /// <param name="rotation">The optional rotation of the ragdoll.</param>
    /// <param name="creationTime">The optional creation time of the ragdoll.</param>
    /// <returns>The created <see cref="Ragdoll"/> instance, or <c>null</c> if creation failed.</returns>
    public static Ragdoll CreateAndSpawn(string Nickname, RoleTypeId role, DamageHandlerBase damageHandlerBase, NebuliPlayer owner = null, Vector3 position = default, Quaternion rotation = default, double creationTime = default)
    {
        owner ??= Server.NebuliHost;
        if (Create(new RagdollData(owner.ReferenceHub, damageHandlerBase, roleType: role, position, rotation, Nickname, creationTime), out Ragdoll ragdoll))
        {
            ragdoll.Spawn();
            return ragdoll;
        }
        return null;
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
    public void Destroy() => NetworkServer.Destroy(GameObject);

    /// <summary>
    /// Spawns the ragdoll.
    /// </summary>
    public void Spawn() => NetworkServer.Spawn(GameObject);

    /// <summary>
    /// Despawns the ragdoll.
    /// </summary>
    public void Despawn() => NetworkServer.UnSpawn(GameObject);
}
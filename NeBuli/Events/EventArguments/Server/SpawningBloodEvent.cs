using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;
using UnityEngine;

namespace Nebuli.Events.EventArguments.Server;

/// <summary>
/// Triggered before blood spawns.
/// </summary>
public class SpawningBloodEvent : EventArgs, ICancellableEvent, IPlayerEvent
{
    public SpawningBloodEvent(ReferenceHub hub, Ray ray, RaycastHit hit, IDestructible target)
    {
        Player = NebuliPlayer.TryGet(hub, out NebuliPlayer ply) ? ply : API.Features.Server.NebuliHost;
        Target = NebuliPlayer.TryGet(target.NetworkId, out NebuliPlayer dt) ? dt : null;
        Ray = ray;
        RaycastHit = hit;
        Position = hit.point;
        IsCancelled = false;
    }

    public bool IsCancelled { get; set; }

    /// <summary>
    /// The <see cref="UnityEngine.Ray"/> for placing the blood.
    /// </summary>
    public Ray Ray { get; set; }

    /// <summary>
    /// The position the blood will be placed at.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// The <see cref="UnityEngine.RaycastHit"/> for placing the blood.
    /// </summary>
    public RaycastHit RaycastHit { get; set; }

    /// <summary>
    /// The <see cref="NebuliPlayer"/> placing the blood.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The <see cref="NebuliPlayer"/> thats being attacked, or null if none.
    /// </summary>
    public NebuliPlayer Target { get; }
}
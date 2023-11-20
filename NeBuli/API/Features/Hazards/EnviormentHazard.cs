// -----------------------------------------------------------------------
// <copyright file=EnviormentHazard.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Hazards;
using Nebuli.API.Features.Map;
using Nebuli.API.Features.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnviormentalHazardBase = Hazards.EnvironmentalHazard;

namespace Nebuli.API.Features.Hazards;

public class EnviormentHazard
{
    /// <summary>
    /// Gets a Dictionary of <see cref="EnviormentalHazardBase"/>, and their wrapper class <see cref="EnviormentHazard"/>.
    /// </summary>
    public static Dictionary<EnviormentalHazardBase, EnviormentHazard> Dictionary = new();

    /// <summary>
    /// Gets the <see cref="EnviormentalHazardBase"/> base.
    /// </summary>
    public EnviormentalHazardBase Base { get; }

    internal EnviormentHazard(EnviormentalHazardBase hazardBase)
    {
        Base = hazardBase;
        Dictionary.Add(Base, this);
    }

    /// <summary>
    /// Gets a collection of all the current hazards.
    /// </summary>
    public static IEnumerable<EnviormentHazard> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the current hazards.
    /// </summary>
    public static List<EnviormentHazard> List => Collection.ToList();

    /// <summary>
    /// Gets the hazards <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets if the hazard is active.
    /// </summary>
    public bool Active => Base.IsActive;

    /// <summary>
    /// Gets or sets the hazards position.
    /// </summary>
    public Vector3 Position
    {
        get => Base.SourcePosition;
        set => Base.SourcePosition = value;
    }

    /// <summary>
    /// Gets or sets the hazards position offset.
    /// </summary>
    public Vector3 SourceOffset
    {
        get => Base.SourceOffset;
        set => Base.SourceOffset = value;
    }

    /// <summary>
    /// Gets or sets the hazards max distance.
    /// </summary>
    public float MaxDistance
    {
        get => Base.MaxDistance;
        set => Base.MaxDistance = value;
    }

    /// <summary>
    /// Gets or sets the hazards max height distance.
    /// </summary>
    public float MaxHeightDistance
    {
        get => Base.MaxHeightDistance;
        set => Base.MaxHeightDistance = value;
    }

    /// <summary>
    /// Gets the hazards current <see cref="Map.Room"/>.
    /// </summary>
    public Room Room => Room.Get(Position);

    /// <summary>
    /// Gets a enviormental hazard by its <see cref="Map.Room"/>.
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    public static EnviormentHazard Get(Room room) => List.FirstOrDefault(x => x.Room == room);

    /// <summary>
    /// Gets a <see cref="EnviormentHazard"/> by its base, <see cref="EnviormentalHazardBase"/>. If none found, one is created.
    /// </summary>
    public static EnviormentHazard Get(EnviormentalHazardBase enviormentalHazardBase)
    {
        return Dictionary.TryGetValue(enviormentalHazardBase, out EnviormentHazard hazard) ? hazard : enviormentalHazardBase switch
        {
            TantrumEnvironmentalHazard tantrum => new TantrumHazard(tantrum),
            PlayerRoles.PlayableScps.Scp939.Scp939AmnesticCloudInstance hazard939 => new AmesticCloudHazard(hazard939),
            SinkholeEnvironmentalHazard sinkhole => new SinkholeHazard(sinkhole),
            global::Hazards.TemporaryHazard temporaryHazard => new TemporaryHazard(temporaryHazard),
            _ => new EnviormentHazard(enviormentalHazardBase),
        };
    }

    /// <summary>
    /// Gets a list of currently affected players of the hazard.
    /// </summary>
    public List<NebuliPlayer> AffectedPlayers => Base.AffectedPlayers.Select(hub => NebuliPlayer.Get(hub)).ToList();

    /// <summary>
    /// Gets if the position is in the area of the hazard.
    /// </summary>
    public bool IsInArea(Vector3 position) => Base.IsInArea(Position, position);
}
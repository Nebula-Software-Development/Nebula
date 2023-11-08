// -----------------------------------------------------------------------
// <copyright file=SinkholeHazard.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using SinkholeHazardBase = Hazards.SinkholeEnvironmentalHazard;

namespace Nebuli.API.Features.Hazards;

public class SinkholeHazard : EnviormentHazard
{
    /// <summary>
    /// Gets the <see cref="SinkholeHazardBase"/> base.
    /// </summary>
    public new SinkholeHazardBase Base { get; }

    internal SinkholeHazard(SinkholeHazardBase hazardBase) : base(hazardBase)
    {
        Base = hazardBase;
    }
}
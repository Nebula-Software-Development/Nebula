// -----------------------------------------------------------------------
// <copyright file=SinkholeHazard.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using SinkholeHazardBase = Hazards.SinkholeEnvironmentalHazard;

namespace Nebula.API.Features.Hazards
{
    public class SinkholeHazard : EnviormentHazard
    {
        internal SinkholeHazard(SinkholeHazardBase hazardBase) : base(hazardBase)
        {
            Base = hazardBase;
        }

        /// <summary>
        ///     Gets the <see cref="SinkholeHazardBase" /> base.
        /// </summary>
        public new SinkholeHazardBase Base { get; }
    }
}
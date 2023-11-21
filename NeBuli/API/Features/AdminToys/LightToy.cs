// -----------------------------------------------------------------------
// <copyright file=LightToy.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Linq;
using Nebuli.API.Features.Enum;
using Nebuli.API.Features.Map;
using UnityEngine;
using SLightToy = AdminToys.LightSourceToy;

namespace Nebuli.API.Features.AdminToys
{
    /// <summary>
    ///     Wrapper class for <see cref="global::AdminToys.LightSourceToy" />.
    /// </summary>
    public class LightToy : AdminToy
    {
        internal LightToy(SLightToy adminToyBase) : base(adminToyBase, ToyType.Light)
        {
            Base = adminToyBase;
        }

        /// <summary>
        ///     Gets the <see cref="SLightToy" /> (base) of the <see cref="LightToy" />.
        /// </summary>
        public new SLightToy Base { get; }

        /// <summary>
        ///     Gets or sets the lights <see cref="UnityEngine.Color" />.
        /// </summary>
        public Color Color
        {
            get => Base.NetworkLightColor;
            set => Base.NetworkLightColor = value;
        }

        /// <summary>
        ///     Gets or sets the lights intensity.
        /// </summary>
        public float Intensity
        {
            get => Base.NetworkLightIntensity;
            set => Base.NetworkLightIntensity = value;
        }

        /// <summary>
        ///     Gets or sets the lights range.
        /// </summary>
        public float Range
        {
            get => Base.NetworkLightRange;
            set => Base.NetworkLightRange = value;
        }

        /// <summary>
        ///     Gets or sets if the lights shadows are enabled.
        /// </summary>
        public bool ShadowsEnabled
        {
            get => Base.NetworkLightShadows;
            set => Base.NetworkLightShadows = value;
        }

        /// <summary>
        ///     Gets a <see cref="LightToy" /> with the matching <see cref="SLightToy" /> base.
        /// </summary>
        public static LightToy Get(SLightToy primitiveObjectToy)
        {
            return Utilites.AdminToys
                .FirstOrDefault(x => x.Base == primitiveObjectToy) as LightToy ?? new LightToy(primitiveObjectToy);
        }

        /// <summary>
        ///     Creates a new <see cref="LightToy" />.
        /// </summary>
        public static LightToy CreateNew(Vector3 position = default, Quaternion rotation = default,
            Vector3 scale = default, bool spawnOnCreate = true)
        {
            LightToy newLight = new(Object.Instantiate(ToyUtilities.LightBase))
            {
                Position = position,
                Rotation = rotation,
                Scale = scale
            };
            if (spawnOnCreate)
            {
                newLight.SpawnToy();
            }

            return newLight;
        }
    }
}
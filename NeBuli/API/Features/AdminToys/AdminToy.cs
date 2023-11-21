// -----------------------------------------------------------------------
// <copyright file=AdminToy.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Linq;
using Mirror;
using Nebuli.API.Features.Enum;
using Nebuli.API.Features.Map;
using UnityEngine;
using SLAdminToy = AdminToys.AdminToyBase;

namespace Nebuli.API.Features.AdminToys
{
    /// <summary>
    ///     Wrapper class for <see cref="global::AdminToys.AdminToyBase" />.
    /// </summary>
    public class AdminToy
    {
        internal AdminToy(SLAdminToy adminToyBase, ToyType toyType)
        {
            Base = adminToyBase;
            Type = toyType;
            Utilites.AdminToys.Add(this);
        }

        /// <summary>
        ///     Gets the <see cref="global::AdminToys.AdminToyBase" /> (base) of the <see cref="AdminToy" />.
        /// </summary>
        public SLAdminToy Base { get; }

        /// <summary>
        ///     Gets the <see cref="ToyType" /> of the primitive.
        /// </summary>
        public ToyType Type { get; }

        /// <summary>
        ///     Gets or sets the <see cref="AdminToy" /> position.
        /// </summary>
        public Vector3 Position
        {
            get => Base.NetworkPosition;
            set => Base.NetworkPosition = value;
        }

        /// <summary>
        ///     Gets the <see cref="AdminToy" /> <see cref="UnityEngine.GameObject" />.
        /// </summary>
        public GameObject GameObject => Base.gameObject;

        /// <summary>
        ///     Gets or sets the <see cref="AdminToy" /> scale.
        /// </summary>
        public Vector3 Scale
        {
            get => Base.NetworkScale;
            set => Base.NetworkScale = value;
        }

        /// <summary>
        ///     Gets or sets the <see cref="AdminToy" /> rotation.
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                LowPrecisionQuaternion lowPrecisionQuaternion = Base.NetworkRotation;
                return new Quaternion(lowPrecisionQuaternion._x, lowPrecisionQuaternion._y, lowPrecisionQuaternion._z,
                    lowPrecisionQuaternion._w);
            }
            set
            {
                LowPrecisionQuaternion lowPrecisionQuaternion = new(value);
                Base.NetworkRotation = lowPrecisionQuaternion;
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="AdminToy" /> MovementSmoothing value.
        /// </summary>
        public byte MovementSmoothness
        {
            get => Base.NetworkMovementSmoothing;
            set => Base.NetworkMovementSmoothing = value;
        }

        /// <summary>
        ///     Gets a <see cref="AdminToy" /> based on the given <see cref="SLAdminToy" />.
        /// </summary>
        /// <param name="toy"></param>
        public static AdminToy Get(SLAdminToy toy)
        {
            return Utilites.AdminToys.FirstOrDefault(x => x.Base == toy);
        }

        /// <summary>
        ///     Spawns the toy.
        /// </summary>
        public void SpawnToy()
        {
            NetworkServer.Spawn(GameObject);
        }

        /// <summary>
        ///     Despawns the toy.
        /// </summary>
        public void UnspawnToy()
        {
            NetworkServer.UnSpawn(GameObject);
        }

        /// <summary>
        ///     Destroys the toy.
        /// </summary>
        public void DestroyToy()
        {
            if (Utilites.AdminToys.Contains(this))
            {
                Utilites.AdminToys.Remove(this);
            }

            NetworkServer.Destroy(GameObject);
        }
    }
}
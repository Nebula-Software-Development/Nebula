// -----------------------------------------------------------------------
// <copyright file=ToyUtilities.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using AdminToys;
using Mirror;
using UnityEngine;

namespace Nebula.API.Features.AdminToys
{
    public static class ToyUtilities
    {
        private static readonly Dictionary<string, Component> primitives = new();

        /// <summary>
        ///     Gets the <see cref="PrimitiveObjectToy" /> object.
        /// </summary>
        public static PrimitiveObjectToy PrimitiveBase => GetBaseObject<PrimitiveObjectToy>("PrimitiveObjectToy");

        /// <summary>
        ///     Gets the <see cref="LightSourceToy" /> object.
        /// </summary>
        public static LightSourceToy LightBase => GetBaseObject<LightSourceToy>("LightSourceToy");

        /// <summary>
        ///     Gets the Sport <see cref="ShootingTarget" /> object.
        /// </summary>
        public static ShootingTarget SportShootingTarget => GetBaseObject<ShootingTarget>("sportTargetPrefab");

        /// <summary>
        ///     Gets the D-Class <see cref="ShootingTarget" /> object.
        /// </summary>
        public static ShootingTarget DClassShootingTarget => GetBaseObject<ShootingTarget>("dboyTargetPrefab");

        /// <summary>
        ///     Gets the Binary <see cref="ShootingTarget" /> object.
        /// </summary>
        public static ShootingTarget BinaryShootingTarget => GetBaseObject<ShootingTarget>("binaryTargetPrefab");

        private static T GetBaseObject<T>(string objectName) where T : Component
        {
            if (!primitives.TryGetValue(objectName, out Component baseObject))
            {
                foreach (GameObject gameObject in NetworkClient.prefabs.Values)
                {
                    if (gameObject.name == objectName && gameObject.TryGetComponent(out T component))
                    {
                        baseObject = component;
                        if (!primitives.ContainsKey(objectName))
                        {
                            primitives.Add(objectName, baseObject);
                        }

                        break;
                    }
                }
            }

            return (T)baseObject;
        }
    }
}
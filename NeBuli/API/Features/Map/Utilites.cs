// -----------------------------------------------------------------------
// <copyright file=Utilites.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Decals;
using Hazards;
using InventorySystem.Items.Firearms.BasicMessages;
using MapGeneration;
using Mirror;
using Nebula.API.Extensions;
using Nebula.API.Features.AdminToys;
using Nebula.API.Features.Items;
using Nebula.API.Features.Items.Pickups;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp173;
using RelativePositioning;
using UnityEngine;
using Utils.Networking;

namespace Nebula.API.Features.Map
{
    public static class Utilites
    {
        private static TantrumEnvironmentalHazard cachedTantrumPrefab;

        /// <summary>
        ///     Gets a list of all current <see cref="AdminToy" /> objects on the server.
        /// </summary>
        public static List<AdminToy> AdminToys = new();

        /// <summary>
        ///     Gets the SCP-173 Tantrum Prefab GameObject.
        /// </summary>
        public static TantrumEnvironmentalHazard TantrumPrefab
        {
            get
            {
                if (cachedTantrumPrefab is not null)
                {
                    return cachedTantrumPrefab;
                }

                if (RoleTypeId.Scp173.GetBaseRole() is Scp173Role scp173Role)
                {
                    if (scp173Role.SubroutineModule.TryGetSubroutine(out Scp173TantrumAbility tantrumAbility))
                    {
                        return cachedTantrumPrefab = tantrumAbility._tantrumPrefab;
                    }
                }

                return null;
            }
        }

        /// <summary>
        ///     Places a SCP-173 tantrum.
        /// </summary>
        /// <param name="position">The positon to place it.</param>
        /// <returns></returns>
        public static TantrumEnvironmentalHazard PlaceTantrum(Vector3 position)
        {
            TantrumEnvironmentalHazard tantrum = Object.Instantiate(TantrumPrefab);
            tantrum.SynchronizedPosition = new RelativePosition(position + (Vector3.up * 0.25f));
            NetworkServer.Spawn(tantrum.gameObject);
            return tantrum;
        }

        /// <summary>
        ///     Turns off all lights in the facility.
        /// </summary>
        /// <param name="duration">The duration of the blackout.</param>
        /// <param name="zones">If not null, will only blackout those specific zones.</param>
        public static void TurnOffAllLights(float duration, List<FacilityZone> zones = null)
        {
            foreach (RoomLightController light in RoomLightController.Instances)
            {
                if (zones is not null && zones.Contains(Room.Get(light.Room).Zone))
                {
                    light.ServerFlickerLights(duration);
                    return;
                }

                light.ServerFlickerLights(duration);
            }
        }

        /// <summary>
        ///     Destroys all pickups in-game.
        /// </summary>
        public static void CleanAllPickups()
        {
            foreach (Pickup pickup in Pickup.List)
            {
                pickup.Destroy();
            }
        }

        /// <summary>
        ///     Destroys all ragdolls in-game.
        /// </summary>
        public static void CleanAllRagdolls()
        {
            foreach (Ragdoll ragdoll in Ragdoll.List)
            {
                ragdoll.Destroy();
            }
        }

        /// <summary>
        ///     Destroys all items in-game.
        /// </summary>
        public static void RemoveAllItems()
        {
            foreach (Item item in Item.List)
            {
                item.Destroy();
            }
        }

        /// <summary>
        ///     Places a decal on the map.
        /// </summary>
        /// <param name="decalPosition"></param>
        /// <param name="decalDirection"></param>
        /// <param name="decalType"></param>
        /// <returns></returns>
        public static GunDecalMessage PlaceDecal(Vector3 decalPosition, Vector3 decalDirection, DecalPoolType decalType)
        {
            GunDecalMessage gunDecalMessage = new(decalPosition, decalDirection, decalType);
            gunDecalMessage.SendToAuthenticated();
            return gunDecalMessage;
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file=Npc.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Mirror;
using Nebula.API.Extensions;
using PlayerRoles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Nebula.API.Features
{
    /// <summary>
    ///     Class for handling <see cref="Npc" /> easily in-game.
    /// </summary>
    public class Npc : Player
    {
        /// <summary>
        ///     Creates a new <see cref="Npc" /> with a specified <see cref="ReferenceHub" />.
        /// </summary>
        /// <param name="hub">The <see cref="ReferenceHub" /> to use to create the NPC.</param>
        internal Npc(ReferenceHub hub) : base(hub)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="Npc" /> with a specified <see cref="GameObject" />.
        /// </summary>
        /// <param name="gameObject">The <see cref="GameObject" /> to use to create the NPC.</param>
        internal Npc(GameObject gameObject) : base(gameObject)
        {
        }

        /// <summary>
        ///     Gets a list of all the <see cref="Npc" /> instances.
        /// </summary>
        public new static List<Npc> List =>
            Player.List
                .Where(player => player is Npc)
                .Cast<Npc>()
                .ToList();


        /// <summary>
        ///     Creates a new NPC with the specified parameters.
        /// </summary>
        /// <param name="name">The name of the NPC.</param>
        /// <param name="role">The <see cref="RoleTypeId" /> of the NPC.</param>
        /// <param name="id">The ID of the NPC.</param>
        /// <param name="userId">The UserID of the NPC.</param>
        /// <returns>A newly created <see cref="Npc" />.</returns>
        public static Npc CreateNPC(string name, RoleTypeId role, int id, string userId = null)
        {
            try
            {
                GameObject newPlayer = Object.Instantiate(NetworkManager.singleton.playerPrefab);
                Npc newNpc = new(newPlayer);
                try
                {
                    newNpc.ReferenceHub.roleManager.InitializeNewRole(RoleTypeId.None, RoleChangeReason.None);
                }
                catch (Exception e)
                {
                    Log.Debug("Safe to ignore, error caused by setting NPC role --->\n" + e);
                }

                NetworkServer.AddPlayerForConnection(new NetworkConnectionToClient(id), newPlayer);

                try
                {
                    newNpc.UserId = userId is not null ? userId : null;
                }
                catch (Exception e)
                {
                    Log.Debug("Safe to ignore, error caused by setting NPC UserID --->\n" + e);
                }

                newNpc.ReferenceHub.nicknameSync.Network_myNickSync = name;

                Timing.CallDelayed(0.4f, () => { newNpc.SetRole(role); });

                newNpc.IsNpc = true;

                return newNpc;
            }
            catch (Exception e)
            {
                Log.Error("Error while creating a NPC! Full error -->\n" + e);
                return null;
            }
        }

        /// <summary>
        ///     Disconnects and destroys the NPC.
        /// </summary>
        public void DestroyNPC()
        {
            NetworkServer.Destroy(GameObject);
            CustomNetworkManager.TypedSingleton.OnServerDisconnect(ReferenceHub.connectionToClient);
            ReferenceHub.OnDestroy();
            Dictionary.RemoveIfContains(ReferenceHub);
        }
    }
}
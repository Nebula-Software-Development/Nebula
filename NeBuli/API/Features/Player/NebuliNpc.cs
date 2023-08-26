using MEC;
using Mirror;
using Nebuli.API.Internal;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Nebuli.API.Features.Player;

/// <summary>
/// Class for handling <see cref="NebuliNpc"/> easily in-game.
/// </summary>
public class NebuliNpc : NebuliPlayer
{
    /// <summary>
    /// Gets a dictionary of all the <see cref="ReferenceHub"/> and <see cref="NebuliNpc"/>.
    /// </summary>
    public static new readonly Dictionary<ReferenceHub, NebuliNpc> Dictionary = new();

    /// <summary>
    /// Creates a new <see cref="NebuliNpc"/> with a specified <see cref="ReferenceHub"/>.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> to use to create the NPC.</param>
    internal NebuliNpc(ReferenceHub hub) : base(hub)
    {
        Dictionary.Add(hub, this);
    }

    /// <summary>
    /// Creates a new <see cref="NebuliNpc"/> with a specified <see cref="GameObject"/>.
    /// </summary>
    /// <param name="gameObject">The <see cref="GameObject"/> to use to create the NPC.</param>
    internal NebuliNpc(GameObject gameObject) : base(gameObject) 
    {      
        Dictionary.Add(ReferenceHub, this);
    }

    /// <summary>
    /// Gets a collection of all the <see cref="NebuliNpc"/> instances.
    /// </summary>
    public static new IEnumerable<NebuliNpc> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the <see cref="NebuliNpc"/> instances.
    /// </summary>
    public static new List<NebuliNpc> List => Collection.ToList();

    /// <summary>
    /// Creates a new NPC with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the NPC.</param>
    /// <param name="role">The <see cref="RoleTypeId"/> of the NPC.</param>
    /// <param name="ID">The ID of the NPC.</param>
    /// <param name="UserId">The UserID of the NPC.</param>
    /// <param name="clientInstanceMode">The <see cref="ClientInstanceMode"/> of the NPC.</param>
    /// <returns></returns>
    public static NebuliNpc CreateNPC(string name, RoleTypeId role, int ID, string UserId = null, ClientInstanceMode clientInstanceMode = ClientInstanceMode.Host)
    {
        try
        {
            GameObject newPlayer = Object.Instantiate(NetworkManager.singleton.playerPrefab);
            NebuliNpc newNPC = new(newPlayer);
            try
            {
                newNPC.ReferenceHub.roleManager.InitializeNewRole(RoleTypeId.None, RoleChangeReason.None);
            }
            catch (Exception e)
            {
                Log.Debug("Safe to ignore, error caused by setting NPC role --->\n" + e);
            }
            FakeConnection fakeConnection = new(ID);
            NetworkServer.AddPlayerForConnection(fakeConnection, newPlayer);
            try
            {
                newNPC.ReferenceHub.characterClassManager.UserId = UserId is not null ? UserId : null;
                newNPC.ReferenceHub.characterClassManager.InstanceMode = clientInstanceMode;
            }
            catch (Exception e)
            {
                Log.Debug("Safe to ignore, error caused by setting NPC UserID --->\n" + e);
            }
            newNPC.ReferenceHub.nicknameSync.Network_myNickSync = name;

            Timing.CallDelayed(0.4f, () =>
            {
                newNPC.SetRole(role);
            });

            newNPC.IsNPC = true;

            newNPC.Health = newNPC.ReferenceHub.playerStats.GetModule<HealthStat>().MaxValue;
            return newNPC;
        }
        catch(Exception e)
        {
            Log.Error("Error while creating a NPC! Full error -->\n" + e);
            return null;
        }
    }

    /// <summary>
    /// Disconnects and destroys the NPC.
    /// </summary>
    public void DestroyNPC()
    {
        NetworkServer.Destroy(GameObject);
        CustomNetworkManager.TypedSingleton.OnServerDisconnect(ReferenceHub.connectionToClient);
        ReferenceHub.OnDestroy();
        Dictionary.Remove(ReferenceHub);
    }

    /// <summary>
    /// Makes the NPC look at the specified position.
    /// </summary>
    /// <param name="position">The position to look at.</param>
    public void LookAt(Vector3 position)
    {
        Vector3 direction = position - Position;
        Quaternion quat = Quaternion.LookRotation(direction, Vector3.up);
        var mouseLook = ((IFpcRole)ReferenceHub.roleManager.CurrentRole).FpcModule.MouseLook;
        (ushort horizontal, ushort vertical) = ToClientUShorts(quat);
        mouseLook.ApplySyncValues(horizontal, vertical);
    }

    internal (ushort horizontal, ushort vertical) ToClientUShorts(Quaternion rotation)
    {
        if (rotation.eulerAngles.z != 0f)
        {
            rotation = Quaternion.LookRotation(rotation * Vector3.forward, Vector3.up);
        }
        float outfHorizontal = rotation.eulerAngles.y;
        float outfVertical = -rotation.eulerAngles.x;

        if (outfVertical < -90f)
        {
            outfVertical += 360f;
        }
        else if (outfVertical > 270f)
        {
            outfVertical -= 360f;
        }

        return (ToHorizontal(outfHorizontal), ToVertical(outfVertical));

        static ushort ToHorizontal(float horizontal)
        {
            const float ToHorizontal = 65535f / 360f;

            horizontal = Mathf.Clamp(horizontal, 0f, 360f);

            return (ushort)Mathf.RoundToInt(horizontal * ToHorizontal);
        }

        static ushort ToVertical(float vertical)
        {
            const float ToVertical = 65535f / 176f;

            vertical = Mathf.Clamp(vertical, -88f, 88f) + 88f;

            return (ushort)Mathf.RoundToInt(vertical * ToVertical);
        }
    }
}

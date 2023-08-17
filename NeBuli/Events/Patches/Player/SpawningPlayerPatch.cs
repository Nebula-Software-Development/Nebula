using HarmonyLib;
using PlayerRoles.FirstPersonControl;
using PlayerRoles;
using UnityEngine;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using static HarmonyLib.AccessTools;
using System.Reflection;
using PlayerRoles.FirstPersonControl.Spawnpoints;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(RoleSpawnpointManager))]
internal class SpawningPlayerPatch
{
    private static MethodInfo TargetMethod() => Method(TypeByName("PlayerRoles.FirstPersonControl.Spawnpoints.RoleSpawnpointManager").GetNestedTypes(all)[1], "<Init>b__2_0");

    private static bool Prefix(ReferenceHub hub, PlayerRoleBase prevRole, PlayerRoleBase newRole)
    {
        if (newRole.ServerSpawnReason is not RoleChangeReason.Destroyed)
        {
            if (newRole is IFpcRole fpcRole && fpcRole.SpawnpointHandler != null &&
                fpcRole.SpawnpointHandler.TryGetSpawnpoint(out Vector3 position, out float horizontalRot))
            {
                HandleFpcRoleSpawn(hub, prevRole, newRole, position, horizontalRot);
            }
            else
            {
                HandleOtherRoleSpawn(hub, prevRole, newRole);
            }
            return false;
        }
        return false;
    }

    private static void HandleFpcRoleSpawn(ReferenceHub hub, PlayerRoleBase prevRole, PlayerRoleBase newRole, Vector3 position, float horizontalRot)
    {
        Vector3 currentPos = position;
        float currentRot = horizontalRot;

        PlayerSpawningEvent ev = new(hub, prevRole, newRole, currentPos, currentRot);
        PlayerHandlers.OnSpawning(ev);

        hub.transform.position = ev.Position;
        if (newRole is IFpcRole fpcRole)
        {
            fpcRole.FpcModule.MouseLook.CurrentHorizontal = ev.HorizontalRotation;
        }
    }

    private static void HandleOtherRoleSpawn(ReferenceHub hub, PlayerRoleBase prevRole, PlayerRoleBase newRole)
    {
        Vector3 currentPos = hub.transform.position;
        float currentRot = (prevRole as IFpcRole)?.FpcModule.MouseLook.CurrentVertical ?? 0;

        PlayerSpawningEvent ev = new(hub, prevRole, newRole, currentPos, currentRot);
        PlayerHandlers.OnSpawning(ev);
    }
}
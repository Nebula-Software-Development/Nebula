// -----------------------------------------------------------------------
// <copyright file=SpawningPlayerPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch]
    internal class SpawningPlayerPatch
    {
        private static MethodInfo TargetMethod()
        {
            return Method(typeof(RoleSpawnpointManager).GetNestedTypes(all)[1], "<Init>b__2_0");
        }

        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<SpawningPlayerPatch>(38, instructions);

            LocalBuilder spawning = generator.DeclareLocal(typeof(PlayerSpawningEvent));
            Label ret = generator.DefineLabel();

            int index = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Ldarg_1);

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Ldarg_2),
                new(OpCodes.Ldarg_3),
                new(OpCodes.Ldloc_1),
                new(OpCodes.Ldloc_2),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerSpawningEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnSpawning))),
                new(OpCodes.Stloc_S, spawning.LocalIndex),
                new(OpCodes.Ldloc_S, spawning.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerSpawningEvent), nameof(PlayerSpawningEvent.Position))),
                new(OpCodes.Stloc_1),
                new(OpCodes.Ldloc_S, spawning.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerSpawningEvent), nameof(PlayerSpawningEvent.HorizontalRotation))),
                new(OpCodes.Stloc_2)
            });

            newInstructions[newInstructions.Count - 1].labels.Add(ret);

            newInstructions.RemoveAt(newInstructions.FindLastIndex(i => i.opcode == OpCodes.Ret));

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
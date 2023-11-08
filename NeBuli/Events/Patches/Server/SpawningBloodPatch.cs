// -----------------------------------------------------------------------
// <copyright file=SpawningBloodPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using HarmonyLib;
using InventorySystem.Items.Firearms.Modules;
using Nebuli.Events.EventArguments.Server;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Server;

[HarmonyPatch(typeof(StandardHitregBase), nameof(StandardHitregBase.PlaceBloodDecal))]
internal class SpawningBloodPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnPlacingBlood(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<SpawningBloodPatch>(22, instructions);

        Label retLabel = generator.DefineLabel();

        LocalBuilder spawning = generator.DeclareLocal(typeof(SpawningBloodEvent));

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(StandardHitregBase), nameof(StandardHitregBase.Hub))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldarg_2),
            new(OpCodes.Ldarg_3),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(SpawningBloodEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(ServerHandlers), nameof(ServerHandlers.OnSpawningBlood))),
            new(OpCodes.Stloc_S, spawning.LocalIndex),
            new(OpCodes.Ldloc_S, spawning.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(SpawningBloodEvent), nameof(SpawningBloodEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, spawning.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(SpawningBloodEvent), nameof(SpawningBloodEvent.Ray))),
            new(OpCodes.Starg_S, 1),
            new(OpCodes.Ldloc_S, spawning.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(SpawningBloodEvent), nameof(SpawningBloodEvent.RaycastHit))),
            new(OpCodes.Starg_S, 2),
            new(OpCodes.Ldarga_S, 2),
            new(OpCodes.Ldloc_S, spawning.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(SpawningBloodEvent), nameof(SpawningBloodEvent.Position))),
            new(OpCodes.Callvirt, PropertySetter(typeof(RaycastHit), nameof(RaycastHit.point))),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
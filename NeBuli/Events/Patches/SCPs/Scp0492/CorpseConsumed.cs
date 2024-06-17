// -----------------------------------------------------------------------
// <copyright file=CorpseConsumed.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp0492;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp0492
{
    [HarmonyPatch(typeof(ZombieConsumeAbility), nameof(ZombieConsumeAbility.ServerComplete))]
    internal class CorpseConsumed
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnCorpseConsumed(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<CorpseConsumed>(18, instructions);

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

            LocalBuilder floatBuilder = generator.DeclareLocal(typeof(float));
            CodeInstruction i = newInstructions.Find(x => x.opcode == OpCodes.Ldc_R4);
            i.opcode = OpCodes.Ldloc_S;
            i.operand = floatBuilder.LocalIndex;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Callvirt, PropertyGetter(typeof(ZombieConsumeAbility), nameof(ZombieConsumeAbility.Owner))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(ZombieConsumeAbility), nameof(ZombieConsumeAbility.CurRagdoll))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp0492CorpseConsumedEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp0492Handlers), nameof(Scp0492Handlers.OnCorpseConsumed))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp0492CorpseConsumedEvent),
                        nameof(Scp0492CorpseConsumedEvent.HealthToReceive))),
                new(OpCodes.Stloc_S, floatBuilder.LocalIndex)
            });

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
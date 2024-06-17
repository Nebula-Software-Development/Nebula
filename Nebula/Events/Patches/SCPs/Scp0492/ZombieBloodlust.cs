// -----------------------------------------------------------------------
// <copyright file=ZombieBloodlust.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
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
    [HarmonyPatch(typeof(ZombieBloodlustAbility), nameof(ZombieBloodlustAbility.AnyTargets))]
    internal class ZombieBloodlust
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnBloodlusting(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<ZombieBloodlust>(57, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_S) - 1;

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_1),
                new(OpCodes.Ldloc_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp0492BloodlustEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp0492Handlers), nameof(Scp0492Handlers.OnBloodLust))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp0492BloodlustEvent), nameof(Scp0492BloodlustEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel)
            });

            newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
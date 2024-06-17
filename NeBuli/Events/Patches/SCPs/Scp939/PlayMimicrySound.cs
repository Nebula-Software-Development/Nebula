// -----------------------------------------------------------------------
// <copyright file=PlayMimicrySound.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp939;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp939
{
    [HarmonyPatch(typeof(EnvironmentalMimicry), nameof(EnvironmentalMimicry.ServerProcessCmd))]
    internal class PlayMimicrySound
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnPlayMimicry(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<PlayMimicrySound>(22, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stfld) + 1;

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(EnvironmentalMimicry), nameof(EnvironmentalMimicry.Owner))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(EnvironmentalMimicry), nameof(EnvironmentalMimicry._syncOption))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939PlaySound))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnPlaySound))),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939PlaySound), nameof(Scp939PlaySound.IsCancelled))),
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
// -----------------------------------------------------------------------
// <copyright file=RemoveSavedVoice.cs company="Nebula-Software-Development">
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
    [HarmonyPatch(typeof(MimicryRecorder), nameof(MimicryRecorder.RemoveRecordingsOfPlayer))]
    internal class RemoveSavedVoice
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnRemoveSavedVoice(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<RemoveSavedVoice>(34, instructions);

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(MimicryRecorder), nameof(MimicryRecorder.Owner))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939RemoveSavedVoiceEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnRemoveVoice))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp939RemoveSavedVoiceEvent),
                        nameof(Scp939RemoveSavedVoiceEvent.IsCancelled))),
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
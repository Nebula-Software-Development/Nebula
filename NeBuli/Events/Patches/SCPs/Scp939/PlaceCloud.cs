// -----------------------------------------------------------------------
// <copyright file=PlaceCloud.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp939;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp939
{
    [HarmonyPatch(typeof(Scp939AmnesticCloudAbility), nameof(Scp939AmnesticCloudAbility.OnStateEnabled))]
    internal class PlaceCloud
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnPlacingCloud(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PlaceCloud>(30, instructions);

            Label retLabel = generator.DefineLabel();
            int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ret) + 1;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp939AmnesticCloudAbility), nameof(Scp939AmnesticCloudAbility.Owner))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939PlaceCloudEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnPlaceCloud))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp939PlaceCloudEvent), nameof(Scp939PlaceCloudEvent.IsCancelled))),
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
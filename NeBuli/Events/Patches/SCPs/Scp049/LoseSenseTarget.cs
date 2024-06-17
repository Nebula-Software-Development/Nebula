// -----------------------------------------------------------------------
// <copyright file=LoseSenseTarget.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp049;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp049
{
    [HarmonyPatch(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility.ServerLoseTarget))]
    internal class LoseSenseTarget
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnLoseSenseTarget(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<LoseSenseTarget>(11, instructions);

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility.Owner))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp049LoseSenseTargetEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp049Handlers), nameof(Scp049Handlers.OnLoseSenseTarget))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp049LoseSenseTargetEvent), nameof(Scp049LoseSenseTargetEvent.IsCancelled))),
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
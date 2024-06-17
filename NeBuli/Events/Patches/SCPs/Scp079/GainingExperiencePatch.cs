// -----------------------------------------------------------------------
// <copyright file=GainingExperiencePatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp079;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp079
{
    [HarmonyPatch(typeof(Scp079TierManager), nameof(Scp079TierManager.ServerGrantExperience))]
    internal class GainingExperiencePatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnGainingExperience(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<GainingExperiencePatch>(23, instructions);

            LocalBuilder gainingExperience = generator.DeclareLocal(typeof(Scp079GainingExperienceEvent));

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp079TierManager), nameof(Scp079TierManager.Owner))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Ldarg_2),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp079GainingExperienceEvent))[0]),
                new(OpCodes.Stloc_S, gainingExperience.LocalIndex),
                new(OpCodes.Ldloc_S, gainingExperience.LocalIndex),
                new(OpCodes.Call, Method(typeof(Scp079Handlers), nameof(Scp079Handlers.OnScp079GainingExpereince))),
                new(OpCodes.Ldloc_S, gainingExperience.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079GainingExperienceEvent),
                        nameof(Scp079GainingExperienceEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, gainingExperience.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079GainingExperienceEvent), nameof(Scp079GainingExperienceEvent.Amount))),
                new(OpCodes.Starg_S, 1),
                new(OpCodes.Ldloc_S, gainingExperience.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079GainingExperienceEvent), nameof(Scp079GainingExperienceEvent.Reason))),
                new(OpCodes.Starg_S, 2)
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
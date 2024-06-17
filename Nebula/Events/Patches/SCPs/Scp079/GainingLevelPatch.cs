// -----------------------------------------------------------------------
// <copyright file=GainingLevelPatch.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
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
    [HarmonyPatch(typeof(Scp079TierManager), nameof(Scp079TierManager.AccessTierIndex), MethodType.Setter)]
    internal class GainingLevelPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnGainingLevel(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<GainingLevelPatch>(53, instructions);

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Sub) - 4;

            LocalBuilder gainingLevel = generator.DeclareLocal(typeof(Scp079GainingLevelEvent));

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp079TierManager), nameof(Scp079TierManager.Owner))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp079GainingLevelEvent))[0]),
                new(OpCodes.Stloc_S, gainingLevel.LocalIndex),
                new(OpCodes.Ldloc_S, gainingLevel.LocalIndex),
                new(OpCodes.Call, Method(typeof(Scp079Handlers), nameof(Scp079Handlers.OnScp079GainingLevel))),
                new(OpCodes.Ldloc_S, gainingLevel.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079GainingLevelEvent), nameof(Scp079GainingLevelEvent.IsCancelled))),
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
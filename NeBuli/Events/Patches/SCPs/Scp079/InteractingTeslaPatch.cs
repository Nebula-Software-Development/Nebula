// -----------------------------------------------------------------------
// <copyright file=InteractingTeslaPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp079;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.Subroutines;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp079
{
    [HarmonyPatch(typeof(Scp079TeslaAbility), nameof(Scp079TeslaAbility.ServerProcessCmd))]
    internal class InteractingTeslaPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnInteractingTesla(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<InteractingTeslaPatch>(58, instructions);

            int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Brtrue_S) + 2;

            LocalBuilder interactingTesla = generator.DeclareLocal(typeof(Scp079GainingLevelEvent));

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Call,
                    PropertyGetter(typeof(StandardSubroutine<Scp079Role>),
                        nameof(StandardSubroutine<Scp079Role>.Owner))),
                new(OpCodes.Ldloc_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp079InteractingTeslaEvent))[0]),
                new(OpCodes.Stloc_S, interactingTesla.LocalIndex),
                new(OpCodes.Ldloc_S, interactingTesla.LocalIndex),
                new(OpCodes.Call, Method(typeof(Scp079Handlers), nameof(Scp079Handlers.OnScp079InteractingTesla))),
                new(OpCodes.Ldloc_S, interactingTesla.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079InteractingTeslaEvent),
                        nameof(Scp079InteractingTeslaEvent.IsCancelled))),
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
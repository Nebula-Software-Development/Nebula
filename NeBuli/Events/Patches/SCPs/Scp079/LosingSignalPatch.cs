// -----------------------------------------------------------------------
// <copyright file=LosingSignalPatch.cs company="NebulaTeam">
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
    [HarmonyPatch(typeof(Scp079LostSignalHandler), nameof(Scp079LostSignalHandler.ServerLoseSignal))]
    internal class LosingSignalPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnLosingSignal(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<LosingSignalPatch>(10, instructions);

            LocalBuilder losingSignal = generator.DeclareLocal(typeof(Scp079LosingSignalEvent));

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079LostSignalHandler), nameof(Scp079LostSignalHandler.Role))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp079LosingSignalEvent))[0]),
                new(OpCodes.Stloc_S, losingSignal.LocalIndex),
                new(OpCodes.Ldloc_S, losingSignal.LocalIndex),
                new(OpCodes.Call, Method(typeof(Scp079Handlers), nameof(Scp079Handlers.OnScp079LosingSignal))),
                new(OpCodes.Ldloc_S, losingSignal.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079LosingSignalEvent), nameof(Scp079LosingSignalEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, losingSignal.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079LosingSignalEvent),
                        nameof(Scp079LosingSignalEvent.DurationOfSignalLoss))),
                new(OpCodes.Starg_S, 1)
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
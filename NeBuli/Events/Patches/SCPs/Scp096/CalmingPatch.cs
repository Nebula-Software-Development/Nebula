// -----------------------------------------------------------------------
// <copyright file=CalmingPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp096;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp096;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp096
{
    [HarmonyPatch(typeof(Scp096RageManager), nameof(Scp096RageManager.ServerEndEnrage))]
    internal class CalmingPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnCalming(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<CalmingPatch>(17, instructions);

            Label retLabel = generator.DefineLabel();

            LocalBuilder calming = generator.DeclareLocal(typeof(Scp096EnragingEvent));

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp096RageManager), nameof(Scp096RageManager.Owner))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp096CalmingEvent))[0]),
                new(OpCodes.Stloc_S, calming.LocalIndex),
                new(OpCodes.Ldloc_S, calming.LocalIndex),
                new(OpCodes.Call, Method(typeof(Scp096Handlers), nameof(Scp096Handlers.OnCalming))),
                new(OpCodes.Ldloc_S, calming.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp096CalmingEvent), nameof(Scp096CalmingEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, calming.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp096CalmingEvent), nameof(Scp096CalmingEvent.ClearingTime))),
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
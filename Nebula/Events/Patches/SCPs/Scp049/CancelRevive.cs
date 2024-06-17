// -----------------------------------------------------------------------
// <copyright file=CancelRevive.cs company="Nebula-Software-Development">
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
    [HarmonyPatch(typeof(RagdollAbilityBase<Scp049Role>), nameof(RagdollAbilityBase<Scp049Role>.ServerValidateCancel))]
    internal class CancelRevive
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnValidatingCancel(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<CancelRevive>(2, instructions);

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(RagdollAbilityBase<Scp049Role>),
                        nameof(RagdollAbilityBase<Scp049Role>.Owner))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp049CancelResurrectEvent))[0]),
                new(OpCodes.Call, Method(typeof(Scp049Handlers), nameof(Scp049Handlers.OnCancelResurrect)))
            });

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
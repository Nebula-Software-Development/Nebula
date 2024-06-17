// -----------------------------------------------------------------------
// <copyright file=DogAttack.cs company="Nebula-Software-Development">
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
using PlayerRoles.PlayableScps.Scp939;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp939
{
    [HarmonyPatch(typeof(Scp939ClawAbility), nameof(Scp939ClawAbility.DamageDestructible))]
    internal class DogAttack
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnClawing(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<DogAttack>(11, instructions);

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939ClawAbility), nameof(Scp939ClawAbility.Owner))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Callvirt, PropertyGetter(typeof(IDestructible), nameof(IDestructible.NetworkId))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939AttackEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnAttack))),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939AttackEvent), nameof(Scp939AttackEvent.IsCancelled))),
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
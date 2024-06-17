// -----------------------------------------------------------------------
// <copyright file=ZombieAttack.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp0492;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp0492
{
    //[HarmonyPatch(typeof(ScpAttackAbilityBase<ZombieRole>), nameof(ScpAttackAbilityBase<ZombieRole>.ServerProcessCmd))]
    internal class ZombieAttack
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnZombieAttack(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<ZombieAttack>(107, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i =>
                i.Calls(Method(typeof(ZombieAttackAbility), nameof(ZombieAttackAbility.ServerPerformAttack)))) - 1;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Callvirt, PropertyGetter(typeof(ZombieAttackAbility), nameof(ZombieAttackAbility.Owner))),
                new(OpCodes.Ldloc_S, 4),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp0492AttackEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp0492Handlers), nameof(Scp0492Handlers.OnAttack))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp0492AttackEvent), nameof(Scp0492AttackEvent.IsCancelled))),
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
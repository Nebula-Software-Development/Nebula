// -----------------------------------------------------------------------
// <copyright file=HurtPlayer.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerStatsSystem;
using PluginAPI.Events;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(PlayerStats), nameof(PlayerStats.DealDamage))]
    internal class HurtPlayer
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnHurting(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<HurtPlayer>(142, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i =>
                i.opcode == OpCodes.Newobj && i.OperandIs(GetDeclaredConstructors(typeof(PlayerDamageEvent))[0])) - 6;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_2).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(PlayerStats), nameof(PlayerStats._hub))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerHurtEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnHurt))),
                new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerHurtEvent), nameof(PlayerHurtEvent.IsCancelled))),
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
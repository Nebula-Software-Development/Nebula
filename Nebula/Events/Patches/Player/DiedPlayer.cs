// -----------------------------------------------------------------------
// <copyright file=DiedPlayer.cs company="Nebula-Software-Development">
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
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(PlayerStats), nameof(PlayerStats.KillPlayer))]
    internal class DiedPlayer
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnDied(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.Count - 1;

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Ldfld, Field(typeof(PlayerStats), nameof(PlayerStats._hub))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerDiedEvent))[0]),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnDied)))
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
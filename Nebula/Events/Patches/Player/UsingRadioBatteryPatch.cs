// -----------------------------------------------------------------------
// <copyright file=UsingRadioBatteryPatch.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using InventorySystem.Items.Radio;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(RadioItem), nameof(RadioItem.Update))]
    internal class UsingRadioBatteryPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnUsingRadioBattery(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<UsingRadioBatteryPatch>(78, instructions);

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_0) + 1;

            LocalBuilder usingBattery = generator.DeclareLocal(typeof(PlayerUsingRadioBatteryEvent));

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Call, PropertyGetter(typeof(RadioItem), nameof(RadioItem.Owner))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldloc_0),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerUsingRadioBatteryEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Dup),
                new(OpCodes.Stloc_S, usingBattery.LocalIndex),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnUsingRadioBattery))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerUsingRadioBatteryEvent),
                        nameof(PlayerUsingRadioBatteryEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, usingBattery.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerUsingRadioBatteryEvent),
                        nameof(PlayerUsingRadioBatteryEvent.DrainAmount))),
                new(OpCodes.Stloc_0)
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
// -----------------------------------------------------------------------
// <copyright file=FlippingCoinPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using InventorySystem.Items;
using InventorySystem.Items.Coin;
using Nebula.API.Features.Enum;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(Coin), nameof(Coin.ServerProcessCmd))]
    internal class FlippingCoinPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnFlipping(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<FlippingCoinPatch>(73, instructions);

            int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Ldarg_0) - 10;

            Label retLabel = generator.DefineLabel();
            LocalBuilder @event = generator.DeclareLocal(typeof(PlayerFlippingCoinEvent));

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(ItemBase), nameof(ItemBase.Owner))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldloc_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerFlippingCoinEvent))[0]),
                new(OpCodes.Stloc_S, @event.LocalIndex),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnFlippingCoin))),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerFlippingCoinEvent), nameof(PlayerFlippingCoinEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerFlippingCoinEvent), nameof(PlayerFlippingCoinEvent.Side))),
                new(OpCodes.Ldc_I4, (int)CoinSide.Tails),
                new(OpCodes.Ceq),
                new(OpCodes.Ldc_I4_1),
                new(OpCodes.Ceq),
                new(OpCodes.Stloc_1)
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
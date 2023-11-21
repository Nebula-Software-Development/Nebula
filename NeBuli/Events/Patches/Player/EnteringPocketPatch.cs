// -----------------------------------------------------------------------
// <copyright file=EnteringPocketPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp106;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player
{
    [HarmonyPatch(typeof(Scp106Attack), nameof(Scp106Attack.ServerShoot))]
    internal class EnteringPocketPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnEntering(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<EnteringPocketPatch>(184, instructions);

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Pop) + 7;

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Ldfld, Field(typeof(Scp106Attack), nameof(Scp106Attack._targetHub))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp106Attack), nameof(Scp106Attack.Owner))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerEnteringPocketDimensionEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnPlayerEnteringPocket))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerEnteringPocketDimensionEvent),
                        nameof(PlayerEnteringPocketDimensionEvent.IsCancelled))),
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
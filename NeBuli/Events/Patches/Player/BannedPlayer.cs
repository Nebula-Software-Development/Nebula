// -----------------------------------------------------------------------
// <copyright file=BannedPlayer.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using CommandSystem;
using Footprinting;
using HarmonyLib;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(BanPlayer), nameof(BanPlayer.BanUser), typeof(Footprint), typeof(ICommandSender),
        typeof(string), typeof(long))]
    internal class BannedPlayer
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnBanning(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<BannedPlayer>(104, instructions);

            Label retLabel = generator.DefineLabel();

            LocalBuilder bannedArgs = generator.DeclareLocal(typeof(PlayerBannedEvent));

            int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ldc_I4_0) + 2;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Ldarg_2),
                new(OpCodes.Ldarg_3),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerBannedEvent))[0]),
                new(OpCodes.Stloc_S, bannedArgs.LocalIndex),
                new(OpCodes.Ldloc_S, bannedArgs.LocalIndex),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnBanned))),
                new(OpCodes.Ldloc_S, bannedArgs.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerBannedEvent), nameof(PlayerBannedEvent.IsCancelled))),
                new(OpCodes.Brfalse_S, retLabel),
                new(OpCodes.Ldloc_S, bannedArgs.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerBannedEvent), nameof(PlayerBannedEvent.Reason))),
                new(OpCodes.Starg_S, 2),
                new(OpCodes.Ldloc_S, bannedArgs.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerBannedEvent), nameof(PlayerBannedEvent.Duration))),
                new(OpCodes.Starg_S, 3)
            });

            newInstructions[newInstructions.Count - 1].WithLabels(retLabel);

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
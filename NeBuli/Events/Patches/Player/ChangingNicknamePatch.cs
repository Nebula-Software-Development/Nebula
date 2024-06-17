// -----------------------------------------------------------------------
// <copyright file=ChangingNicknamePatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(NicknameSync), nameof(NicknameSync.Network_displayName), MethodType.Setter)]
    internal class ChangingNicknamePatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnChangingNickname(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<ChangingNicknamePatch>(11, instructions);

            Label retLabel = generator.DefineLabel();

            LocalBuilder @event = generator.DeclareLocal(typeof(PlayerChangingNicknameEvent));

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(NicknameSync), nameof(NicknameSync._hub))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerChangingNicknameEvent))[0]),
                new(OpCodes.Stloc_S, @event.LocalIndex),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnChangingNickname))),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerChangingNicknameEvent),
                        nameof(PlayerChangingNicknameEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerChangingNicknameEvent), nameof(PlayerChangingNicknameEvent.NewName))),
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
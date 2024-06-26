﻿// -----------------------------------------------------------------------
// <copyright file=NoClipTogglePatch.cs company="Nebula-Software-Development">
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
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.NetworkMessages;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(FpcNoclipToggleMessage), nameof(FpcNoclipToggleMessage.ProcessMessage))]
    internal class NoClipTogglePatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnTogglingNoClip(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<NoClipTogglePatch>(28, instructions);

            Label retLabel = generator.DefineLabel();
            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldloc_0);

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldloc_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Call, Method(typeof(FpcNoclip), nameof(FpcNoclip.IsPermitted))),
                new(OpCodes.Ldloc_0),
                new(OpCodes.Call,
                    Method(typeof(API.Features.Player), nameof(API.Features.Player.Get),
                        new[] { typeof(ReferenceHub) })),
                new(OpCodes.Dup),
                new(OpCodes.Call, PropertyGetter(typeof(API.Features.Player), nameof(API.Features.Player.HasNoClip))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerTogglingNoClipEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnPlayerTogglingNoClip))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerTogglingNoClipEvent), nameof(PlayerTogglingNoClipEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel)
            });
            ;

            newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
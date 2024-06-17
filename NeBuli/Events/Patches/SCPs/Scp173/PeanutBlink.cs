// -----------------------------------------------------------------------
// <copyright file=PeanutBlink.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp173;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp173
{
    [HarmonyPatch(typeof(Scp173BlinkTimer), nameof(Scp173BlinkTimer.ServerBlink))]
    internal class PeanutBlink
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnBlink(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PeanutBlink>(21, instructions);

            LocalBuilder @event = generator.DeclareLocal(typeof(Scp173BlinkEvent));
            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(Scp173BlinkTimer), nameof(Scp173BlinkTimer._fpcModule))),
                new(OpCodes.Ldfld, Field(typeof(Scp173MovementModule), nameof(Scp173MovementModule._role))),
                new(OpCodes.Ldfld, Field(typeof(Scp173Role), nameof(Scp173Role._owner))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(Scp173BlinkTimer), nameof(Scp173BlinkTimer._observers))),
                new(OpCodes.Ldfld, Field(typeof(Scp173ObserversTracker), nameof(Scp173ObserversTracker.Observers))),
                new(OpCodes.Call, Method(typeof(PeanutBlink), nameof(GetPlayersBlinking))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp173BlinkEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp173Handlers), nameof(Scp173Handlers.OnBlink))),
                new(OpCodes.Stloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173BlinkEvent), nameof(Scp173BlinkEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173BlinkEvent), nameof(Scp173BlinkEvent.Position))),
                new(OpCodes.Starg_S, 1)
            });

            newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }

        private static List<API.Features.Player> GetPlayersBlinking(HashSet<ReferenceHub> hubs)
        {
            return hubs.Select(API.Features.Player.Get).ToList();
        }
    }
}
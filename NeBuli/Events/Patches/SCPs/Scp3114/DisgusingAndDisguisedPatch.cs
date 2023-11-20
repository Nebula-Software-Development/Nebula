// -----------------------------------------------------------------------
// <copyright file=DisgusingAndDisguisedPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp3114;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp3114;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp3114;

[HarmonyPatch(typeof(Scp3114Disguise), nameof(Scp3114Disguise.ServerComplete))]
internal class DisgusingAndDisguisedPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnDisguising(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<DisgusingAndDisguisedPatch>(21, instructions);

        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114Disguise), nameof(Scp3114Disguise.Owner))),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114Disguise), nameof(Scp3114Disguise.CurRagdoll))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp3114DisguisingEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp3114Handlers), nameof(Scp3114Handlers.OnDisguising))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114DisguisingEvent), nameof(Scp3114DisguisingEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114Disguise), nameof(Scp3114Disguise.Owner))),
            new(OpCodes.Ldloc_0),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp3114DisguisedEvent))[0]),
            new(OpCodes.Call, Method(typeof(Scp3114Handlers), nameof(Scp3114Handlers.OnDisguised))),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}

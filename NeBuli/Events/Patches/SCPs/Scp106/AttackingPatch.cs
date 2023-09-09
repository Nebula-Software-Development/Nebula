﻿using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp106;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp106;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp106;

[HarmonyPatch(typeof(Scp106Attack), nameof(Scp106Attack.ServerShoot))]
internal class AttackingPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnAddTarget(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ceq) + 7;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
           new(OpCodes.Ldarg_0),
           new(OpCodes.Callvirt, PropertyGetter(typeof(Scp106Attack), nameof(Scp106Attack.Owner))),
           new(OpCodes.Ldarg_0),
           new(OpCodes.Ldfld, Field(typeof(Scp106Attack), nameof(Scp106Attack._targetHub))),
           new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp106AttackingEvent))[0]),
           new(OpCodes.Dup),
           new(OpCodes.Call, Method(typeof(Scp106Handlers), nameof(Scp106Handlers.OnAttacking))),
           new(OpCodes.Callvirt, PropertyGetter(typeof(Scp106AttackingEvent), nameof(Scp106AttackingEvent.IsCancelled))),
           new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}

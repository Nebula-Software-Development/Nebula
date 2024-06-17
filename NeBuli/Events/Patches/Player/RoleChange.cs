// -----------------------------------------------------------------------
// <copyright file=RoleChange.cs company="Nebula-Software-Development">
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
using PlayerRoles;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(PlayerRoleManager), nameof(PlayerRoleManager.InitializeNewRole))]
    internal class RoleChange
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnChangingRole(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<RoleChange>(111, instructions);

            Label retLabel = generator.DefineLabel();
            LocalBuilder @event = generator.DeclareLocal(typeof(PlayerRoleChangeEvent));

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerRoleManager), nameof(PlayerRoleManager.Hub))),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Ldarg_2),
                new(OpCodes.Ldarg_3),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerRoleChangeEvent))[0]),
                new(OpCodes.Stloc_S, @event.LocalIndex),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnRoleChange))),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerRoleChangeEvent), nameof(PlayerRoleChangeEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerRoleChangeEvent), nameof(PlayerRoleChangeEvent.NewRole))),
                new(OpCodes.Starg_S, 1),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerRoleChangeEvent), nameof(PlayerRoleChangeEvent.Reason))),
                new(OpCodes.Starg_S, 2),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerRoleChangeEvent), nameof(PlayerRoleChangeEvent.SpawnFlags))),
                new(OpCodes.Starg_S, 3)
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
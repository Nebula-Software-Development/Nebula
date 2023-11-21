// -----------------------------------------------------------------------
// <copyright file=ChangingCamerasPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp079;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp079
{
    [HarmonyPatch(typeof(Scp079CurrentCameraSync), nameof(Scp079CurrentCameraSync.ServerProcessCmd))]
    internal class ChangingCamerasPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnChangingCamera(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<ChangingCamerasPatch>(117, instructions);

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Conv_R4) + 2;

            LocalBuilder changingCamera = generator.DeclareLocal(typeof(Scp079ChangingCameraEvent));

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079CurrentCameraSync), nameof(Scp079CurrentCameraSync.Owner))),
                new(OpCodes.Ldloc_0),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld,
                    Field(typeof(Scp079CurrentCameraSync), nameof(Scp079CurrentCameraSync._switchTarget))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp079ChangingCameraEvent))[0]),
                new(OpCodes.Stloc_S, changingCamera.LocalIndex),
                new(OpCodes.Ldloc_S, changingCamera.LocalIndex),
                new(OpCodes.Call, Method(typeof(Scp079Handlers), nameof(Scp079Handlers.OnScp079ChangingCamera))),
                new(OpCodes.Ldloc_S, changingCamera.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079ChangingCameraEvent), nameof(Scp079ChangingCameraEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, changingCamera.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp079ChangingCameraEvent), nameof(Scp079ChangingCameraEvent.AuxDrain))),
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
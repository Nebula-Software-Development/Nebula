using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(PlayerRoleManager), nameof(PlayerRoleManager.InitializeNewRole))]
internal class RoleChange
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnChangingRole(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<RoleChange>(111, instructions);
        
        Label retLabel = generator.DefineLabel();
        LocalBuilder @event = generator.DeclareLocal(typeof(PlayerRoleChange));
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerRoleManager), nameof(PlayerRoleManager.Hub))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldarg_2),
            new(OpCodes.Ldarg_3),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerRoleChange))[0]),
            new(OpCodes.Stloc_S, @event.LocalIndex),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnRoleChange))),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerRoleChange), nameof(PlayerRoleChange.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerRoleChange), nameof(PlayerRoleChange.NewRole))),
            new(OpCodes.Starg_S, 1),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerRoleChange), nameof(PlayerRoleChange.Reason))),
            new(OpCodes.Starg_S, 2),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerRoleChange), nameof(PlayerRoleChange.SpawnFlags))),
            new(OpCodes.Starg_S, 3),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
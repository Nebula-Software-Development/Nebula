using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp173;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs;

[HarmonyPatch(typeof(Scp173BlinkTimer), nameof(Scp173BlinkTimer.ServerBlink))]
public class PeanutBlink
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnBlink(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PeanutBlink>(21, instructions);
        
        LocalBuilder @event = generator.DeclareLocal(typeof(Scp173Blink));
        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldfld, Field(typeof(Scp173BlinkTimer), nameof(Scp173BlinkTimer._fpcModule))),
            new(OpCodes.Ldfld, Field(typeof(Scp173MovementModule), nameof(Scp173MovementModule._role))),
            new(OpCodes.Ldfld, Field(typeof(Scp173Role), nameof(Scp173Role._owner))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp173Blink))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp173Handlers), nameof(Scp173Handlers.OnBlink))),
            new(OpCodes.Stloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173Blink), nameof(Scp173Blink.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173Blink), nameof(Scp173Blink.Position))),
            new(OpCodes.Starg_S, 1)
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
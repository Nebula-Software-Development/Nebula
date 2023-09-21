using HarmonyLib;
using Hazards;

namespace Nebuli.Events.Patches.Game;

[HarmonyPatch(typeof(EnvironmentalHazard), nameof(EnvironmentalHazard.Start))]
internal class HazardPatchStart
{
    [HarmonyPostfix]
    private static void AddNew(EnvironmentalHazard __instance) => _ = API.Features.Hazards.EnviormentHazard.Get(__instance);
}

[HarmonyPatch(typeof(EnvironmentalHazard), nameof(EnvironmentalHazard.OnDestroy))]
internal class HazardPatchDestroy
{
    [HarmonyPostfix]
    private static void Destroy(EnvironmentalHazard __instance) => _ = API.Features.Hazards.EnviormentHazard.Dictionary.Remove(__instance);
}
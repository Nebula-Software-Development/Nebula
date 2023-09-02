using HarmonyLib;

namespace Nebuli.Events.Patches.Round;

[HarmonyPatch(typeof(RoundSummary), nameof(RoundSummary.Start))]
internal class RoundEndPatch
{

}

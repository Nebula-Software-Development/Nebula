using HarmonyLib;

namespace Nebuli.Events.Patches.Round;

[HarmonyPatch(typeof(RoundSummary), nameof(RoundSummary._ProcessServerSideCode))]
internal class RoundEndPatch
{

}

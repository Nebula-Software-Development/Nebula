using HarmonyLib;


namespace Nebuli.Events.Patches.Player
{
    [HarmonyPatch(typeof(BanHandler), nameof(BanHandler.IssueBan))]
    internal class BannedPlayer
    {
    }
}

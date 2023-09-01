using HarmonyLib;
using Nebuli.Loader;

namespace Nebuli.Events.Patches.Game;

[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]
internal class NamePatch
{
    [HarmonyPostfix]
    private static void PostFix()
    {
        if (Loader.Loader.Configuration.ServerNameTracking) ServerConsole._serverName += $"<color=#00000000><size=1>Nebuli{NebuliInfo.NebuliVersionConst}</color></size>";
    }
}

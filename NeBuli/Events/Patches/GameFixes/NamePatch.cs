using HarmonyLib;
using Nebuli.API.Features;
using Nebuli.Loader;

namespace Nebuli.Events.Patches.GameFixes;

[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]
internal class NamePatch
{
    private static void Postfix()
    {
        if (Loader.Loader.Configuration.ServerNameTracking) ServerConsole._serverName += $"<color=#00000000>Nebuli{NebuliInfo.NebuliVersionConst}</color>";
        Log.Info("Server name =" + ServerConsole._serverName);
    }
}

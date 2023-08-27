using HarmonyLib;
using Nebuli.API.Features;
using Nebuli.Loader;

namespace Nebuli.Events.Patches.GameFixes;

[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]
internal class NamePatch
{
    internal void PostFix()
    {
        if (Loader.Loader.Configuration.ServerNameTracking) ServerConsole._serverName += $"<color=#00000000><size=1>Nebuli{NebuliInfo.NebuliVersionConst}</color></size>";
        Log.Info("Server name =" + ServerConsole._serverName);
    }
}

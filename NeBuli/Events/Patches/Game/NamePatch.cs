﻿using HarmonyLib;
using Nebuli.Loader;

namespace Nebuli.Events.Patches.Game;

[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]
internal class NamePatch
{
    [HarmonyPostfix]
    private static void PostFix()
    {
        if (Loader.Loader.Configuration.ServerNameTracking) ServerConsole._serverName += $"<size=1>Nebuli{NebuliInfo.NebuliVersionConst}</size>";
    }
}

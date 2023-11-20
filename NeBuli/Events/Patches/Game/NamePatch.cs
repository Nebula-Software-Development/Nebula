// -----------------------------------------------------------------------
// <copyright file=NamePatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using HarmonyLib;
using Nebuli.Loader;

namespace Nebuli.Events.Patches.Game;

[HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.ReloadServerName))]
internal class NamePatch
{
    [HarmonyPostfix]
    private static void PostFix()
    {
        if (LoaderClass.Configuration.ServerNameTracking) ServerConsole._serverName += $"<color=#00000000><size=1>Nebuli{NebuliInfo.NebuliVersionConst}</size></color>";
    }
}
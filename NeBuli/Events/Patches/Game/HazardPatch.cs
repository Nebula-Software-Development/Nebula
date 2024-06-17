// -----------------------------------------------------------------------
// <copyright file=HazardPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using HarmonyLib;
using Hazards;
using Nebula.API.Features.Hazards;

namespace Nebula.Events.Patches.Game
{
    [HarmonyPatch(typeof(EnvironmentalHazard), nameof(EnvironmentalHazard.Start))]
    internal class HazardPatchStart
    {
        [HarmonyPostfix]
        private static void AddNew(EnvironmentalHazard __instance)
        {
            EnviormentHazard.Get(__instance);
        }
    }

    [HarmonyPatch(typeof(EnvironmentalHazard), nameof(EnvironmentalHazard.OnDestroy))]
    internal class HazardPatchDestroy
    {
        [HarmonyPostfix]
        private static void Destroy(EnvironmentalHazard __instance)
        {
            EnviormentHazard.Dictionary.Remove(__instance);
        }
    }
}
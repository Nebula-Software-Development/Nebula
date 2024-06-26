﻿// -----------------------------------------------------------------------
// <copyright file=CustomFriendlyFireManager.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using HarmonyLib;
using PlayerRoles;
using PlayerStatsSystem;

namespace Nebula.Events.Patches.Game
{
    internal class CustomFriendlyFireManager
    {
        public static bool GetHarmPermission(ReferenceHub attacker, ReferenceHub victim, bool ignoreFFConfig = false)
        {
            if (attacker == null || victim == null)
            {
                return true;
            }

            if (attacker == victim)
            {
                return true;
            }

            if (API.Features.Player.Get(attacker).IgnoreFFRules || API.Features.Player.Get(victim).IgnoreFFRules)
            {
                return true;
            }

            Team attackerTeam = attacker.GetTeam();
            Team victimTeam = victim.GetTeam();

            return attackerTeam != Team.Dead && victimTeam
                   != Team.Dead && (attackerTeam
                       != Team.SCPs || victimTeam
                       != Team.SCPs) &&
                   ((!ignoreFFConfig
                     && (ServerConfigSynchronizer.Singleton.MainBoolsSync & 1) == 1)
                    || attackerTeam.GetFaction() != victimTeam.GetFaction());
        }

        [HarmonyPatch(typeof(AttackerDamageHandler), nameof(AttackerDamageHandler.ProcessDamage))]
        [HarmonyPrefix]
        public static bool ProcessDamagePrefix(AttackerDamageHandler __instance, ReferenceHub ply)
        {
            if (__instance.DisableSpawnProtect(__instance.Attacker.Hub, ply))
            {
                __instance.Damage = 0f;
                return false;
            }

            if (ply.networkIdentity.netId == __instance.Attacker.NetId || __instance.ForceFullFriendlyFire)
            {
                if (!__instance.AllowSelfDamage && !__instance.ForceFullFriendlyFire)
                {
                    __instance.Damage = 0f;
                    return false;
                }

                __instance.IsSuicide = true;
            }
            else if (!GetHarmPermission(__instance.Attacker.Hub, ply, true))
            {
                __instance.Damage *= AttackerDamageHandler._ffMultiplier;
                __instance.IsFriendlyFire = true;
            }

            return true;
        }
    }
}
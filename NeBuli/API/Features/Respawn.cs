// -----------------------------------------------------------------------
// <copyright file=Respawn.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Respawning;

namespace Nebula.API.Features
{
    /// <summary>
    ///     Class for handling in-game respawns easier.
    /// </summary>
    public static class Respawn
    {
        /// <summary>
        ///     Gets or sets the current <see cref="RespawnManager.RespawnSequencePhase" />.
        /// </summary>
        public static RespawnManager.RespawnSequencePhase RespawnSequencePhase
        {
            get => RespawnManager.CurrentSequence();
            set => RespawnManager.Singleton._curSequence = value;
        }

        /// <summary>
        ///     Gets if a wave is currently respawning.
        /// </summary>
        public static bool IsSpawning
        {
            get
            {
                RespawnManager.RespawnSequencePhase currentPhase = RespawnManager.Singleton._curSequence;
                return currentPhase == RespawnManager.RespawnSequencePhase.PlayingEntryAnimations ||
                       currentPhase == RespawnManager.RespawnSequencePhase.SpawningSelectedTeam;
            }
        }

        /// <summary>
        ///     Gets or sets the next known team.
        /// </summary>
        public static SpawnableTeamType NextKnownTeam
        {
            get => RespawnManager.Singleton.NextKnownTeam;
            set => RespawnManager.Singleton.NextKnownTeam = value;
        }

        /// <summary>
        ///     Gets the current leading team.
        /// </summary>
        public static SpawnableTeamType LeadingTeam => RespawnTokensManager.DominatingTeam;

        /// <summary>
        ///     Gets or sets the time, in seconds, until the next respawn.
        /// </summary>
        public static int NextRespawnTime
        {
            get => RespawnManager.Singleton.TimeTillRespawn;
            set => RespawnManager.Singleton.TimeTillRespawn = value;
        }

        /// <summary>
        ///     Gets or sets Nine-Tailed Fox tokens.
        /// </summary>
        public static float NineTailedFoxTokens
        {
            get => RespawnTokensManager.Counters[1].Amount;
            set => RespawnTokensManager.Counters[1].Amount = value;
        }

        /// <summary>
        ///     Gets or sets Chaos Insurgencys tokens.
        /// </summary>
        public static float ChaosInsurgencyTokens
        {
            get => RespawnTokensManager.Counters[0].Amount;
            set => RespawnTokensManager.Counters[0].Amount = value;
        }

        /// <summary>
        ///     Grants tokens to the specified team.
        /// </summary>
        /// <param name="team">The team to give the tokens to.</param>
        /// <param name="tokenAmount">The amount of tokens to give.</param>
        public static void GiveTickets(SpawnableTeamType team, float tokenAmount)
        {
            RespawnTokensManager.GrantTokens(team, tokenAmount);
        }

        /// <summary>
        ///     Resets all tickets.
        /// </summary>
        public static void ResetTickets()
        {
            RespawnTokensManager.ResetTokens();
        }

        /// <summary>
        ///     Forces a leading team.
        /// </summary>
        /// <param name="team"></param>
        /// <param name="val"></param>
        public static void ForceLeadingTeam(SpawnableTeamType team, float val)
        {
            RespawnTokensManager.ForceTeamDominance(team, val);
        }

        /// <summary>
        ///     Forces a respawn.
        /// </summary>
        /// <param name="respawnEffects">If effects should play with the respawn.</param>
        /// <param name="team">The team to force respawn.</param>
        public static void ForceRespawn(bool respawnEffects, SpawnableTeamType team)
        {
            if (respawnEffects)
            {
                RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, team);
            }
            else
            {
                RespawnManager.Singleton.ForceSpawnTeam(team);
            }
        }

        /// <summary>
        ///     Gets a team's dominance tokens.
        /// </summary>
        /// <param name="team"></param>
        public static float GetTeamDominance(SpawnableTeamType team)
        {
            return RespawnTokensManager.GetTeamDominance(team);
        }
    }
}
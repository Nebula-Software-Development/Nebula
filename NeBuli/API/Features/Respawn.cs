using Respawning;

namespace Nebuli.API.Features;

/// <summary>
/// Class for handling in-game respawns easier.
/// </summary>
public static class Respawn
{
    /// <summary>
    /// Gets the current <see cref="RespawnManager.RespawnSequencePhase"/>.
    /// </summary>
    /// <returns></returns>
    public static RespawnManager.RespawnSequencePhase CurrentRespawnSequence() => RespawnManager.CurrentSequence();

    /// <summary>
    /// Gets the next known team.
    /// </summary>
    public static SpawnableTeamType NextKnownTeam => RespawnManager.Singleton.NextKnownTeam;

    /// <summary>
    /// Gets the current leading team.
    /// </summary>
    public static SpawnableTeamType LeadingTeam => RespawnTokensManager.DominatingTeam;

    /// <summary>
    /// Gets the time, in seconds, until the next respawn.
    /// </summary>
    public static int NextRespawnTime => RespawnManager.Singleton.TimeTillRespawn;

    /// <summary>
    /// Grants tokens to the specified team.
    /// </summary>
    /// <param name="team">The team to give the tokens to.</param>
    /// <param name="tokenAmount">The amount of tokens to give.</param>
    public static void GiveTickets(SpawnableTeamType team, float tokenAmount) => RespawnTokensManager.GrantTokens(team, tokenAmount);

    /// <summary>
    /// Resets all tickets.
    /// </summary>
    public static void ResetTickets() => RespawnTokensManager.ResetTokens();

    /// <summary>
    /// Forces a leading team.
    /// </summary>
    /// <param name="team"></param>
    /// <param name="val"></param>
    public static void ForceLeadingTeam(SpawnableTeamType team, float val) => RespawnTokensManager.ForceTeamDominance(team, val);

    /// <summary>
    /// Forces a respawn.
    /// </summary>
    /// <param name="respawnEffects">If effects should play with the respawn.</param>
    /// <param name="team">The team to force respawn.</param>
    public static void ForceRespawn(bool respawnEffects, SpawnableTeamType team)
    {
        if(respawnEffects)
        RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, team);
        else
        RespawnManager.Singleton.ForceSpawnTeam(team);
    }

    /// <summary>
    /// Gets or sets Nine-Tailed Fox tokens.
    /// </summary>
    public static float NineTailedFoxTokens
    {
        get => RespawnTokensManager.Counters[1].Amount;
        set => GiveTickets(SpawnableTeamType.NineTailedFox, value);
    }

    /// <summary>
    /// Gets or sets Chaos Insurgencys tokens.
    /// </summary>
    public static float ChaosInsurgencyTokens
    {
        get => RespawnTokensManager.Counters[0].Amount;
        set => GiveTickets(SpawnableTeamType.ChaosInsurgency, value);
    }
}

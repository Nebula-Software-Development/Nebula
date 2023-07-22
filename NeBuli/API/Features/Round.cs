using PlayerRoles;

namespace Nebuli.API.Features;

/// <summary>
/// Allows easier control and use of round related features.
/// </summary>
public static class Round
{
    /// <summary>
    /// Gets if the round has started or not.
    /// </summary>
    public static bool RoundStarted => Server.NebuliHost.ReferenceHub.characterClassManager.RoundStarted;

    /// <summary>
    /// Forces the round to start if not already.
    /// </summary>
    public static void ForceStartRound() => CharacterClassManager.ForceRoundStart();

    /// <summary>
    /// Gets if the round is active.
    /// </summary>
    public static bool RoundActive => RoundSummary.singleton._summaryActive;

    /// <summary>
    /// Gets if the round has ended.
    /// </summary>
    public static bool RoundEnded => RoundSummary.singleton._roundEnded;

    /// <summary>
    /// Forces the round to end.
    /// </summary>
    public static void ForceEndRound() => RoundSummary.singleton.ForceEnd();

    /// <summary>
    /// Counts the number of players with the given <see cref="RoleTypeId"/>.
    /// </summary>
    /// <param name="role">The <see cref="RoleTypeId"/> to count.</param>
    /// <returns>The number of players with the given <see cref="RoleTypeId"/>.</returns>
    public static int CountRole(RoleTypeId role) => RoundSummary.singleton.CountRole(role);

    /// <summary>
    /// Counts the number of teams with the given <see cref="Team"/>.
    /// </summary>
    /// <param name="team">The <see cref="Team"/> to count.</param>
    /// <returns>The number of teams with the given <see cref="Team"/>.</returns>
    public static int CountTeam(Team team) => RoundSummary.singleton.CountTeam(team);

    /// <summary>
    /// Adds the specified <see cref="Team"/> to the list of spawned teams.
    /// </summary>
    /// <param name="team">The <see cref="Team"/> to add.</param>
    public static void AddSpawnedTeam(Team team) => RoundSummary.singleton.AddSpawnedTeam(team);

    /// <summary>
    /// Gets the total number of D-Class personnel that have escaped during the round.
    /// </summary>
    public static int TotalEscapedDClass => RoundSummary.EscapedClassD;

    /// <summary>
    /// Gets the total number of scientists that have escaped during the round.
    /// </summary>
    public static int TotalEscapedScientist => RoundSummary.EscapedScientists;

    /// <summary>
    /// Gets the total number of players that have turned into SCP-049-2 (Zombie) during the round.
    /// </summary>
    public static int TotalPlayerTurnedToSCP0492 => RoundSummary.ChangedIntoZombies;

    /// <summary>
    /// Gets the total number of kills during the round.
    /// </summary>
    public static int TotalKills => RoundSummary.Kills;

    /// <summary>
    /// Gets the total number of kills by SCPs during the round.
    /// </summary>
    public static int SCPKills => RoundSummary.KilledBySCPs;

    /// <summary>
    /// Gets or sets a value indicating whether the round is locked or not.
    /// </summary>
    public static bool RoundLock
    {
        get => RoundSummary.RoundLock;
        set => RoundSummary.RoundLock = value;
    }

    /// <summary>
    /// Gets the total round time in seconds.
    /// </summary>
    public static int TotalRoundTime => RoundSummary.roundTime;

    /// <summary>
    /// Gets the singleton instance of the <see cref="RoundSummary"/> class.
    /// </summary>
    public static RoundSummary Singleton => RoundSummary.singleton;

    /// <summary>
    /// Gets the number of surviving SCPs during the round.
    /// </summary>
    public static int NumberOfSurvivingSCPs => RoundSummary.SurvivingSCPs;
}
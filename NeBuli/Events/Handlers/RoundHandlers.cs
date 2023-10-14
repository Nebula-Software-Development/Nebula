using Nebuli.Events.EventArguments.Round;

namespace Nebuli.Events.Handlers;

public static class RoundHandlers
{
    /// <summary>
    /// Triggered when a team is respawning.
    /// </summary>
    public static event EventManager.CustomEventHandler<RespawningTeamEvent> RespawningTeam;

    /// <summary>
    /// Triggered at the start of a round.
    /// </summary>
    public static event EventManager.CustomEventHandler RoundStart;

    /// <summary>
    /// Triggered at the end of a round.
    /// </summary>
    public static event EventManager.CustomEventHandler<RoundEndEvent> RoundEndEvent;

    internal static void OnRespawning(RespawningTeamEvent ev) => RespawningTeam.CallEvent(ev);

    internal static void OnRoundStart() => RoundStart.CallEmptyEvent();

    internal static void OnRoundEnd(RoundEndEvent ev) => RoundEndEvent.CallEvent(ev);
}
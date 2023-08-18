using Nebuli.Events.EventArguments.Round;

namespace Nebuli.Events.Handlers;

public static class RoundHandlers
{
    public static event EventManager.CustomEventHandler<RespawningTeamEvent> RespawningTeam;

    public static event EventManager.CustomEventHandler RoundStart;

    internal static void OnRespawning(RespawningTeamEvent ev) => RespawningTeam.CallEvent(ev);

    internal static void OnRoundStart() => RoundStart.CallEmptyEvent();
}

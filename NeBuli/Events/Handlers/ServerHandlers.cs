using Nebuli.Events.EventArguments.Server;

namespace Nebuli.Events.Handlers;

public static class ServerHandlers
{

    public static event EventManager.CustomEventHandler<WarheadDetonatingEvent> WarheadDetonated;

    public static event EventManager.CustomEventHandler MapGenerated;

    public static event EventManager.CustomEventHandler WaitingForPlayers;

    public static event EventManager.CustomEventHandler<SpawningItemEvent> SpawningItem;

    internal static void OnWarheadDetonated(WarheadDetonatingEvent ev) => WarheadDetonated.CallEvent(ev);

    internal static void OnMapGenerated() => MapGenerated.CallEmptyEvent();

    internal static void OnWaitingForPlayers() => WaitingForPlayers.CallEmptyEvent();

    internal static void OnSpawningItem(SpawningItemEvent ev) => SpawningItem.CallEvent(ev);
}
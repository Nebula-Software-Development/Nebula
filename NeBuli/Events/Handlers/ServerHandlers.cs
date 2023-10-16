using Nebuli.Events.EventArguments.Server;

namespace Nebuli.Events.Handlers;

public static class ServerHandlers
{
    /// <summary>
    /// Triggered before the warhead detonates.
    /// </summary>
    public static event EventManager.CustomEventHandler<WarheadDetonatingEvent> WarheadDetonated;

    /// <summary>
    /// Triggered after the map is generated.
    /// </summary>
    public static event EventManager.CustomEventHandler MapGenerated;

    /// <summary>
    /// Triggered when the server starts waiting for players to join.
    /// </summary>
    public static event EventManager.CustomEventHandler WaitingForPlayers;

    /// <summary>
    /// Triggered before an item spawns.
    /// </summary>
    public static event EventManager.CustomEventHandler<SpawningItemEvent> SpawningItem;

    /// <summary>
    /// Triggered before blood spawns.
    /// </summary>
    public static event EventManager.CustomEventHandler<SpawningBloodEvent> SpawningBlood;

    /// <summary>
    /// Triggered before the warhead starts its detonation sequence.
    /// </summary>
    public static event EventManager.CustomEventHandler<WarheadStartingEvent> WarheadStarting;

    internal static void OnWarheadDetonated(WarheadDetonatingEvent ev) => WarheadDetonated.CallEvent(ev);

    internal static void OnMapGenerated() => MapGenerated.CallEmptyEvent();

    internal static void OnWaitingForPlayers() => WaitingForPlayers.CallEmptyEvent();

    internal static void OnSpawningItem(SpawningItemEvent ev) => SpawningItem.CallEvent(ev);

    internal static void OnSpawningBlood(SpawningBloodEvent ev) => SpawningBlood.CallEvent(ev);

    internal static void OnWarheadStarting(WarheadStartingEvent ev) => WarheadStarting.CallEvent(ev);
}
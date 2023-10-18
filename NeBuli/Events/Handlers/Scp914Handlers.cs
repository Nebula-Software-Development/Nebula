using Nebuli.Events.EventArguments.SCPs.Scp914;

namespace Nebuli.Events.Handlers;

public static class Scp914Handlers
{
    /// <summary>
    /// Triggered when a player is being upgrading in SCP-914.
    /// </summary>
    public static event EventManager.CustomEventHandler<UpgradingPlayerEvent> UpgradingPlayer;

    /// <summary>
    /// Triggered when an item is being upgraded in SCP-914.
    /// </summary>
    public static event EventManager.CustomEventHandler<UpgradingItemEvent> UpgradingItem;

    internal static void OnUpgradingPlayer(UpgradingPlayerEvent ev) => UpgradingPlayer.CallEvent(ev);

    internal static void OnUpgradingItem(UpgradingItemEvent ev) => UpgradingItem.CallEvent(ev);
}
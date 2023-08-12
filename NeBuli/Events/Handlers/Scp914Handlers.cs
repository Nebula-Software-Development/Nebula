using Nebuli.Events.EventArguments.SCPs.Scp914;

namespace Nebuli.Events.Handlers;

public static class Scp914Handlers
{
    public static event EventManager.CustomEventHandler<UpgradingPlayerEvent> UpgradingPlayer;

    public static event EventManager.CustomEventHandler<UpgradingItemEvent> UpgradingItem;

    internal static void OnUpgradingPlayer(UpgradingPlayerEvent ev) => UpgradingPlayer.CallEvent(ev);

    internal static void OnUpgradingItem(UpgradingItemEvent ev) => UpgradingItem.CallEvent(ev);
}

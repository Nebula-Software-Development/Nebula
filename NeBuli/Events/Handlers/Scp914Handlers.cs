using Nebuli.Events.EventArguments.SCPs.Scp914;

namespace Nebuli.Events.Handlers;

public static class Scp914Handlers
{
    public static event EventManager.CustomEventHandler<UpgradingPlayer> UpgradingPlayer;

    public static event EventManager.CustomEventHandler<UpgradingItem> UpgradingItem;

    internal static void OnUpgradingPlayer(UpgradingPlayer ev) => UpgradingPlayer.CallEvent(ev);

    internal static void OnUpgradingItem(UpgradingItem ev) => UpgradingItem.CallEvent(ev);
}

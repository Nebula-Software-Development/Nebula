using Nebuli.Events.EventArguments.SCPs.Scp173;

namespace Nebuli.Events.Handlers;

public static class Scp173Handlers
{
    public static event EventManager.CustomEventHandler<Scp173Blink> Blink;

    internal static void OnBlink(Scp173Blink ev) => Blink.CallEvent(ev);
}
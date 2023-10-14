using Nebuli.Events.EventArguments.SCPs.Scp330;

namespace Nebuli.Events.Handlers;

public static class Scp330Handlers
{
    /// <summary>
    /// Triggered when a player is interacting with SCP-330.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp330InteractEvent> Interacting;


    internal static void OnInteracting(Scp330InteractEvent ev) => Interacting.CallEvent(ev);
}
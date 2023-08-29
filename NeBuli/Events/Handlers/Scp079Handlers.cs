using Nebuli.Events.EventArguments.SCPs.Scp079;

namespace Nebuli.Events.Handlers;

public static class Scp079Handlers
{
    public static event EventManager.CustomEventHandler<Scp079PingingEvent> Pinging;

    public static event EventManager.CustomEventHandler<Scp079LosingSignalEvent> LosingSignal;

    public static event EventManager.CustomEventHandler<Scp079GainingExperienceEvent> GainingExperience;

    public static event EventManager.CustomEventHandler<Scp079GainingLevelEvent > GainingLevel;

    public static event EventManager.CustomEventHandler<Scp079ChangingCameraEvent> ChangingCamera;

    public static event EventManager.CustomEventHandler<Scp079InteractingTeslaEvent> InteractingTesla;

    internal static void OnScp079Pinging(Scp079PingingEvent ev) => Pinging.CallEvent(ev);

    internal static void OnScp079LosingSignal(Scp079LosingSignalEvent ev) => LosingSignal.CallEvent(ev);

    internal static void OnScp079GainingExpereince(Scp079GainingExperienceEvent ev) => GainingExperience.CallEvent(ev);

    internal static void OnScp079GainingLevel(Scp079GainingLevelEvent ev) => GainingLevel.CallEvent(ev);

    internal static void OnScp079ChangingCamera(Scp079ChangingCameraEvent ev) => ChangingCamera.CallEvent(ev);

    internal static void OnScp079InteractingTesla(Scp079InteractingTeslaEvent ev) => InteractingTesla.CallEvent(ev);
}

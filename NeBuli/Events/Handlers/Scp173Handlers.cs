using Nebuli.Events.EventArguments.SCPs.Scp173;

namespace Nebuli.Events.Handlers;

public static class Scp173Handlers
{
    public static event EventManager.CustomEventHandler<Scp173Blink> Blink;
    
    public static event EventManager.CustomEventHandler<Scp173PlaceTantrum> PlaceTantrum;
    
    public static event EventManager.CustomEventHandler<Scp173ToggleBreakneckSpeed> ToggleBreakneckSpeed;

    public static event EventManager.CustomEventHandler<Scp173Attack> Attack; 

    internal static void OnBlink(Scp173Blink ev) => Blink.CallEvent(ev);
    
    internal static void OnPlaceTantrum(Scp173PlaceTantrum ev) => PlaceTantrum.CallEvent(ev);
    
    internal static void OnToggleBreakneckSpeed(Scp173ToggleBreakneckSpeed ev) => ToggleBreakneckSpeed.CallEvent(ev);

    internal static void OnAttack(Scp173Attack ev) => Attack.CallEvent(ev);
}
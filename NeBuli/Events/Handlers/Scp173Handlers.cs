using Nebuli.Events.EventArguments.SCPs.Scp173;

namespace Nebuli.Events.Handlers;

public static class Scp173Handlers
{
    public static event EventManager.CustomEventHandler<Scp173BlinkEvent> Blink;
    
    public static event EventManager.CustomEventHandler<Scp173PlaceTantrumEvent> PlaceTantrum;
    
    public static event EventManager.CustomEventHandler<Scp173ToggleBreakneckSpeedEvent> ToggleBreakneckSpeed;

    public static event EventManager.CustomEventHandler<Scp173AttackEvent> Attack; 

    internal static void OnBlink(Scp173BlinkEvent ev) => Blink.CallEvent(ev);
    
    internal static void OnPlaceTantrum(Scp173PlaceTantrumEvent ev) => PlaceTantrum.CallEvent(ev);
    
    internal static void OnToggleBreakneckSpeed(Scp173ToggleBreakneckSpeedEvent ev) => ToggleBreakneckSpeed.CallEvent(ev);

    internal static void OnAttack(Scp173AttackEvent ev) => Attack.CallEvent(ev);
}
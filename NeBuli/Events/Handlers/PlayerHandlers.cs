using Nebuli.Events.EventArguments.Player;
using PlayerRoles.Ragdolls;

namespace Nebuli.Events.Handlers;

public static class PlayerHandlers
{
    public static event EventManager.CustomEventHandler<PlayerJoinEventArgs> Join;

    public static event EventManager.CustomEventHandler<PlayerLeaveEventArgs> Leave;

    public static event EventManager.CustomEventHandler<PlayerHurtEventArgs> Hurt; 

    internal static void OnJoin(PlayerJoinEventArgs ev) => Join.CallEvent(ev);
    
    internal static void OnLeave(PlayerLeaveEventArgs ev) => Leave.CallEvent(ev);
    
    internal static void OnHurt(PlayerHurtEventArgs ev) => Hurt.CallEvent(ev);

}
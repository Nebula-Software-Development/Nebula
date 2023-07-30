using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939Attack : EventArgs, IDamageEvent
{
    public Scp939Attack(ReferenceHub player, uint netId)
    {
        Attacker = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(netId); // Note: The Player can be null if the attacker attacks a window or something else that is not a player.
    }
    
    public NebuliPlayer Attacker { get; }

    public NebuliPlayer Target { get; }
}

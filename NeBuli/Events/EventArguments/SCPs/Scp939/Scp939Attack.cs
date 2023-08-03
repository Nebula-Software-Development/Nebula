using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939Attack : EventArgs, IDamageEvent, ICancellableEvent
{
    public Scp939Attack(ReferenceHub player, uint netId)
    {
        Attacker = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(netId);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets the attacking player, SCP-939
    /// </summary>
    public NebuliPlayer Attacker { get; }

    /// <summary>
    /// Gets the player being attacked. 
    /// NOTE: The Player can be null if the attacker attacks a window or something else that is not a player.
    /// </summary>
    public NebuliPlayer Target { get; }

    public bool IsCancelled { get; set; }
}

using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.Interfaces;

public interface IDamageEvent
{
    /// <summary>
    /// The attacker of the damage handler.
    /// </summary>
    public NebuliPlayer Attacker { get; }

    /// <summary>
    /// The Target of the damage handler.
    /// </summary>
    public NebuliPlayer Target { get; }
}
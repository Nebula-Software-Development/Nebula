using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments;

public interface IDamageEvent
{
    public NebuliPlayer Attacker { get; }

    public NebuliPlayer Target { get; }
}
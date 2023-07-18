using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments;

public interface IPlayerEvent
{
    public NebuliPlayer Player { get; }
}
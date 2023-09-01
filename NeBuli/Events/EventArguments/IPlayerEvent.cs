using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments;

public interface IPlayerEvent
{
    /// <summary>
    /// The player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }
}
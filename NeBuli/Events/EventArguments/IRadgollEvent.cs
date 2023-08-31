using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments;

public interface IRadgollEvent
{
    /// <summary>
    /// The ragdoll of the event.
    /// </summary>
    public Ragdoll Ragdoll { get; }
}
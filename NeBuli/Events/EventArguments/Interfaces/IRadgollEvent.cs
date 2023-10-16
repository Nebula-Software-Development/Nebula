using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.Interfaces;

public interface IRadgollEvent
{
    /// <summary>
    /// The ragdoll of the event.
    /// </summary>
    public Ragdoll Ragdoll { get; }
}
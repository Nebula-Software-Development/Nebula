using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments;

public interface IRadgollEvent
{
    public Ragdoll Ragdoll { get; }
}
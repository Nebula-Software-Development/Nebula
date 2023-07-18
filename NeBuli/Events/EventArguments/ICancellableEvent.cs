namespace Nebuli.Events.EventArguments;

public interface ICancellableEvent
{
    public bool IsCancelled { get; set; }
}
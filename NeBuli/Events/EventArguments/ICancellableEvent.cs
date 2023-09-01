namespace Nebuli.Events.EventArguments;

public interface ICancellableEvent
{
    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
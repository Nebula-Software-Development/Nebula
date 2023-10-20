namespace Nebuli.Events.EventArguments.Interfaces;

public interface ICancellableEvent
{
    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
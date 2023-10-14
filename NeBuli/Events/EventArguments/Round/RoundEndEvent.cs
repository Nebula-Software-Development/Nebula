using System;

namespace Nebuli.Events.EventArguments.Round;

/// <summary>
/// Triggered at the end of a round.
/// </summary>
public class RoundEndEvent : EventArgs
{
    public RoundEndEvent(RoundSummary.LeadingTeam leadingTeam)
    {
        LeadingTeam = leadingTeam;
    }

    /// <summary>
    /// Gets the <see cref="RoundSummary.LeadingTeam"/> of the event.
    /// </summary>
    public RoundSummary.LeadingTeam LeadingTeam { get; }
}
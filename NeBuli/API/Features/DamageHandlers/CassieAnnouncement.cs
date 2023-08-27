using System.Collections.Generic;
using System.Linq;

namespace Nebuli.API.Features.DamageHandlers;

/// <summary>
/// Represents a wrapper for modifying Cassie announcements.
/// </summary>
public class CassieAnnouncement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CassieAnnouncement"/> class.
    /// </summary>
    /// <param name="announcement">The announcement text to set.</param>
    public CassieAnnouncement(string announcement)
    {
        Announcement = announcement;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CassieAnnouncement"/> class.
    /// </summary>
    /// <param name="announcement">The announcement text to set.</param>
    /// <param name="subtitleParts">The subtitle parts to set.</param>
    public CassieAnnouncement(string announcement, IEnumerable<Subtitles.SubtitlePart> subtitleParts): this(announcement)
    {
        SubtitleParts = subtitleParts;
    }

    /// <summary>
    /// Gets the default Cassie announcement.
    /// </summary>
    public static CassieAnnouncement Default => PlayerStatsSystem.DamageHandlerBase.CassieAnnouncement.Default;

    /// <summary>
    /// Gets or sets the announcement text.
    /// </summary>
    public string Announcement { get; set; }

    /// <summary>
    /// Gets or sets the subtitle parts associated with this Cassie announcement.
    /// </summary>
    public IEnumerable<Subtitles.SubtitlePart> SubtitleParts { get; set; }

    /// <summary>
    /// Implicitly converts the given <see cref="CassieAnnouncement"/> instance to a <see cref="PlayerStatsSystem.DamageHandlerBase.CassieAnnouncement"/> instance.
    /// </summary>
    /// <param name="cassieAnnouncement">The <see cref="CassieAnnouncement"/> instance.</param>
    public static implicit operator PlayerStatsSystem.DamageHandlerBase.CassieAnnouncement(CassieAnnouncement cassieAnnouncement) =>
        new()
        {
            Announcement = cassieAnnouncement.Announcement,
            SubtitleParts = cassieAnnouncement.SubtitleParts?.ToArray(),
        };

    /// <summary>
    /// Implicitly converts the given <see cref="PlayerStatsSystem.DamageHandlerBase.CassieAnnouncement"/> instance to a <see cref="CassieAnnouncement"/> instance.
    /// </summary>
    /// <param name="cassieAnnouncement">The <see cref="PlayerStatsSystem.DamageHandlerBase.CassieAnnouncement"/> instance.</param>
    public static implicit operator CassieAnnouncement(PlayerStatsSystem.DamageHandlerBase.CassieAnnouncement cassieAnnouncement) => new(cassieAnnouncement.Announcement, cassieAnnouncement.SubtitleParts);
}
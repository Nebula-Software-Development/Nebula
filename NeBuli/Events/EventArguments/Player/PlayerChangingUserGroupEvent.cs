using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerChangingUserGroupEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerChangingUserGroupEvent(ReferenceHub ply, UserGroup group)
    {
        Player = NebuliPlayer.Get(ply);
        Group = group;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="UserGroup"/> thats being changed to.
    /// </summary>
    public UserGroup Group { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
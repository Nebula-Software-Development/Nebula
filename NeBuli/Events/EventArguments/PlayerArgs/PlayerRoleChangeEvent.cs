using Nebuli.API.Features.Player;
using PlayerRoles;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerRoleChangeEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerRoleChangeEvent(ReferenceHub ply, RoleTypeId newRole, RoleChangeReason roleChangeReason, RoleSpawnFlags roleSpawnFlags)
    {
        Player = API.Features.Player.Player.Get(ply);
        NewRole = newRole;
        Reason = roleChangeReason;
        SpawnFlags = roleSpawnFlags;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player that triggered the event.
    /// </summary>
    public API.Features.Player.Player Player { get; }

    /// <summary>
    /// Gets or sets the new role.
    /// </summary>
    public RoleTypeId NewRole { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="RoleChangeReason"/>.
    /// </summary>
    public RoleChangeReason Reason { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="RoleSpawnFlags"/>.
    /// </summary>
    public RoleSpawnFlags SpawnFlags { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
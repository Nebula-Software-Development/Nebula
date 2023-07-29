using Nebuli.API.Features.Player;
using PlayerRoles;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerRoleChange : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerRoleChange(ReferenceHub ply, RoleTypeId newRole, RoleChangeReason roleChangeReason, RoleSpawnFlags roleSpawnFlags)
    {
        Player = NebuliPlayer.Get(ply);
        NewRole = newRole;
        Reason = roleChangeReason;
        SpawnFlags = roleSpawnFlags;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player that triggered the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    public RoleTypeId NewRole { get; set; }

    public RoleChangeReason Reason { get; set; }

    public RoleSpawnFlags SpawnFlags { get; set; }

    public bool IsCancelled { get; set; }
}
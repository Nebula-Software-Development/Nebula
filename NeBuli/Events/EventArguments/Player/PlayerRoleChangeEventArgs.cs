using Nebuli.API.Features.Player;
using PlayerRoles;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerRoleChangeEventArgs : EventArgs, IPlayerEvent
{
    public PlayerRoleChangeEventArgs(ReferenceHub ply, RoleTypeId newRole, RoleChangeReason roleChangeReason, RoleSpawnFlags roleSpawnFlags = RoleSpawnFlags.All)
    {
        Player = NebuliPlayer.Get(ply);
        NewRole = newRole;
        Reason = roleChangeReason;
        SpawnFlags = roleSpawnFlags;
    }

    /// <summary>
    /// Gets the player that triggered the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    public RoleTypeId NewRole { get; }

    public RoleChangeReason Reason { get; }

    public RoleSpawnFlags SpawnFlags { get; }
}
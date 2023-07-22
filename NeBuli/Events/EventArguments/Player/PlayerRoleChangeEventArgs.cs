using Nebuli.API.Features.Player;
using PlayerRoles;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerRoleChangeEventArgs : EventArgs, IPlayerEvent
{
    public PlayerRoleChangeEventArgs(ReferenceHub ply, RoleTypeId newRole, RoleChangeReason roleChangeReason, RoleSpawnFlags roleSpawnFlags = RoleSpawnFlags.All)
    {
    }

    /// <summary>
    /// Gets the player that triggered the event.
    /// </summary>
    public NebuliPlayer Player { get; }
}
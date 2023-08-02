using Nebuli.API.Features.Player;
using PlayerRoles;
using System;
using static Escape;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerEscaping : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerEscaping(ReferenceHub player, RoleTypeId newRole, EscapeScenarioType escapeType)
    {
        Player = NebuliPlayer.Get(player);
        NewRole = newRole;
        OldRole = Player.CurrentRoleType;
        EscapeScenario = escapeType;
        IsCancelled = false;
    }

    /// <summary>
    /// The player that is escaping.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// If the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The new role the escaping event will use.
    /// </summary>
    public RoleTypeId NewRole { get; set; }

    /// <summary>
    /// The previous role of the player before the event.
    /// </summary>
    public RoleTypeId OldRole { get; }

    /// <summary>
    /// The <see cref="EscapeScenarioType"/> of the event.
    /// </summary>
    public EscapeScenarioType EscapeScenario { get; set; }
}

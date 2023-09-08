using Nebuli.API.Features.Enum;
using PlayerRoles;
using System;
using static Escape;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerEscapingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerEscapingEvent(ReferenceHub player, RoleTypeId newRole, EscapeScenarioType escapeType, EscapeMessage escapeMessage)
    {
        Player = API.Features.Player.Get(player);
        NewRole = newRole;
        EscapeMessage = escapeMessage;
        OldRole = player.GetRoleId();
        EscapeScenario = (EscapeType)escapeType;
        IsCancelled = false;
        if (EscapeScenario == EscapeType.PluginEscape)
            IsCancelled = true;
    }

    /// <summary>
    /// The player that is escaping.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets the <see cref="Escape.EscapeMessage"/> for the event.
    /// </summary>
    public EscapeMessage EscapeMessage { get; set; }

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
    /// The <see cref="EscapeType"/> of the event.
    /// </summary>
    public EscapeType EscapeScenario { get; }
}

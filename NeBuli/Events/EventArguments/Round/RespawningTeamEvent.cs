using Nebuli.Events.EventArguments.Interfaces;
using Respawning;
using System;
using System.Collections.Generic;

namespace Nebuli.Events.EventArguments.Round;

/// <summary>
/// Triggered when a team is respawning.
/// </summary>
public class RespawningTeamEvent : EventArgs, ICancellableEvent
{
    public RespawningTeamEvent(List<ReferenceHub> respawningPlayers, SpawnableTeamType respawningTeam, int maxWaveSize)
    {
        PlayersRespawning = respawningPlayers;
        IsCancelled = false;
        MaxWaveSize = maxWaveSize;
        RespawningTeamType = respawningTeam;
    }

    /// <summary>
    /// Gets if the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the max wave size for this respawn.
    /// </summary>
    public int MaxWaveSize { get; set; }

    /// <summary>
    /// Gets a <see cref="List{ReferenceHub}"/> of all the players respawning.
    /// </summary>
    public List<ReferenceHub> PlayersRespawning { get; set; }

    /// <summary>
    /// Gets the respawns <see cref="SpawnableTeamType"/>.
    /// </summary>
    public SpawnableTeamType RespawningTeamType { get; }
}
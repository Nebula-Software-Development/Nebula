using CommandSystem;
using CustomPlayerEffects;
using GameCore;
using Mirror;
using Nebuli.API.Features.Player;
using PlayerRoles.RoleAssign;
using RoundRestarting;
using static Broadcast;

namespace Nebuli.API.Features;

/// <summary>
/// Makes accessing server variables easier.
/// </summary>
public static class Server
{

    /// <summary>
    /// Gets the servers <see cref="NebuliPlayer"/> Host.
    /// </summary>
    public static NebuliPlayer NebuliHost { get; internal set; } = null;

    /// <summary>
    /// Gets or sets whether friendly fire is on or not.
    /// </summary>
    public static bool FriendlyFire
    {
        get => ServerConsole.FriendlyFire;          
        set
        {
            ServerConsole.FriendlyFire = value;
            ServerConfigSynchronizer.Singleton.RefreshMainBools();
            PlayerStatsSystem.AttackerDamageHandler.RefreshConfigs();
        }
        
    }
    /// <summary>
    /// Gets if LateJoin is enabled
    /// </summary>
    public static bool LateJoin => LateJoinTime > 0;

    /// <summary>
    /// Gets the late join time.
    /// </summary>
    public static float LateJoinTime => ConfigFile.ServerConfig.GetFloat(RoleAssigner.LateJoinKey, 0f);

    /// <summary>
    /// Gets or sets the max player count of the server.
    /// </summary>
    public static int MaxPlayerCount
    {
        get => CustomNetworkManager.slots;
        set => CustomNetworkManager.slots = value;
    }

    /// <summary>
    /// Gets or sets whether the server is Heavily Modded or not.
    /// </summary>
    public static bool HeavilyModded
    {
        get => CustomNetworkManager.HeavilyModded; 
        set => CustomNetworkManager.HeavilyModded = value;
    }

    /// <summary>
    /// Gets or sets whether the server is whitelisted.
    /// </summary>
    public static bool Whitelisted
    {
        get => ServerConsole.WhiteListEnabled;
        set => ServerConsole.WhiteListEnabled = value;
    }

    /// <summary>
    /// Gets or sets whether respawn protection is enabled.
    /// </summary>
    public static bool RespawnProtection
    {
        get => SpawnProtected.IsProtectionEnabled;
        set => SpawnProtected.IsProtectionEnabled = value;
    }    
    
    /// <summary>
    /// Shuts the server down completely.
    /// </summary>
    public static void ServerShutdown() => Shutdown.Quit(true);

    /// <summary>
    /// Sends a server command to the console.
    /// </summary>
    /// <param name="command">The command to send.</param>
    /// <param name="sender">The player sending the command.</param>
    public static void RunServerCommand(string command, NebuliPlayer sender = null)
    {
        sender ??= NebuliHost;
        ServerConsole.EnterCommand(command, sender.Sender);
    }
    /// <summary>
    /// Gets the <see cref="Broadcast.Broadcast"/> singleton.
    /// </summary>
    public static Broadcast Broadcast => Singleton;

    /// <summary>
    /// Gets the server's port.
    /// </summary>
    public static ushort ServerPort => ServerStatic.ServerPort;

    /// <summary>
    /// Gets the server's IP address.
    /// </summary>
    public static string ServerIP => ServerConsole.Ip;

    /// <summary>
    /// Restarts the server.
    /// </summary>
    /// <param name="roundRestartType">The <see cref="RoundRestartType"/> to use.</param>
    /// <param name="reconnectPlayers">If the server should reconnect players after the restart.</param>
    /// <param name="offset">The restart offset.</param>
    /// <param name="port">The port to reconnect to, if null, it will be the current port.</param>
    /// <param name="extendedReconnectionPeriod">If the reconnection period should take longer.</param>
    public static void RestartServer(RoundRestartType roundRestartType = RoundRestartType.FullRestart, bool reconnectPlayers = true, float offset = 0, ushort? port = null, bool extendedReconnectionPeriod = false)
    {
        ushort portValue = port ?? ServerPort;
        NetworkServer.SendToAll(new RoundRestartMessage(roundRestartType, offset, portValue, reconnectPlayers, extendedReconnectionPeriod));
    }

    /// <summary>
    /// Gets or sets the server's name.
    /// </summary>
    public static string ServerName
    {
        get => ServerConsole._serverName;
        set
        {
            ServerConsole._serverName = value;
            ServerConsole.ReloadServerName();
        }
    }

    /// <summary>
    /// Broadcasts a message to all the players on the server.
    /// </summary>
    /// <param name="message">The message to broadcast.</param>
    /// <param name="duration">The duration of the broadcast.</param>
    /// <param name="broadcastFlags">The <see cref="BroadcastFlags"/> to show.</param>
    public static void BroadcastAll(string message, ushort duration = 5, BroadcastFlags broadcastFlags = BroadcastFlags.Normal)
    {
        foreach(NebuliPlayer ply in NebuliPlayer.List)
        {
            Broadcast.TargetAddElement(ply.ReferenceHub.connectionToClient, message, duration, broadcastFlags);
        }
    }

    /// <summary>
    /// Clears all broadcasts for all players on the server.
    /// </summary>
    public static void ClearAllBroadcasts()
    {
        foreach(NebuliPlayer ply in NebuliPlayer.List)
        {
            Broadcast.TargetClearElements(ply.ReferenceHub.connectionToClient);
        }
    }

    /// <summary>
    /// Gets or sets if IdleMode is enabled.
    /// </summary>
    public static bool IdleModeEnabled
    {
        get => IdleMode.IdleModeEnabled;
        set => IdleMode.IdleModeEnabled = value;
    }

    /// <summary>
    /// Gets or sets the IdleMode server tick rate.
    /// </summary>
    public static short IdleModeTickRate
    {
        get => IdleMode.IdleModeTickrate;
        set => IdleMode.IdleModeTickrate = value;
    }

    /// <summary>
    /// Gets or sets the ServerTicketRate.
    /// </summary>
    public static short ServerTicketRate
    {
        get => ServerStatic.ServerTickrate;
        set => ServerStatic.ServerTickrate = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="ServerStatic.NextRoundAction"/>.
    /// </summary>
    public static ServerStatic.NextRoundAction NextRoundAction
    {
        get => ServerStatic.StopNextRound;
        set => ServerStatic.StopNextRound = value;
    }
}

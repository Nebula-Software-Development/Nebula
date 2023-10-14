using CommandSystem;
using CustomPlayerEffects;
using GameCore;
using Mirror;
using Nebuli.API.Features.Player;
using PlayerRoles.RoleAssign;
using RoundRestarting;
using System;
using System.Reflection;
using UnityEngine;
using static Broadcast;
using static ServerStatic;

namespace Nebuli.API.Features;

/// <summary>
/// Makes accessing server variables easier.
/// </summary>
public static class Server
{
    private static MethodInfo serverMessage;

    /// <summary>
    /// Gets the current player count of the server.
    /// </summary>
    public static int PlayerCount => NebuliPlayer.List.Count;

    /// <summary>
    /// Gets the servers <see cref="NebuliPlayer"/> Host.
    /// </summary>
    public static NebuliPlayer NebuliHost { get; internal set; } = null;

    /// <summary>
    /// Gets the servers GameConsoleCommandHandler.
    /// </summary>
    public static GameConsoleCommandHandler GameConsoleCommandHandler => GameCore.Console.singleton.ConsoleCommandHandler;

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
    public static float LateJoinTime => ConfigFile.ServerConfig.GetFloat(RoleAssigner.LateJoinKey);

    /// <summary>
    /// Gets or sets the max player count of the server.
    /// </summary>
    public static int MaxPlayerCount
    {
        get => CustomNetworkManager.slots;
        set => CustomNetworkManager.slots = value;
    }

    /// <summary>
    /// Gets or sets whether the server is Transparently Modded or not.
    /// </summary>
    public static bool TransparentlyModded
    {
        get => ServerConsole.TransparentlyModdedServerConfig;
        set => ServerConsole.TransparentlyModdedServerConfig = value;
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
    /// Gets the <see cref="global::Broadcast"/> singleton.
    /// </summary>
    public static global::Broadcast Broadcast => Singleton;

    /// <summary>
    /// Gets the Network servers "SendSpawnMessage" method.
    /// </summary>
    public static MethodInfo SendSpawnMessage => serverMessage ??= typeof(NetworkServer).GetMethod("SendSpawnMessage", BindingFlags.NonPublic | BindingFlags.Static);

    /// <summary>
    /// Gets the server's port.
    /// </summary>
    public static ushort ServerPort => ServerStatic.ServerPort;

    /// <summary>
    /// Gets the server's IP address.
    /// </summary>
    public static string ServerIP => ServerConsole.Ip;

    /// <summary>
    /// Gets if the server is verified.
    /// </summary>
    public static bool IsVerified => CustomNetworkManager.IsVerified;

    /// <summary>
    /// Gets if the server has streaming allowed.
    /// </summary>
    public static bool IsStreamingAllowed => GameCore.Version.StreamingAllowed;

    /// <summary>
    /// Gets if the server is a release version.
    /// </summary>
    public static bool IsReleaseVersion => GameCore.Version.ReleaseCandidate;

    /// <summary>
    /// Gets if the server is a private beta version.
    /// </summary>
    public static bool IsPrivateBeta => GameCore.Version.PrivateBeta;

    /// <summary>
    /// Gets if the server is a public beta version.
    /// </summary>
    public static bool IsPublicBeta => GameCore.Version.PublicBeta;

    /// <summary>
    /// Restarts the server.
    /// </summary>
    /// <param name="roundRestartType">The <see cref="ServerStatic.NextRoundAction"/> to use.</param>
    /// <param name="FastRestart">If the restart is a fast restart.</param>

    public static void RestartServer(NextRoundAction roundRestartType = NextRoundAction.Restart, bool FastRestart = false)
    {
        StopNextRound = roundRestartType;
        bool oldValue = CustomNetworkManager.EnableFastRestart;
        CustomNetworkManager.EnableFastRestart = FastRestart;
        RoundRestart.InitiateRoundRestart();
        CustomNetworkManager.EnableFastRestart = oldValue;
    }

    /// <summary>
    /// Redirects players to another server.
    /// </summary>
    ///<param name="port">The port to redirect to.</param>
    public static void RedirectPlayers(ushort port = default)
    {
        NetworkServer.SendToAll(new RoundRestartMessage(RoundRestartType.RedirectRestart, 0.3f, port, true, false));
        RestartServer(NextRoundAction.Restart, false);
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
        foreach (NebuliPlayer ply in NebuliPlayer.List)
        {
            Broadcast.TargetAddElement(ply.ReferenceHub.connectionToClient, message, duration, broadcastFlags);
        }
    }

    /// <summary>
    /// Clears all broadcasts for all players on the server.
    /// </summary>
    public static void ClearAllBroadcasts()
    {
        foreach (NebuliPlayer ply in NebuliPlayer.List)
        {
            ply.ClearBroadcasts();
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
    /// Gets or sets the ServerTickRate.
    /// </summary>
    public static short ServerTickRate
    {
        get => ServerTickrate;
        set => ServerTickrate = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="ServerStatic.NextRoundAction"/>.
    /// </summary>
    public static NextRoundAction NextRoundAction
    {
        get => StopNextRound;
        set => StopNextRound = value;
    }

    /// <summary>
    /// Gets the servers ticks-per-second.
    /// </summary>
    public static double ServerTPS => Math.Round(1f / Time.smoothDeltaTime);
}
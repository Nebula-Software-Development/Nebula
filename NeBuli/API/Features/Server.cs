using CustomPlayerEffects;
using GameCore;
using PlayerRoles.RoleAssign;

namespace Nebuli.API.Features
{
    public static class Server
    {
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
        public static void RunServerCommand(string command, CommandSender sender = null) => ServerConsole.EnterCommand(command, sender);
    }
}

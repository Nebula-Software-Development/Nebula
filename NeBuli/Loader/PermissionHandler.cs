// -----------------------------------------------------------------------
// <copyright file=PermissionHandler.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandSystem;
using Nebula.API.Features;
using Nebula.Loader;
using PluginAPI.Loader;

namespace Nebula.Permissions
{
    public static class PermissionsHandler
    {
        private static Assembly _nwapiPermissionCache;
        private static MethodInfo _nwapiMethodCache;

        /// <summary>
        ///     Gets a dictionary of SL group names with their corresponding <see cref="Group" />.
        /// </summary>
        public static Dictionary<string, Group> Groups { get; internal set; } = new();

        internal static void LoadPermissions()
        {
            try
            {
                if (!File.Exists(Paths.Permissions.FullName))
                {
                    GenerateDefaultPermissionsFile(Paths.Permissions.FullName);
                }

                PermissionsConfig permissionsConfig =
                    LoaderClass.Deserializer.Deserialize<PermissionsConfig>(
                        File.ReadAllText(Paths.Permissions.FullName));

                for (int index = 0; index < permissionsConfig.Permissions.Keys.ToList().Count; index++)
                {
                    string group = permissionsConfig.Permissions.Keys.ToList()[index];
                    if (string.Equals(group, "user", StringComparison.OrdinalIgnoreCase) ||
                        ServerStatic.PermissionsHandler._groups.ContainsKey(group))
                    {
                        continue;
                    }

                    Log.Error($"{group} is not a valid permission group!", "Permissions");
                    permissionsConfig.Permissions.Remove(group);
                }

                if (permissionsConfig?.Permissions == null)
                {
                    return;
                }

                Groups.Clear();
                foreach (KeyValuePair<string, Group> kvp in permissionsConfig.Permissions)
                {
                    kvp.Value.GroupName = kvp.Key;
                    Groups.Add(kvp.Key, kvp.Value);
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load permissions from file: {e.Message}", "Permissions");
            }
        }

        internal static void SavePermissions()
        {
            PermissionsConfig permissionsConfig = new()
            {
                Permissions = Groups
            };

            try
            {
                string yaml = LoaderClass.Serializer.Serialize(permissionsConfig);
                File.WriteAllText(Paths.Permissions.FullName, yaml);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to save permissions to file: {e.Message}", "Permissions");
            }
        }

        /// <summary>
        ///     Gets if the <see cref="Player" /> has the specified permission.
        /// </summary>
        public static bool HasPermission(this Player ply, string permission)
        {
            return HasPermission(ply.Sender, permission);
        }

        /// <summary>
        ///     Gets if the <see cref="Player" /> has the specified NWAPI permission.
        /// </summary>
        /// <remarks>
        ///     Requires the NWAPIPermissionSystem be loaded by NWAPI, if it cannot be found, the method returns <c>false</c>
        /// </remarks>
        public static bool HasNWAPIPermission(this Player ply, string permission)
        {
            return HasNWAPIPermission(ply.Sender, permission);
        }

        /// <summary>
        ///     Gets if the <see cref="Player" /> has the either a specified Nebula permission OR a specified NWAPI permission.
        /// </summary>
        /// <remarks>
        ///     Requires the NWAPIPermissionSystem be loaded by NWAPI, if it cannot be found and the player has no Nebula
        ///     permission, the method returns <c>false</c>
        /// </remarks>
        public static bool HasPermissionAnywhere(this Player player, string permission)
        {
            return HasPermissionAnywhere(player.Sender, permission);
        }

        /// <summary>
        ///     Gets if the <see cref="ICommandSender" /> has the either a specified Nebula permission OR a specified NWAPI
        ///     permission.
        /// </summary>
        /// <remarks>
        ///     Requires the NWAPIPermissionSystem be loaded by NWAPI, if it cannot be found and the player has no Nebula
        ///     permission, the method returns <c>false</c>
        /// </remarks>
        public static bool HasPermissionAnywhere(this ICommandSender sender, string permission)
        {
            return HasPermission(sender, permission) || HasNWAPIPermission(sender, permission);
        }

        /// <summary>
        ///     Gets if the <see cref="ICommandSender" /> has the specified permission.
        /// </summary>
        public static bool HasPermission(this ICommandSender commandSender, string permission)
        {
            return commandSender is ServerConsoleSender || Player.TryGet(commandSender, out Player ply) &&
                (ply.ReferenceHub == ReferenceHub.HostHub ||
                 (Groups.TryGetValue(ply.GroupName,
                      out Group playerGroup) &&
                  (playerGroup.Permissions.Contains(".*") ||
                   playerGroup.Permissions.Contains(permission))));
        }

        /// <summary>
        ///     Gets if the <see cref="ICommandSender" /> has the specified NWAPI permission.
        /// </summary>
        /// <remarks>
        ///     Requires the NWAPIPermissionSystem be loaded by NWAPI, if it cannot be found, the method returns <c>false</c>
        /// </remarks>
        public static bool HasNWAPIPermission(this ICommandSender sender, string permission)
        {
            bool found = false;
            if (_nwapiPermissionCache == null)
            {
                foreach (Assembly assemblyPlugin in AssemblyLoader.Plugins.Keys)
                {
                    if (assemblyPlugin.GetName().Name != "NWAPIPermissionSystem")
                    {
                        continue;
                    }

                    _nwapiPermissionCache = assemblyPlugin;
                    found = true;
                    break;
                }
            }
            else
            {
                found = true;
            }

            if (!found)
            {
                Log.Debug(
                    "Could not find NWAPIPermissionSystem! This likely means the NWAPIPermissionSystem is not installed and a plugin tried to use it!");
                return false;
            }

            if (_nwapiMethodCache != null)
            {
                return (bool)_nwapiMethodCache.Invoke(null, new object[] { sender, permission });
            }

            Type permissionHandlerType = _nwapiPermissionCache.GetType("NWAPIPermissionSystem.PermissionHandler");

            if (permissionHandlerType != null)
            {
                _nwapiMethodCache = permissionHandlerType.GetMethod("CheckPermission",
                    new[] { typeof(ICommandSender), typeof(string) });
            }
            else
            {
                Log.Debug("Unable to find 'NWAPIPermissionSystem.PermissionHandler'!");
                return false;
            }

            return (bool)_nwapiMethodCache.Invoke(null, new object[] { sender, permission });
        }

        internal static void GenerateDefaultPermissionsFile(string filePath)
        {
            PermissionsConfig defaultPermissionsConfig = new()
            {
                Permissions = new Dictionary<string, Group>
                {
                    { "user", new Group { Default = true, Permissions = new List<string> { "" } } },
                    { "owner", new Group { Permissions = new List<string> { ".*" } } },
                    { "admin", new Group { Permissions = new List<string> { "testplugin.admin", "testplugin.*" } } },
                    { "moderator", new Group { Permissions = new List<string> { "testplugin.moderator" } } }
                }
            };

            try
            {
                File.WriteAllText(filePath, LoaderClass.Serializer.Serialize(defaultPermissionsConfig));
            }
            catch (Exception e)
            {
                Log.Error($"Failed to generate default permissions file: {e.Message}", "Permissions");
            }
        }
    }

//this is here because i wanted to.
    public class Group
    {
        public string GroupName { get; set; } = string.Empty;
        public bool Default { get; set; }
        public List<string> Permissions { get; set; } = new();
    }

    internal class PermissionsConfig
    {
        public Dictionary<string, Group> Permissions { get; set; }
    }
}
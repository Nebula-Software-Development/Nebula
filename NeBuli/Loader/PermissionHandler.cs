using CommandSystem;
using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Loader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Log = Nebuli.API.Features.Log;

namespace Nebuli.Permissions;

public static class PermissionsHandler
{
    private static Assembly _nwapiPermissionCache = null;
    private static MethodInfo _nwapiMethodCache = null;
    public static Dictionary<string, Group> Groups { get; internal set; } = new();

    internal static void LoadPermissions()
    {
        try
        {
            if (!File.Exists(Paths.Permissions.FullName))
            {
                GenerateDefaultPermissionsFile(Paths.Permissions.FullName);
            }

            PermissionsConfig permissionsConfig = LoaderClass.Deserializer.Deserialize<PermissionsConfig>(File.ReadAllText(Paths.Permissions.FullName));

            foreach (string group in permissionsConfig.Permissions.Keys.ToList())
            {
                if (string.Equals(group, "user", StringComparison.OrdinalIgnoreCase) || ServerStatic.PermissionsHandler._groups.ContainsKey(group))
                    continue;
                Log.Error($"{group} is not a valid permission group!", "Permissions");
                permissionsConfig.Permissions.Remove(group);
            }

            if (permissionsConfig?.Permissions != null)
            {
                Groups.Clear();
                foreach (var kvp in permissionsConfig.Permissions)
                {
                    kvp.Value.GroupName = kvp.Key;
                    Groups.Add(kvp.Key, kvp.Value);
                }
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
    /// Gets if the <see cref="NebuliPlayer"/> has the specified permission.
    /// </summary>
    public static bool HasPermission(this NebuliPlayer ply, string permission, bool checkNWAPI = true) => HasPermission(ply.Sender, permission, checkNWAPI);

    /// <summary>
    /// Gets if the <see cref="NebuliPlayer"/> has the specified NWAPI permission.
    /// </summary>
    /// <remarks>Requires the NWAPIPermissionSystem be loaded by NWAPI, if it cannot be found, the method returns <c>false</c></remarks>
    public static bool HasNWAPIPermission(this NebuliPlayer ply, string permission) => HasNWAPIPermission(ply.Sender, permission);

    /// <summary>
    /// Gets if the <see cref="ICommandSender"/> has the specified permission.
    /// </summary>
    public static bool HasPermission(this ICommandSender commandSender, string permission, bool checkNWAPI = true)
    {
        if (commandSender is ServerConsoleSender) return true;
        if (!NebuliPlayer.TryGet(commandSender, out NebuliPlayer ply)) return false;
        if (ply.ReferenceHub == ReferenceHub.HostHub ||
            Groups.TryGetValue(ply.GroupName, out Group playerGroup) &&
            (playerGroup.Permissions.Contains(".*") || playerGroup.Permissions.Contains(permission)))
        {
            return true;
        }

        if (checkNWAPI) return HasNWAPIPermission(commandSender, permission);

        return false;      
    }

    /// <summary>
    /// Gets if the <see cref="ICommandSender"/> has the specified NWAPI permission.
    /// </summary>
    /// <remarks>Requires the NWAPIPermissionSystem be loaded by NWAPI, if it cannot be found, the method returns <c>false</c></remarks>
    public static bool HasNWAPIPermission(this ICommandSender sender, string permission)
    {
        bool found = false;
        if (_nwapiPermissionCache == null)
        {           
            foreach (Assembly assemblyPlugin in PluginAPI.Loader.AssemblyLoader.Plugins.Keys)
            {
                if(assemblyPlugin.GetName().Name == "NWAPIPermissionSystem")
                {
                    _nwapiPermissionCache = assemblyPlugin;
                    found = true;
                    break;
                }
            }
        }
        else found = true;

        if (!found)
        {
            Log.Debug("Could not find NWAPIPermissionSystem! This likely means the NWAPIPermissionSystem is not installed and a plugin tried to use it!");
            return false;
        }

        if (_nwapiMethodCache == null)
        {
            Type permissionHandlerType = _nwapiPermissionCache.GetType("NWAPIPermissionSystem.PermissionHandler");

            if (permissionHandlerType != null)
            {
                _nwapiMethodCache = permissionHandlerType.GetMethod("CheckPermission", new[] {typeof(ICommandSender), typeof(string)});
            }
            else
            {
                Log.Debug("Unable to find 'NWAPIPermissionSystem.PermissionHandler'!");
                return false;
            }
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

public class Group
{
    public string GroupName { get; set; } = string.Empty;
    public bool Default { get; set; } = false;
    public List<string> Permissions { get; set; } = new List<string>();
}

internal class PermissionsConfig
{
    public Dictionary<string, Group> Permissions { get; set; }
}